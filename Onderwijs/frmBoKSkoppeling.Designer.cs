namespace Onderwijs
{
    partial class frmBoKSkoppeling
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
            this.grpOnderwijsdoel = new System.Windows.Forms.GroupBox();
            this.txtOnderwijsdoel = new System.Windows.Forms.TextBox();
            this.cmdAnnuleer = new System.Windows.Forms.Button();
            this.cmdAccepteer = new System.Windows.Forms.Button();
            this.tabBoKS = new System.Windows.Forms.TabControl();
            this.tabET = new System.Windows.Forms.TabPage();
            this.lvwETBoKSitem = new System.Windows.Forms.ListView();
            this.hdrET = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.hdrETBoKS = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.hdrETCategorie = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.hdrETItemnummer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.hdrETItem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabTI = new System.Windows.Forms.TabPage();
            this.lvwTIBoKSitem = new System.Windows.Forms.ListView();
            this.hdrTI = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.hdrTIBoKS = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.hdrTICategorie = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.hdrTIItemnummer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.hdrTIItem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.grpOnderwijsdoel.SuspendLayout();
            this.tabBoKS.SuspendLayout();
            this.tabET.SuspendLayout();
            this.tabTI.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpOnderwijsdoel
            // 
            this.grpOnderwijsdoel.Controls.Add(this.txtOnderwijsdoel);
            this.grpOnderwijsdoel.Location = new System.Drawing.Point(11, 2);
            this.grpOnderwijsdoel.Name = "grpOnderwijsdoel";
            this.grpOnderwijsdoel.Size = new System.Drawing.Size(861, 86);
            this.grpOnderwijsdoel.TabIndex = 7;
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
            this.txtOnderwijsdoel.Size = new System.Drawing.Size(849, 61);
            this.txtOnderwijsdoel.TabIndex = 0;
            // 
            // cmdAnnuleer
            // 
            this.cmdAnnuleer.Location = new System.Drawing.Point(650, 685);
            this.cmdAnnuleer.Name = "cmdAnnuleer";
            this.cmdAnnuleer.Size = new System.Drawing.Size(105, 30);
            this.cmdAnnuleer.TabIndex = 8;
            this.cmdAnnuleer.Text = "Annuleer";
            this.toolTip.SetToolTip(this.cmdAnnuleer, "Sluiten zonder opslaan.");
            this.cmdAnnuleer.UseVisualStyleBackColor = true;
            this.cmdAnnuleer.Click += new System.EventHandler(this.cmdAnnuleer_Click);
            // 
            // cmdAccepteer
            // 
            this.cmdAccepteer.Location = new System.Drawing.Point(761, 685);
            this.cmdAccepteer.Name = "cmdAccepteer";
            this.cmdAccepteer.Size = new System.Drawing.Size(105, 30);
            this.cmdAccepteer.TabIndex = 9;
            this.cmdAccepteer.Text = "Accepteer";
            this.toolTip.SetToolTip(this.cmdAccepteer, "Opslaan en sluiten.");
            this.cmdAccepteer.UseVisualStyleBackColor = true;
            this.cmdAccepteer.Click += new System.EventHandler(this.cmdAccepteer_Click);
            // 
            // tabBoKS
            // 
            this.tabBoKS.Controls.Add(this.tabET);
            this.tabBoKS.Controls.Add(this.tabTI);
            this.tabBoKS.Location = new System.Drawing.Point(11, 94);
            this.tabBoKS.Name = "tabBoKS";
            this.tabBoKS.SelectedIndex = 0;
            this.tabBoKS.Size = new System.Drawing.Size(861, 585);
            this.tabBoKS.TabIndex = 11;
            // 
            // tabET
            // 
            this.tabET.Controls.Add(this.lvwETBoKSitem);
            this.tabET.Location = new System.Drawing.Point(4, 22);
            this.tabET.Name = "tabET";
            this.tabET.Padding = new System.Windows.Forms.Padding(3);
            this.tabET.Size = new System.Drawing.Size(853, 559);
            this.tabET.TabIndex = 0;
            this.tabET.Text = "BoKS Elektrotechniek";
            this.tabET.UseVisualStyleBackColor = true;
            // 
            // lvwETBoKSitem
            // 
            this.lvwETBoKSitem.CheckBoxes = true;
            this.lvwETBoKSitem.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.hdrET,
            this.hdrETBoKS,
            this.hdrETCategorie,
            this.hdrETItemnummer,
            this.hdrETItem});
            this.lvwETBoKSitem.FullRowSelect = true;
            this.lvwETBoKSitem.GridLines = true;
            this.lvwETBoKSitem.HideSelection = false;
            this.lvwETBoKSitem.Location = new System.Drawing.Point(6, 6);
            this.lvwETBoKSitem.MultiSelect = false;
            this.lvwETBoKSitem.Name = "lvwETBoKSitem";
            this.lvwETBoKSitem.Size = new System.Drawing.Size(841, 547);
            this.lvwETBoKSitem.Sorting = System.Windows.Forms.SortOrder.Descending;
            this.lvwETBoKSitem.TabIndex = 1;
            this.lvwETBoKSitem.UseCompatibleStateImageBehavior = false;
            this.lvwETBoKSitem.View = System.Windows.Forms.View.Details;
            this.lvwETBoKSitem.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwETBoKSitem_ColumnClick);
            this.lvwETBoKSitem.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvwETBoKSitem_ItemCheck);
            // 
            // hdrET
            // 
            this.hdrET.Text = "Selectie";
            this.hdrET.Width = 50;
            // 
            // hdrETBoKS
            // 
            this.hdrETBoKS.Text = "BoKS";
            this.hdrETBoKS.Width = 135;
            // 
            // hdrETCategorie
            // 
            this.hdrETCategorie.Text = "Categorie";
            this.hdrETCategorie.Width = 150;
            // 
            // hdrETItemnummer
            // 
            this.hdrETItemnummer.Text = "Nr";
            this.hdrETItemnummer.Width = 40;
            // 
            // hdrETItem
            // 
            this.hdrETItem.Text = "Item";
            this.hdrETItem.Width = 750;
            // 
            // tabTI
            // 
            this.tabTI.Controls.Add(this.lvwTIBoKSitem);
            this.tabTI.Location = new System.Drawing.Point(4, 22);
            this.tabTI.Name = "tabTI";
            this.tabTI.Padding = new System.Windows.Forms.Padding(3);
            this.tabTI.Size = new System.Drawing.Size(853, 559);
            this.tabTI.TabIndex = 1;
            this.tabTI.Text = "BoKS Technische Informatica";
            this.tabTI.UseVisualStyleBackColor = true;
            // 
            // lvwTIBoKSitem
            // 
            this.lvwTIBoKSitem.CheckBoxes = true;
            this.lvwTIBoKSitem.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.hdrTI,
            this.hdrTIBoKS,
            this.hdrTICategorie,
            this.hdrTIItemnummer,
            this.hdrTIItem});
            this.lvwTIBoKSitem.FullRowSelect = true;
            this.lvwTIBoKSitem.GridLines = true;
            this.lvwTIBoKSitem.HideSelection = false;
            this.lvwTIBoKSitem.Location = new System.Drawing.Point(6, 6);
            this.lvwTIBoKSitem.MultiSelect = false;
            this.lvwTIBoKSitem.Name = "lvwTIBoKSitem";
            this.lvwTIBoKSitem.Size = new System.Drawing.Size(841, 547);
            this.lvwTIBoKSitem.Sorting = System.Windows.Forms.SortOrder.Descending;
            this.lvwTIBoKSitem.TabIndex = 2;
            this.lvwTIBoKSitem.UseCompatibleStateImageBehavior = false;
            this.lvwTIBoKSitem.View = System.Windows.Forms.View.Details;
            this.lvwTIBoKSitem.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwTIBoKSitem_ColumnClick);
            this.lvwTIBoKSitem.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvwTIBoKSitem_ItemCheck);
            // 
            // hdrTI
            // 
            this.hdrTI.Text = "Selectie";
            this.hdrTI.Width = 50;
            // 
            // hdrTIBoKS
            // 
            this.hdrTIBoKS.Text = "BoKS";
            this.hdrTIBoKS.Width = 135;
            // 
            // hdrTICategorie
            // 
            this.hdrTICategorie.Text = "Categorie";
            this.hdrTICategorie.Width = 150;
            // 
            // hdrTIItemnummer
            // 
            this.hdrTIItemnummer.Text = "Nr";
            this.hdrTIItemnummer.Width = 40;
            // 
            // hdrTIItem
            // 
            this.hdrTIItem.Text = "Item";
            this.hdrTIItem.Width = 750;
            // 
            // frmBoKSkoppeling
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 720);
            this.ControlBox = false;
            this.Controls.Add(this.tabBoKS);
            this.Controls.Add(this.cmdAnnuleer);
            this.Controls.Add(this.cmdAccepteer);
            this.Controls.Add(this.grpOnderwijsdoel);
            this.MaximumSize = new System.Drawing.Size(900, 759);
            this.MinimumSize = new System.Drawing.Size(900, 759);
            this.Name = "frmBoKSkoppeling";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BoKS-koppeling";
            this.Load += new System.EventHandler(this.frmBoKSkoppeling_Load);
            this.grpOnderwijsdoel.ResumeLayout(false);
            this.grpOnderwijsdoel.PerformLayout();
            this.tabBoKS.ResumeLayout(false);
            this.tabET.ResumeLayout(false);
            this.tabTI.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpOnderwijsdoel;
        private System.Windows.Forms.TextBox txtOnderwijsdoel;
        private System.Windows.Forms.Button cmdAnnuleer;
        private System.Windows.Forms.Button cmdAccepteer;
        private System.Windows.Forms.TabControl tabBoKS;
        private System.Windows.Forms.TabPage tabET;
        private System.Windows.Forms.TabPage tabTI;
        private System.Windows.Forms.ListView lvwETBoKSitem;
        private System.Windows.Forms.ColumnHeader hdrET;
        private System.Windows.Forms.ColumnHeader hdrETBoKS;
        private System.Windows.Forms.ColumnHeader hdrETCategorie;
        private System.Windows.Forms.ColumnHeader hdrETItem;
        private System.Windows.Forms.ColumnHeader hdrETItemnummer;
        private System.Windows.Forms.ListView lvwTIBoKSitem;
        private System.Windows.Forms.ColumnHeader hdrTI;
        private System.Windows.Forms.ColumnHeader hdrTIBoKS;
        private System.Windows.Forms.ColumnHeader hdrTICategorie;
        private System.Windows.Forms.ColumnHeader hdrTIItemnummer;
        private System.Windows.Forms.ColumnHeader hdrTIItem;
        private System.Windows.Forms.ToolTip toolTip;
    }
}