namespace Onderwijs
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            this.grpToetscodes = new System.Windows.Forms.GroupBox();
            this.lstToetscodes = new System.Windows.Forms.ListBox();
            this.txtOnderwerpen1 = new System.Windows.Forms.TextBox();
            this.grpDoel1 = new System.Windows.Forms.GroupBox();
            this.btnResultaatdoel1 = new System.Windows.Forms.RadioButton();
            this.btnLeerdoel1 = new System.Windows.Forms.RadioButton();
            this.linDoeltype1 = new System.Windows.Forms.Label();
            this.lblDoeltype1 = new System.Windows.Forms.Label();
            this.cmdSave1 = new System.Windows.Forms.Button();
            this.lblDoelId1 = new System.Windows.Forms.Label();
            this.cmdCompetenties1 = new System.Windows.Forms.Button();
            this.cmdBoKS1 = new System.Windows.Forms.Button();
            this.lstCompetenties1 = new System.Windows.Forms.ListBox();
            this.lstBoKS1 = new System.Windows.Forms.ListBox();
            this.lblCompetenties1 = new System.Windows.Forms.Label();
            this.lblBoKS1 = new System.Windows.Forms.Label();
            this.lblWeging1 = new System.Windows.Forms.Label();
            this.txtWeging1 = new System.Windows.Forms.TextBox();
            this.linBloom1 = new System.Windows.Forms.Label();
            this.lblBloom1 = new System.Windows.Forms.Label();
            this.chkCreeren1 = new System.Windows.Forms.CheckBox();
            this.chkEvalueren1 = new System.Windows.Forms.CheckBox();
            this.chkAnalyseren1 = new System.Windows.Forms.CheckBox();
            this.chkToepassen1 = new System.Windows.Forms.CheckBox();
            this.chkBegrijpen1 = new System.Windows.Forms.CheckBox();
            this.chkOnthouden1 = new System.Windows.Forms.CheckBox();
            this.cmdClear1 = new System.Windows.Forms.Button();
            this.cmdDelete1 = new System.Windows.Forms.Button();
            this.lblOnderwerpen1 = new System.Windows.Forms.Label();
            this.lblOmschrijving1 = new System.Windows.Forms.Label();
            this.txtOnderwijsdoel1 = new System.Windows.Forms.TextBox();
            this.cmdAdd = new System.Windows.Forms.Button();
            this.ttpOnderwijs = new System.Windows.Forms.ToolTip(this.components);
            this.grpBediening = new System.Windows.Forms.GroupBox();
            this.txtTotaal = new System.Windows.Forms.Label();
            this.lblTotaal = new System.Windows.Forms.Label();
            this.cmdDoelenOpslaan = new System.Windows.Forms.Button();
            this.cmdToetsdetails = new System.Windows.Forms.Button();
            this.grpToetscodes.SuspendLayout();
            this.grpDoel1.SuspendLayout();
            this.grpBediening.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpToetscodes
            // 
            this.grpToetscodes.Controls.Add(this.cmdToetsdetails);
            this.grpToetscodes.Controls.Add(this.lstToetscodes);
            this.grpToetscodes.Location = new System.Drawing.Point(3, 3);
            this.grpToetscodes.Name = "grpToetscodes";
            this.grpToetscodes.Size = new System.Drawing.Size(155, 858);
            this.grpToetscodes.TabIndex = 2;
            this.grpToetscodes.TabStop = false;
            this.grpToetscodes.Text = "Toetscode:";
            // 
            // lstToetscodes
            // 
            this.lstToetscodes.FormattingEnabled = true;
            this.lstToetscodes.IntegralHeight = false;
            this.lstToetscodes.Location = new System.Drawing.Point(6, 19);
            this.lstToetscodes.Name = "lstToetscodes";
            this.lstToetscodes.Size = new System.Drawing.Size(142, 798);
            this.lstToetscodes.TabIndex = 2;
            this.lstToetscodes.SelectedIndexChanged += new System.EventHandler(this.lstToetscodes_SelectedIndexChanged);
            // 
            // txtOnderwerpen1
            // 
            this.txtOnderwerpen1.Location = new System.Drawing.Point(532, 32);
            this.txtOnderwerpen1.Multiline = true;
            this.txtOnderwerpen1.Name = "txtOnderwerpen1";
            this.txtOnderwerpen1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOnderwerpen1.Size = new System.Drawing.Size(300, 62);
            this.txtOnderwerpen1.TabIndex = 11;
            this.txtOnderwerpen1.Tag = "txtOnderwerpen";
            this.txtOnderwerpen1.TextChanged += new System.EventHandler(this.txtGeneral_TextChanged);
            // 
            // grpDoel1
            // 
            this.grpDoel1.Controls.Add(this.btnResultaatdoel1);
            this.grpDoel1.Controls.Add(this.btnLeerdoel1);
            this.grpDoel1.Controls.Add(this.linDoeltype1);
            this.grpDoel1.Controls.Add(this.lblDoeltype1);
            this.grpDoel1.Controls.Add(this.cmdSave1);
            this.grpDoel1.Controls.Add(this.lblDoelId1);
            this.grpDoel1.Controls.Add(this.cmdCompetenties1);
            this.grpDoel1.Controls.Add(this.cmdBoKS1);
            this.grpDoel1.Controls.Add(this.lstCompetenties1);
            this.grpDoel1.Controls.Add(this.lstBoKS1);
            this.grpDoel1.Controls.Add(this.lblCompetenties1);
            this.grpDoel1.Controls.Add(this.lblBoKS1);
            this.grpDoel1.Controls.Add(this.lblWeging1);
            this.grpDoel1.Controls.Add(this.txtWeging1);
            this.grpDoel1.Controls.Add(this.linBloom1);
            this.grpDoel1.Controls.Add(this.lblBloom1);
            this.grpDoel1.Controls.Add(this.chkCreeren1);
            this.grpDoel1.Controls.Add(this.chkEvalueren1);
            this.grpDoel1.Controls.Add(this.chkAnalyseren1);
            this.grpDoel1.Controls.Add(this.chkToepassen1);
            this.grpDoel1.Controls.Add(this.chkBegrijpen1);
            this.grpDoel1.Controls.Add(this.chkOnthouden1);
            this.grpDoel1.Controls.Add(this.cmdClear1);
            this.grpDoel1.Controls.Add(this.cmdDelete1);
            this.grpDoel1.Controls.Add(this.lblOnderwerpen1);
            this.grpDoel1.Controls.Add(this.lblOmschrijving1);
            this.grpDoel1.Controls.Add(this.txtOnderwijsdoel1);
            this.grpDoel1.Controls.Add(this.txtOnderwerpen1);
            this.grpDoel1.Location = new System.Drawing.Point(165, 3);
            this.grpDoel1.Name = "grpDoel1";
            this.grpDoel1.Size = new System.Drawing.Size(1415, 100);
            this.grpDoel1.TabIndex = 11;
            this.grpDoel1.TabStop = false;
            this.grpDoel1.Tag = "grpDoel";
            this.grpDoel1.Text = "Doel 1:";
            // 
            // btnResultaatdoel1
            // 
            this.btnResultaatdoel1.AutoSize = true;
            this.btnResultaatdoel1.Location = new System.Drawing.Point(117, 57);
            this.btnResultaatdoel1.Name = "btnResultaatdoel1";
            this.btnResultaatdoel1.Size = new System.Drawing.Size(90, 17);
            this.btnResultaatdoel1.TabIndex = 37;
            this.btnResultaatdoel1.Tag = "btnResultaatdoel";
            this.btnResultaatdoel1.Text = "Resultaatdoel";
            this.btnResultaatdoel1.UseVisualStyleBackColor = true;
            // 
            // btnLeerdoel1
            // 
            this.btnLeerdoel1.AutoSize = true;
            this.btnLeerdoel1.Checked = true;
            this.btnLeerdoel1.Location = new System.Drawing.Point(117, 38);
            this.btnLeerdoel1.Name = "btnLeerdoel1";
            this.btnLeerdoel1.Size = new System.Drawing.Size(66, 17);
            this.btnLeerdoel1.TabIndex = 36;
            this.btnLeerdoel1.TabStop = true;
            this.btnLeerdoel1.Tag = "btnLeerdoel";
            this.btnLeerdoel1.Text = "Leerdoel";
            this.btnLeerdoel1.UseVisualStyleBackColor = true;
            this.btnLeerdoel1.CheckedChanged += new System.EventHandler(this.btnGeneral_CheckedChanged);
            // 
            // linDoeltype1
            // 
            this.linDoeltype1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.linDoeltype1.Location = new System.Drawing.Point(116, 32);
            this.linDoeltype1.Name = "linDoeltype1";
            this.linDoeltype1.Size = new System.Drawing.Size(100, 1);
            this.linDoeltype1.TabIndex = 35;
            this.linDoeltype1.Tag = "linDoeltype";
            // 
            // lblDoeltype1
            // 
            this.lblDoeltype1.AutoSize = true;
            this.lblDoeltype1.Location = new System.Drawing.Point(113, 16);
            this.lblDoeltype1.Name = "lblDoeltype1";
            this.lblDoeltype1.Size = new System.Drawing.Size(101, 13);
            this.lblDoeltype1.TabIndex = 34;
            this.lblDoeltype1.Tag = "lblDoeltype";
            this.lblDoeltype1.Text = "Type onderwijsdoel:";
            // 
            // cmdSave1
            // 
            this.cmdSave1.Location = new System.Drawing.Point(6, 32);
            this.cmdSave1.Name = "cmdSave1";
            this.cmdSave1.Size = new System.Drawing.Size(105, 29);
            this.cmdSave1.TabIndex = 33;
            this.cmdSave1.Tag = "cmdSave";
            this.cmdSave1.Text = "Doel opslaan";
            this.cmdSave1.UseVisualStyleBackColor = true;
            this.cmdSave1.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // lblDoelId1
            // 
            this.lblDoelId1.AutoSize = true;
            this.lblDoelId1.ForeColor = System.Drawing.Color.DarkGray;
            this.lblDoelId1.Location = new System.Drawing.Point(6, 16);
            this.lblDoelId1.Name = "lblDoelId1";
            this.lblDoelId1.Size = new System.Drawing.Size(34, 13);
            this.lblDoelId1.TabIndex = 32;
            this.lblDoelId1.Tag = "lblDoelId";
            this.lblDoelId1.Text = "(Id=0)";
            // 
            // cmdCompetenties1
            // 
            this.cmdCompetenties1.Location = new System.Drawing.Point(1140, 10);
            this.cmdCompetenties1.Name = "cmdCompetenties1";
            this.cmdCompetenties1.Size = new System.Drawing.Size(28, 20);
            this.cmdCompetenties1.TabIndex = 31;
            this.cmdCompetenties1.Tag = "cmdCompetenties";
            this.cmdCompetenties1.Text = "···";
            this.cmdCompetenties1.UseVisualStyleBackColor = true;
            this.cmdCompetenties1.Click += new System.EventHandler(this.cmdCompetenties_Click);
            // 
            // cmdBoKS1
            // 
            this.cmdBoKS1.Location = new System.Drawing.Point(1043, 10);
            this.cmdBoKS1.Name = "cmdBoKS1";
            this.cmdBoKS1.Size = new System.Drawing.Size(28, 20);
            this.cmdBoKS1.TabIndex = 30;
            this.cmdBoKS1.Tag = "cmdBoKS";
            this.cmdBoKS1.Text = "···";
            this.cmdBoKS1.UseVisualStyleBackColor = true;
            this.cmdBoKS1.Click += new System.EventHandler(this.cmdBoKS_Click);
            // 
            // lstCompetenties1
            // 
            this.lstCompetenties1.FormattingEnabled = true;
            this.lstCompetenties1.IntegralHeight = false;
            this.lstCompetenties1.Location = new System.Drawing.Point(1141, 32);
            this.lstCompetenties1.Name = "lstCompetenties1";
            this.lstCompetenties1.Size = new System.Drawing.Size(193, 62);
            this.lstCompetenties1.TabIndex = 29;
            this.lstCompetenties1.Tag = "lstCompetenties";
            // 
            // lstBoKS1
            // 
            this.lstBoKS1.FormattingEnabled = true;
            this.lstBoKS1.IntegralHeight = false;
            this.lstBoKS1.Location = new System.Drawing.Point(1044, 32);
            this.lstBoKS1.Name = "lstBoKS1";
            this.lstBoKS1.Size = new System.Drawing.Size(91, 62);
            this.lstBoKS1.TabIndex = 28;
            this.lstBoKS1.Tag = "lstBoKS";
            // 
            // lblCompetenties1
            // 
            this.lblCompetenties1.AutoSize = true;
            this.lblCompetenties1.Location = new System.Drawing.Point(1169, 16);
            this.lblCompetenties1.Name = "lblCompetenties1";
            this.lblCompetenties1.Size = new System.Drawing.Size(146, 13);
            this.lblCompetenties1.TabIndex = 27;
            this.lblCompetenties1.Tag = "lblCompetenties";
            this.lblCompetenties1.Text = "Competenties/gedragskenm.:";
            // 
            // lblBoKS1
            // 
            this.lblBoKS1.AutoSize = true;
            this.lblBoKS1.Location = new System.Drawing.Point(1072, 16);
            this.lblBoKS1.Name = "lblBoKS1";
            this.lblBoKS1.Size = new System.Drawing.Size(64, 13);
            this.lblBoKS1.TabIndex = 26;
            this.lblBoKS1.Tag = "lblBoKS";
            this.lblBoKS1.Text = "BoKS-items:";
            // 
            // lblWeging1
            // 
            this.lblWeging1.AutoSize = true;
            this.lblWeging1.Location = new System.Drawing.Point(835, 16);
            this.lblWeging1.Name = "lblWeging1";
            this.lblWeging1.Size = new System.Drawing.Size(47, 13);
            this.lblWeging1.TabIndex = 25;
            this.lblWeging1.Tag = "lblWeging";
            this.lblWeging1.Text = "Weging:";
            this.ttpOnderwijs.SetToolTip(this.lblWeging1, "Weging in procent (0..100)");
            // 
            // txtWeging1
            // 
            this.txtWeging1.Location = new System.Drawing.Point(838, 32);
            this.txtWeging1.Name = "txtWeging1";
            this.txtWeging1.Size = new System.Drawing.Size(38, 20);
            this.txtWeging1.TabIndex = 24;
            this.txtWeging1.Tag = "txtWeging";
            this.txtWeging1.Text = "0";
            this.txtWeging1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtWeging1.TextChanged += new System.EventHandler(this.txtWeging_TextChanged);
            this.txtWeging1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtWeging_KeyPress);
            // 
            // linBloom1
            // 
            this.linBloom1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.linBloom1.Location = new System.Drawing.Point(884, 32);
            this.linBloom1.Name = "linBloom1";
            this.linBloom1.Size = new System.Drawing.Size(150, 1);
            this.linBloom1.TabIndex = 23;
            this.linBloom1.Tag = "linBloom";
            // 
            // lblBloom1
            // 
            this.lblBloom1.AutoSize = true;
            this.lblBloom1.Location = new System.Drawing.Point(881, 16);
            this.lblBloom1.Name = "lblBloom1";
            this.lblBloom1.Size = new System.Drawing.Size(68, 13);
            this.lblBloom1.TabIndex = 22;
            this.lblBloom1.Tag = "lblBloom";
            this.lblBloom1.Text = "Denkniveau:";
            // 
            // chkCreeren1
            // 
            this.chkCreeren1.AutoSize = true;
            this.chkCreeren1.Location = new System.Drawing.Point(964, 73);
            this.chkCreeren1.Name = "chkCreeren1";
            this.chkCreeren1.Size = new System.Drawing.Size(63, 17);
            this.chkCreeren1.TabIndex = 21;
            this.chkCreeren1.Tag = "chkCreeren";
            this.chkCreeren1.Text = "Creëren";
            this.chkCreeren1.UseVisualStyleBackColor = true;
            this.chkCreeren1.CheckedChanged += new System.EventHandler(this.chkGeneral_CheckedChanged);
            // 
            // chkEvalueren1
            // 
            this.chkEvalueren1.AutoSize = true;
            this.chkEvalueren1.Location = new System.Drawing.Point(964, 55);
            this.chkEvalueren1.Name = "chkEvalueren1";
            this.chkEvalueren1.Size = new System.Drawing.Size(74, 17);
            this.chkEvalueren1.TabIndex = 20;
            this.chkEvalueren1.Tag = "chkEvalueren";
            this.chkEvalueren1.Text = "Evalueren";
            this.chkEvalueren1.UseVisualStyleBackColor = true;
            this.chkEvalueren1.CheckedChanged += new System.EventHandler(this.chkGeneral_CheckedChanged);
            // 
            // chkAnalyseren1
            // 
            this.chkAnalyseren1.AutoSize = true;
            this.chkAnalyseren1.Location = new System.Drawing.Point(964, 37);
            this.chkAnalyseren1.Name = "chkAnalyseren1";
            this.chkAnalyseren1.Size = new System.Drawing.Size(78, 17);
            this.chkAnalyseren1.TabIndex = 19;
            this.chkAnalyseren1.Tag = "chkAnalyseren";
            this.chkAnalyseren1.Text = "Analyseren";
            this.chkAnalyseren1.UseVisualStyleBackColor = true;
            this.chkAnalyseren1.CheckedChanged += new System.EventHandler(this.chkGeneral_CheckedChanged);
            // 
            // chkToepassen1
            // 
            this.chkToepassen1.AutoSize = true;
            this.chkToepassen1.Location = new System.Drawing.Point(884, 73);
            this.chkToepassen1.Name = "chkToepassen1";
            this.chkToepassen1.Size = new System.Drawing.Size(79, 17);
            this.chkToepassen1.TabIndex = 18;
            this.chkToepassen1.Tag = "chkToepassen";
            this.chkToepassen1.Text = "Toepassen";
            this.chkToepassen1.UseVisualStyleBackColor = true;
            this.chkToepassen1.CheckedChanged += new System.EventHandler(this.chkGeneral_CheckedChanged);
            // 
            // chkBegrijpen1
            // 
            this.chkBegrijpen1.AutoSize = true;
            this.chkBegrijpen1.Location = new System.Drawing.Point(884, 55);
            this.chkBegrijpen1.Name = "chkBegrijpen1";
            this.chkBegrijpen1.Size = new System.Drawing.Size(70, 17);
            this.chkBegrijpen1.TabIndex = 17;
            this.chkBegrijpen1.Tag = "chkBegrijpen";
            this.chkBegrijpen1.Text = "Begrijpen";
            this.chkBegrijpen1.UseVisualStyleBackColor = true;
            this.chkBegrijpen1.CheckedChanged += new System.EventHandler(this.chkGeneral_CheckedChanged);
            // 
            // chkOnthouden1
            // 
            this.chkOnthouden1.AutoSize = true;
            this.chkOnthouden1.Location = new System.Drawing.Point(884, 37);
            this.chkOnthouden1.Name = "chkOnthouden1";
            this.chkOnthouden1.Size = new System.Drawing.Size(79, 17);
            this.chkOnthouden1.TabIndex = 16;
            this.chkOnthouden1.Tag = "chkOnthouden";
            this.chkOnthouden1.Text = "Onthouden";
            this.chkOnthouden1.UseVisualStyleBackColor = true;
            this.chkOnthouden1.CheckedChanged += new System.EventHandler(this.chkGeneral_CheckedChanged);
            // 
            // cmdClear1
            // 
            this.cmdClear1.Location = new System.Drawing.Point(385, 48);
            this.cmdClear1.Name = "cmdClear1";
            this.cmdClear1.Size = new System.Drawing.Size(105, 29);
            this.cmdClear1.TabIndex = 15;
            this.cmdClear1.Tag = "cmdClear";
            this.cmdClear1.Text = "Doel wissen";
            this.cmdClear1.UseVisualStyleBackColor = true;
            this.cmdClear1.Visible = false;
            this.cmdClear1.Click += new System.EventHandler(this.cmdClear_Click);
            // 
            // cmdDelete1
            // 
            this.cmdDelete1.Enabled = false;
            this.cmdDelete1.Location = new System.Drawing.Point(6, 65);
            this.cmdDelete1.Name = "cmdDelete1";
            this.cmdDelete1.Size = new System.Drawing.Size(105, 29);
            this.cmdDelete1.TabIndex = 14;
            this.cmdDelete1.Tag = "cmdDelete";
            this.cmdDelete1.Text = "Doel verwijderen";
            this.cmdDelete1.UseVisualStyleBackColor = true;
            this.cmdDelete1.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // lblOnderwerpen1
            // 
            this.lblOnderwerpen1.AutoSize = true;
            this.lblOnderwerpen1.Location = new System.Drawing.Point(530, 16);
            this.lblOnderwerpen1.Name = "lblOnderwerpen1";
            this.lblOnderwerpen1.Size = new System.Drawing.Size(74, 13);
            this.lblOnderwerpen1.TabIndex = 13;
            this.lblOnderwerpen1.Tag = "lblOnderwerpen";
            this.lblOnderwerpen1.Text = "Onderwerpen:";
            // 
            // lblOmschrijving1
            // 
            this.lblOmschrijving1.AutoSize = true;
            this.lblOmschrijving1.Location = new System.Drawing.Point(224, 16);
            this.lblOmschrijving1.Name = "lblOmschrijving1";
            this.lblOmschrijving1.Size = new System.Drawing.Size(67, 13);
            this.lblOmschrijving1.TabIndex = 12;
            this.lblOmschrijving1.Tag = "lblOmschrijving";
            this.lblOmschrijving1.Text = "Beschrijving:";
            // 
            // txtOnderwijsdoel1
            // 
            this.txtOnderwijsdoel1.Location = new System.Drawing.Point(226, 32);
            this.txtOnderwijsdoel1.Multiline = true;
            this.txtOnderwijsdoel1.Name = "txtOnderwijsdoel1";
            this.txtOnderwijsdoel1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOnderwijsdoel1.Size = new System.Drawing.Size(300, 62);
            this.txtOnderwijsdoel1.TabIndex = 11;
            this.txtOnderwijsdoel1.Tag = "txtOnderwijsdoel";
            this.txtOnderwijsdoel1.TextChanged += new System.EventHandler(this.txtGeneral_TextChanged);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(5, 19);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(105, 29);
            this.cmdAdd.TabIndex = 12;
            this.cmdAdd.Text = "Doel toevoegen";
            this.cmdAdd.UseVisualStyleBackColor = true;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // ttpOnderwijs
            // 
            this.ttpOnderwijs.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // grpBediening
            // 
            this.grpBediening.Controls.Add(this.txtTotaal);
            this.grpBediening.Controls.Add(this.lblTotaal);
            this.grpBediening.Controls.Add(this.cmdDoelenOpslaan);
            this.grpBediening.Controls.Add(this.cmdAdd);
            this.grpBediening.Location = new System.Drawing.Point(165, 107);
            this.grpBediening.Name = "grpBediening";
            this.grpBediening.Size = new System.Drawing.Size(1230, 54);
            this.grpBediening.TabIndex = 13;
            this.grpBediening.TabStop = false;
            this.grpBediening.Text = "Bediening:";
            // 
            // txtTotaal
            // 
            this.txtTotaal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.txtTotaal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotaal.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTotaal.Location = new System.Drawing.Point(838, 26);
            this.txtTotaal.Name = "txtTotaal";
            this.txtTotaal.Size = new System.Drawing.Size(38, 20);
            this.txtTotaal.TabIndex = 35;
            this.txtTotaal.Text = "0";
            this.txtTotaal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.txtTotaal.TextChanged += new System.EventHandler(this.txtTotaal_TextChanged);
            // 
            // lblTotaal
            // 
            this.lblTotaal.AutoSize = true;
            this.lblTotaal.Location = new System.Drawing.Point(836, 11);
            this.lblTotaal.Name = "lblTotaal";
            this.lblTotaal.Size = new System.Drawing.Size(40, 13);
            this.lblTotaal.TabIndex = 34;
            this.lblTotaal.Tag = "lblTotaal";
            this.lblTotaal.Text = "Totaal:";
            // 
            // cmdDoelenOpslaan
            // 
            this.cmdDoelenOpslaan.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.cmdDoelenOpslaan.Location = new System.Drawing.Point(116, 19);
            this.cmdDoelenOpslaan.Name = "cmdDoelenOpslaan";
            this.cmdDoelenOpslaan.Size = new System.Drawing.Size(105, 29);
            this.cmdDoelenOpslaan.TabIndex = 14;
            this.cmdDoelenOpslaan.Text = "Doelen opslaan";
            this.cmdDoelenOpslaan.UseVisualStyleBackColor = true;
            this.cmdDoelenOpslaan.Click += new System.EventHandler(this.cmdDoelenOpslaan_Click);
            // 
            // cmdToetsdetails
            // 
            this.cmdToetsdetails.Location = new System.Drawing.Point(6, 823);
            this.cmdToetsdetails.Name = "cmdToetsdetails";
            this.cmdToetsdetails.Size = new System.Drawing.Size(142, 29);
            this.cmdToetsdetails.TabIndex = 13;
            this.cmdToetsdetails.Text = "Toetsdetails";
            this.cmdToetsdetails.UseVisualStyleBackColor = true;
            this.cmdToetsdetails.Click += new System.EventHandler(this.cmdToetsdetails_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1588, 869);
            this.Controls.Add(this.grpBediening);
            this.Controls.Add(this.grpDoel1);
            this.Controls.Add(this.grpToetscodes);
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Onderwijs ET/TI";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.grpToetscodes.ResumeLayout(false);
            this.grpDoel1.ResumeLayout(false);
            this.grpDoel1.PerformLayout();
            this.grpBediening.ResumeLayout(false);
            this.grpBediening.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox grpToetscodes;
        private System.Windows.Forms.ListBox lstToetscodes;
        private System.Windows.Forms.TextBox txtOnderwerpen1;
        private System.Windows.Forms.GroupBox grpDoel1;
        private System.Windows.Forms.TextBox txtOnderwijsdoel1;
        private System.Windows.Forms.Label lblOnderwerpen1;
        private System.Windows.Forms.Label lblOmschrijving1;
        private System.Windows.Forms.Button cmdAdd;
        private System.Windows.Forms.Button cmdDelete1;
        private System.Windows.Forms.Button cmdClear1;
        private System.Windows.Forms.CheckBox chkCreeren1;
        private System.Windows.Forms.CheckBox chkEvalueren1;
        private System.Windows.Forms.CheckBox chkAnalyseren1;
        private System.Windows.Forms.CheckBox chkToepassen1;
        private System.Windows.Forms.CheckBox chkBegrijpen1;
        private System.Windows.Forms.CheckBox chkOnthouden1;
        private System.Windows.Forms.Label linBloom1;
        private System.Windows.Forms.Label lblBloom1;
        private System.Windows.Forms.Label lblWeging1;
        private System.Windows.Forms.TextBox txtWeging1;
        private System.Windows.Forms.ToolTip ttpOnderwijs;
        private System.Windows.Forms.ListBox lstCompetenties1;
        private System.Windows.Forms.ListBox lstBoKS1;
        private System.Windows.Forms.Label lblCompetenties1;
        private System.Windows.Forms.Label lblBoKS1;
        private System.Windows.Forms.Button cmdCompetenties1;
        private System.Windows.Forms.Button cmdBoKS1;
        private System.Windows.Forms.Label lblDoelId1;
        private System.Windows.Forms.GroupBox grpBediening;
        private System.Windows.Forms.Button cmdDoelenOpslaan;
        private System.Windows.Forms.Button cmdSave1;
        private System.Windows.Forms.Label lblTotaal;
        private System.Windows.Forms.Label txtTotaal;
        private System.Windows.Forms.Label linDoeltype1;
        private System.Windows.Forms.Label lblDoeltype1;
        private System.Windows.Forms.RadioButton btnLeerdoel1;
        private System.Windows.Forms.RadioButton btnResultaatdoel1;
        private System.Windows.Forms.Button cmdToetsdetails;
    }
}