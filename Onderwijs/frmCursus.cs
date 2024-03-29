﻿using System;
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
    public partial class frmCursus : Form
    {
        private SqlConnection cnnOnderwijs = new SqlConnection("Data Source=" + Globals.DB_SERVER + ";Initial Catalog=" + Globals.DB_NAME + ";User ID=" + Globals.DB_USER + ";Password=" + Globals.DB_PASSWORD + ";MultipleActiveResultSets=true;");
        private String strToetscode = "<onbekend>";
        private String strCursuscode = "<onbekend>";

        public frmCursus(String strToetscode_)
        {
            InitializeComponent();
            strToetscode = strToetscode_;
        }

        private void frmCursus_Load(object sender, EventArgs e)
        {

            cnnOnderwijs.Open();

            // Selecteer de cursus die hoort bij de toetscode:
            using (SqlCommand cmdCursus = new SqlCommand("SELECT TOP 1 * FROM qryToets WHERE Toetscode = '" + strToetscode + "'", cnnOnderwijs))
            {
                using (SqlDataReader rdrCursus = cmdCursus.ExecuteReader())
                {
                    rdrCursus.Read();
                    strCursuscode = rdrCursus["Cursuscode"].ToString();
                    txtCursuscode.Text = strCursuscode;
                    txtCursusnaam.Text = rdrCursus["Cursusnaam"].ToString();
                    btnProject.Checked = rdrCursus["Cursustype"].ToString().Equals("Project");
                }
            }
        }
    }
}
