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
    public partial class frmBeroepsproductkoppeling : Form
    {
        private SqlConnection cnnOnderwijs = new SqlConnection("Data Source=" + Globals.DB_SERVER + ";Initial Catalog=" + Globals.DB_NAME + ";User ID=" + Globals.DB_USER + ";Password=" + Globals.DB_PASSWORD + ";MultipleActiveResultSets=true;");
        //private bool blnControlsReady = false;
        //private bool blnFormLoadReady = false;
        private int intDoelID;
        private bool blnErIsIetsGewijzigd = false;

        public frmBeroepsproductkoppeling(int intDoelID_)
        {
            InitializeComponent();
            intDoelID = intDoelID_;
        }

        private void frmBeroepsproductkoppeling_Load(object sender, EventArgs e)
        {
            cnnOnderwijs.Open();
            udsStatischeVulling();

            if (intDoelID > 0)
            {
                udsDynamischeVulling(intDoelID);
            }
            //blnFormLoadReady = true;
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

            // Gekoppelde beroepsproducten:
            using (SqlCommand cmdBeroepsproducten = new SqlCommand("SELECT BeroepsproductId FROM qryDoelBeroepsproduct WHERE OnderwijsdoelID = " + intDoelID.ToString(), cnnOnderwijs))
            {
                using (SqlDataReader rdrBeroepsproducten = cmdBeroepsproducten.ExecuteReader())
                {
                    if (rdrBeroepsproducten.HasRows)
                    {
                        while (rdrBeroepsproducten.Read())
                        {
                            lvwBeroepsproducten.Items[((int)rdrBeroepsproducten["BeroepsproductId"]).ToString()].Checked = true;
                        }
                    }
                    rdrBeroepsproducten.Close();
                }
            }




        }

        private void udsStatischeVulling()
        {
            // BoKS-lijst vullen:
            using (SqlCommand cmdBeroepsproduct = new SqlCommand("SELECT * FROM tblBeroepsproduct", cnnOnderwijs))
            {
                using (SqlDataReader rdrBeroepsproduct = cmdBeroepsproduct.ExecuteReader())
                {
                    if (rdrBeroepsproduct.HasRows)
                    {
                        int intVolgnummer = 0;
                        while (rdrBeroepsproduct.Read())
                        {
                            // Kolom 1, Checkbox en Volgnummer ("Selectie"):
                            intVolgnummer++;
                            String strVolgnummer = "0" + intVolgnummer.ToString();
                            strVolgnummer = strVolgnummer.Substring(strVolgnummer.Length - 2);
                            ListViewItem itmInstance = lvwBeroepsproducten.Items.Add(strVolgnummer);
                            itmInstance.Name = rdrBeroepsproduct["pkId"].ToString();

                            // Kolom 2, 3 en 4, afkorting, naam en omschrijving:
                            itmInstance.SubItems.Add(rdrBeroepsproduct["strAfkorting"].ToString());
                            itmInstance.SubItems.Add(rdrBeroepsproduct["strNaam"].ToString());
                            itmInstance.SubItems.Add(rdrBeroepsproduct["strBeschrijving"].ToString());
                        }
                    }
                    rdrBeroepsproduct.Close();
                }
            }
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

        private void lvwBeroepsproducten_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            blnErIsIetsGewijzigd = true;
        }

        private void cmdAccepteer_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Alle eventuele aanpassingen in dit scherm worden opgeslagen. Weet je het zeker?", "Accepteren...", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int intRecordsInserted = 0;
                int intRecordsDeleted = 0;
                String strQueryDel = "";
                String strQueryIns = "";

                // Stap 1: Start een database-transactie:
                SqlTransaction transOnderwijs = cnnOnderwijs.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    // Stap 2: Verwijder alle records uit tblDoelBeroepsproduct met betrekking tot het leerdoel.
                    strQueryDel = "DELETE FROM tblDoelBeroepsproduct WHERE fkDoel = " + intDoelID.ToString();
                    using (SqlCommand cmdDelete = new SqlCommand(strQueryDel, cnnOnderwijs, transOnderwijs))
                    {
                        intRecordsDeleted = cmdDelete.ExecuteNonQuery();
                    }

                    // Stap 3: Lees de maximale pk-waarde uit tblDoelBeroepsproduct:
                    int intDoelBeroepsproductId = 0;
                    using (SqlCommand cmdMaxId = new SqlCommand("SELECT MAX(pkId) AS maxId FROM tblDoelBeroepsproduct", cnnOnderwijs, transOnderwijs))
                    {
                        using (SqlDataReader rdrMaxId = cmdMaxId.ExecuteReader())
                        {
                            rdrMaxId.Read();
                            if (!rdrMaxId.IsDBNull(0))
                            {
                                intDoelBeroepsproductId = (int)rdrMaxId["maxId"];
                            }
                            rdrMaxId.Close();
                        }
                    }

                    // Stap 4: Maak nieuwe records aan in tblDoelBeroepsproducten voor de gekoppelde beroepsproducten.
                    //         Loop hiervoor door alle gecheckte beroepsproducten:
                    foreach (ListViewItem itmInstance in lvwBeroepsproducten.CheckedItems)
                    {
                        // Stap 4.1: Maak record aan voor deze ene beroepsproductkoppeling:
                        intDoelBeroepsproductId++;
                        strQueryIns = "INSERT INTO tblDoelBeroepsproduct (pkId, fkDoel, fkBeroepsproduct) " +
                            "VALUES (" + intDoelBeroepsproductId.ToString() + ", " + intDoelID.ToString() + ", " + itmInstance.Name.ToString() + ")";
                        using (SqlCommand cmdInsert = new SqlCommand(strQueryIns, cnnOnderwijs, transOnderwijs))
                        {
                            intRecordsInserted += cmdInsert.ExecuteNonQuery();
                        }
                    }

                    // Stap 5: Commit database-transactie:
                    transOnderwijs.Commit();
                    Program.logMessage("Commit 1/2: " + Program.removeSpecialChars(strQueryDel), cnnOnderwijs);
                    Program.logMessage("Commit 2/2: " + Program.removeSpecialChars(strQueryIns), cnnOnderwijs);
                    if (Globals.DEBUG)
                    {
                        MessageBox.Show("Commit:\nAantal records deleted: " + intRecordsDeleted.ToString() + "\nAantal records inserted: " + intRecordsInserted.ToString(), "Beroepsproducten opslaan...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch
                {
                    transOnderwijs.Rollback();
                    Program.logMessage("Rollback 1/2: " + Program.removeSpecialChars(strQueryDel), cnnOnderwijs);
                    Program.logMessage("Rollback 2/2: " + Program.removeSpecialChars(strQueryIns), cnnOnderwijs);
                    MessageBox.Show("Rollback: Iets gaat hier niet chocotof!", "Whoopsy Daisy...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Stap 6: Sluit het scherm:
                Close();
            }
        }
    }
}
