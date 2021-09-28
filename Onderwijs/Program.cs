using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;

namespace Onderwijs
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }

        public static String strNiveau(int intAvT, int intAvC, int intMvZ)
        {
            String strReturn;
            switch (intNiveau(intAvT, intAvC, intMvZ))
            {
                case (1):
                    strReturn = "I";
                    break;
                case (2):
                    strReturn = "II";
                    break;
                case (3):
                    strReturn = "III";
                    break;
                default:
                    strReturn = "0";
                    break;
            }
            return strReturn;
        }

        public static int intNiveau(int intAvT, int intAvC, int intMvZ)
        {
            if (
                (intAvT == 0 && intAvC == 0) ||
                (intAvT == 0 && intMvZ == 0) ||
                (intAvC == 0 && intMvZ == 0))
            {
                return 0;
            }
            else if (
                (intAvT == 3 && intAvC == 3) ||
                (intAvT == 3 && intMvZ == 3) ||
                (intAvC == 3 && intMvZ == 3))
            {
                return 3;
            }
            else if (
                (intAvT == 2 && intAvC >= 2 && intMvZ <= 2) || (intAvT == 2 && intAvC <= 2 && intMvZ >= 2) ||
                (intAvC == 2 && intMvZ >= 2 && intAvT <= 2) || (intAvC == 2 && intMvZ <= 2 && intAvT >= 2) ||
                (intMvZ == 2 && intAvT >= 2 && intAvC <= 2) || (intMvZ == 2 && intAvT <= 2 && intAvC >= 2))
            {
                return 2;
            }
            else
            {
                return 1;
            }
        }

        public static String removeSpecialChars(String input)
        {
            // Speciale karaters verwijderen t.b.v. SQL string.
            String output = input;
            output = Regex.Replace(output, @"[']", "''");
            output = Regex.Replace(output, @"[\""]", "\"");
            output = Regex.Replace(output, @"[“”]", "\"");
            output = Regex.Replace(output, @"[‘’]", "''");
            output = Regex.Replace(output, @"[^0-9a-zA-Z,\.\(\) /><`ëéèôïüä/+?:\x0A\x0D\-–;\*\='\""]", "_");
            return output;
        }

        public static void logMessage(String strMessage, SqlConnection cnnOnderwijs)
        {
            // Log message in database tblLog.
            String strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
            String strUserName = Environment.UserName;
            int intMaxId = -1;

            SqlTransaction transOnderwijs = cnnOnderwijs.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                using (SqlCommand cmdMaxId = new SqlCommand("SELECT MAX(pkId) AS maxId FROM tblLog", cnnOnderwijs, transOnderwijs))
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

                using (SqlCommand cmdInsert = new SqlCommand("INSERT INTO tblLog (pkId, strDateTime, strUserName, strMessage) " +
                    "VALUES (" +
                    intMaxId.ToString() + ", '" +
                    strDateTime + "', '" +
                    strUserName + "', '" +
                    strMessage + "')", cnnOnderwijs, transOnderwijs))
                {
                    int intAantalRecords = cmdInsert.ExecuteNonQuery();
                }
                transOnderwijs.Commit();
            }
            catch
            {
                transOnderwijs.Rollback();
                MessageBox.Show("Iets gaat hier niet chocotof!", "Whoopsy Daisy...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
