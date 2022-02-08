using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace Onderwijs
{
    public partial class frmMain : Form
    {
        private const int MAX_DOELEN = 6;
        private SqlConnection cnnOnderwijs = new SqlConnection("Data Source=" + Globals.DB_SERVER + ";Initial Catalog=" + Globals.DB_NAME + ";User ID=" + Globals.DB_USER + ";Password=" + Globals.DB_PASSWORD + ";MultipleActiveResultSets=true;");
        private int intDoel = 1;
        private int intSelectedToetsIndex = -1;
        private int intSelectedCursusIndex = -1;
        private int intPrevCursusIndex = -1;
        private bool blnStartupReady = false;
        private bool blnErIsIetsGewijzigd = false;
        private bool selectieGecanceld = false;
        private int blokFilter = 0;
        private String pathToInstallation = Environment.CurrentDirectory;
        private String pathToTemp = Path.GetTempPath();
        private String formTitel;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            cnnOnderwijs.Open();
            Program.logMessage("Applicatie gestart.", cnnOnderwijs);
            udsStatischeVulling();

            FileVersionInfo fviAssembly = FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);

            formTitel = "Onderwijs ET/TI  (versie " + fviAssembly.FileMajorPart + "." + fviAssembly.FileBuildPart + ")";
            this.Text = formTitel;
            blnStartupReady = true;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (blnErIsIetsGewijzigd)
            {
                e.Cancel = (MessageBox.Show("Moeten er nog wijzigingen opgeslagen worden?", "Afsluiten...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes);
            }
        }

        private void udsStatischeVulling()
        {
            // Cursuscodes:
            lstCursuscodes.Items.Clear();
            lstCursuscodes.ClearSelected();
            lstToetscodes.ClearSelected();
            String sqlStatement = "SELECT Cursuscode FROM qryCursus ORDER BY Cursuscode";
            if (blokFilter > 0)
            {
                sqlStatement = "SELECT Cursuscode FROM qryBlokCursus WHERE Blok = '" + blokFilter + "'ORDER BY Cursuscode";
            }
            using (SqlCommand cmdCursus = new SqlCommand(sqlStatement, cnnOnderwijs))
            {
                using (SqlDataReader rdrCursus = cmdCursus.ExecuteReader())
                {
                    while (rdrCursus.Read())
                    {
                        lstCursuscodes.Items.Add(rdrCursus["Cursuscode"].ToString());
                    }
                }
            }
            lstCursuscodes.SelectedIndex = 0;
        }

        private void lstCursuscodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstCursuscodes.SelectedIndex < 0 || selectieGecanceld)
            {
                return;
            }

            if (blnStartupReady && blnErIsIetsGewijzigd)
            {
                // Alle wijzigingen van de "vorige" toetscode opgeslagen?
                if (MessageBox.Show("Moeten er nog wijzigingen opgeslagen worden?", "Verandering van modulecode...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Ga terug...
                    selectieGecanceld = true;
                    lstCursuscodes.SelectedIndex = intSelectedCursusIndex;
                    selectieGecanceld = false;
                    return;
                }
                else
                {
                    blnErIsIetsGewijzigd = false;
                }
            }

            // Onthoud de geselecteerde index voor later...
            intSelectedCursusIndex = lstCursuscodes.SelectedIndex;

            String strCursuscode = ((ListBox)sender).SelectedItem.ToString();

            // Toetscodes:
            lstToetscodes.Items.Clear();
            using (SqlCommand cmdToets = new SqlCommand("SELECT Toetscode FROM qryToets WHERE Cursuscode = '" + strCursuscode + "' ORDER BY ToetsCode", cnnOnderwijs))
            {
                using (SqlDataReader rdrToets = cmdToets.ExecuteReader())
                {
                    while (rdrToets.Read())
                    {
                        lstToetscodes.Items.Add(rdrToets["Toetscode"].ToString());
                    }
                }
            }
            if (lstToetscodes.Items.Count > 0)
            {
                lstToetscodes.SelectedIndex = 0;
            }
            else
            {
                // Verwijder huidige onderwijsdoelen van het scherm:
                for (int i = intDoel; i > 1; i--)
                {
                    udsRemoveDoel();
                }
                udsClearDoel(1);
                blnErIsIetsGewijzigd = false;
            }
        }

        private void lstToetscodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstToetscodes.SelectedIndex < 0 || selectieGecanceld)
            {
                return;
            }

            if (blnStartupReady && blnErIsIetsGewijzigd)
            {
                // Alle wijzigingen van de "vorige" toetscode opgeslagen?
                if (MessageBox.Show("Moeten er nog wijzigingen opgeslagen worden?", "Verandering van toetscode...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Ga terug...
                    selectieGecanceld = true;
                    lstToetscodes.SelectedIndex = intSelectedToetsIndex;
                    selectieGecanceld = false;
                    return;
                }
                else
                {
                    blnErIsIetsGewijzigd = false;
                }
            }

            // Onthoud de geselecteerde index voor later...
            intSelectedToetsIndex = lstToetscodes.SelectedIndex;
            intPrevCursusIndex = lstCursuscodes.SelectedIndex;

            // Toetscode geselecteerd in lijst. Verwijder huidige onderwijsdoelen van het scherm:
            for (int i = intDoel; i > 1; i--)
            {
                udsRemoveDoel();
            }
            udsClearDoel(1);

            // Haal bij toetscode horende onderwijsdoelen op:
            String strToetscode = lstToetscodes.SelectedItem.ToString();
            String strCursuscode = lstCursuscodes.SelectedItem.ToString();
            int intDoelTeller = 0;
            using (SqlCommand cmdOnderwijsdoel = new SqlCommand("SELECT * FROM qryDoel WHERE Toetscode = '" + strToetscode + "' AND Cursuscode = '" + strCursuscode + "' AND Verwijderd = 0 ORDER BY DoelId", cnnOnderwijs))
            {
                using (SqlDataReader rdrOnderwijsdoel = cmdOnderwijsdoel.ExecuteReader())
                {
                    while (rdrOnderwijsdoel.Read())
                    {
                        // Voeg onderwijsdoel toe op scherm (de eerste staat al...):
                        intDoelTeller++;
                        if (intDoelTeller > 1)
                        {
                            udsAddDoel();
                        }

                        // Vul het doel met informatie uit de huidige record:
                        GroupBox grpDoel = (GroupBox)Controls["grpDoel" + intDoelTeller.ToString()];
                        int intOnderwijsdoelId = (int)rdrOnderwijsdoel["DoelId"];
                        ((Label)grpDoel.Controls["lblDoelId" + intDoelTeller.ToString()]).Text = "(Id=" + intOnderwijsdoelId.ToString() + ")";
                        ((TextBox)grpDoel.Controls["txtOnderwijsdoel" + intDoelTeller.ToString()]).Text = rdrOnderwijsdoel["Omschrijving"].ToString();
                        ((TextBox)grpDoel.Controls["txtOnderwerpen" + intDoelTeller.ToString()]).Text = rdrOnderwijsdoel["Onderwerpen"].ToString();
                        ((TextBox)grpDoel.Controls["txtWeging" + intDoelTeller.ToString()]).Text = (Convert.ToDecimal(rdrOnderwijsdoel["Weging"].ToString()) / 1.00000m).ToString();
                        ((CheckBox)grpDoel.Controls["chkOnthouden" + intDoelTeller.ToString()]).Checked = (bool)rdrOnderwijsdoel["Onthouden"];
                        ((CheckBox)grpDoel.Controls["chkBegrijpen" + intDoelTeller.ToString()]).Checked = (bool)rdrOnderwijsdoel["Begrijpen"];
                        ((CheckBox)grpDoel.Controls["chkToepassen" + intDoelTeller.ToString()]).Checked = (bool)rdrOnderwijsdoel["Toepassen"];
                        ((CheckBox)grpDoel.Controls["chkAnalyseren" + intDoelTeller.ToString()]).Checked = (bool)rdrOnderwijsdoel["Analyseren"];
                        ((CheckBox)grpDoel.Controls["chkEvalueren" + intDoelTeller.ToString()]).Checked = (bool)rdrOnderwijsdoel["Evalueren"];
                        ((CheckBox)grpDoel.Controls["chkCreeren" + intDoelTeller.ToString()]).Checked = (bool)rdrOnderwijsdoel["Creeren"];
                        ((RadioButton)grpDoel.Controls["btnLeerdoel" + intDoelTeller.ToString()]).Checked = ((int)rdrOnderwijsdoel["Doeltype"] == 1);
                        ((RadioButton)grpDoel.Controls["btnResultaatdoel" + intDoelTeller.ToString()]).Checked = ((int)rdrOnderwijsdoel["Doeltype"] == 2);

                        // Vul de lijst met gekoppelde BoKS-items:
                        udsShowDoelBoKSItems(intDoelTeller);

                        // Vul de lijst met gekoppelde competenties:
                        udsShowDoelCompetenties(intDoelTeller);

                        // Vul de lijst met gekoppelde beroepsproducten:
                        udsShowDoelBeroepsproducten(intDoelTeller);
                    }
                }
            }

            // Toon toetsinformatie:
            String strCursusnaam;
            String strCursustype;

            using (SqlCommand cmdToets = new SqlCommand("SELECT * FROM qryToets WHERE Toetscode = '" + strToetscode + "' AND Cursuscode = '" + strCursuscode + "'", cnnOnderwijs))
            {
                using (SqlDataReader rdrToets = cmdToets.ExecuteReader())
                {
                    rdrToets.Read();
                    //        strCursuscode = rdrToets["Cursuscode"].ToString();
                    strCursusnaam = rdrToets["Cursusnaam"].ToString();
                    strCursustype = rdrToets["Cursustype"].ToString();
                    txtBeoordelingswijze.Text = rdrToets["Beoordelingswijze"].ToString();
                    txtToetscode.Text = rdrToets["Toetscode"].ToString();
                    txtToetsnaam.Text = rdrToets["Toets"].ToString();
                    txtToetsvorm.Text = rdrToets["Toetsvorm"].ToString();
                    txtPeriode.Text = rdrToets["Periode"].ToString();
                    txtBlok.Text = rdrToets["Blok"].ToString();
                    txtECs.Text = Convert.ToSingle(rdrToets["ECs"].ToString()).ToString();
                    txtKeuzedeel.Text = rdrToets["Keuzedeel"].ToString().Equals("True") ? "Ja" : "Nee";
                    chkGecontroleerd.Checked = rdrToets["Gecontroleerd"].ToString().Equals("True");
                    txtControleur.Text = rdrToets["Controleur"].ToString();
                    txtTijdstip.Text = rdrToets["Controletijdstip"].ToString();
                }
            }

            // Toon cursusinformatie:
            txtCursuscode.Text = strCursuscode;
            txtCursusnaam.Text = strCursusnaam;
            btnProject.Checked = strCursustype.Equals("Project");
            btnVak.Checked = strCursustype.Equals("Vak");

            // 1: Trajecten:
            using (SqlCommand cmdTrajectCursus = new SqlCommand("SELECT * FROM qryTrajectCursus WHERE Cursuscode = '" + strCursuscode + "'", cnnOnderwijs))
            {
                using (SqlDataReader rdrTrajectCursus = cmdTrajectCursus.ExecuteReader())
                {
                    chkHW.Checked = false;
                    chkAU.Checked = false;
                    chkSW.Checked = false;
                    while (rdrTrajectCursus.Read())
                    {
                        switch (rdrTrajectCursus["Trajectcode"].ToString())
                        {
                            case ("HW"):
                                chkHW.Checked = true;
                                break;
                            case ("AU"):
                                chkAU.Checked = true;
                                break;
                            case ("SW"):
                                chkSW.Checked = true;
                                break;
                        }
                    }
                }
            }

            // 2: Blok/Periode:
            String strPeriodes = "";
            String strBlokken = "";
            bool eersteBlok = true;
            using (SqlCommand cmdBlokCursus = new SqlCommand("SELECT * FROM qryBlokCursus WHERE Cursuscode = '" + strCursuscode + "'", cnnOnderwijs))
            {
                using (SqlDataReader rdrBlokCursus = cmdBlokCursus.ExecuteReader())
                {
                    while (rdrBlokCursus.Read())
                    {
                        if (eersteBlok)
                        {
                            eersteBlok = false;
                            strPeriodes += rdrBlokCursus["Periode"].ToString();
                            strBlokken += rdrBlokCursus["Blok"].ToString();
                        }
                        else
                        {
                            strPeriodes += ", " + rdrBlokCursus["Periode"].ToString();
                            strBlokken += ", " + rdrBlokCursus["Blok"].ToString();
                        }
                    }
                    txtCursusBlok.Text = strBlokken;
                    txtCursusPeriode.Text = strPeriodes;
                }
            }

            // 3: Toetsen, EC's:
            float fltECs = (float)0.0;
            bool eersteKeuzedeel = true;
            lstCursusToetsen.Items.Clear();
            using (SqlCommand cmdCursusToets = new SqlCommand("SELECT * FROM qryToets WHERE Cursuscode = '" + strCursuscode + "'", cnnOnderwijs))
            {
                using (SqlDataReader rdrCursusToets = cmdCursusToets.ExecuteReader())
                {
                    while (rdrCursusToets.Read())
                    {
                        lstCursusToetsen.Items.Add(rdrCursusToets["Toetscode"].ToString());

                        // In geval van 'keuzedeel', dan alleen EC's van de eerste instantie sommeren:
                        if (rdrCursusToets["Keuzedeel"].ToString().Equals("True"))
                        {
                            if (eersteKeuzedeel)
                            {
                                eersteKeuzedeel = false;
                                fltECs += Convert.ToSingle(rdrCursusToets["ECs"].ToString());
                            }
                        }
                        else
                        {
                            fltECs += Convert.ToSingle(rdrCursusToets["ECs"].ToString());
                        }
                    }
                }
            }
            txtCursusECs.Text = fltECs.ToString();


            blnErIsIetsGewijzigd = false;
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            udsAddDoel();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            int intGeselecteerdDoel = Convert.ToInt32(((Button)sender).Name.Substring(((Button)sender).Name.Length - 1, 1));
            int intDoelId = udfDoelId(intGeselecteerdDoel);

            if (intDoelId > 0)
            {
                if (MessageBox.Show(this, "Het onderwijsdoel wordt verwijderd. Weet je het zeker?", "Onderwijsdoel verwijderen...", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    // Verwijder het (onderste) onderwijsdoel:
                    udsDeleteDoel(intDoelId);
                }
            }
            else
            {
                // Verwijder het (onderste) onderwijsdoel (er staat nog niets van in de Database):
                udsDeleteDoel(0);
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Alle wijzigingen in het onderwijsdoel worden opgeslagen. Weet je het zeker?", "Onderwijsdoel opslaan...", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // Sla alle wijzigingen in het doel op:
                int intRecordsUpdated = 0;
                int intRecordsInserted = 0;
                if (udsSaveDoel(Convert.ToInt32(((Button)sender).Name.Substring(((Button)sender).Name.Length - 1, 1)), ref intRecordsUpdated, ref intRecordsInserted) > 0)
                {
                    if (Globals.DEBUG)
                    {
                        MessageBox.Show("Aantal records updated: " + intRecordsUpdated.ToString() + "\nAantal records inserted: " + intRecordsInserted.ToString(), "Doelen opslaan...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Iets gaat hier niet chocotof!", "Whoopsy Daisy...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Program.logMessage("Save MISLUKT", cnnOnderwijs);
                }
            }
        }

        private void udsAddDoel()
        {
            intDoel++;

            // Verplaats de bedienknoppen naar beneden (onder het nieuwe doel):
            grpBediening.Top += 100;

            // Groep voor nieuw doel:
            GroupBox grpNew = new GroupBox();
            grpNew.Name = grpDoel1.Tag.ToString() + intDoel.ToString();
            grpNew.Text = "Doel " + intDoel.ToString() + ":";
            grpNew.Size = grpDoel1.Size;
            grpNew.Left = grpDoel1.Left;
            grpNew.Top = grpDoel1.Top + (intDoel - 1) * 100;
            Controls.Add(grpNew);

            foreach (Control ctrFrom in grpDoel1.Controls)
            {
                switch (ctrFrom.GetType().Name)
                {
                    case ("Label"):
                        Label lblTo = new Label();
                        lblTo.Location = ctrFrom.Location;
                        lblTo.Size = ctrFrom.Size;
                        lblTo.Name = ctrFrom.Tag.ToString() + intDoel.ToString();
                        lblTo.Tag = ctrFrom.Tag;
                        if (ctrFrom.Tag.ToString().Equals("lblDoelId"))
                        {
                            lblTo.Text = "(Id=0)";
                        }
                        else
                        {
                            lblTo.Text = ctrFrom.Text;
                        }
                        lblTo.BorderStyle = ((Label)ctrFrom).BorderStyle;
                        lblTo.ForeColor = ((Label)ctrFrom).ForeColor;
                        lblTo.AutoSize = ((Label)ctrFrom).AutoSize;
                        grpNew.Controls.Add(lblTo);
                        break;

                    case ("Button"):
                        Button cmdTo = new Button();
                        cmdTo.Location = ctrFrom.Location;
                        cmdTo.Size = ctrFrom.Size;
                        cmdTo.Name = ctrFrom.Tag.ToString() + intDoel.ToString();
                        cmdTo.Text = ctrFrom.Text;
                        cmdTo.Visible = ctrFrom.Visible;
                        switch (ctrFrom.Tag.ToString())
                        {
                            case ("cmdDelete"):
                                cmdTo.Click += new System.EventHandler(cmdDelete_Click);
                                break;
                            case ("cmdSave"):
                                cmdTo.Click += new System.EventHandler(cmdSave_Click);
                                break;
                            default:
                                // Do Nothing
                                break;
                        }
                        grpNew.Controls.Add(cmdTo);
                        break;

                    case ("CheckBox"):
                        CheckBox chkTo = new CheckBox();
                        chkTo.Location = ctrFrom.Location;
                        chkTo.Size = ctrFrom.Size;
                        chkTo.Name = ctrFrom.Tag.ToString() + intDoel.ToString();
                        chkTo.Tag = ctrFrom.Tag;
                        chkTo.Text = ctrFrom.Text;
                        chkTo.CheckedChanged += new System.EventHandler(chkGeneral_CheckedChanged);
                        grpNew.Controls.Add(chkTo);
                        break;

                    case ("TextBox"):
                        TextBox txtTo = new TextBox();
                        txtTo.Location = ctrFrom.Location;
                        txtTo.Size = ctrFrom.Size;
                        txtTo.Multiline = ((TextBox)ctrFrom).Multiline;
                        txtTo.Name = ctrFrom.Tag.ToString() + intDoel.ToString();
                        txtTo.ScrollBars = ((TextBox)ctrFrom).ScrollBars;
                        txtTo.TextAlign = ((TextBox)ctrFrom).TextAlign;
                        //txtTo.BackColor = ((TextBox)ctrFrom).BackColor;
                        if (ctrFrom.Tag.ToString().Equals("txtWeging"))
                        {
                            txtTo.Text = "0";
                            txtTo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtWeging_KeyPress);
                            txtTo.TextChanged += new System.EventHandler(txtWeging_TextChanged);
                        }
                        else
                        {
                            txtTo.TextChanged += new System.EventHandler(txtGeneral_TextChanged);
                        }
                        grpNew.Controls.Add(txtTo);
                        break;

                    case ("ListBox"):
                        ListBox lstTo = new ListBox();
                        lstTo.Location = ctrFrom.Location;
                        lstTo.Size = ctrFrom.Size;
                        lstTo.Name = ctrFrom.Tag.ToString() + intDoel.ToString();
                        lstTo.IntegralHeight = ((ListBox)ctrFrom).IntegralHeight;
                        lstTo.BackColor = ((ListBox)ctrFrom).BackColor;
                        switch (ctrFrom.Tag.ToString())
                        {
                            case ("lstBoKS"):
                                lstTo.Click += new System.EventHandler(lstBoKS_Click);
                                break;
                            case ("lstCompetenties"):
                                lstTo.Click += new System.EventHandler(lstCompetenties_Click);
                                break;
                            case ("lstBeroepsproducten"):
                                lstTo.Click += new System.EventHandler(lstBeroepsproducten_Click);
                                break;
                            default:
                                // Debug:
                                MessageBox.Show(ctrFrom.Name, "Geen event gekoppeld!");
                                break;
                        }
                        grpNew.Controls.Add(lstTo);
                        break;

                    case ("RadioButton"):
                        RadioButton btnTo = new RadioButton();
                        btnTo.Location = ctrFrom.Location;
                        btnTo.Size = ctrFrom.Size;
                        btnTo.Name = ctrFrom.Tag.ToString() + intDoel.ToString();
                        btnTo.Tag = ctrFrom.Tag.ToString();
                        btnTo.Text = ctrFrom.Text;
                        btnTo.TabIndex = ctrFrom.TabIndex;
                        btnTo.CheckedChanged += new System.EventHandler(btnGeneral_CheckedChanged);
                        grpNew.Controls.Add(btnTo);
                        btnTo.Checked = btnTo.Tag.ToString().Equals("btnLeerdoel");
                        break;

                    default:
                        // Debug:
                        MessageBox.Show(ctrFrom.Name, "Control niet gekopiëerd!");
                        break;
                }
            }

            // Disable de add-knop als er al MAX_DOELEN doelen staan (facultatief maximum):
            if (intDoel == MAX_DOELEN)
            {
                cmdAdd.Enabled = false;
            }

            // Disable de delete-knop van het "vorige" doel. Alleen het laatste doel mag weggehaald worden:
            if (intDoel > 1)
            {
                Controls["grpDoel" + (intDoel - 1).ToString()].Controls["cmdDelete" + (intDoel - 1).ToString()].Enabled = false;
            }
        }
        private void udsDeleteDoel(int intDoelToDeleteId)
        {
            int intRecordsDeleted = 0;
            if (intDoelToDeleteId > 0)
            {
                // Verwijder onderwijsdoel uit de database (alléén het onderste doel op het scherm kan deleted worden).
                // Door de cascading delete (referentiële integriteit) worden alle gerelateerde records (competentiekoppelingen, BoKS-koppelingen) ook verwijderd.

                /*************************
                 * 
                 * Aanpassing 2021-09-28: DELETE-query vervangen voor blnMarkedForDeletion = true
                 * 
                 *************************/


                //String strQuery = "DELETE FROM tblDoel WHERE pkId = " + intDoelToDeleteId.ToString();
                String strQuery = "UPDATE tblDoel SET blnMarkedForDeletion = 1 WHERE pkId = " + intDoelToDeleteId.ToString();
                SqlTransaction transOnderwijs = cnnOnderwijs.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    using (SqlCommand cmdDelete = new SqlCommand(strQuery, cnnOnderwijs, transOnderwijs))
                    {
                        int intAantalRecords = cmdDelete.ExecuteNonQuery();
                        intRecordsDeleted += intAantalRecords;
                    }
                    transOnderwijs.Commit();
                    Program.logMessage("Commit: " + Program.removeSpecialChars(strQuery), cnnOnderwijs);
                    if (Globals.DEBUG)
                    {
                        MessageBox.Show("Aantal records gemarkeerd als verwijderd: " + intRecordsDeleted.ToString(), "Doel verwijderen...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                catch
                {
                    transOnderwijs.Rollback();
                    Program.logMessage("Rollback: " + Program.removeSpecialChars(strQuery), cnnOnderwijs);
                    MessageBox.Show("Iets gaat hier niet chocotof!", "Whoopsy Daisy...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            udsRemoveDoel();
        }

        private void udsClearDoel(int intDoelVolgnummer)
        {
            // Schoon het gewenste doel op door alle bijbehorende controls te initialiseren.
            // Er worden géén database-bewerkingen uitgevoerd!!!
            // intDoelVolgnummer: Het volgnummer op het scherm, beginnende met 1 voor het bovenste getoonde onderwijsdoel (<> DoelId in de database).

            GroupBox grpInstance = (GroupBox)Controls["grpDoel" + intDoelVolgnummer.ToString()];
            foreach (Control ctrInstance in grpInstance.Controls)
            {
                switch (ctrInstance.GetType().Name)
                {
                    case ("TextBox"):
                        if (((TextBox)ctrInstance).Tag.ToString().Equals("txtWeging"))
                        {
                            ((TextBox)ctrInstance).Text = "0";
                        }
                        else
                        {
                            ((TextBox)ctrInstance).Text = "";
                        }
                        break;
                    case ("CheckBox"):
                        ((CheckBox)ctrInstance).Checked = false;
                        break;
                    case ("Label"):
                        if (((Label)ctrInstance).Tag.ToString().Equals("lblDoelId"))
                        {
                            ((Label)ctrInstance).Text = "(Id=0)";
                        }
                        break;
                    case ("ListBox"):
                        ((ListBox)ctrInstance).Items.Clear();
                        break;
                    default:
                        // Do Nothing...
                        break;
                }
            }
        }

        private void udsRemoveDoel()
        {
            // Haal het doel met het hoogste volgnummer (onderste) van het scherm.
            // Er worden géén database-bewerkingen uitgevoerd!!!
            // intDoelVolgnummer: Het volgnummer op het scherm, beginnende met 1 voor het bovenste getoonde onderwijsdoel (<> DoelId in de database).

            if (intDoel > 1)
            {
                // Verwijder het onderste doel (de GroupBox met alle controls) van het form:
                Controls.RemoveByKey("grpDoel" + (intDoel).ToString());
                intDoel--;

                // Verplaats de bedienknoppen naar boven:
                grpBediening.Top -= 100;
                cmdAdd.Enabled = true;

                // Enable de delete-knop van het volgende (eentje hoger op het scherm) doel:
                Controls["grpDoel" + (intDoel).ToString()].Controls["cmdDelete" + (intDoel).ToString()].Enabled = true;
            }
            else
            {
                // Schoon het eerste doel op het form op:
                udsClearDoel(1);
                Controls["grpDoel1"].Controls["cmdDelete1"].Enabled = false;
            }
            udsBerekenTotaal();
        }

        private void udsShowDoelCompetenties(int intDoelVolgnummer)
        {
            // Vul de competentie-informatie in voor het geselecteerde doel.
            // intDoelVolgnummer:   Het volgnummer op het scherm, beginnende met 1 voor het bovenste getoonde onderwijsdoel.

            int intOnderwijsdoelId = udfDoelId(intDoelVolgnummer);
            using (SqlCommand cmdDoelCompetenties = new SqlCommand("SELECT * FROM qryDoelCompetentie WHERE OnderwijsdoelId = " + intOnderwijsdoelId + " ORDER BY CompetentieId", cnnOnderwijs))
            {
                using (SqlDataReader rdrDoelCompetenties = cmdDoelCompetenties.ExecuteReader())
                {
                    String strRegel = "";
                    ListBox lstCompetenties = (ListBox)((GroupBox)Controls["grpDoel" + intDoelVolgnummer.ToString()]).Controls["lstCompetenties" + intDoelVolgnummer.ToString()];
                    lstCompetenties.Items.Clear();
                    while (rdrDoelCompetenties.Read())
                    {
                        // Start de regel met de competentienaam:
                        strRegel = rdrDoelCompetenties["Competentie"].ToString();
                        if (strRegel.Length > 15)
                        {
                            strRegel = strRegel.Substring(0, 14) + ".";
                        }

                        // Voeg daarbij het niveau van de competentie:
                        int intAvT = (int)rdrDoelCompetenties["AvT"];
                        int intAvC = (int)rdrDoelCompetenties["AvC"];
                        int intMvZ = (int)rdrDoelCompetenties["MvZ"];
                        // strRegel += " (" + Program.intNiveau(intAvT, intAvC, intMvZ).ToString() + ")";
                        strRegel += " (" + niveau2String(intAvT) + ", ";
                        strRegel += niveau2String(intAvC) + ", ";
                        strRegel += niveau2String(intMvZ) + ")";

                        // En dan nog de gedragskenmerken (GK) die bij deze doel-competentie horen...
                        String strAfkorting = rdrDoelCompetenties["CompetentieAfkorting"].ToString();
                        using (SqlCommand cmdDoelCompetentieGedragskenmerken = new SqlCommand("SELECT * FROM qryDoelCompetentieGedragskenmerk WHERE DoelId = " + intOnderwijsdoelId.ToString() + " AND CompetentieAfkorting = '" + strAfkorting + "' ORDER BY GedragskenmerkIndex", cnnOnderwijs))
                        {
                            using (SqlDataReader rdrDoelCompetentieGedragskenmerken = cmdDoelCompetentieGedragskenmerken.ExecuteReader())
                            {
                                int intGKTeller = 0;
                                while (rdrDoelCompetentieGedragskenmerken.Read())
                                {
                                    intGKTeller++;
                                    if (intGKTeller == 1)
                                    {
                                        //strRegel += "\t ";
                                        strRegel += " / ";
                                    }
                                    else
                                    {
                                        strRegel += ", ";
                                    }
                                    strRegel += rdrDoelCompetentieGedragskenmerken["GedragskenmerkIndex"].ToString();
                                }
                            }
                        }

                        // Voeg de regel toe aan de lijst:
                        lstCompetenties.Items.Add(strRegel);
                    }
                }
            }
        }

        private void udsShowDoelBoKSItems(int intDoelVolgnummer)
        {
            // Vul de BoKS-iteminformatie in voor het geselecteerde doel.
            // intDoelVolgnummer:   Het volgnummer op het scherm, beginnende met 1 voor het bovenste getoonde onderwijsdoel.

            int intOnderwijsdoelId = udfDoelId(intDoelVolgnummer);
            using (SqlCommand cmdDoelBoKSItems = new SqlCommand("SELECT * FROM qryDoelBoKSItem WHERE OnderwijsdoelId = " + intOnderwijsdoelId + " ORDER BY BoKSItemId", cnnOnderwijs))
            {
                using (SqlDataReader rdrDoelBoKSItems = cmdDoelBoKSItems.ExecuteReader())
                {
                    String strRegel = "";
                    ListBox lstBoKSItems = (ListBox)((GroupBox)Controls["grpDoel" + intDoelVolgnummer.ToString()]).Controls["lstBoKS" + intDoelVolgnummer.ToString()];
                    lstBoKSItems.Items.Clear();
                    while (rdrDoelBoKSItems.Read())
                    {
                        // Start elke regel met de opleidingsafkorting:
                        strRegel = rdrDoelBoKSItems["Opleidingsafkorting"].ToString() + "-";

                        // Hieraan geplakt de BoKS-afkorting ("BB" staat voor BasisBoKS):
                        strRegel += rdrDoelBoKSItems["BoKSafkorting"].ToString() + "-";

                        // En dan het categorienummer en itemnummer gecombineerd:
                        String strNummer = "";
                        if (rdrDoelBoKSItems["Categorienummer"].ToString().Length == 1)
                        {
                            strNummer += ("0" + rdrDoelBoKSItems["Categorienummer"].ToString());
                        }
                        else
                        {
                            strNummer += rdrDoelBoKSItems["Categorienummer"].ToString();
                        }
                        strNummer += ".";
                        if (rdrDoelBoKSItems["Itemnummer"].ToString().Length == 1)
                        {
                            strNummer += ("0" + rdrDoelBoKSItems["Itemnummer"].ToString());
                        }
                        else
                        {
                            strNummer += rdrDoelBoKSItems["Itemnummer"].ToString();
                        }
                        strRegel += strNummer;

                        // Voeg de regel toe aan de lijst:
                        lstBoKSItems.Items.Add(strRegel);
                    }
                }
            }
        }

        private void udsShowDoelBeroepsproducten(int intDoelVolgnummer)
        {
            // Vul de beroepsproductinformatie in voor het geselecteerde doel.
            // intDoelVolgnummer:   Het volgnummer op het scherm, beginnende met 1 voor het bovenste getoonde onderwijsdoel.

            int intOnderwijsdoelId = udfDoelId(intDoelVolgnummer);
            using (SqlCommand cmdDoelBeroepsproducten = new SqlCommand("SELECT * FROM qryDoelBeroepsproduct WHERE OnderwijsdoelId = " + intOnderwijsdoelId + " ORDER BY BeroepsproductId", cnnOnderwijs))
            {
                using (SqlDataReader rdrDoelBeroepsproducten = cmdDoelBeroepsproducten.ExecuteReader())
                {
                    ListBox lstBeroepsproducten = (ListBox)((GroupBox)Controls["grpDoel" + intDoelVolgnummer.ToString()]).Controls["lstBeroepsproducten" + intDoelVolgnummer.ToString()];
                    lstBeroepsproducten.Items.Clear();
                    while (rdrDoelBeroepsproducten.Read())
                    {
                        // Voeg item toe aan de lijst:
                        lstBeroepsproducten.Items.Add(rdrDoelBeroepsproducten["Beroepsproduct"].ToString());
                    }
                }
            }
        }

        private void udsEditBoKS(int intOnderwijsdoelId)
        {
            // Open nieuw form voor het editen van de gekoppelde BoKS-items:
            // intOnderwijsdoelId: De unieke identifier (primary key) van het onderwijsdoel.
            frmBoKSkoppeling frmModal = new frmBoKSkoppeling(intOnderwijsdoelId);
            frmModal.ShowDialog();
        }

        private void udsEditCompetenties(int intOnderwijsdoelId)
        {
            // Open nieuw form voor het editen van de gekoppelde Competenties:
            // intOnderwijsdoelId: De unieke identifier (primary key) van het onderwijsdoel.
            frmCompetentiekoppeling frmModal = new frmCompetentiekoppeling(intOnderwijsdoelId);
            frmModal.ShowDialog();
        }

        private void udsEditBeroepsproducten(int intOnderwijsdoelId)
        {
            // Open nieuw form voor het editen van de gekoppelde Beroepsproducten:
            // intOnderwijsdoelId: De unieke identifier (primary key) van het onderwijsdoel.
            frmBeroepsproductkoppeling frmModal = new frmBeroepsproductkoppeling(intOnderwijsdoelId);
            frmModal.ShowDialog();
        }

        private int udfDoelId(int intDoelVolgnummer)
        {
            // Bepaal het DoelId ( is gelijk aan de primary key van het doel in de database) op basis van het volgnummer op het scherm.
            // Het DoelId zit "verstopt" in het grijze labeltje linksboven in de GroupBox:
            Label lblDoelId = (Label)Controls["grpDoel" + intDoelVolgnummer.ToString()].Controls["lblDoelId" + intDoelVolgnummer.ToString()];
            String strDoelId = lblDoelId.Text.Substring(4);
            int intDoelId = Convert.ToInt32(strDoelId.Substring(0, strDoelId.Length - 1));
            return intDoelId;
        }

        private int udsSaveDoel(int intDoelVolgnummer, ref int refRecordsUpdated, ref int refRecordsInserted)
        {
            // Sla de gegevens van het doel met intOnderwijsdoelId (primary key) op in de database.
            // Ter info: de competentie- en BoKS-koppelingen worden NIET opgeslagen. Dat is al eerder gebeurd.

            int intOnderwijsdoelId = udfDoelId(intDoelVolgnummer);
            int intToetsId = 0;
            int intRecordsUpdated = 0;
            int intRecordsInserted = 0;
            String strQuery = "";

            // Stap 1: Haal informatie uit de controls van het scherm:
            GroupBox grpDoel = (GroupBox)Controls["grpDoel" + intDoelVolgnummer.ToString()];
            int intDoeltype = ((RadioButton)grpDoel.Controls["btnLeerdoel" + intDoelVolgnummer.ToString()]).Checked ? 1 : 2;
            String strOmschrijving = Program.removeSpecialChars(((TextBox)grpDoel.Controls["txtOnderwijsdoel" + intDoelVolgnummer.ToString()]).Text);
            String strOnderwerpen = Program.removeSpecialChars(((TextBox)grpDoel.Controls["txtOnderwerpen" + intDoelVolgnummer.ToString()]).Text);
            decimal decWeging = Convert.ToDecimal(((TextBox)grpDoel.Controls["txtWeging" + intDoelVolgnummer.ToString()]).Text);
            bool blnOnthouden = ((CheckBox)grpDoel.Controls["chkOnthouden" + intDoelVolgnummer.ToString()]).Checked;
            bool blnBegrijpen = ((CheckBox)grpDoel.Controls["chkBegrijpen" + intDoelVolgnummer.ToString()]).Checked;
            bool blnToepassen = ((CheckBox)grpDoel.Controls["chkToepassen" + intDoelVolgnummer.ToString()]).Checked;
            bool blnAnalyseren = ((CheckBox)grpDoel.Controls["chkAnalyseren" + intDoelVolgnummer.ToString()]).Checked;
            bool blnEvalueren = ((CheckBox)grpDoel.Controls["chkEvalueren" + intDoelVolgnummer.ToString()]).Checked;
            bool blnCreeren = ((CheckBox)grpDoel.Controls["chkCreeren" + intDoelVolgnummer.ToString()]).Checked;

            // Stap 2: haal ToetsId op uit de database:
            String strToetscode = lstToetscodes.SelectedItem.ToString();
            String strCursuscode = lstCursuscodes.SelectedItem.ToString();
            using (SqlCommand cmdToetscode = new SqlCommand("SELECT ToetsId FROM qryToets WHERE Toetscode = '" + strToetscode + "' AND Cursuscode = '" + strCursuscode + "'", cnnOnderwijs))
            {
                using (SqlDataReader rdrToetscode = cmdToetscode.ExecuteReader())
                {
                    rdrToetscode.Read();
                    intToetsId = Convert.ToInt32(rdrToetscode["ToetsId"]);
                }
            }

            if (intOnderwijsdoelId > 0)
            {
                // A: Het doel bestaat al in de database.
                // Stap 3a: Het record wordt geüpdated:
                strQuery = "UPDATE tblDoel SET " +
                        "fkDoeltype = " + intDoeltype + ", " +
                        "strOmschrijving = '" + strOmschrijving + "', " +
                        "strOnderwerpen = '" + strOnderwerpen + "', " +
                        "decWeging = " + decWeging.ToString() + ", " +
                        "blnOnthouden = " + (blnOnthouden ? 1 : 0) + ", " +
                        "blnBegrijpen = " + (blnBegrijpen ? 1 : 0) + ", " +
                        "blnToepassen = " + (blnToepassen ? 1 : 0) + ", " +
                        "blnAnalyseren = " + (blnAnalyseren ? 1 : 0) + ", " +
                        "blnEvalueren = " + (blnEvalueren ? 1 : 0) + ", " +
                        "blnCreeren = " + (blnCreeren ? 1 : 0) +
                        " WHERE pkId = " + intOnderwijsdoelId.ToString();
                SqlTransaction transOnderwijs = cnnOnderwijs.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    using (SqlCommand cmdUpdate = new SqlCommand(strQuery, cnnOnderwijs, transOnderwijs))
                    {
                        //MessageBox.Show(cmdUpdate.CommandText);
                        int intAantalRecords = cmdUpdate.ExecuteNonQuery();
                        intRecordsUpdated += intAantalRecords;
                    }
                    transOnderwijs.Commit();
                    Program.logMessage("Commit: " + Program.removeSpecialChars(strQuery), cnnOnderwijs);
                }
                catch
                {
                    transOnderwijs.Rollback();
                    Program.logMessage("Rollback: " + Program.removeSpecialChars(strQuery), cnnOnderwijs);
                    return -1;
                }
            }
            else
            {
                // B: Het doel is nieuw.
                // Stap 3b: Lees de maximale pk-waarde uit tblDoel en hoog deze met één op. Dit is de primary key van het nieuwe record:
                int intMaxId = 0;
                SqlTransaction transOnderwijs = cnnOnderwijs.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    using (SqlCommand cmdMaxId = new SqlCommand("SELECT MAX(pkId) AS maxId FROM tblDoel", cnnOnderwijs, transOnderwijs))
                    {
                        using (SqlDataReader rdrMaxId = cmdMaxId.ExecuteReader())
                        {
                            rdrMaxId.Read();
                            if (!rdrMaxId.IsDBNull(0))
                            {
                                intMaxId = (int)rdrMaxId["maxId"];
                            }
                            rdrMaxId.Close();
                        }
                    }
                    intMaxId++;

                    // Stap 4b: Er wordt een nieuwe record aangemaakt:
                    strQuery = "INSERT INTO tblDoel (pkId, fkDoeltype, fkToets, strOmschrijving, strOnderwerpen, decWeging, blnOnthouden, blnBegrijpen, blnToepassen, blnAnalyseren, blnEvalueren, blnCreeren) " +
                        "VALUES (" +
                        intMaxId.ToString() + ", " +
                        intDoeltype.ToString() + ", " +
                        intToetsId.ToString() + ", '" +
                        strOmschrijving + "', '" +
                        strOnderwerpen + "', " +
                        decWeging.ToString() + ", " +
                        (blnOnthouden ? 1 : 0) + ", " +
                        (blnBegrijpen ? 1 : 0) + ", " +
                        (blnToepassen ? 1 : 0) + ", " +
                        (blnAnalyseren ? 1 : 0) + ", " +
                        (blnEvalueren ? 1 : 0) + ", " +
                        (blnCreeren ? 1 : 0) + ")";
                    using (SqlCommand cmdInsert = new SqlCommand(strQuery, cnnOnderwijs, transOnderwijs))
                    {
                        int intAantalRecords = cmdInsert.ExecuteNonQuery();
                        intRecordsInserted += intAantalRecords;
                    }
                    transOnderwijs.Commit();
                    Program.logMessage("Commit: " + Program.removeSpecialChars(strQuery), cnnOnderwijs);
                }
                catch
                {
                    transOnderwijs.Rollback();
                    Program.logMessage("Rollback: " + Program.removeSpecialChars(strQuery), cnnOnderwijs);
                    return -2;
                }
                // Stap 5b: Toon het doel-id (is de primary key) in het form:
                ((Label)grpDoel.Controls["lblDoelId" + intDoelVolgnummer.ToString()]).Text = "(Id=" + intMaxId.ToString() + ")";
            }
            refRecordsUpdated += intRecordsUpdated;
            refRecordsInserted += intRecordsInserted;
            return 1;
        }

        private void udsSaveDoelen()
        {
            // Sla de gegevens van alle doelen op het scherm op.
            // Ter info: de competentie- en BoKS-koppelingen worden NIET opgeslagen. Dat is al eerder gebeurd.
            int intRecordsUpdated = 0;
            int intRecordsInserted = 0;
            for (int i = 1; i <= intDoel; i++)
            {
                if (udsSaveDoel(i, ref intRecordsUpdated, ref intRecordsInserted) < 0)
                {
                    MessageBox.Show("Iets gaat hier niet chocotof!", "Whoopsy Daisy...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (Globals.DEBUG)
            {
                MessageBox.Show("Aantal records updated: " + intRecordsUpdated.ToString() + "\nAantal records inserted: " + intRecordsInserted.ToString(), "Doelen opslaan...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtWeging_TextChanged(object sender, EventArgs e)
        {
            udsBerekenTotaal();
            blnErIsIetsGewijzigd = true;
        }

        private void udsBerekenTotaal()
        {
            int intTotaal = 0;
            for (int i = 1; i <= intDoel; i++)
            {
                if (Int32.TryParse(((TextBox)Controls["grpDoel" + i.ToString()].Controls["txtWeging" + i.ToString()]).Text, out int intNumber))
                {
                    intTotaal += intNumber;
                }
            }
            txtTotaal.Text = intTotaal.ToString();
        }

        private void txtWeging_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == '\b');
        }

        private void txtTotaal_TextChanged(object sender, EventArgs e)
        {
            if (((Label)sender).Text.Equals("100"))
            {
                ((Label)sender).BackColor = Color.FromArgb(192, 255, 192);
            }
            else
            {
                ((Label)sender).BackColor = Color.FromArgb(255, 192, 192);
            }
        }

        private void cmdDoelenOpslaan_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Alle bovenstaande onderwijsdoelen worden opgeslagen. Weet je het zeker?", "Onderwijsdoelen opslaan...", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                udsSaveDoelen();
                blnErIsIetsGewijzigd = false;
            }
        }

        private void txtGeneral_TextChanged(object sender, EventArgs e)
        {
            // Er is iets gewijzigd...
            blnErIsIetsGewijzigd = true;
        }

        private void chkGeneral_CheckedChanged(object sender, EventArgs e)
        {
            // Er is iets gewijzigd...
            blnErIsIetsGewijzigd = true;
        }

        private void btnGeneral_CheckedChanged(object sender, EventArgs e)
        {
            // Er is iets gewijzigd...
            blnErIsIetsGewijzigd = true;
        }

        private void cmdToetsdetails_Click(object sender, EventArgs e)
        {
            // Open detailformulier van geselecteerde toets:
            frmCursus frmModal = new frmCursus(lstToetscodes.SelectedItem.ToString());
            frmModal.ShowDialog();

        }

        private void blokSelectie(object sender, EventArgs e)
        {
            RadioButton radiobutton = (RadioButton)sender;
            if (!radiobutton.Checked)
            {
                if (blnStartupReady && blnErIsIetsGewijzigd)
                {
                    // Alle wijzigingen van de "vorige" toetscode opgeslagen?
                    if (MessageBox.Show("Moeten er nog wijzigingen opgeslagen worden?", "Blokselectie...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // Cancel...
                        return;
                    }
                }
                foreach (RadioButton rbInstance in grpBlokSelectie.Controls)
                {
                    rbInstance.Checked = false;
                }
                radiobutton.Checked = true;
                blokFilter = Convert.ToInt32(radiobutton.Tag);
                blnErIsIetsGewijzigd = false;

                // Modulecodes opnieuw in lijst plaatsen:
                udsStatischeVulling();
            }
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.logMessage("Applicatie gestopt.", cnnOnderwijs);
        }

        private void lstBoKS_Click(object sender, EventArgs e)
        {
            int intGeselecteerdDoel = Convert.ToInt32(((ListBox)sender).Name.Substring(((ListBox)sender).Name.Length - 1, 1));
            int intDoelId = udfDoelId(intGeselecteerdDoel);
            if (intDoelId > 0)
            {
                // Open het formulier en edit de gekoppelde BoKS-items:
                udsEditBoKS(intDoelId);

                // Ververs de BoKS-info in het hoofdscherm:
                udsShowDoelBoKSItems(intGeselecteerdDoel);

            }
            else
            {
                MessageBox.Show("Sla het nieuwe onderwijsdoel eerst op alvorens BoKS-items te koppelen.", "BoKS-items koppelen (DoelId = 0)", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void lstCompetenties_Click(object sender, EventArgs e)
        {
            int intGeselecteerdDoel = Convert.ToInt32(((ListBox)sender).Name.Substring(((ListBox)sender).Name.Length - 1, 1));
            int intDoelId = udfDoelId(intGeselecteerdDoel);
            if (intDoelId > 0)
            {
                // Open het formulier en edit de gekoppelde competenties:
                udsEditCompetenties(intDoelId);

                // Ververs de competentie-info in het hoofdscherm:
                udsShowDoelCompetenties(intGeselecteerdDoel);
            }
            else
            {
                MessageBox.Show("Sla het nieuwe onderwijsdoel eerst op alvorens competenties te koppelen.", "Competenties koppelen (DoelId = 0)", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void lstBeroepsproducten_Click(object sender, EventArgs e)
        {
            int intGeselecteerdDoel = Convert.ToInt32(((ListBox)sender).Name.Substring(((ListBox)sender).Name.Length - 1, 1));
            int intDoelId = udfDoelId(intGeselecteerdDoel);
            if (intDoelId > 0)
            {
                // Open het formulier en edit de gekoppelde beroepsproducten:
                udsEditBeroepsproducten(intDoelId);

                // Ververs de beroepsproductinfo-info in het hoofdscherm:
                udsShowDoelBeroepsproducten(intGeselecteerdDoel);
            }
            else
            {
                MessageBox.Show("Sla het nieuwe onderwijsdoel eerst op alvorens beroepsproducten te koppelen.", "Beroepsproducten koppelen (DoelId = 0)", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private String niveau2String(int niveau)
        {
            switch (niveau)
            {
                case 1:
                    return "I";
                case 2:
                    return "II";
                case 3:
                    return "III";
                default:
                    return "0";
            }
        }

        private void chkGecontroleerd_Click(object sender, EventArgs e)
        {
            // Toevoeging na presentatie aan team:

            // Stap 1: haal ToetsId op uit de database:
            int intToetsId = 0;
            String strToetscode = lstToetscodes.SelectedItem.ToString();
            String strCursuscode = lstCursuscodes.SelectedItem.ToString();
            using (SqlCommand cmdToetscode = new SqlCommand("SELECT ToetsId FROM qryToets WHERE Toetscode = '" + strToetscode + "' AND Cursuscode = '" + strCursuscode + "'", cnnOnderwijs))
            {
                using (SqlDataReader rdrToetscode = cmdToetscode.ExecuteReader())
                {
                    rdrToetscode.Read();
                    intToetsId = Convert.ToInt32(rdrToetscode["ToetsId"]);
                }
            }

            // Stap 2: Update toets-record in tblToets:
            String strQuery = "UPDATE tblToets SET blnGecontroleerd = ";
            String strControleur;
            String strTijdstip;
            if (!chkGecontroleerd.Checked)
            {
                strControleur = Environment.UserName;
                strTijdstip = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                strQuery += "1";
            }
            else
            {
                strControleur = "";
                strTijdstip = "";
                strQuery += "0";
            }
            strQuery += ", strGecontroleerd = '" + strControleur + "', dtmGecontroleerd = '" + strTijdstip + "' WHERE pkId = " + intToetsId.ToString();

            SqlTransaction transOnderwijs = cnnOnderwijs.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                using (SqlCommand cmdUpdate = new SqlCommand(strQuery, cnnOnderwijs, transOnderwijs))
                {
                    int intAantalRecords = cmdUpdate.ExecuteNonQuery();
                }
                transOnderwijs.Commit();
                txtControleur.Text = strControleur;
                txtTijdstip.Text = strTijdstip;
                chkGecontroleerd.Checked = !chkGecontroleerd.Checked;
                Program.logMessage("Gecontroleerd: " + Program.removeSpecialChars(strQuery), cnnOnderwijs);
            }
            catch
            {
                transOnderwijs.Rollback();
                Program.logMessage("Rollback: " + Program.removeSpecialChars(strQuery), cnnOnderwijs);
                MessageBox.Show("Iets gaat hier niet chocotof!", "Whoopsy Daisy...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdExport_Click(object sender, EventArgs e)
        {
            //Export het huidige blad naar een Excel-bestand:
            Excel.Application xlApplication = new Excel.Application();
            Excel.Workbook xlWorkbook;
            Excel.Worksheet xlWorksheet;
            object misValue = System.Reflection.Missing.Value;

            this.Text = formTitel + " Exporteren gestart...";

            try //Kopieer leeg bestand (tm.xlsx) naar tijdelijk bestand (tijdelijk.xlsx):
            {
                File.Copy(pathToInstallation + @"\tm.xlsx", pathToTemp + @"\tijdelijk.xlsx", true);
                File.SetAttributes(pathToTemp + @"\tijdelijk.xlsx", File.GetAttributes(pathToTemp + @"\tijdelijk.xlsx") & ~FileAttributes.ReadOnly);
            }
            catch
            {
                String errorMessage = "Iets gaat er mis bij het maken van een tijdelijk bestand!";
                MessageBox.Show(errorMessage, "Whoopsy Daisy...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.logMessage("Exporteren - MISLUKT: " + errorMessage, cnnOnderwijs);
                xlApplication.Quit();
                releaseObject(xlApplication);
                this.Text = formTitel + " Exporteren mislukt";
                tmrFormTitel.Enabled = true;
                return;
            }

            try //Open Workbook:
            {
                xlWorkbook = xlApplication.Workbooks.Open(pathToTemp + @"\tijdelijk.xlsx");
            }
            catch
            {
                String errorMessage = "Iets gaat er mis bij het openen van xlWorkbook!";
                MessageBox.Show(errorMessage, "Whoopsy Daisy...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.logMessage("Exporteren - MISLUKT: " + errorMessage, cnnOnderwijs);
                xlApplication.Quit();
                releaseObject(xlApplication);
                this.Text = formTitel + " Exporteren mislukt";
                tmrFormTitel.Enabled = true;
                return;
            }

            try //Select Worksheet:
            {
                xlWorksheet = (Excel.Worksheet)xlWorkbook.Worksheets.get_Item("Toetsmatrijs");
            }
            catch
            {
                String errorMessage = "Iets gaat er mis bij het selecteren van xlWorksheet!";
                MessageBox.Show(errorMessage, "Whoopsy Daisy...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.logMessage("Exporteren - MISLUKT: " + errorMessage, cnnOnderwijs);
                xlApplication.Quit();
                releaseObject(xlWorkbook);
                releaseObject(xlApplication);
                this.Text = formTitel + " Exporteren mislukt";
                tmrFormTitel.Enabled = true;
                return;
            }

            //Toestinfo:
            xlWorksheet.Cells[1, 1] = "TOETSMATRIJS (export d.d. " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ")";
            xlWorksheet.Cells[3, 2] = txtToetscode.Text;
            xlWorksheet.Cells[4, 2] = txtCursuscode.Text;
            xlWorksheet.Cells[5, 2] = txtBlok.Text;
            xlWorksheet.Cells[6, 2] = txtToetsvorm.Text;
            xlWorksheet.Cells[7, 2] = txtBeoordelingswijze.Text;

            for (int doel = 1; doel <= intDoel; doel++)
            {
                xlWorksheet.Cells[9 + doel, 1] = ((TextBox)Controls["grpDoel" + doel.ToString()].Controls["txtOnderwijsdoel" + doel.ToString()]).Text;
                xlWorksheet.Cells[9 + doel, 3] = ((TextBox)Controls["grpDoel" + doel.ToString()].Controls["txtOnderwerpen" + doel.ToString()]).Text;
                xlWorksheet.Cells[9 + doel, 4] = ((TextBox)Controls["grpDoel" + doel.ToString()].Controls["txtWeging" + doel.ToString()]).Text;
                xlWorksheet.Cells[9 + doel, 5] = ((CheckBox)Controls["grpDoel" + doel.ToString()].Controls["chkOnthouden" + doel.ToString()]).Checked ? "X" : "";
                xlWorksheet.Cells[9 + doel, 6] = ((CheckBox)Controls["grpDoel" + doel.ToString()].Controls["chkBegrijpen" + doel.ToString()]).Checked ? "X" : "";
                xlWorksheet.Cells[9 + doel, 7] = ((CheckBox)Controls["grpDoel" + doel.ToString()].Controls["chkToepassen" + doel.ToString()]).Checked ? "X" : "";
                xlWorksheet.Cells[9 + doel, 8] = ((CheckBox)Controls["grpDoel" + doel.ToString()].Controls["chkAnalyseren" + doel.ToString()]).Checked ? "X" : "";
                xlWorksheet.Cells[9 + doel, 9] = ((CheckBox)Controls["grpDoel" + doel.ToString()].Controls["chkEvalueren" + doel.ToString()]).Checked ? "X" : "";
                xlWorksheet.Cells[9 + doel, 10] = ((CheckBox)Controls["grpDoel" + doel.ToString()].Controls["chkCreeren" + doel.ToString()]).Checked ? "X" : "";
                String strLijst = "";
                foreach (string strItem in ((ListBox)Controls["grpDoel" + doel.ToString()].Controls["lstBoKS" + doel.ToString()]).Items)
                {
                    strLijst += strItem + "\n";
                }
                xlWorksheet.Cells[9 + doel, 11] = strLijst;
                strLijst = "";
                foreach (string strItem in ((ListBox)Controls["grpDoel" + doel.ToString()].Controls["lstCompetenties" + doel.ToString()]).Items)
                {
                    strLijst += strItem + "\n";
                }
                xlWorksheet.Cells[9 + doel, 12] = strLijst;
                strLijst = "";
                foreach (string strItem in ((ListBox)Controls["grpDoel" + doel.ToString()].Controls["lstBeroepsproducten" + doel.ToString()]).Items)
                {
                    strLijst += strItem + "\n";
                }
                xlWorksheet.Cells[9 + doel, 13] = strLijst;
            }

            try //Save As Workbook:
            {
                saveToetsmatrijs.FileName = "Toetsmatrijs " + txtToetscode.Text + ".xlsx";

                if (saveToetsmatrijs.ShowDialog() == DialogResult.OK)
                {
                    xlWorkbook.SaveCopyAs(saveToetsmatrijs.FileName);
                }
                else
                {
                    this.Text = formTitel + " Exporteren geannuleerd";
                    this.Refresh();
                }
            }
            catch
            {
                String errorMessage = "Iets gaat er mis bij het opslaan van xlWorkbook!";
                MessageBox.Show(errorMessage, "Whoopsy Daisy...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.logMessage("Exporteren - MISLUKT: " + errorMessage, cnnOnderwijs);
                this.Text = formTitel + " Exporteren mislukt";
            }

            try //Close Workbook:
            {
                xlWorkbook.Close(false, misValue, misValue);
            }
            catch
            {
                String errorMessage = "Iets gaat er mis bij het sluiten van xlWorkbook!";
                MessageBox.Show(errorMessage, "Whoopsy Daisy...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.logMessage("Exporteren - MISLUKT: " + errorMessage, cnnOnderwijs);
                this.Text = formTitel + " Afbouwen mislukt";
            }

            try //Quit Excel:
            {
                xlApplication.Quit();
            }
            catch
            {
                String errorMessage = "Iets gaat er mis bij het afsluiten van xlApplication!";
                MessageBox.Show(errorMessage, "Whoopsy Daisy...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.logMessage("Exporteren - MISLUKT: " + errorMessage, cnnOnderwijs);
                this.Text = formTitel + " Afbouwen mislukt";
            }

            releaseObject(xlWorksheet);
            releaseObject(xlWorkbook);
            releaseObject(xlApplication);

            try //Delete tijdelijk.xlsx:
            {
                File.Delete(pathToTemp + @"\tijdelijk.xlsx");
            }
            catch
            {
                String errorMessage = "Iets gaat er mis bij het verwijderen van het tijdelijke bestand!";
                MessageBox.Show(errorMessage, "Whoopsy Daisy...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.logMessage("Exporteren - MISLUKT: " + errorMessage, cnnOnderwijs);
                this.Text = formTitel + " Afbouwen mislukt";
            }

            if (!(this.Text.Contains("mislukt") || this.Text.Contains("geannuleerd")))
            {
                this.Text = formTitel + " Exporteren gereed";
                Program.logMessage("Toetsmatrijs van Module " + txtCursuscode.Text + ", Toetscode " + txtToetscode.Text + " is geëxporteerd.", cnnOnderwijs);
            }
            tmrFormTitel.Enabled = true;
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Unable to release the Object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void tmrFormTitel_Tick(object sender, EventArgs e)
        {
            this.Text = formTitel;
            tmrFormTitel.Enabled = false;
        }
    }
}
