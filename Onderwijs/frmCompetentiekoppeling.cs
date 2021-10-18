using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Onderwijs
{
    public partial class frmCompetentiekoppeling : Form
    {
        private SqlConnection cnnOnderwijs = new SqlConnection("Data Source=" + Globals.DB_SERVER + ";Initial Catalog=" + Globals.DB_NAME + ";User ID=" + Globals.DB_USER + ";Password=" + Globals.DB_PASSWORD + ";MultipleActiveResultSets=true;");
        private List<String> strNiveauAvT = new List<String>();
        private List<String> strNiveauAvC = new List<String>();
        private List<String> strNiveauMvZ = new List<String>();
        private bool blnControlsReady = false;
        private bool blnFormLoadReady = false;
        private int intDoelID;
        private bool blnErIsIetsGewijzigd = false;

        public frmCompetentiekoppeling(int intDoelID_)
        {
            InitializeComponent();
            intDoelID = intDoelID_;
        }

        private void frmCompetentiekoppeling_Load(object sender, EventArgs e)
        {
            cnnOnderwijs.Open();
            udsNiveauOmschrijvingen();
            udsCreeerTabbladen();
            blnControlsReady = udfCreeerControls();
            udsStatischeVulling();
            if (intDoelID > 0)
            {
                udsDynamischeVulling(intDoelID);
            }
            blnFormLoadReady = true;
            blnErIsIetsGewijzigd = false;
        }

        private void udsDynamischeVulling(int intDoelID)
        {
            // Onderwijsdoel:
            using (SqlCommand cmdDoel = new SqlCommand("SELECT strOmschrijving FROM tblDoel WHERE pkId = " + intDoelID.ToString(), cnnOnderwijs))
            {
                using (SqlDataReader rdrDoel = cmdDoel.ExecuteReader())
                {
                    if (rdrDoel.HasRows)
                    {
                        while (rdrDoel.Read())
                        {
                            txtOnderwijsdoel.Text = rdrDoel["strOmschrijving"].ToString();
                        }
                    }
                    rdrDoel.Close();
                }
            }

            // Gekoppelde competenties:
            using (SqlCommand cmdDoelCompetentie = new SqlCommand("SELECT * FROM qryDoelCompetentie WHERE OnderwijsdoelId = " + intDoelID.ToString(), cnnOnderwijs))
            {
                using (SqlDataReader rdrDoelCompetentie = cmdDoelCompetentie.ExecuteReader())
                {
                    if (rdrDoelCompetentie.HasRows)
                    {
                        while (rdrDoelCompetentie.Read())
                        {

                            // Kies de juiste tab:
                            String strCompetentieAfkorting = rdrDoelCompetentie["CompetentieAfkorting"].ToString();
                            TabPage tabInstance = tabCompetenties.TabPages["tab" + strCompetentieAfkorting];

                            // Toon dat de competentie gekoppeld is:
                            ((CheckBox)tabInstance.Controls["chk" + strCompetentieAfkorting + "Gekoppeld"]).Checked = true;
                            tabInstance.Text = tabInstance.ToolTipText.ToUpper();

                            // Toon competentieniveaus:
                            ((RadioButton)tabInstance.Controls["grp" + strCompetentieAfkorting + "AvT"].Controls["btn" + strCompetentieAfkorting + "T" + rdrDoelCompetentie["AvT"].ToString()]).Checked = true;
                            ((RadioButton)tabInstance.Controls["grp" + strCompetentieAfkorting + "AvC"].Controls["btn" + strCompetentieAfkorting + "C" + rdrDoelCompetentie["AvC"].ToString()]).Checked = true;
                            ((RadioButton)tabInstance.Controls["grp" + strCompetentieAfkorting + "MvZ"].Controls["btn" + strCompetentieAfkorting + "Z" + rdrDoelCompetentie["MvZ"].ToString()]).Checked = true;

                            // Toon geselecteerde gedragskenmerken:
                            using (SqlCommand cmdKenmerk = new SqlCommand("SELECT * FROM qryDoelCompetentieGedragskenmerk WHERE DoelId = " + intDoelID.ToString() + " AND CompetentieAfkorting = '" + strCompetentieAfkorting + "'", cnnOnderwijs))
                            {
                                using (SqlDataReader rdrKenmerk = cmdKenmerk.ExecuteReader())
                                {
                                    if (rdrKenmerk.HasRows)
                                    {
                                        while (rdrKenmerk.Read())
                                        {
                                            // Kenmerk selecteren in de CheckedListBox:
                                            ((CheckedListBox)tabInstance.Controls["grp" + strCompetentieAfkorting + "Gedragskenmerken"].Controls["chklst" + strCompetentieAfkorting + "Gedragskenmerken"]).SetItemChecked((int)Convert.ToChar(rdrKenmerk["GedragskenmerkIndex"].ToString()) - (int)Convert.ToChar("a"), true);
                                        }
                                    }
                                    rdrKenmerk.Close();
                                }
                            }
                        }
                        rdrDoelCompetentie.Close();
                    }
                }
            }
        }

        private void udsCreeerTabbladen()
        {
            //Creeer voor elke competentie een tabblad:
            using (SqlCommand cmdCompetentie = new SqlCommand("SELECT strAfkorting, strNaam FROM tblCompetentie ORDER BY pkId", cnnOnderwijs))
            {
                using (SqlDataReader rdrCompetentie = cmdCompetentie.ExecuteReader())
                {
                    while (rdrCompetentie.Read())
                    {
                        TabPage tabInstance = new TabPage();
                        tabInstance.Name = "tab" + rdrCompetentie["strAfkorting"].ToString();
                        tabInstance.Tag = rdrCompetentie["strAfkorting"].ToString();
                        tabInstance.Text = rdrCompetentie["strNaam"].ToString();
                        tabInstance.ToolTipText = rdrCompetentie["strNaam"].ToString();
                        tabCompetenties.TabPages.Add(tabInstance);
                    }
                }
            }
        }

        private bool udfCreeerControls()
        {
            // Loop door alle tabbladen om controls te plaatsen:
            foreach (TabPage tabInstance in tabCompetenties.TabPages)
            {
                if (!(tabInstance.Tag.ToString().Equals("INI")))
                {
                    udsCloneControls(tabCompetenties.TabPages[0], tabInstance);
                }
            }
            tabCompetenties.TabPages.Remove(tabCompetenties.TabPages[0]);
            return true;
        }

        private void udsCloneControls(Control ctrParentFrom, Control ctrParentTo)
        {
            //Kopieer alle controls van ctrParentFrom (tabINI) naar ctrParentTo (alle Competentie-tabs):
            foreach (Control ctrFrom in ctrParentFrom.Controls)
            {
                switch (ctrFrom.GetType().Name)
                {
                    case ("Label"):
                        Label lblTo = new Label();
                        lblTo.Location = ctrFrom.Location;
                        lblTo.Size = ctrFrom.Size;
                        lblTo.Name = ctrFrom.Name.Substring(0, 3) + ctrParentTo.Tag.ToString() + ctrFrom.Name.Substring(6);
                        lblTo.Text = ctrFrom.Text;
                        lblTo.Font = ctrFrom.Font;
                        ctrParentTo.Controls.Add(lblTo);
                        break;
                    case ("CheckBox"):
                        CheckBox chkTo = new CheckBox();
                        chkTo.Location = ctrFrom.Location;
                        chkTo.Size = ctrFrom.Size;
                        chkTo.Name = ctrFrom.Name.Substring(0, 3) + ctrParentTo.Tag.ToString() + ctrFrom.Name.Substring(6);
                        chkTo.Tag = ctrFrom.Tag;
                        chkTo.Text = ctrFrom.Text;
                        chkTo.CheckedChanged += new System.EventHandler(chkINI_CheckedChanged);
                        ctrParentTo.Controls.Add(chkTo);
                        break;
                    case ("TextBox"):
                        TextBox txtTo = new TextBox();
                        txtTo.Location = ctrFrom.Location;
                        txtTo.Size = ctrFrom.Size;
                        txtTo.Multiline = ((TextBox)ctrFrom).Multiline;
                        txtTo.Name = ctrFrom.Name.Substring(0, 3) + ctrParentTo.Tag.ToString() + ctrFrom.Name.Substring(6);
                        txtTo.Tag = ctrFrom.Tag.ToString();
                        txtTo.TextAlign = ((TextBox)ctrFrom).TextAlign;
                        txtTo.Text = ctrFrom.Text;
                        txtTo.ReadOnly = ((TextBox)ctrFrom).ReadOnly;
                        txtTo.BackColor = ((TextBox)ctrFrom).BackColor;
                        txtTo.TabIndex = ctrFrom.TabIndex;
                        txtTo.TabStop = ctrFrom.TabStop;
                        ctrParentTo.Controls.Add(txtTo);
                        break;
                    case ("RadioButton"):
                        RadioButton btnTo = new RadioButton();
                        btnTo.Location = ctrFrom.Location;
                        btnTo.Size = ctrFrom.Size;
                        btnTo.Name = ctrFrom.Name.Substring(0, 3) + ctrParentTo.Tag.ToString() + ctrFrom.Name.Substring(6);
                        btnTo.Tag = ctrFrom.Tag.ToString();
                        btnTo.Text = ctrFrom.Text;
                        btnTo.TabIndex = ctrFrom.TabIndex;
                        ctrParentTo.Controls.Add(btnTo);
                        btnTo.CheckedChanged += new System.EventHandler(btnINI_CheckedChanged);
                        btnTo.Checked = ((RadioButton)ctrFrom).Checked;
                        break;
                    case ("GroupBox"):
                        GroupBox grpTo = new GroupBox();
                        grpTo.Location = ctrFrom.Location;
                        grpTo.Size = ctrFrom.Size;
                        grpTo.Name = ctrFrom.Name.Substring(0, 3) + ctrParentTo.Tag.ToString() + ctrFrom.Name.Substring(6);
                        grpTo.Tag = ctrParentTo.Tag.ToString();
                        grpTo.Text = ctrFrom.Text;
                        ctrParentTo.Controls.Add(grpTo);
                        udsCloneControls(ctrFrom, grpTo); //Kopieer alle controls uit deze group (recursief).
                        break;
                    case ("CheckedListBox"):
                        CheckedListBox chklstTo = new CheckedListBox();
                        chklstTo.Location = ctrFrom.Location;
                        chklstTo.Size = ctrFrom.Size;
                        chklstTo.Name = ctrFrom.Name.Substring(0, 6) + ctrParentTo.Tag.ToString() + ctrFrom.Name.Substring(9);
                        chklstTo.Tag = ctrFrom.Tag.ToString();
                        chklstTo.HorizontalScrollbar = ((CheckedListBox)ctrFrom).HorizontalScrollbar;
                        chklstTo.CheckOnClick = ((CheckedListBox)ctrFrom).CheckOnClick;
                        chklstTo.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(chklstINIGedragskenmerken_ItemCheck);
                        ctrParentTo.Controls.Add(chklstTo);

                        break;
                    default:
                        MessageBox.Show(ctrFrom.Name, "Control niet gekopiëerd!");
                        break;
                }
            }
        }

        private void udsStatischeVulling()
        {
            // Competentieomschrijving:
            using (SqlCommand cmdCompetentie = new SqlCommand("SELECT * FROM tblCompetentie ORDER BY pkId", cnnOnderwijs))
            {
                using (SqlDataReader rdrCompetentie = cmdCompetentie.ExecuteReader())
                {
                    while (rdrCompetentie.Read())
                    {
                        tabCompetenties.TabPages["tab" + rdrCompetentie["strAfkorting"].ToString()].Controls["grp" + rdrCompetentie["strAfkorting"].ToString() + "Beschrijving"].Controls[0].Text = rdrCompetentie["strBeschrijving"].ToString();
                    }
                }
            }

            // Gedragskenmerken:
            using (SqlCommand cmdKenmerken = new SqlCommand("SELECT * FROM qryGedragskenmerk ORDER BY Competentieafkorting, Gedragskenmerkindex", cnnOnderwijs))
            {
                using (SqlDataReader rdrKenmerken = cmdKenmerken.ExecuteReader())
                {
                    while (rdrKenmerken.Read())
                    {
                        CheckedListBox chklstInstance = tabCompetenties.TabPages["tab" + rdrKenmerken["Competentieafkorting"].ToString()].Controls["grp" + rdrKenmerken["Competentieafkorting"].ToString() + "Gedragskenmerken"].Controls[0] as CheckedListBox;
                        chklstInstance.Items.Add(rdrKenmerken["Gedragskenmerkindex"].ToString() + ": " + rdrKenmerken["Gedragskenmerkbeschrijving"].ToString());
                    }
                }
            }

        }

        private void udsNiveauOmschrijvingen()
        {
            // Toon omschrijving die hoort bij de niveau-aanduiding:
            using (SqlCommand cmdCompetentieniveau = new SqlCommand("SELECT Afkorting, Omschrijving FROM qryCompetentieNiveau ORDER BY Niveau", cnnOnderwijs))
            {
                using (SqlDataReader rdrCompetentieniveau = cmdCompetentieniveau.ExecuteReader())
                {
                    while (rdrCompetentieniveau.Read())
                    {
                        switch (rdrCompetentieniveau["Afkorting"].ToString())
                        {
                            case ("AvT"):
                                strNiveauAvT.Add(rdrCompetentieniveau["Omschrijving"].ToString());
                                break;
                            case ("AvC"):
                                strNiveauAvC.Add(rdrCompetentieniveau["Omschrijving"].ToString());
                                break;
                            case ("MvZ"):
                                strNiveauMvZ.Add(rdrCompetentieniveau["Omschrijving"].ToString());
                                break;
                            default:
                                MessageBox.Show("Afkorting " + rdrCompetentieniveau["Afkorting"].ToString() + " onverwacht!", "Onverwacht!");
                                break;
                        }
                    }
                    rdrCompetentieniveau.Close();
                }
            }
        }

        private void chkINI_CheckedChanged(object sender, EventArgs e)
        {
            // Eventmethode voor alle controls van het type CheckBox:
            CheckBox chkInstance = (CheckBox)sender;
            if (blnFormLoadReady)
            {
                blnErIsIetsGewijzigd = true;
            }

            switch (chkInstance.Tag.ToString())
            {
                case ("Gekoppeld"):

                    //Toon de Checked state op het tabje zelf:
                    TabPage tabParent = chkInstance.Parent as TabPage;
                    if (chkInstance.Checked)
                    {
                        tabParent.Text = tabParent.ToolTipText.ToUpper();
                    }
                    else
                    {
                        tabParent.Text = tabParent.ToolTipText;
                    }
                    break;
                case ("IetsAnders"):
                    MessageBox.Show("Iets anders...");
                    break;
                default:
                    break;
            }
        }

        private void btnINI_CheckedChanged(object sender, EventArgs e)
        {
            // Eventmethode voor alle controls van het type RadioButton:
            RadioButton btnInstance = (RadioButton)sender;
            TextBox txtOmschrijving = new TextBox();
            if (blnFormLoadReady)
            {
                blnErIsIetsGewijzigd = true;
            }

            // Zoek textbox om niveauomschrijving te plaatsen:
            foreach (Control ctrInstance in btnInstance.Parent.Controls)
            {
                if (ctrInstance.GetType().Name == "TextBox")
                {
                    txtOmschrijving = (TextBox)ctrInstance;
                }
            }

            // Plaats niveauomschrijving:
            switch (btnInstance.Tag.ToString())
            {
                case ("AvT"):
                    txtOmschrijving.Text = strNiveauAvT[Convert.ToInt32(btnInstance.Name.Substring(7, 1))];
                    break;
                case ("AvC"):
                    txtOmschrijving.Text = strNiveauAvC[Convert.ToInt32(btnInstance.Name.Substring(7, 1))];
                    break;
                case ("MvZ"):
                    txtOmschrijving.Text = strNiveauMvZ[Convert.ToInt32(btnInstance.Name.Substring(7, 1))];
                    break;
                default:
                    break;
            }

            // Toon het competentieniveau op basis van de drie subniveaus:
            if (blnControlsReady)
            {
                String strCompetentieAfkorting = btnInstance.Parent.Tag.ToString();
                int intAvT = intNiveau(strCompetentieAfkorting, "AvT");
                int intAvC = intNiveau(strCompetentieAfkorting, "AvC");
                int intMvZ = intNiveau(strCompetentieAfkorting, "MvZ");
                tabCompetenties.TabPages["tab" + strCompetentieAfkorting].Controls["txt" + strCompetentieAfkorting + "Niveau"].Text = Program.strNiveau(intAvT, intAvC, intMvZ);
            }
        }

        private int intNiveau(String strCompetentieafkorting, String strNiveauFactor)
        {
            // Bepaal competentieniveau op basis van geselecteerde radiobutton voor één niveaufactor van één competentie:
            int intReturn = 0;
            foreach (Control ctrInstance in tabCompetenties.TabPages["tab" + strCompetentieafkorting].Controls["grp" + strCompetentieafkorting + strNiveauFactor].Controls)
            {
                if (ctrInstance.GetType().Name == "RadioButton")
                {
                    if (((RadioButton)ctrInstance).Checked)
                    {
                        intReturn = Convert.ToInt32(ctrInstance.Name.Substring(7, 1));
                    }
                }
            }
            return intReturn;
        }

        private void cmdAnnuleer_Click(object sender, EventArgs e)
        {
            if (blnErIsIetsGewijzigd)
            {
                if (MessageBox.Show(this, "Alle eventuele aanpassingen in dit scherm gaan verloren. Weet je het zeker?", "Annuleren...", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            }
            // Sluit het scherm zonder wijzigingen over te nemen:
            Close();
        }

        private void cmdAccepteer_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Alle wijzigingen in dit scherm worden opgeslagen. Weet je het zeker?", "Accepteren...", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int intRecordsInserted = 0;
                int intRecordsDeleted = 0;
                String strQuery1 = "";
                String strQuery2 = "";
                String strQuery3 = "";

                // Stap 1: Start een mega database-transactie:
                SqlTransaction transOnderwijs = cnnOnderwijs.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    // Stap 2: Verwijder alle records uit tblDoelCompetentie met betrekking tot het leerdoel.
                    //         Vanwege Cascading Update worden alle gekoppelde gedragskenmerken automatisch verwijderd uit tblDoelCompetentieGedragskenmerken.
                    strQuery1 = "DELETE FROM tblDoelCompetentie WHERE fkDoel = " + intDoelID.ToString();
                    using (SqlCommand cmdDelete = new SqlCommand(strQuery1, cnnOnderwijs, transOnderwijs))
                    {
                        intRecordsDeleted = cmdDelete.ExecuteNonQuery();
                    }

                    // Stap 3: Lees de maximale pk-waarde uit tblDoelCompetentie:
                    int intDoelCompetentieId = 0;
                    using (SqlCommand cmdMaxId = new SqlCommand("SELECT MAX(pkId) AS maxId FROM tblDoelCompetentie", cnnOnderwijs, transOnderwijs))
                    {
                        using (SqlDataReader rdrMaxId = cmdMaxId.ExecuteReader())
                        {
                            rdrMaxId.Read();
                            if (!rdrMaxId.IsDBNull(0))
                            {
                                intDoelCompetentieId = (int)rdrMaxId["maxId"];
                            }
                            rdrMaxId.Close();
                        }
                    }

                    // Stap 4: Maak nieuwe records aan in tblDoelCompetentie voor de gekoppelde competenties.
                    //         Loop hiervoor door alle gekoppelde competenties:
                    foreach (TabPage tabInstance in tabCompetenties.TabPages)
                    {
                        String strCompetentieAfkorting = tabInstance.Tag.ToString();
                        if (((CheckBox)tabInstance.Controls["chk" + strCompetentieAfkorting + "Gekoppeld"]).Checked)
                        {
                            int intCompetentieId;
                            int intAvTNiveauId;
                            int intAvCNiveauId;
                            int intMvZNiveauId;

                            // Stap 4.1: Lees van de competentie de pk-waarde uit tblCompetentie:
                            using (SqlCommand cmdCompetentieId = new SqlCommand("SELECT pkId FROM tblCompetentie WHERE strAfkorting = '" + strCompetentieAfkorting + "'", cnnOnderwijs, transOnderwijs))
                            {
                                using (SqlDataReader rdrCompetentieId = cmdCompetentieId.ExecuteReader())
                                {
                                    rdrCompetentieId.Read();
                                    intCompetentieId = (int)rdrCompetentieId["pkId"];
                                    rdrCompetentieId.Close();
                                }
                            }

                            // Stap 4.2: Lees van de competentieniveaus (3x) de pk-waarde uit qryCompetentieNiveau:
                            int intAvT = intNiveau(strCompetentieAfkorting, "AvT");
                            using (SqlCommand cmdNiveauId = new SqlCommand("SELECT pkNiveau FROM qryCompetentieNiveau WHERE Afkorting = 'AvT' AND Niveau = " + intAvT, cnnOnderwijs, transOnderwijs))
                            {
                                using (SqlDataReader rdrNiveauId = cmdNiveauId.ExecuteReader())
                                {
                                    rdrNiveauId.Read();
                                    intAvTNiveauId = (int)rdrNiveauId["pkNiveau"];
                                    rdrNiveauId.Close();
                                }
                            }
                            int intAvC = intNiveau(strCompetentieAfkorting, "AvC");
                            using (SqlCommand cmdNiveauId = new SqlCommand("SELECT pkNiveau FROM qryCompetentieNiveau WHERE Afkorting = 'AvC' AND Niveau = " + intAvC, cnnOnderwijs, transOnderwijs))
                            {
                                using (SqlDataReader rdrNiveauId = cmdNiveauId.ExecuteReader())
                                {
                                    rdrNiveauId.Read();
                                    intAvCNiveauId = (int)rdrNiveauId["pkNiveau"];
                                    rdrNiveauId.Close();
                                }
                            }
                            int intMvZ = intNiveau(strCompetentieAfkorting, "MvZ");
                            using (SqlCommand cmdNiveauId = new SqlCommand("SELECT pkNiveau FROM qryCompetentieNiveau WHERE Afkorting = 'MvZ' AND Niveau = " + intMvZ, cnnOnderwijs, transOnderwijs))
                            {
                                using (SqlDataReader rdrNiveauId = cmdNiveauId.ExecuteReader())
                                {
                                    rdrNiveauId.Read();
                                    intMvZNiveauId = (int)rdrNiveauId["pkNiveau"];
                                    rdrNiveauId.Close();
                                }
                            }

                            // Stap 4.3: Maak record aan voor deze ene competentiekoppeling:
                            intDoelCompetentieId++;
                            strQuery2 = "INSERT INTO tblDoelCompetentie (pkId, fkCompetentie, fkDoel, fkNiveauT, fkNiveauC, fkNiveauZ) " +
                                "VALUES (" + intDoelCompetentieId.ToString() + ", " + intCompetentieId.ToString() + ", " + intDoelID.ToString() + ", " +
                                intAvTNiveauId.ToString() + ", " + intAvCNiveauId.ToString() + ", " + intMvZNiveauId.ToString() + ")";
                            using (SqlCommand cmdInsert = new SqlCommand(strQuery2, cnnOnderwijs, transOnderwijs))
                            {
                                intRecordsInserted += cmdInsert.ExecuteNonQuery();
                            }

                            // Stap 4.4: Lees de maximale pk-waarde uit tblDoelCompetentieGedragskenmerk:
                            int intDoelCompetentieGedragskenmerkId = 0;
                            using (SqlCommand cmdMaxId = new SqlCommand("SELECT MAX(pkId) AS maxId FROM tblDoelCompetentieGedragskenmerk", cnnOnderwijs, transOnderwijs))
                            {
                                using (SqlDataReader rdrMaxId = cmdMaxId.ExecuteReader())
                                {
                                    rdrMaxId.Read();
                                    if (!rdrMaxId.IsDBNull(0))
                                    {
                                        intDoelCompetentieGedragskenmerkId = (int)rdrMaxId["maxId"];
                                    }
                                    rdrMaxId.Close();
                                }
                            }

                            // Stap 4.5: Maak nieuwe records aan in tblDoelCompetentieGedragskenmerk voor aan de gekoppelde competentie gekoppelde gedragskenmerken:
                            //           Loop hiervoor door de items in de CheckedListBox:
                            CheckedListBox chklstInstance = (CheckedListBox)tabInstance.Controls["grp" + strCompetentieAfkorting + "Gedragskenmerken"].Controls["chklst" + strCompetentieAfkorting + "Gedragskenmerken"];
                            for (int intIndex = 0; intIndex < chklstInstance.Items.Count; intIndex++)
                            {
                                if (chklstInstance.GetItemCheckState(intIndex) == CheckState.Checked)
                                {
                                    int intGedragskenmerkId;

                                    // Stap 4.5.1: Haal indexletter (a, b, ...) van gedragskenmerk op uit CheckedListBox:
                                    String strGedragskenmerkIndex = chklstInstance.Items[intIndex].ToString().Substring(0, 1);

                                    // Stap 4.5.2: Lees van het gedragskenmerk de pk-waarde uit tblGedragskenmerk:
                                    using (SqlCommand cmdGedragskenmerkId = new SqlCommand("SELECT pkId FROM tblGedragskenmerk WHERE fkCompetentie = " + intCompetentieId.ToString() + " AND strIndex = '" + strGedragskenmerkIndex + "'", cnnOnderwijs, transOnderwijs))
                                    {
                                        using (SqlDataReader rdrGedragskenmerkId = cmdGedragskenmerkId.ExecuteReader())
                                        {
                                            rdrGedragskenmerkId.Read();
                                            intGedragskenmerkId = (int)rdrGedragskenmerkId["pkId"];
                                            rdrGedragskenmerkId.Close();
                                        }
                                    }

                                    // Stap 4.5.3: Maak record aan voor deze ene gedragskenmerkkoppeling:
                                    intDoelCompetentieGedragskenmerkId++;
                                    strQuery3 = "INSERT INTO tblDoelCompetentieGedragskenmerk (pkId, fkGedragskenmerk, fkDoelCompetentie) " +
                                        "VALUES (" + intDoelCompetentieGedragskenmerkId.ToString() + ", " + intGedragskenmerkId.ToString() + ", " + intDoelCompetentieId.ToString() + ")";
                                    using (SqlCommand cmdInsert = new SqlCommand(strQuery3, cnnOnderwijs, transOnderwijs))
                                    {
                                        // Ter info: dit aantal wordt niet opgeteld bij het totaal omdat bij het deleten ze ook niet meegenomen worden (omdat ze door de DB zelf deleted worden middels cascading delete).
                                        int intAantalRecords = cmdInsert.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    }

                    // Stap 5: Commit database-transactie:
                    transOnderwijs.Commit();
                    Program.logMessage("Commit 1/3: " + Program.removeSpecialChars(strQuery1), cnnOnderwijs);
                    Program.logMessage("Commit 2/3: " + Program.removeSpecialChars(strQuery2), cnnOnderwijs);
                    Program.logMessage("Commit 3/3: " + Program.removeSpecialChars(strQuery3), cnnOnderwijs);
                    if (Globals.DEBUG)
                    {
                        MessageBox.Show("Commit:\nAantal records deleted: " + intRecordsDeleted.ToString() + "\nAantal records inserted: " + intRecordsInserted.ToString(), "Competentiekoppelingen opslaan...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch
                {
                    transOnderwijs.Rollback();
                    Program.logMessage("Rollback 1/3: " + Program.removeSpecialChars(strQuery1), cnnOnderwijs);
                    Program.logMessage("Rollback 2/3: " + Program.removeSpecialChars(strQuery2), cnnOnderwijs);
                    Program.logMessage("Rollback 3/3: " + Program.removeSpecialChars(strQuery3), cnnOnderwijs);
                    MessageBox.Show("Rollback: Iets gaat hier niet chocotof!", "Whoopsy Daisy...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Stap 6: Sluit het scherm:
                Close();
            }
        }

        private void chklstINIGedragskenmerken_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (blnFormLoadReady)
            {
                blnErIsIetsGewijzigd = true;
            }
        }
    }
}
