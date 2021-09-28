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
using System.Text.RegularExpressions;

namespace Onderwijs
{
    public partial class frmBoKSkoppeling : Form
    {
        private SqlConnection cnnOnderwijs = new SqlConnection("Data Source=" + Globals.DB_SERVER + ";Initial Catalog=" + Globals.DB_NAME + ";User ID=" + Globals.DB_USER + ";Password=" + Globals.DB_PASSWORD + ";MultipleActiveResultSets=true;");
        private int intDoelID;
        private ListViewColumnSorter lvwETColumnSorter;
        private ListViewColumnSorter lvwTIColumnSorter;
        private bool blnErIsIetsGewijzigd = false;

        public frmBoKSkoppeling(int intDoelID_)
        {
            InitializeComponent();
            intDoelID = intDoelID_;

            lvwETColumnSorter = new ListViewColumnSorter();
            lvwTIColumnSorter = new ListViewColumnSorter();
            lvwETBoKSitem.ListViewItemSorter = lvwETColumnSorter;
            lvwTIBoKSitem.ListViewItemSorter = lvwTIColumnSorter;
        }

        private void frmBoKSkoppeling_Load(object sender, EventArgs e)
        {
            // Initialiseer het scherm:
            cnnOnderwijs.Open();
            udsBoKSVulling("ET", lvwETBoKSitem);
            udsBoKSVulling("TI", lvwTIBoKSitem);
            if (intDoelID > 0)
            {
                udsDynamischeVulling(intDoelID);
            }
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

            // Gekoppelde items BoKS ET:
            using (SqlCommand cmdItems = new SqlCommand("SELECT BoKSItemId FROM qryDoelBoKSItem WHERE OnderwijsdoelID = " + intDoelID.ToString() + " AND Opleidingsafkorting = 'ET'", cnnOnderwijs))
            {
                using (SqlDataReader rdrItems = cmdItems.ExecuteReader())
                {
                    if (rdrItems.HasRows)
                    {
                        while (rdrItems.Read())
                        {
                            lvwETBoKSitem.Items[((int)rdrItems["BoKSItemId"]).ToString()].Checked = true;
                        }
                    }
                    rdrItems.Close();
                }
            }

            // Gekoppelde items BoKS TI:
            using (SqlCommand cmdItems = new SqlCommand("SELECT BoKSItemId FROM qryDoelBoKSItem WHERE OnderwijsdoelID = " + intDoelID.ToString() + " AND Opleidingsafkorting = 'TI'", cnnOnderwijs))
            {
                using (SqlDataReader rdrItems = cmdItems.ExecuteReader())
                {
                    if (rdrItems.HasRows)
                    {
                        while (rdrItems.Read())
                        {
                            lvwTIBoKSitem.Items[((int)rdrItems["BoKSItemId"]).ToString()].Checked = true;
                        }
                    }
                    rdrItems.Close();
                }
            }
        }

        private void udsBoKSVulling(String strOpleidingsafkorting, ListView lvwBoKSList)
        {
            // BoKS-lijst vullen:
            using (SqlCommand cmdBoKSItem = new SqlCommand("SELECT * FROM qryBoKSItem WHERE Opleidingsafkorting = '" + strOpleidingsafkorting + "'", cnnOnderwijs))
            {
                using (SqlDataReader rdrBoKSItem = cmdBoKSItem.ExecuteReader())
                {
                    if (rdrBoKSItem.HasRows)
                    {
                        int intVolgnummer = 0;
                        while (rdrBoKSItem.Read())
                        {
                            // Kolom 1, Checkbox en Volgnummer:
                            intVolgnummer++;
                            String strVolgnummer = "00" + intVolgnummer.ToString();
                            strVolgnummer = strVolgnummer.Substring(strVolgnummer.Length - 3);
                            ListViewItem itmInstance = lvwBoKSList.Items.Add(strVolgnummer);
                            itmInstance.Name = rdrBoKSItem["pkItem"].ToString();

                            // Kolom 2 en 3, BoKS-naam en -categorie:
                            if (!(bool)rdrBoKSItem["Specialisatie"])
                            {
                                itmInstance.SubItems.Add(rdrBoKSItem["BoKSnaam"].ToString());
                                itmInstance.SubItems.Add(rdrBoKSItem["Categorie"].ToString());
                            }
                            else
                            {
                                itmInstance.SubItems.Add(rdrBoKSItem["BoKSnaam"].ToString());
                                itmInstance.SubItems.Add("-");
                            }

                            // Kolom 4, BoKS-itemnummer (aangevuld met voorloopnullen, zodat er goed op gesorteerd kan worden):
                            String strNummer = "";
                            if (rdrBoKSItem["Categorienummer"].ToString().Length == 1)
                            {
                                strNummer += ("0" + rdrBoKSItem["Categorienummer"].ToString());
                            }
                            else
                            {
                                strNummer += rdrBoKSItem["Categorienummer"].ToString();
                            }
                            strNummer += ".";
                            if (rdrBoKSItem["Itemnummer"].ToString().Length == 1)
                            {
                                strNummer += ("0" + rdrBoKSItem["Itemnummer"].ToString());
                            }
                            else
                            {
                                strNummer += rdrBoKSItem["Itemnummer"].ToString();
                            }
                            itmInstance.SubItems.Add(strNummer);

                            // Kolom 5, omschrijving BoKS-item:
                            itmInstance.SubItems.Add(rdrBoKSItem["Item"].ToString());
                        }
                    }
                    rdrBoKSItem.Close();
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

        private void cmdAccepteer_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Alle eventuele aanpassingen in dit scherm worden opgeslagen. Weet je het zeker?", "Accepteren...", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int intRecordsInserted = 0;
                int intRecordsDeleted = 0;
                String strQuery1 = "";
                String strQuery2 = "";
                String strQuery3 = "";

                // Stap 1: Start een database-transactie:
                SqlTransaction transOnderwijs = cnnOnderwijs.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    // Stap 2: Verwijder alle records uit tblDoelBoKSItem met betrekking tot het leerdoel.
                    strQuery1 = "DELETE FROM tblDoelBoKSItem WHERE fkDoel = " + intDoelID.ToString();
                    using (SqlCommand cmdDelete = new SqlCommand(strQuery1, cnnOnderwijs, transOnderwijs))
                    {
                        intRecordsDeleted = cmdDelete.ExecuteNonQuery();
                    }

                    // Stap 3: Lees de maximale pk-waarde uit tblDoelBoKSItem:
                    int intDoelBoKSItemId = 0;
                    using (SqlCommand cmdMaxId = new SqlCommand("SELECT MAX(pkId) AS maxId FROM tblDoelBoKSItem", cnnOnderwijs, transOnderwijs))
                    {
                        using (SqlDataReader rdrMaxId = cmdMaxId.ExecuteReader())
                        {
                            rdrMaxId.Read();
                            if (!rdrMaxId.IsDBNull(0))
                            {
                                intDoelBoKSItemId = (int)rdrMaxId["maxId"];
                            }
                            rdrMaxId.Close();
                        }
                    }

                    // Stap 4: Maak nieuwe records aan in tblDoelBoKSItem voor de gekoppelde BoKS-items.
                    //         Loop hiervoor door alle gecheckte BoKS-items:
                    foreach (ListViewItem itmInstance in lvwETBoKSitem.CheckedItems)
                    {
                        // Stap 4.1: Maak record aan voor deze ene BoKS-itemkoppeling (opleiding ET):
                        intDoelBoKSItemId++;
                        strQuery2 = "INSERT INTO tblDoelBoKSItem (pkId, fkDoel, fkBoKSItem) " +
                            "VALUES (" + intDoelBoKSItemId.ToString() + ", " + intDoelID.ToString() + ", " + itmInstance.Name.ToString() + ")";
                        using (SqlCommand cmdInsert = new SqlCommand(strQuery2, cnnOnderwijs, transOnderwijs))
                        {
                            intRecordsInserted += cmdInsert.ExecuteNonQuery();
                        }
                    }
                    foreach (ListViewItem itmInstance in lvwTIBoKSitem.CheckedItems)
                    {
                        // Stap 4.2: Maak record aan voor deze ene BoKS-itemkoppeling (opleiding TI):
                        intDoelBoKSItemId++;
                        strQuery3 = "INSERT INTO tblDoelBoKSItem (pkId, fkDoel, fkBoKSItem) " +
                            "VALUES (" + intDoelBoKSItemId.ToString() + ", " + intDoelID.ToString() + ", " + itmInstance.Name.ToString() + ")";
                        using (SqlCommand cmdInsert = new SqlCommand(strQuery3, cnnOnderwijs, transOnderwijs))
                        {
                            intRecordsInserted += cmdInsert.ExecuteNonQuery();
                        }
                    }

                    // Stap 5: Commit database-transactie:
                    transOnderwijs.Commit();
                    Program.logMessage("Commit 1/3: " + Program.removeSpecialChars(strQuery1), cnnOnderwijs);
                    Program.logMessage("Commit 2/3: " + Program.removeSpecialChars(strQuery2), cnnOnderwijs);
                    Program.logMessage("Commit 3/3: " + Program.removeSpecialChars(strQuery3), cnnOnderwijs);
                    MessageBox.Show("Commit:\nAantal records deleted: " + intRecordsDeleted.ToString() + "\nAantal records inserted: " + intRecordsInserted.ToString(), "BoKS-items opslaan...", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void lvwETBoKSitem_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Order items in de lijst op basis van de geklikte kolom.

            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwETColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwETColumnSorter.Order == System.Windows.Forms.SortOrder.Ascending)
                {
                    lvwETColumnSorter.Order = System.Windows.Forms.SortOrder.Descending;
                }
                else
                {
                    lvwETColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwETColumnSorter.SortColumn = e.Column;
                lvwETColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            lvwETBoKSitem.Sort();
        }

        private void lvwTIBoKSitem_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Order items in de lijst op basis van de geklikte kolom.

            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwTIColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwTIColumnSorter.Order == System.Windows.Forms.SortOrder.Ascending)
                {
                    lvwTIColumnSorter.Order = System.Windows.Forms.SortOrder.Descending;
                }
                else
                {
                    lvwTIColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwTIColumnSorter.SortColumn = e.Column;
                lvwTIColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            lvwTIBoKSitem.Sort();
        }

        private void lvwETBoKSitem_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            blnErIsIetsGewijzigd = true;
        }

        private void lvwTIBoKSitem_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            blnErIsIetsGewijzigd = true;
        }
    }
}
