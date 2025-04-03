namespace MediaTekDocuments.view
{
    partial class FrmAccueil
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Button btnDocuments;
        private System.Windows.Forms.Button btnCommandes;
        private System.Windows.Forms.Button btnAbonnements;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAccueil));
            panelMenu = new Panel();
            btnAbonnements = new Button();
            btnCommandes = new Button();
            btnDocuments = new Button();
            lblBienvenue = new Label();
            pbLogout = new PictureBox();
            pbBanniere = new PictureBox();
            panelMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbLogout).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbBanniere).BeginInit();
            SuspendLayout();
            // 
            // panelMenu
            // 
            panelMenu.BackColor = Color.FromArgb(19, 68, 98);
            panelMenu.Controls.Add(btnAbonnements);
            panelMenu.Controls.Add(btnCommandes);
            panelMenu.Controls.Add(btnDocuments);
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Location = new Point(0, 0);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(328, 723);
            panelMenu.TabIndex = 1;
            // 
            // btnAbonnements
            // 
            btnAbonnements.BackColor = Color.FromArgb(16, 57, 82);
            btnAbonnements.Dock = DockStyle.Top;
            btnAbonnements.FlatStyle = FlatStyle.Flat;
            btnAbonnements.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAbonnements.ForeColor = Color.WhiteSmoke;
            btnAbonnements.Location = new Point(0, 120);
            btnAbonnements.Name = "btnAbonnements";
            btnAbonnements.Size = new Size(328, 60);
            btnAbonnements.TabIndex = 0;
            btnAbonnements.Text = "Gestion des abonnements";
            btnAbonnements.UseVisualStyleBackColor = false;
            btnAbonnements.Click += btnAbonnements_Click;
            // 
            // btnCommandes
            // 
            btnCommandes.BackColor = Color.FromArgb(16, 57, 82);
            btnCommandes.Dock = DockStyle.Top;
            btnCommandes.FlatStyle = FlatStyle.Flat;
            btnCommandes.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCommandes.ForeColor = Color.WhiteSmoke;
            btnCommandes.Location = new Point(0, 60);
            btnCommandes.Name = "btnCommandes";
            btnCommandes.Size = new Size(328, 60);
            btnCommandes.TabIndex = 1;
            btnCommandes.Text = "Gestion des commandes";
            btnCommandes.UseVisualStyleBackColor = false;
            btnCommandes.Click += btnCommandes_Click;
            // 
            // btnDocuments
            // 
            btnDocuments.BackColor = Color.FromArgb(16, 57, 82);
            btnDocuments.Dock = DockStyle.Top;
            btnDocuments.FlatStyle = FlatStyle.Flat;
            btnDocuments.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDocuments.ForeColor = Color.WhiteSmoke;
            btnDocuments.Location = new Point(0, 0);
            btnDocuments.Name = "btnDocuments";
            btnDocuments.Size = new Size(328, 60);
            btnDocuments.TabIndex = 2;
            btnDocuments.Text = "Gestion des documents";
            btnDocuments.UseVisualStyleBackColor = false;
            btnDocuments.Click += btnDocuments_Click;
            // 
            // lblBienvenue
            // 
            lblBienvenue.AutoSize = true;
            lblBienvenue.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblBienvenue.ForeColor = Color.ForestGreen;
            lblBienvenue.Location = new Point(334, 22);
            lblBienvenue.Name = "lblBienvenue";
            lblBienvenue.Size = new Size(0, 32);
            lblBienvenue.TabIndex = 2;
            // 
            // pbLogout
            // 
            pbLogout.Image = (Image)resources.GetObject("pbLogout.Image");
            pbLogout.Location = new Point(1401, 22);
            pbLogout.Name = "pbLogout";
            pbLogout.Size = new Size(39, 46);
            pbLogout.SizeMode = PictureBoxSizeMode.Zoom;
            pbLogout.TabIndex = 3;
            pbLogout.TabStop = false;
            pbLogout.Click += pbLogout_Click;
            // 
            // pbBanniere
            // 
            pbBanniere.BackColor = Color.WhiteSmoke;
            pbBanniere.Image = (Image)resources.GetObject("pbBanniere.Image");
            pbBanniere.Location = new Point(315, 234);
            pbBanniere.Name = "pbBanniere";
            pbBanniere.Size = new Size(1168, 679);
            pbBanniere.SizeMode = PictureBoxSizeMode.StretchImage;
            pbBanniere.TabIndex = 4;
            pbBanniere.TabStop = false;
            // 
            // FrmAccueil
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(250, 250, 250);
            ClientSize = new Size(1462, 723);
            Controls.Add(lblBienvenue);
            Controls.Add(pbLogout);
            Controls.Add(panelMenu);
            Controls.Add(pbBanniere);
            Name = "FrmAccueil";
            Text = "Accueil - Mediatek Documents";
            panelMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbLogout).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbBanniere).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblBienvenue;
        private PictureBox pbLogout;
        private PictureBox pbBanniere;
    }
}
