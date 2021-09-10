namespace Onderwijs
{
    partial class frmCursus
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lstToetscodes = new System.Windows.Forms.ListBox();
            this.chkHW = new System.Windows.Forms.CheckBox();
            this.chkAU = new System.Windows.Forms.CheckBox();
            this.chkSW = new System.Windows.Forms.CheckBox();
            this.grpCursuscode = new System.Windows.Forms.GroupBox();
            this.txtCursuscode = new System.Windows.Forms.TextBox();
            this.grpTraject = new System.Windows.Forms.GroupBox();
            this.grpToetcodes = new System.Windows.Forms.GroupBox();
            this.grpBlok = new System.Windows.Forms.GroupBox();
            this.txtBlok = new System.Windows.Forms.TextBox();
            this.txtPeriode = new System.Windows.Forms.TextBox();
            this.grpCursustype = new System.Windows.Forms.GroupBox();
            this.btnProject = new System.Windows.Forms.RadioButton();
            this.btnVak = new System.Windows.Forms.RadioButton();
            this.grpCursusnaam = new System.Windows.Forms.GroupBox();
            this.txtCursusnaam = new System.Windows.Forms.TextBox();
            this.grpCursus = new System.Windows.Forms.GroupBox();
            this.grpToets = new System.Windows.Forms.GroupBox();
            this.grpResultaatschaal = new System.Windows.Forms.GroupBox();
            this.grpToetsECs = new System.Windows.Forms.GroupBox();
            this.grpToetsvorm = new System.Windows.Forms.GroupBox();
            this.grpCursuscode.SuspendLayout();
            this.grpTraject.SuspendLayout();
            this.grpToetcodes.SuspendLayout();
            this.grpBlok.SuspendLayout();
            this.grpCursustype.SuspendLayout();
            this.grpCursusnaam.SuspendLayout();
            this.grpCursus.SuspendLayout();
            this.grpToets.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstToetscodes
            // 
            this.lstToetscodes.FormattingEnabled = true;
            this.lstToetscodes.Location = new System.Drawing.Point(6, 19);
            this.lstToetscodes.Name = "lstToetscodes";
            this.lstToetscodes.Size = new System.Drawing.Size(180, 82);
            this.lstToetscodes.TabIndex = 2;
            // 
            // chkHW
            // 
            this.chkHW.AutoSize = true;
            this.chkHW.Location = new System.Drawing.Point(6, 19);
            this.chkHW.Name = "chkHW";
            this.chkHW.Size = new System.Drawing.Size(125, 17);
            this.chkHW.TabIndex = 3;
            this.chkHW.Text = "Smart Hardware (ET)";
            this.chkHW.UseVisualStyleBackColor = true;
            // 
            // chkAU
            // 
            this.chkAU.AutoSize = true;
            this.chkAU.Location = new System.Drawing.Point(6, 42);
            this.chkAU.Name = "chkAU";
            this.chkAU.Size = new System.Drawing.Size(132, 17);
            this.chkAU.TabIndex = 4;
            this.chkAU.Text = "Smart Automation (ET)";
            this.chkAU.UseVisualStyleBackColor = true;
            // 
            // chkSW
            // 
            this.chkSW.AutoSize = true;
            this.chkSW.Location = new System.Drawing.Point(6, 65);
            this.chkSW.Name = "chkSW";
            this.chkSW.Size = new System.Drawing.Size(117, 17);
            this.chkSW.TabIndex = 5;
            this.chkSW.Text = "Smart Software (TI)";
            this.chkSW.UseVisualStyleBackColor = true;
            // 
            // grpCursuscode
            // 
            this.grpCursuscode.Controls.Add(this.txtCursuscode);
            this.grpCursuscode.Location = new System.Drawing.Point(6, 19);
            this.grpCursuscode.Name = "grpCursuscode";
            this.grpCursuscode.Size = new System.Drawing.Size(192, 47);
            this.grpCursuscode.TabIndex = 6;
            this.grpCursuscode.TabStop = false;
            this.grpCursuscode.Text = "Cursuscode:";
            // 
            // txtCursuscode
            // 
            this.txtCursuscode.Location = new System.Drawing.Point(6, 19);
            this.txtCursuscode.Name = "txtCursuscode";
            this.txtCursuscode.Size = new System.Drawing.Size(180, 20);
            this.txtCursuscode.TabIndex = 2;
            // 
            // grpTraject
            // 
            this.grpTraject.Controls.Add(this.chkSW);
            this.grpTraject.Controls.Add(this.chkHW);
            this.grpTraject.Controls.Add(this.chkAU);
            this.grpTraject.Location = new System.Drawing.Point(6, 72);
            this.grpTraject.Name = "grpTraject";
            this.grpTraject.Size = new System.Drawing.Size(192, 89);
            this.grpTraject.TabIndex = 7;
            this.grpTraject.TabStop = false;
            this.grpTraject.Text = "Trajecten:";
            // 
            // grpToetcodes
            // 
            this.grpToetcodes.Controls.Add(this.lstToetscodes);
            this.grpToetcodes.Location = new System.Drawing.Point(204, 72);
            this.grpToetcodes.Name = "grpToetcodes";
            this.grpToetcodes.Size = new System.Drawing.Size(191, 111);
            this.grpToetcodes.TabIndex = 8;
            this.grpToetcodes.TabStop = false;
            this.grpToetcodes.Text = "Toestcodes:";
            // 
            // grpBlok
            // 
            this.grpBlok.Controls.Add(this.txtBlok);
            this.grpBlok.Controls.Add(this.txtPeriode);
            this.grpBlok.Location = new System.Drawing.Point(105, 167);
            this.grpBlok.Name = "grpBlok";
            this.grpBlok.Size = new System.Drawing.Size(93, 73);
            this.grpBlok.TabIndex = 10;
            this.grpBlok.TabStop = false;
            this.grpBlok.Text = "Periode/Blok:";
            // 
            // txtBlok
            // 
            this.txtBlok.Location = new System.Drawing.Point(6, 45);
            this.txtBlok.Name = "txtBlok";
            this.txtBlok.Size = new System.Drawing.Size(81, 20);
            this.txtBlok.TabIndex = 3;
            // 
            // txtPeriode
            // 
            this.txtPeriode.Location = new System.Drawing.Point(6, 19);
            this.txtPeriode.Name = "txtPeriode";
            this.txtPeriode.Size = new System.Drawing.Size(81, 20);
            this.txtPeriode.TabIndex = 2;
            // 
            // grpCursustype
            // 
            this.grpCursustype.Controls.Add(this.btnProject);
            this.grpCursustype.Controls.Add(this.btnVak);
            this.grpCursustype.Location = new System.Drawing.Point(6, 167);
            this.grpCursustype.Name = "grpCursustype";
            this.grpCursustype.Size = new System.Drawing.Size(93, 73);
            this.grpCursustype.TabIndex = 11;
            this.grpCursustype.TabStop = false;
            this.grpCursustype.Text = "Cursustype:";
            // 
            // btnProject
            // 
            this.btnProject.AutoSize = true;
            this.btnProject.Location = new System.Drawing.Point(6, 42);
            this.btnProject.Name = "btnProject";
            this.btnProject.Size = new System.Drawing.Size(58, 17);
            this.btnProject.TabIndex = 1;
            this.btnProject.Text = "Project";
            this.btnProject.UseVisualStyleBackColor = true;
            // 
            // btnVak
            // 
            this.btnVak.AutoSize = true;
            this.btnVak.Checked = true;
            this.btnVak.Location = new System.Drawing.Point(6, 19);
            this.btnVak.Name = "btnVak";
            this.btnVak.Size = new System.Drawing.Size(44, 17);
            this.btnVak.TabIndex = 0;
            this.btnVak.TabStop = true;
            this.btnVak.Text = "Vak";
            this.btnVak.UseVisualStyleBackColor = true;
            // 
            // grpCursusnaam
            // 
            this.grpCursusnaam.Controls.Add(this.txtCursusnaam);
            this.grpCursusnaam.Location = new System.Drawing.Point(204, 19);
            this.grpCursusnaam.Name = "grpCursusnaam";
            this.grpCursusnaam.Size = new System.Drawing.Size(191, 47);
            this.grpCursusnaam.TabIndex = 12;
            this.grpCursusnaam.TabStop = false;
            this.grpCursusnaam.Text = "Cursusnaam:";
            // 
            // txtCursusnaam
            // 
            this.txtCursusnaam.Location = new System.Drawing.Point(6, 19);
            this.txtCursusnaam.Multiline = true;
            this.txtCursusnaam.Name = "txtCursusnaam";
            this.txtCursusnaam.Size = new System.Drawing.Size(180, 20);
            this.txtCursusnaam.TabIndex = 13;
            // 
            // grpCursus
            // 
            this.grpCursus.Controls.Add(this.grpCursuscode);
            this.grpCursus.Controls.Add(this.grpCursusnaam);
            this.grpCursus.Controls.Add(this.grpTraject);
            this.grpCursus.Controls.Add(this.grpCursustype);
            this.grpCursus.Controls.Add(this.grpToetcodes);
            this.grpCursus.Controls.Add(this.grpBlok);
            this.grpCursus.Location = new System.Drawing.Point(12, 12);
            this.grpCursus.Name = "grpCursus";
            this.grpCursus.Size = new System.Drawing.Size(402, 248);
            this.grpCursus.TabIndex = 13;
            this.grpCursus.TabStop = false;
            this.grpCursus.Text = "Cursusdetails:";
            // 
            // grpToets
            // 
            this.grpToets.Controls.Add(this.grpResultaatschaal);
            this.grpToets.Controls.Add(this.grpToetsECs);
            this.grpToets.Controls.Add(this.grpToetsvorm);
            this.grpToets.Location = new System.Drawing.Point(12, 281);
            this.grpToets.Name = "grpToets";
            this.grpToets.Size = new System.Drawing.Size(402, 203);
            this.grpToets.TabIndex = 14;
            this.grpToets.TabStop = false;
            this.grpToets.Text = "Toetsdetails:";
            // 
            // grpResultaatschaal
            // 
            this.grpResultaatschaal.Location = new System.Drawing.Point(12, 119);
            this.grpResultaatschaal.Name = "grpResultaatschaal";
            this.grpResultaatschaal.Size = new System.Drawing.Size(186, 44);
            this.grpResultaatschaal.TabIndex = 16;
            this.grpResultaatschaal.TabStop = false;
            this.grpResultaatschaal.Text = "Resultaatschaal:";
            // 
            // grpToetsECs
            // 
            this.grpToetsECs.Location = new System.Drawing.Point(12, 69);
            this.grpToetsECs.Name = "grpToetsECs";
            this.grpToetsECs.Size = new System.Drawing.Size(186, 44);
            this.grpToetsECs.TabIndex = 15;
            this.grpToetsECs.TabStop = false;
            this.grpToetsECs.Text = "Studiepunten:";
            // 
            // grpToetsvorm
            // 
            this.grpToetsvorm.Location = new System.Drawing.Point(12, 19);
            this.grpToetsvorm.Name = "grpToetsvorm";
            this.grpToetsvorm.Size = new System.Drawing.Size(186, 44);
            this.grpToetsvorm.TabIndex = 0;
            this.grpToetsvorm.TabStop = false;
            this.grpToetsvorm.Text = "Toetsvorm:";
            // 
            // frmCursus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 527);
            this.Controls.Add(this.grpToets);
            this.Controls.Add(this.grpCursus);
            this.Name = "frmCursus";
            this.Text = "Cursus-details (under construction)";
            this.Load += new System.EventHandler(this.frmCursus_Load);
            this.grpCursuscode.ResumeLayout(false);
            this.grpCursuscode.PerformLayout();
            this.grpTraject.ResumeLayout(false);
            this.grpTraject.PerformLayout();
            this.grpToetcodes.ResumeLayout(false);
            this.grpBlok.ResumeLayout(false);
            this.grpBlok.PerformLayout();
            this.grpCursustype.ResumeLayout(false);
            this.grpCursustype.PerformLayout();
            this.grpCursusnaam.ResumeLayout(false);
            this.grpCursusnaam.PerformLayout();
            this.grpCursus.ResumeLayout(false);
            this.grpToets.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListBox lstToetscodes;
        private System.Windows.Forms.CheckBox chkHW;
        private System.Windows.Forms.CheckBox chkAU;
        private System.Windows.Forms.CheckBox chkSW;
        private System.Windows.Forms.GroupBox grpCursuscode;
        private System.Windows.Forms.TextBox txtCursuscode;
        private System.Windows.Forms.GroupBox grpTraject;
        private System.Windows.Forms.GroupBox grpToetcodes;
        private System.Windows.Forms.GroupBox grpBlok;
        private System.Windows.Forms.TextBox txtPeriode;
        private System.Windows.Forms.GroupBox grpCursustype;
        private System.Windows.Forms.RadioButton btnProject;
        private System.Windows.Forms.RadioButton btnVak;
        private System.Windows.Forms.GroupBox grpCursusnaam;
        private System.Windows.Forms.TextBox txtCursusnaam;
        private System.Windows.Forms.GroupBox grpCursus;
        private System.Windows.Forms.GroupBox grpToets;
        private System.Windows.Forms.GroupBox grpResultaatschaal;
        private System.Windows.Forms.GroupBox grpToetsECs;
        private System.Windows.Forms.GroupBox grpToetsvorm;
        private System.Windows.Forms.TextBox txtBlok;
    }
}