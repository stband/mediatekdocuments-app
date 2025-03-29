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
        private System.Windows.Forms.Label lblWelcome;

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
            panelMenu = new Panel();
            btnAbonnements = new Button();
            btnCommandes = new Button();
            btnDocuments = new Button();
            lblWelcome = new Label();
            panelMenu.SuspendLayout();
            SuspendLayout();
            // 
            // panelMenu
            // 
            panelMenu.BackColor = Color.FromArgb(50, 50, 70);
            panelMenu.Controls.Add(btnAbonnements);
            panelMenu.Controls.Add(btnCommandes);
            panelMenu.Controls.Add(btnDocuments);
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Location = new Point(0, 0);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(348, 723);
            panelMenu.TabIndex = 1;
            // 
            // btnAbonnements
            // 
            btnAbonnements.Dock = DockStyle.Top;
            btnAbonnements.FlatStyle = FlatStyle.Flat;
            btnAbonnements.ForeColor = Color.White;
            btnAbonnements.Location = new Point(0, 120);
            btnAbonnements.Name = "btnAbonnements";
            btnAbonnements.Size = new Size(348, 60);
            btnAbonnements.TabIndex = 0;
            btnAbonnements.Text = "Gestion des abonnements";
            btnAbonnements.Click += btnAbonnements_Click;
            // 
            // btnCommandes
            // 
            btnCommandes.Dock = DockStyle.Top;
            btnCommandes.FlatStyle = FlatStyle.Flat;
            btnCommandes.ForeColor = Color.White;
            btnCommandes.Location = new Point(0, 60);
            btnCommandes.Name = "btnCommandes";
            btnCommandes.Size = new Size(348, 60);
            btnCommandes.TabIndex = 1;
            btnCommandes.Text = "Gestion des commandes";
            btnCommandes.Click += btnCommandes_Click;
            // 
            // btnDocuments
            // 
            btnDocuments.Dock = DockStyle.Top;
            btnDocuments.FlatStyle = FlatStyle.Flat;
            btnDocuments.ForeColor = Color.White;
            btnDocuments.Location = new Point(0, 0);
            btnDocuments.Name = "btnDocuments";
            btnDocuments.Size = new Size(348, 60);
            btnDocuments.TabIndex = 2;
            btnDocuments.Text = "Gestion des documents";
            btnDocuments.Click += btnDocuments_Click;
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblWelcome.Location = new Point(372, 9);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(1048, 59);
            lblWelcome.TabIndex = 0;
            lblWelcome.Text = "Bienvenue dans l'application Mediatek Documents";
            // 
            // FrmAccueil
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1462, 723);
            Controls.Add(lblWelcome);
            Controls.Add(panelMenu);
            Name = "FrmAccueil";
            Text = "Accueil - Mediatek Documents";
            panelMenu.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
