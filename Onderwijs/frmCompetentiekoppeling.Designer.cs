namespace Onderwijs
{
    partial class frmCompetentiekoppeling
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
            this.tabCompetenties = new System.Windows.Forms.TabControl();
            this.tabINI = new System.Windows.Forms.TabPage();
            this.lblININiveau = new System.Windows.Forms.Label();
            this.txtININiveau = new System.Windows.Forms.TextBox();
            this.grpINIGedragskenmerken = new System.Windows.Forms.GroupBox();
            this.chklstINIGedragskenmerken = new System.Windows.Forms.CheckedListBox();
            this.grpINIBeschrijving = new System.Windows.Forms.GroupBox();
            this.txtINIOmschrijving = new System.Windows.Forms.TextBox();
            this.grpINIMvZ = new System.Windows.Forms.GroupBox();
            this.txtINIMvZ = new System.Windows.Forms.TextBox();
            this.btnINIZ3 = new System.Windows.Forms.RadioButton();
            this.btnINIZ2 = new System.Windows.Forms.RadioButton();
            this.btnINIZ1 = new System.Windows.Forms.RadioButton();
            this.btnINIZ0 = new System.Windows.Forms.RadioButton();
            this.grpINIAvC = new System.Windows.Forms.GroupBox();
            this.txtINIAvC = new System.Windows.Forms.TextBox();
            this.btnINIC3 = new System.Windows.Forms.RadioButton();
            this.btnINIC2 = new System.Windows.Forms.RadioButton();
            this.btnINIC1 = new System.Windows.Forms.RadioButton();
            this.btnINIC0 = new System.Windows.Forms.RadioButton();
            this.grpINIAvT = new System.Windows.Forms.GroupBox();
            this.txtINIAvT = new System.Windows.Forms.TextBox();
            this.btnINIT3 = new System.Windows.Forms.RadioButton();
            this.btnINIT2 = new System.Windows.Forms.RadioButton();
            this.btnINIT1 = new System.Windows.Forms.RadioButton();
            this.btnINIT0 = new System.Windows.Forms.RadioButton();
            this.chkINIGekoppeld = new System.Windows.Forms.CheckBox();
            this.cmdAccepteer = new System.Windows.Forms.Button();
            this.cmdAnnuleer = new System.Windows.Forms.Button();
            this.grpOnderwijsdoel = new System.Windows.Forms.GroupBox();
            this.txtOnderwijsdoel = new System.Windows.Forms.TextBox();
            this.tabCompetenties.SuspendLayout();
            this.tabINI.SuspendLayout();
            this.grpINIGedragskenmerken.SuspendLayout();
            this.grpINIBeschrijving.SuspendLayout();
            this.grpINIMvZ.SuspendLayout();
            this.grpINIAvC.SuspendLayout();
            this.grpINIAvT.SuspendLayout();
            this.grpOnderwijsdoel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabCompetenties
            // 
            this.tabCompetenties.Controls.Add(this.tabINI);
            this.tabCompetenties.Location = new System.Drawing.Point(11, 94);
            this.tabCompetenties.Name = "tabCompetenties";
            this.tabCompetenties.SelectedIndex = 0;
            this.tabCompetenties.Size = new System.Drawing.Size(611, 585);
            this.tabCompetenties.TabIndex = 3;
            // 
            // tabINI
            // 
            this.tabINI.Controls.Add(this.lblININiveau);
            this.tabINI.Controls.Add(this.txtININiveau);
            this.tabINI.Controls.Add(this.grpINIGedragskenmerken);
            this.tabINI.Controls.Add(this.grpINIBeschrijving);
            this.tabINI.Controls.Add(this.grpINIMvZ);
            this.tabINI.Controls.Add(this.grpINIAvC);
            this.tabINI.Controls.Add(this.grpINIAvT);
            this.tabINI.Controls.Add(this.chkINIGekoppeld);
            this.tabINI.Location = new System.Drawing.Point(4, 22);
            this.tabINI.Name = "tabINI";
            this.tabINI.Size = new System.Drawing.Size(603, 559);
            this.tabINI.TabIndex = 8;
            this.tabINI.Tag = "INI";
            this.tabINI.Text = "Initieel";
            this.tabINI.ToolTipText = "Initieel";
            this.tabINI.UseVisualStyleBackColor = true;
            // 
            // lblININiveau
            // 
            this.lblININiveau.AutoSize = true;
            this.lblININiveau.Location = new System.Drawing.Point(450, 10);
            this.lblININiveau.Name = "lblININiveau";
            this.lblININiveau.Size = new System.Drawing.Size(101, 13);
            this.lblININiveau.TabIndex = 12;
            this.lblININiveau.Text = "Competentieniveau:";
            // 
            // txtININiveau
            // 
            this.txtININiveau.BackColor = System.Drawing.SystemColors.Window;
            this.txtININiveau.Location = new System.Drawing.Point(556, 7);
            this.txtININiveau.Name = "txtININiveau";
            this.txtININiveau.ReadOnly = true;
            this.txtININiveau.Size = new System.Drawing.Size(37, 20);
            this.txtININiveau.TabIndex = 11;
            this.txtININiveau.TabStop = false;
            this.txtININiveau.Tag = "Niveau";
            this.txtININiveau.Text = "0";
            this.txtININiveau.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // grpINIGedragskenmerken
            // 
            this.grpINIGedragskenmerken.Controls.Add(this.chklstINIGedragskenmerken);
            this.grpINIGedragskenmerken.Location = new System.Drawing.Point(9, 400);
            this.grpINIGedragskenmerken.Name = "grpINIGedragskenmerken";
            this.grpINIGedragskenmerken.Size = new System.Drawing.Size(584, 151);
            this.grpINIGedragskenmerken.TabIndex = 10;
            this.grpINIGedragskenmerken.TabStop = false;
            this.grpINIGedragskenmerken.Tag = "INI";
            this.grpINIGedragskenmerken.Text = "Gedragskenmerken:";
            // 
            // chklstINIGedragskenmerken
            // 
            this.chklstINIGedragskenmerken.CheckOnClick = true;
            this.chklstINIGedragskenmerken.FormattingEnabled = true;
            this.chklstINIGedragskenmerken.HorizontalScrollbar = true;
            this.chklstINIGedragskenmerken.Items.AddRange(new object[] {
            "Hallo",
            "Wereld"});
            this.chklstINIGedragskenmerken.Location = new System.Drawing.Point(6, 19);
            this.chklstINIGedragskenmerken.Name = "chklstINIGedragskenmerken";
            this.chklstINIGedragskenmerken.Size = new System.Drawing.Size(572, 124);
            this.chklstINIGedragskenmerken.TabIndex = 0;
            this.chklstINIGedragskenmerken.Tag = "Gedragskenmerken";
            // 
            // grpINIBeschrijving
            // 
            this.grpINIBeschrijving.Controls.Add(this.txtINIOmschrijving);
            this.grpINIBeschrijving.Location = new System.Drawing.Point(283, 32);
            this.grpINIBeschrijving.Name = "grpINIBeschrijving";
            this.grpINIBeschrijving.Size = new System.Drawing.Size(310, 363);
            this.grpINIBeschrijving.TabIndex = 6;
            this.grpINIBeschrijving.TabStop = false;
            this.grpINIBeschrijving.Tag = "INI";
            this.grpINIBeschrijving.Text = "Competentieomschrijving:";
            // 
            // txtINIOmschrijving
            // 
            this.txtINIOmschrijving.BackColor = System.Drawing.SystemColors.Window;
            this.txtINIOmschrijving.Location = new System.Drawing.Point(6, 19);
            this.txtINIOmschrijving.Multiline = true;
            this.txtINIOmschrijving.Name = "txtINIOmschrijving";
            this.txtINIOmschrijving.ReadOnly = true;
            this.txtINIOmschrijving.Size = new System.Drawing.Size(298, 332);
            this.txtINIOmschrijving.TabIndex = 0;
            this.txtINIOmschrijving.TabStop = false;
            this.txtINIOmschrijving.Tag = "Competentieomschrijving";
            // 
            // grpINIMvZ
            // 
            this.grpINIMvZ.Controls.Add(this.txtINIMvZ);
            this.grpINIMvZ.Controls.Add(this.btnINIZ3);
            this.grpINIMvZ.Controls.Add(this.btnINIZ2);
            this.grpINIMvZ.Controls.Add(this.btnINIZ1);
            this.grpINIMvZ.Controls.Add(this.btnINIZ0);
            this.grpINIMvZ.Location = new System.Drawing.Point(9, 278);
            this.grpINIMvZ.Name = "grpINIMvZ";
            this.grpINIMvZ.Size = new System.Drawing.Size(268, 117);
            this.grpINIMvZ.TabIndex = 9;
            this.grpINIMvZ.TabStop = false;
            this.grpINIMvZ.Tag = "INI";
            this.grpINIMvZ.Text = "Mate van zelfstandigheid:";
            // 
            // txtINIMvZ
            // 
            this.txtINIMvZ.BackColor = System.Drawing.SystemColors.Window;
            this.txtINIMvZ.Location = new System.Drawing.Point(46, 19);
            this.txtINIMvZ.Multiline = true;
            this.txtINIMvZ.Name = "txtINIMvZ";
            this.txtINIMvZ.ReadOnly = true;
            this.txtINIMvZ.Size = new System.Drawing.Size(216, 86);
            this.txtINIMvZ.TabIndex = 4;
            this.txtINIMvZ.TabStop = false;
            this.txtINIMvZ.Tag = "MvZ";
            // 
            // btnINIZ3
            // 
            this.btnINIZ3.AutoSize = true;
            this.btnINIZ3.Location = new System.Drawing.Point(6, 88);
            this.btnINIZ3.Name = "btnINIZ3";
            this.btnINIZ3.Size = new System.Drawing.Size(34, 17);
            this.btnINIZ3.TabIndex = 3;
            this.btnINIZ3.TabStop = true;
            this.btnINIZ3.Tag = "MvZ";
            this.btnINIZ3.Text = "III";
            this.btnINIZ3.UseVisualStyleBackColor = true;
            this.btnINIZ3.CheckedChanged += new System.EventHandler(this.btnINI_CheckedChanged);
            // 
            // btnINIZ2
            // 
            this.btnINIZ2.AutoSize = true;
            this.btnINIZ2.Location = new System.Drawing.Point(6, 65);
            this.btnINIZ2.Name = "btnINIZ2";
            this.btnINIZ2.Size = new System.Drawing.Size(31, 17);
            this.btnINIZ2.TabIndex = 2;
            this.btnINIZ2.TabStop = true;
            this.btnINIZ2.Tag = "MvZ";
            this.btnINIZ2.Text = "II";
            this.btnINIZ2.UseVisualStyleBackColor = true;
            this.btnINIZ2.CheckedChanged += new System.EventHandler(this.btnINI_CheckedChanged);
            // 
            // btnINIZ1
            // 
            this.btnINIZ1.AutoSize = true;
            this.btnINIZ1.Location = new System.Drawing.Point(6, 42);
            this.btnINIZ1.Name = "btnINIZ1";
            this.btnINIZ1.Size = new System.Drawing.Size(28, 17);
            this.btnINIZ1.TabIndex = 1;
            this.btnINIZ1.TabStop = true;
            this.btnINIZ1.Tag = "MvZ";
            this.btnINIZ1.Text = "I";
            this.btnINIZ1.UseVisualStyleBackColor = true;
            this.btnINIZ1.CheckedChanged += new System.EventHandler(this.btnINI_CheckedChanged);
            // 
            // btnINIZ0
            // 
            this.btnINIZ0.AutoSize = true;
            this.btnINIZ0.Checked = true;
            this.btnINIZ0.Location = new System.Drawing.Point(6, 19);
            this.btnINIZ0.Name = "btnINIZ0";
            this.btnINIZ0.Size = new System.Drawing.Size(31, 17);
            this.btnINIZ0.TabIndex = 0;
            this.btnINIZ0.TabStop = true;
            this.btnINIZ0.Tag = "MvZ";
            this.btnINIZ0.Text = "0";
            this.btnINIZ0.UseVisualStyleBackColor = true;
            this.btnINIZ0.CheckedChanged += new System.EventHandler(this.btnINI_CheckedChanged);
            // 
            // grpINIAvC
            // 
            this.grpINIAvC.Controls.Add(this.txtINIAvC);
            this.grpINIAvC.Controls.Add(this.btnINIC3);
            this.grpINIAvC.Controls.Add(this.btnINIC2);
            this.grpINIAvC.Controls.Add(this.btnINIC1);
            this.grpINIAvC.Controls.Add(this.btnINIC0);
            this.grpINIAvC.Location = new System.Drawing.Point(9, 155);
            this.grpINIAvC.Name = "grpINIAvC";
            this.grpINIAvC.Size = new System.Drawing.Size(268, 117);
            this.grpINIAvC.TabIndex = 8;
            this.grpINIAvC.TabStop = false;
            this.grpINIAvC.Tag = "INI";
            this.grpINIAvC.Text = "Aard van de context:";
            // 
            // txtINIAvC
            // 
            this.txtINIAvC.BackColor = System.Drawing.SystemColors.Window;
            this.txtINIAvC.Location = new System.Drawing.Point(46, 19);
            this.txtINIAvC.Multiline = true;
            this.txtINIAvC.Name = "txtINIAvC";
            this.txtINIAvC.ReadOnly = true;
            this.txtINIAvC.Size = new System.Drawing.Size(216, 86);
            this.txtINIAvC.TabIndex = 4;
            this.txtINIAvC.TabStop = false;
            this.txtINIAvC.Tag = "AvC";
            // 
            // btnINIC3
            // 
            this.btnINIC3.AutoSize = true;
            this.btnINIC3.Location = new System.Drawing.Point(6, 88);
            this.btnINIC3.Name = "btnINIC3";
            this.btnINIC3.Size = new System.Drawing.Size(34, 17);
            this.btnINIC3.TabIndex = 3;
            this.btnINIC3.TabStop = true;
            this.btnINIC3.Tag = "AvC";
            this.btnINIC3.Text = "III";
            this.btnINIC3.UseVisualStyleBackColor = true;
            this.btnINIC3.CheckedChanged += new System.EventHandler(this.btnINI_CheckedChanged);
            // 
            // btnINIC2
            // 
            this.btnINIC2.AutoSize = true;
            this.btnINIC2.Location = new System.Drawing.Point(6, 65);
            this.btnINIC2.Name = "btnINIC2";
            this.btnINIC2.Size = new System.Drawing.Size(31, 17);
            this.btnINIC2.TabIndex = 2;
            this.btnINIC2.TabStop = true;
            this.btnINIC2.Tag = "AvC";
            this.btnINIC2.Text = "II";
            this.btnINIC2.UseVisualStyleBackColor = true;
            this.btnINIC2.CheckedChanged += new System.EventHandler(this.btnINI_CheckedChanged);
            // 
            // btnINIC1
            // 
            this.btnINIC1.AutoSize = true;
            this.btnINIC1.Location = new System.Drawing.Point(6, 42);
            this.btnINIC1.Name = "btnINIC1";
            this.btnINIC1.Size = new System.Drawing.Size(28, 17);
            this.btnINIC1.TabIndex = 1;
            this.btnINIC1.TabStop = true;
            this.btnINIC1.Tag = "AvC";
            this.btnINIC1.Text = "I";
            this.btnINIC1.UseVisualStyleBackColor = true;
            this.btnINIC1.CheckedChanged += new System.EventHandler(this.btnINI_CheckedChanged);
            // 
            // btnINIC0
            // 
            this.btnINIC0.AutoSize = true;
            this.btnINIC0.Checked = true;
            this.btnINIC0.Location = new System.Drawing.Point(6, 19);
            this.btnINIC0.Name = "btnINIC0";
            this.btnINIC0.Size = new System.Drawing.Size(31, 17);
            this.btnINIC0.TabIndex = 0;
            this.btnINIC0.TabStop = true;
            this.btnINIC0.Tag = "AvC";
            this.btnINIC0.Text = "0";
            this.btnINIC0.UseVisualStyleBackColor = true;
            this.btnINIC0.CheckedChanged += new System.EventHandler(this.btnINI_CheckedChanged);
            // 
            // grpINIAvT
            // 
            this.grpINIAvT.Controls.Add(this.txtINIAvT);
            this.grpINIAvT.Controls.Add(this.btnINIT3);
            this.grpINIAvT.Controls.Add(this.btnINIT2);
            this.grpINIAvT.Controls.Add(this.btnINIT1);
            this.grpINIAvT.Controls.Add(this.btnINIT0);
            this.grpINIAvT.Location = new System.Drawing.Point(9, 32);
            this.grpINIAvT.Name = "grpINIAvT";
            this.grpINIAvT.Size = new System.Drawing.Size(268, 117);
            this.grpINIAvT.TabIndex = 7;
            this.grpINIAvT.TabStop = false;
            this.grpINIAvT.Tag = "INI";
            this.grpINIAvT.Text = "Aard van de taak:";
            // 
            // txtINIAvT
            // 
            this.txtINIAvT.BackColor = System.Drawing.SystemColors.Window;
            this.txtINIAvT.Location = new System.Drawing.Point(46, 19);
            this.txtINIAvT.Multiline = true;
            this.txtINIAvT.Name = "txtINIAvT";
            this.txtINIAvT.ReadOnly = true;
            this.txtINIAvT.Size = new System.Drawing.Size(216, 86);
            this.txtINIAvT.TabIndex = 4;
            this.txtINIAvT.TabStop = false;
            this.txtINIAvT.Tag = "AvT";
            // 
            // btnINIT3
            // 
            this.btnINIT3.AutoSize = true;
            this.btnINIT3.Location = new System.Drawing.Point(6, 88);
            this.btnINIT3.Name = "btnINIT3";
            this.btnINIT3.Size = new System.Drawing.Size(34, 17);
            this.btnINIT3.TabIndex = 3;
            this.btnINIT3.TabStop = true;
            this.btnINIT3.Tag = "AvT";
            this.btnINIT3.Text = "III";
            this.btnINIT3.UseVisualStyleBackColor = true;
            this.btnINIT3.CheckedChanged += new System.EventHandler(this.btnINI_CheckedChanged);
            // 
            // btnINIT2
            // 
            this.btnINIT2.AutoSize = true;
            this.btnINIT2.Location = new System.Drawing.Point(6, 65);
            this.btnINIT2.Name = "btnINIT2";
            this.btnINIT2.Size = new System.Drawing.Size(31, 17);
            this.btnINIT2.TabIndex = 2;
            this.btnINIT2.TabStop = true;
            this.btnINIT2.Tag = "AvT";
            this.btnINIT2.Text = "II";
            this.btnINIT2.UseVisualStyleBackColor = true;
            this.btnINIT2.CheckedChanged += new System.EventHandler(this.btnINI_CheckedChanged);
            // 
            // btnINIT1
            // 
            this.btnINIT1.AutoSize = true;
            this.btnINIT1.Location = new System.Drawing.Point(6, 42);
            this.btnINIT1.Name = "btnINIT1";
            this.btnINIT1.Size = new System.Drawing.Size(28, 17);
            this.btnINIT1.TabIndex = 1;
            this.btnINIT1.TabStop = true;
            this.btnINIT1.Tag = "AvT";
            this.btnINIT1.Text = "I";
            this.btnINIT1.UseVisualStyleBackColor = true;
            this.btnINIT1.CheckedChanged += new System.EventHandler(this.btnINI_CheckedChanged);
            // 
            // btnINIT0
            // 
            this.btnINIT0.AutoSize = true;
            this.btnINIT0.Checked = true;
            this.btnINIT0.Location = new System.Drawing.Point(6, 19);
            this.btnINIT0.Name = "btnINIT0";
            this.btnINIT0.Size = new System.Drawing.Size(31, 17);
            this.btnINIT0.TabIndex = 0;
            this.btnINIT0.TabStop = true;
            this.btnINIT0.Tag = "AvT";
            this.btnINIT0.Text = "0";
            this.btnINIT0.UseVisualStyleBackColor = true;
            this.btnINIT0.CheckedChanged += new System.EventHandler(this.btnINI_CheckedChanged);
            // 
            // chkINIGekoppeld
            // 
            this.chkINIGekoppeld.AutoSize = true;
            this.chkINIGekoppeld.Location = new System.Drawing.Point(9, 9);
            this.chkINIGekoppeld.Name = "chkINIGekoppeld";
            this.chkINIGekoppeld.Size = new System.Drawing.Size(166, 17);
            this.chkINIGekoppeld.TabIndex = 5;
            this.chkINIGekoppeld.Tag = "Gekoppeld";
            this.chkINIGekoppeld.Text = "Gekoppeld aan onderwijsdoel";
            this.chkINIGekoppeld.UseVisualStyleBackColor = true;
            this.chkINIGekoppeld.CheckedChanged += new System.EventHandler(this.chkINI_CheckedChanged);
            // 
            // cmdAccepteer
            // 
            this.cmdAccepteer.Location = new System.Drawing.Point(513, 685);
            this.cmdAccepteer.Name = "cmdAccepteer";
            this.cmdAccepteer.Size = new System.Drawing.Size(105, 30);
            this.cmdAccepteer.TabIndex = 5;
            this.cmdAccepteer.Text = "Accepteer";
            this.cmdAccepteer.UseVisualStyleBackColor = true;
            this.cmdAccepteer.Click += new System.EventHandler(this.cmdAccepteer_Click);
            // 
            // cmdAnnuleer
            // 
            this.cmdAnnuleer.Location = new System.Drawing.Point(402, 685);
            this.cmdAnnuleer.Name = "cmdAnnuleer";
            this.cmdAnnuleer.Size = new System.Drawing.Size(105, 30);
            this.cmdAnnuleer.TabIndex = 4;
            this.cmdAnnuleer.Text = "Annuleer";
            this.cmdAnnuleer.UseVisualStyleBackColor = true;
            this.cmdAnnuleer.Click += new System.EventHandler(this.cmdAnnuleer_Click);
            // 
            // grpOnderwijsdoel
            // 
            this.grpOnderwijsdoel.Controls.Add(this.txtOnderwijsdoel);
            this.grpOnderwijsdoel.Location = new System.Drawing.Point(11, 2);
            this.grpOnderwijsdoel.Name = "grpOnderwijsdoel";
            this.grpOnderwijsdoel.Size = new System.Drawing.Size(606, 86);
            this.grpOnderwijsdoel.TabIndex = 6;
            this.grpOnderwijsdoel.TabStop = false;
            this.grpOnderwijsdoel.Text = "Onderwijsdoel:";
            // 
            // txtOnderwijsdoel
            // 
            this.txtOnderwijsdoel.BackColor = System.Drawing.SystemColors.Window;
            this.txtOnderwijsdoel.Location = new System.Drawing.Point(6, 19);
            this.txtOnderwijsdoel.Multiline = true;
            this.txtOnderwijsdoel.Name = "txtOnderwijsdoel";
            this.txtOnderwijsdoel.ReadOnly = true;
            this.txtOnderwijsdoel.Size = new System.Drawing.Size(594, 61);
            this.txtOnderwijsdoel.TabIndex = 0;
            // 
            // frmCompetentiekoppeling
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 720);
            this.ControlBox = false;
            this.Controls.Add(this.grpOnderwijsdoel);
            this.Controls.Add(this.cmdAnnuleer);
            this.Controls.Add(this.cmdAccepteer);
            this.Controls.Add(this.tabCompetenties);
            this.MaximumSize = new System.Drawing.Size(649, 759);
            this.MinimumSize = new System.Drawing.Size(649, 759);
            this.Name = "frmCompetentiekoppeling";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Competentiekoppeling";
            this.Load += new System.EventHandler(this.frmCompetentiekoppeling_Load);
            this.tabCompetenties.ResumeLayout(false);
            this.tabINI.ResumeLayout(false);
            this.tabINI.PerformLayout();
            this.grpINIGedragskenmerken.ResumeLayout(false);
            this.grpINIBeschrijving.ResumeLayout(false);
            this.grpINIBeschrijving.PerformLayout();
            this.grpINIMvZ.ResumeLayout(false);
            this.grpINIMvZ.PerformLayout();
            this.grpINIAvC.ResumeLayout(false);
            this.grpINIAvC.PerformLayout();
            this.grpINIAvT.ResumeLayout(false);
            this.grpINIAvT.PerformLayout();
            this.grpOnderwijsdoel.ResumeLayout(false);
            this.grpOnderwijsdoel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabCompetenties;
        private System.Windows.Forms.TabPage tabINI;
        private System.Windows.Forms.CheckBox chkINIGekoppeld;
        private System.Windows.Forms.GroupBox grpINIBeschrijving;
        private System.Windows.Forms.TextBox txtINIOmschrijving;
        private System.Windows.Forms.GroupBox grpINIAvT;
        private System.Windows.Forms.TextBox txtINIAvT;
        private System.Windows.Forms.RadioButton btnINIT3;
        private System.Windows.Forms.RadioButton btnINIT2;
        private System.Windows.Forms.RadioButton btnINIT1;
        private System.Windows.Forms.RadioButton btnINIT0;
        private System.Windows.Forms.GroupBox grpINIMvZ;
        private System.Windows.Forms.TextBox txtINIMvZ;
        private System.Windows.Forms.RadioButton btnINIZ3;
        private System.Windows.Forms.RadioButton btnINIZ2;
        private System.Windows.Forms.RadioButton btnINIZ1;
        private System.Windows.Forms.RadioButton btnINIZ0;
        private System.Windows.Forms.GroupBox grpINIAvC;
        private System.Windows.Forms.TextBox txtINIAvC;
        private System.Windows.Forms.RadioButton btnINIC3;
        private System.Windows.Forms.RadioButton btnINIC2;
        private System.Windows.Forms.RadioButton btnINIC1;
        private System.Windows.Forms.RadioButton btnINIC0;
        private System.Windows.Forms.GroupBox grpINIGedragskenmerken;
        private System.Windows.Forms.CheckedListBox chklstINIGedragskenmerken;
        private System.Windows.Forms.TextBox txtININiveau;
        private System.Windows.Forms.Label lblININiveau;
        private System.Windows.Forms.Button cmdAccepteer;
        private System.Windows.Forms.Button cmdAnnuleer;
        private System.Windows.Forms.GroupBox grpOnderwijsdoel;
        private System.Windows.Forms.TextBox txtOnderwijsdoel;
    }
}

