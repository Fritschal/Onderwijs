
namespace Onderwijs
{
    partial class frmBeroepsproductkoppeling
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
            this.grpOnderwijsdoel = new System.Windows.Forms.GroupBox();
            this.txtOnderwijsdoel = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lvwBeroepsproducten = new System.Windows.Forms.ListView();
            this.hdrSelectie = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.hdrAfkorting = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.hdrBeroepsproduct = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.hdrOmschrijving = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmdAnnuleer = new System.Windows.Forms.Button();
            this.cmdAccepteer = new System.Windows.Forms.Button();
            this.grpOnderwijsdoel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpOnderwijsdoel
            // 
            this.grpOnderwijsdoel.Controls.Add(this.txtOnderwijsdoel);
            this.grpOnderwijsdoel.Location = new System.Drawing.Point(11, 2);
            this.grpOnderwijsdoel.Name = "grpOnderwijsdoel";
            this.grpOnderwijsdoel.Size = new System.Drawing.Size(606, 86);
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
            this.txtOnderwijsdoel.Size = new System.Drawing.Size(594, 61);
            this.txtOnderwijsdoel.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lvwBeroepsproducten);
            this.groupBox1.Location = new System.Drawing.Point(11, 94);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(606, 301);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "De volgende beroepsproducten zijn van toepassing:";
            // 
            // lvwBeroepsproducten
            // 
            this.lvwBeroepsproducten.CheckBoxes = true;
            this.lvwBeroepsproducten.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.hdrSelectie,
            this.hdrAfkorting,
            this.hdrBeroepsproduct,
            this.hdrOmschrijving});
            this.lvwBeroepsproducten.FullRowSelect = true;
            this.lvwBeroepsproducten.GridLines = true;
            this.lvwBeroepsproducten.HideSelection = false;
            this.lvwBeroepsproducten.Location = new System.Drawing.Point(6, 19);
            this.lvwBeroepsproducten.MultiSelect = false;
            this.lvwBeroepsproducten.Name = "lvwBeroepsproducten";
            this.lvwBeroepsproducten.Size = new System.Drawing.Size(594, 275);
            this.lvwBeroepsproducten.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvwBeroepsproducten.TabIndex = 9;
            this.lvwBeroepsproducten.UseCompatibleStateImageBehavior = false;
            this.lvwBeroepsproducten.View = System.Windows.Forms.View.Details;
            this.lvwBeroepsproducten.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvwBeroepsproducten_ItemCheck);
            // 
            // hdrSelectie
            // 
            this.hdrSelectie.Text = "Selectie";
            this.hdrSelectie.Width = 50;
            // 
            // hdrAfkorting
            // 
            this.hdrAfkorting.Text = "BP";
            this.hdrAfkorting.Width = 50;
            // 
            // hdrBeroepsproduct
            // 
            this.hdrBeroepsproduct.Text = "Beroepsproduct";
            this.hdrBeroepsproduct.Width = 150;
            // 
            // hdrOmschrijving
            // 
            this.hdrOmschrijving.Text = "Omschrijving";
            this.hdrOmschrijving.Width = 400;
            // 
            // cmdAnnuleer
            // 
            this.cmdAnnuleer.Location = new System.Drawing.Point(401, 401);
            this.cmdAnnuleer.Name = "cmdAnnuleer";
            this.cmdAnnuleer.Size = new System.Drawing.Size(105, 30);
            this.cmdAnnuleer.TabIndex = 10;
            this.cmdAnnuleer.Text = "Annuleren";
            this.cmdAnnuleer.UseVisualStyleBackColor = true;
            this.cmdAnnuleer.Click += new System.EventHandler(this.cmdAnnuleer_Click);
            // 
            // cmdAccepteer
            // 
            this.cmdAccepteer.Location = new System.Drawing.Point(512, 401);
            this.cmdAccepteer.Name = "cmdAccepteer";
            this.cmdAccepteer.Size = new System.Drawing.Size(105, 30);
            this.cmdAccepteer.TabIndex = 11;
            this.cmdAccepteer.Text = "Opslaan";
            this.cmdAccepteer.UseVisualStyleBackColor = true;
            this.cmdAccepteer.Click += new System.EventHandler(this.cmdAccepteer_Click);
            // 
            // frmBeroepsproductkoppeling
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 440);
            this.Controls.Add(this.cmdAnnuleer);
            this.Controls.Add(this.cmdAccepteer);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpOnderwijsdoel);
            this.Name = "frmBeroepsproductkoppeling";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Koppeling beroepsproducten";
            this.Load += new System.EventHandler(this.frmBeroepsproductkoppeling_Load);
            this.grpOnderwijsdoel.ResumeLayout(false);
            this.grpOnderwijsdoel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpOnderwijsdoel;
        private System.Windows.Forms.TextBox txtOnderwijsdoel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView lvwBeroepsproducten;
        private System.Windows.Forms.ColumnHeader hdrSelectie;
        private System.Windows.Forms.ColumnHeader hdrBeroepsproduct;
        private System.Windows.Forms.ColumnHeader hdrOmschrijving;
        private System.Windows.Forms.ColumnHeader hdrAfkorting;
        private System.Windows.Forms.Button cmdAnnuleer;
        private System.Windows.Forms.Button cmdAccepteer;
    }
}