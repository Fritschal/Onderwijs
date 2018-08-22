using System;
using System.Windows.Forms;

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
    }
}
