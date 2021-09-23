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
        private bool blnNeeGeantwoord = false;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            cnnOnderwijs.Open();
            udsStatischeVulling();
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
            using (SqlCommand cmdCursus = new SqlCommand("SELECT * FROM tblCursus ORDER BY strCode", cnnOnderwijs))
            {
                using (SqlDataReader rdrCursus = cmdCursus.ExecuteReader())
                {
                    while (rdrCursus.Read())
                    {
                        lstCursuscodes.Items.Add(rdrCursus["strCode"].ToString());
                    }
                }
            }
            lstCursuscodes.SelectedIndex = 0;
        }

        private void lstCursuscodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Op dezelfde cursuscode geklikt:
            if (lstCursuscodes.SelectedIndex == intSelectedCursusIndex)
            {
                return;
            }

            if (blnStartupReady && blnErIsIetsGewijzigd)
            {
                // Alle wijzigingen van de "vorige" toetscode opgeslagen?
                if (MessageBox.Show("Moeten er nog wijzigingen opgeslagen worden?", "Verandering van toetscode...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Ga terug...
                    lstCursuscodes.SelectedIndex = intSelectedCursusIndex;
                    lstToetscodes.SelectedIndex = intSelectedToetsIndex;
                    return;
                }
                else
                {
                    blnNeeGeantwoord = true;
                }
            }

            // Onthoud de geselecteerde index voor later...
            intSelectedCursusIndex = lstCursuscodes.SelectedIndex;

            String strCursuscode = ((ListBox)sender).SelectedItem.ToString();

            // Toetscodes:
            lstToetscodes.Items.Clear();
            using (SqlCommand cmdToets = new SqlCommand("SELECT * FROM qryCursusToets WHERE Cursuscode = '" + strCursuscode + "' ORDER BY ToetsCode", cnnOnderwijs))
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
            // Op dezelfde cursuscode/toetscode geklikt:
            if (lstCursuscodes.SelectedIndex == intPrevCursusIndex && lstToetscodes.SelectedIndex == intSelectedToetsIndex)
            {
                return;
            }

            if (blnStartupReady && blnErIsIetsGewijzigd && !blnNeeGeantwoord)
            {
                // Alle wijzigingen van de "vorige" toetscode opgeslagen?
                blnNeeGeantwoord = false;
                if (MessageBox.Show("Moeten er nog wijzigingen opgeslagen worden?", "Verandering van toetscode...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Ga terug...
                    lstToetscodes.SelectedIndex = intSelectedToetsIndex;
                    return;
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
            using (SqlCommand cmdOnderwijsdoel = new SqlCommand("SELECT * FROM qryDoel WHERE Toetscode = '" + strToetscode + "' AND Cursuscode = '" + strCursuscode + "' ORDER BY DoelId", cnnOnderwijs))
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
            using (SqlCommand cmdCursusToets = new SqlCommand("SELECT * FROM qryCursusToets WHERE Cursuscode = '" + strCursuscode + "'", cnnOnderwijs))
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
                if (MessageBox.Show(this, "Het onderwijsdoel wordt verwijderd (óók uit de database). Weet je het zeker?", "Onderwijsdoel verwijderen...", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
                udsSaveDoel(Convert.ToInt32(((Button)sender).Name.Substring(((Button)sender).Name.Length - 1, 1)), ref intRecordsUpdated, ref intRecordsInserted);
                MessageBox.Show("Aantal records updated: " + intRecordsUpdated.ToString() + "\nAantal records inserted: " + intRecordsInserted.ToString(), "Doelen opslaan...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            /*
            udsClearDoel(Convert.ToInt32(((Button)sender).Name.Substring(((Button)sender).Name.Length - 1, 1)));
            */
        }

        private void cmdBoKS_Click(object sender, EventArgs e)
        {
            int intGeselecteerdDoel = Convert.ToInt32(((Button)sender).Name.Substring(((Button)sender).Name.Length - 1, 1));
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

        private void cmdCompetenties_Click(object sender, EventArgs e)
        {
            int intGeselecteerdDoel = Convert.ToInt32(((Button)sender).Name.Substring(((Button)sender).Name.Length - 1, 1));
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
                            case ("cmdClear"):
                                cmdTo.Click += new System.EventHandler(cmdClear_Click);
                                break;
                            case ("cmdBoKS"):
                                cmdTo.Click += new System.EventHandler(cmdBoKS_Click);
                                break;
                            case ("cmdCompetenties"):
                                cmdTo.Click += new System.EventHandler(cmdCompetenties_Click);
                                break;
                            case ("cmdSave"):
                                cmdTo.Click += new System.EventHandler(cmdSave_Click);
                                break;
                            default:
                                // Debug:
                                MessageBox.Show(ctrFrom.Name, "Geen event gekoppeld!");
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
                using (SqlCommand cmdDelete = new SqlCommand("DELETE FROM tblDoel WHERE pkId = " + intDoelToDeleteId.ToString(), cnnOnderwijs))
                {
                    //MessageBox.Show("Doel met ID = " + intDoelToDeleteId.ToString() + " wordt verwijderd!");
                    int intAantalRecords = cmdDelete.ExecuteNonQuery();
                    intRecordsDeleted += intAantalRecords;
                }
            }
            udsRemoveDoel();
            MessageBox.Show("Aantal records verwijderd: " + intRecordsDeleted.ToString(), "Doel verwijderen...", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        strRegel += " (" + Program.intNiveau(intAvT, intAvC, intMvZ).ToString() + ")";

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
                                        strRegel += "\t ";
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

        private int udfDoelId(int intDoelVolgnummer)
        {
            // Bepaal het DoelId ( is gelijk aan de primary key van het doel in de database) op basis van het volgnummer op het scherm.
            // Het DoelId zit "verstopt" in het grijze labeltje linksboven in de GroupBox:
            Label lblDoelId = (Label)Controls["grpDoel" + intDoelVolgnummer.ToString()].Controls["lblDoelId" + intDoelVolgnummer.ToString()];
            String strDoelId = lblDoelId.Text.Substring(4);
            int intDoelId = Convert.ToInt32(strDoelId.Substring(0, strDoelId.Length - 1));
            return intDoelId;
        }

        private void udsSaveDoel(int intDoelVolgnummer, ref int refRecordsUpdated, ref int refRecordsInserted)
        {
            // Sla de gegevens van het doel met intOnderwijsdoelId (primary key) op in de database.
            // Ter info: de competentie- en BoKS-koppelingen worden NIET opgeslagen. Dat is al eerder gebeurd.

            int intOnderwijsdoelId = udfDoelId(intDoelVolgnummer);
            int intToetsId = 0;
            int intRecordsUpdated = 0;
            int intRecordsInserted = 0;

            /*
             LET OP: HIERONDER MOET NOG GECONTROLEERD WORDEN OP SQL-TEKENS (strOmschrijving EN strOnderwerpen) DIE DE BOEL KUNNEN LATEN CRASHEN!
             */

            // Stap 1: Haal informatie uit de controls van het scherm:
            GroupBox grpDoel = (GroupBox)Controls["grpDoel" + intDoelVolgnummer.ToString()];
            int intDoeltype = ((RadioButton)grpDoel.Controls["btnLeerdoel" + intDoelVolgnummer.ToString()]).Checked ? 1 : 2;
            String strOmschrijving = removeSpecialChars(((TextBox)grpDoel.Controls["txtOnderwijsdoel" + intDoelVolgnummer.ToString()]).Text);
            String strOnderwerpen = removeSpecialChars(((TextBox)grpDoel.Controls["txtOnderwerpen" + intDoelVolgnummer.ToString()]).Text);
            decimal decWeging = Convert.ToDecimal(((TextBox)grpDoel.Controls["txtWeging" + intDoelVolgnummer.ToString()]).Text);
            bool blnOnthouden = ((CheckBox)grpDoel.Controls["chkOnthouden" + intDoelVolgnummer.ToString()]).Checked;
            bool blnBegrijpen = ((CheckBox)grpDoel.Controls["chkBegrijpen" + intDoelVolgnummer.ToString()]).Checked;
            bool blnToepassen = ((CheckBox)grpDoel.Controls["chkToepassen" + intDoelVolgnummer.ToString()]).Checked;
            bool blnAnalyseren = ((CheckBox)grpDoel.Controls["chkAnalyseren" + intDoelVolgnummer.ToString()]).Checked;
            bool blnEvalueren = ((CheckBox)grpDoel.Controls["chkEvalueren" + intDoelVolgnummer.ToString()]).Checked;
            bool blnCreeren = ((CheckBox)grpDoel.Controls["chkCreeren" + intDoelVolgnummer.ToString()]).Checked;

            // Stap 2: haal ToetsId op uit de database:
            String strToetscode = lstToetscodes.SelectedItem.ToString();
            using (SqlCommand cmdToetscode = new SqlCommand("SELECT pkId FROM tblToets WHERE strCode = '" + strToetscode + "'", cnnOnderwijs))
            {
                using (SqlDataReader rdrToetscode = cmdToetscode.ExecuteReader())
                {
                    rdrToetscode.Read();
                    intToetsId = Convert.ToInt32(rdrToetscode["pkId"]);
                }
            }

            if (intOnderwijsdoelId > 0)
            {
                // A: Het doel bestaat al in de database.
                // Stap 3a: Het record wordt geüpdated:
                using (SqlCommand cmdUpdate = new SqlCommand("UPDATE tblDoel SET " +
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
                    " WHERE pkId = " + intOnderwijsdoelId.ToString(), cnnOnderwijs))
                {
                    //MessageBox.Show(cmdUpdate.CommandText);
                    int intAantalRecords = cmdUpdate.ExecuteNonQuery();
                    intRecordsUpdated += intAantalRecords;
                }
            }
            else
            {
                // B: Het doel is nieuw.
                // Stap 3b: Lees de maximale pk-waarde uit tblDoel en hoog deze met één op. Dit is de primary key van het nieuwe record:
                int intMaxId = 0;
                using (SqlCommand cmdMaxId = new SqlCommand("SELECT MAX(pkId) AS maxId FROM tblDoel", cnnOnderwijs))
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
                using (SqlCommand cmdInsert = new SqlCommand("INSERT INTO tblDoel (pkId, fkDoeltype, fkToets, strOmschrijving, strOnderwerpen, decWeging, blnOnthouden, blnBegrijpen, blnToepassen, blnAnalyseren, blnEvalueren, blnCreeren) " +
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
                    (blnCreeren ? 1 : 0) + ")", cnnOnderwijs))
                {
                    //MessageBox.Show(cmdInsert.CommandText);
                    int intAantalRecords = cmdInsert.ExecuteNonQuery();
                    intRecordsInserted += intAantalRecords;
                }

                // Sap 5b: Toon het doel-id (is de primary key) in het form:
                ((Label)grpDoel.Controls["lblDoelId" + intDoelVolgnummer.ToString()]).Text = "(Id=" + intMaxId.ToString() + ")";
            }
            refRecordsUpdated += intRecordsUpdated;
            refRecordsInserted += intRecordsInserted;
        }

        private void udsSaveDoelen()
        {
            // Sla de gegevens van alle doelen op het scherm op.
            // Ter info: de competentie- en BoKS-koppelingen worden NIET opgeslagen. Dat is al eerder gebeurd.
            int intRecordsUpdated = 0;
            int intRecordsInserted = 0;
            for (int i = 1; i <= intDoel; i++)
            {
                udsSaveDoel(i, ref intRecordsUpdated, ref intRecordsInserted);
            }
            MessageBox.Show("Aantal records updated: " + intRecordsUpdated.ToString() + "\nAantal records inserted: " + intRecordsInserted.ToString(), "Doelen opslaan...", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (MessageBox.Show(this, "Alle wijzigingen in de onderwijsdoelen worden opgeslagen. Weet je het zeker?", "Onderwijsdoelen opslaan...", MessageBoxButtons.YesNo) == DialogResult.Yes)
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

        private String removeSpecialChars(String input)
        {
            String output = input;
            output = Regex.Replace(output, @"[\""'“”‘’]", "`");
            output = Regex.Replace(output, @"[^0-9a-zA-Z,\.\(\) /><`ëé/+?:\x0A\x0D\-–;\*]", "_");
            return output;
        }
    }
}
