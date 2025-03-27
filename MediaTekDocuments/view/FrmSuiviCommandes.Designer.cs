namespace MediaTekDocuments.view
{
    partial class FrmSuiviCommandes
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        private void InitializeComponent()
        {
            tabCommandes = new TabControl();
            tabToutesCommandes = new TabPage();
            lblIDCommandeSelectionne = new Label();
            lblCommandeSelectionne = new Label();
            gbSupprimerCommande = new GroupBox();
            btnSupprimerCommande = new Button();
            gbModifierCommande = new GroupBox();
            cbStatutCommande = new ComboBox();
            btnModifierStatut = new Button();
            lblResultat = new Label();
            txtRecherche1 = new TextBox();
            btnClear1 = new Button();
            dgvToutesCommandes = new DataGridView();
            tabCommandesLivres = new TabPage();
            txtRecherche2 = new TextBox();
            btnClear2 = new Button();
            dgvCommandesLivre = new DataGridView();
            lblNouvelleCommande2 = new Label();
            gbNouvelleCommandeLivres = new GroupBox();
            lblID = new Label();
            lblIdDuLivre = new Label();
            lblMontant = new Label();
            txtMontant = new TextBox();
            lblExemplaires = new Label();
            nudNbExemplaires = new NumericUpDown();
            btnCommander1 = new Button();
            tabCommandesDVD = new TabPage();
            txtRecherche3 = new TextBox();
            btnClear3 = new Button();
            dgvCommandesDVD = new DataGridView();
            lblNouvelleCommande3 = new Label();
            gbNouvelleCommandeDVD = new GroupBox();
            lblID_DVD = new Label();
            lblIdDuDVD = new Label();
            lblMontant_DVD = new Label();
            txtMontantDVD = new TextBox();
            lblExemplaires_DVD = new Label();
            nudNbExemplairesDVD = new NumericUpDown();
            btnCommanderDVD = new Button();
            tabCommandes.SuspendLayout();
            tabToutesCommandes.SuspendLayout();
            gbSupprimerCommande.SuspendLayout();
            gbModifierCommande.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvToutesCommandes).BeginInit();
            tabCommandesLivres.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCommandesLivre).BeginInit();
            gbNouvelleCommandeLivres.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudNbExemplaires).BeginInit();
            tabCommandesDVD.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCommandesDVD).BeginInit();
            gbNouvelleCommandeDVD.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudNbExemplairesDVD).BeginInit();
            SuspendLayout();
            // 
            // tabCommandes
            // 
            tabCommandes.Controls.Add(tabToutesCommandes);
            tabCommandes.Controls.Add(tabCommandesLivres);
            tabCommandes.Controls.Add(tabCommandesDVD);
            tabCommandes.Location = new Point(0, 0);
            tabCommandes.Margin = new Padding(5);
            tabCommandes.Name = "tabCommandes";
            tabCommandes.SelectedIndex = 0;
            tabCommandes.Size = new Size(1462, 1040);
            tabCommandes.TabIndex = 0;
            tabCommandes.SelectedIndexChanged += tabCommandes_SelectedIndexChanged;
            // 
            // tabToutesCommandes
            // 
            tabToutesCommandes.Controls.Add(lblIDCommandeSelectionne);
            tabToutesCommandes.Controls.Add(lblCommandeSelectionne);
            tabToutesCommandes.Controls.Add(gbSupprimerCommande);
            tabToutesCommandes.Controls.Add(gbModifierCommande);
            tabToutesCommandes.Controls.Add(lblResultat);
            tabToutesCommandes.Controls.Add(txtRecherche1);
            tabToutesCommandes.Controls.Add(btnClear1);
            tabToutesCommandes.Controls.Add(dgvToutesCommandes);
            tabToutesCommandes.Location = new Point(8, 46);
            tabToutesCommandes.Margin = new Padding(5);
            tabToutesCommandes.Name = "tabToutesCommandes";
            tabToutesCommandes.Padding = new Padding(5);
            tabToutesCommandes.Size = new Size(1446, 986);
            tabToutesCommandes.TabIndex = 0;
            tabToutesCommandes.Text = "Toutes les commandes";
            tabToutesCommandes.UseVisualStyleBackColor = true;
            // 
            // lblIDCommandeSelectionne
            // 
            lblIDCommandeSelectionne.AutoSize = true;
            lblIDCommandeSelectionne.ForeColor = Color.Blue;
            lblIDCommandeSelectionne.Location = new Point(317, 798);
            lblIDCommandeSelectionne.Name = "lblIDCommandeSelectionne";
            lblIDCommandeSelectionne.Size = new Size(0, 32);
            lblIDCommandeSelectionne.TabIndex = 9;
            // 
            // lblCommandeSelectionne
            // 
            lblCommandeSelectionne.AutoSize = true;
            lblCommandeSelectionne.Location = new Point(32, 798);
            lblCommandeSelectionne.Name = "lblCommandeSelectionne";
            lblCommandeSelectionne.Size = new Size(292, 32);
            lblCommandeSelectionne.TabIndex = 8;
            lblCommandeSelectionne.Text = "Commande selectionnée :";
            // 
            // gbSupprimerCommande
            // 
            gbSupprimerCommande.Controls.Add(btnSupprimerCommande);
            gbSupprimerCommande.Location = new Point(1013, 798);
            gbSupprimerCommande.Name = "gbSupprimerCommande";
            gbSupprimerCommande.Size = new Size(400, 135);
            gbSupprimerCommande.TabIndex = 7;
            gbSupprimerCommande.TabStop = false;
            gbSupprimerCommande.Text = "Supprimer la commande";
            // 
            // btnSupprimerCommande
            // 
            btnSupprimerCommande.Location = new Point(15, 52);
            btnSupprimerCommande.Name = "btnSupprimerCommande";
            btnSupprimerCommande.Size = new Size(141, 39);
            btnSupprimerCommande.TabIndex = 5;
            btnSupprimerCommande.Text = "Supprimer";
            btnSupprimerCommande.UseVisualStyleBackColor = true;
            btnSupprimerCommande.Click += btnSupprimerCommande_Click;
            // 
            // gbModifierCommande
            // 
            gbModifierCommande.Controls.Add(cbStatutCommande);
            gbModifierCommande.Controls.Add(btnModifierStatut);
            gbModifierCommande.Location = new Point(566, 798);
            gbModifierCommande.Name = "gbModifierCommande";
            gbModifierCommande.Size = new Size(429, 135);
            gbModifierCommande.TabIndex = 6;
            gbModifierCommande.TabStop = false;
            gbModifierCommande.Text = "Modifier le statut de la commande";
            // 
            // cbStatutCommande
            // 
            cbStatutCommande.FormattingEnabled = true;
            cbStatutCommande.Items.AddRange(new object[] { "en cours", "relancée", "livrée", "réglée" });
            cbStatutCommande.Location = new Point(13, 51);
            cbStatutCommande.Name = "cbStatutCommande";
            cbStatutCommande.Size = new Size(242, 40);
            cbStatutCommande.TabIndex = 7;
            // 
            // btnModifierStatut
            // 
            btnModifierStatut.Location = new Point(270, 51);
            btnModifierStatut.Name = "btnModifierStatut";
            btnModifierStatut.Size = new Size(141, 39);
            btnModifierStatut.TabIndex = 6;
            btnModifierStatut.Text = "Modifier";
            btnModifierStatut.UseVisualStyleBackColor = true;
            btnModifierStatut.Click += btnModifierStatut_Click;
            // 
            // lblResultat
            // 
            lblResultat.AutoSize = true;
            lblResultat.Location = new Point(37, 72);
            lblResultat.Name = "lblResultat";
            lblResultat.Size = new Size(0, 32);
            lblResultat.TabIndex = 4;
            // 
            // txtRecherche1
            // 
            txtRecherche1.ForeColor = Color.Gray;
            txtRecherche1.Location = new Point(32, 26);
            txtRecherche1.Margin = new Padding(5);
            txtRecherche1.Name = "txtRecherche1";
            txtRecherche1.Size = new Size(1271, 39);
            txtRecherche1.TabIndex = 0;
            txtRecherche1.Text = "Rechercher";
            txtRecherche1.TextChanged += txtRecherche1_TextChanged;
            txtRecherche1.Enter += txtRecherche1_Enter;
            txtRecherche1.Leave += txtRecherche1_Leave;
            // 
            // btnClear1
            // 
            btnClear1.Location = new Point(1313, 26);
            btnClear1.Margin = new Padding(5);
            btnClear1.Name = "btnClear1";
            btnClear1.Size = new Size(100, 39);
            btnClear1.TabIndex = 1;
            btnClear1.Text = "Effacer";
            btnClear1.UseVisualStyleBackColor = true;
            btnClear1.Click += btnClear1_Click;
            // 
            // dgvToutesCommandes
            // 
            dgvToutesCommandes.AllowUserToAddRows = false;
            dgvToutesCommandes.AllowUserToOrderColumns = true;
            dgvToutesCommandes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvToutesCommandes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvToutesCommandes.Location = new Point(32, 109);
            dgvToutesCommandes.Margin = new Padding(5);
            dgvToutesCommandes.Name = "dgvToutesCommandes";
            dgvToutesCommandes.ReadOnly = true;
            dgvToutesCommandes.RowHeadersWidth = 51;
            dgvToutesCommandes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvToutesCommandes.Size = new Size(1381, 670);
            dgvToutesCommandes.TabIndex = 3;
            dgvToutesCommandes.CellClick += dgvToutesCommandes_CellClick;
            dgvToutesCommandes.ColumnHeaderMouseClick += dgvToutesCommandes_ColumnHeaderMouseClick;
            // 
            // tabCommandesLivres
            // 
            tabCommandesLivres.Controls.Add(txtRecherche2);
            tabCommandesLivres.Controls.Add(btnClear2);
            tabCommandesLivres.Controls.Add(dgvCommandesLivre);
            tabCommandesLivres.Controls.Add(lblNouvelleCommande2);
            tabCommandesLivres.Controls.Add(gbNouvelleCommandeLivres);
            tabCommandesLivres.Location = new Point(8, 46);
            tabCommandesLivres.Name = "tabCommandesLivres";
            tabCommandesLivres.Padding = new Padding(5);
            tabCommandesLivres.Size = new Size(1446, 986);
            tabCommandesLivres.TabIndex = 1;
            tabCommandesLivres.Text = "Commandes Livres";
            tabCommandesLivres.UseVisualStyleBackColor = true;
            // 
            // txtRecherche2
            // 
            txtRecherche2.ForeColor = Color.Gray;
            txtRecherche2.Location = new Point(32, 31);
            txtRecherche2.Name = "txtRecherche2";
            txtRecherche2.Size = new Size(1277, 39);
            txtRecherche2.TabIndex = 1;
            txtRecherche2.Text = "Rechercher";
            txtRecherche2.TextChanged += txtRecherche2_TextChanged;
            txtRecherche2.Enter += txtRecherche2_Enter;
            txtRecherche2.Leave += txtRecherche2_Leave;
            // 
            // btnClear2
            // 
            btnClear2.Location = new Point(1318, 29);
            btnClear2.Name = "btnClear2";
            btnClear2.Size = new Size(108, 41);
            btnClear2.TabIndex = 2;
            btnClear2.Text = "Effacer";
            btnClear2.UseVisualStyleBackColor = true;
            btnClear2.Click += btnClear2_Click;
            // 
            // dgvCommandesLivre
            // 
            dgvCommandesLivre.AllowUserToAddRows = false;
            dgvCommandesLivre.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCommandesLivre.ColumnHeadersHeight = 46;
            dgvCommandesLivre.Location = new Point(32, 89);
            dgvCommandesLivre.Name = "dgvCommandesLivre";
            dgvCommandesLivre.ReadOnly = true;
            dgvCommandesLivre.RowHeadersWidth = 82;
            dgvCommandesLivre.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCommandesLivre.Size = new Size(1394, 562);
            dgvCommandesLivre.TabIndex = 3;
            dgvCommandesLivre.CellContentClick += dgvCommandesLivre_CellClick;
            dgvCommandesLivre.ColumnHeaderMouseClick += dgvCommandesLivre_ColumnHeaderMouseClick;
            // 
            // lblNouvelleCommande2
            // 
            lblNouvelleCommande2.AutoSize = true;
            lblNouvelleCommande2.Location = new Point(32, 680);
            lblNouvelleCommande2.Name = "lblNouvelleCommande2";
            lblNouvelleCommande2.Size = new Size(0, 32);
            lblNouvelleCommande2.TabIndex = 4;
            // 
            // gbNouvelleCommandeLivres
            // 
            gbNouvelleCommandeLivres.Controls.Add(lblID);
            gbNouvelleCommandeLivres.Controls.Add(lblIdDuLivre);
            gbNouvelleCommandeLivres.Controls.Add(lblMontant);
            gbNouvelleCommandeLivres.Controls.Add(txtMontant);
            gbNouvelleCommandeLivres.Controls.Add(lblExemplaires);
            gbNouvelleCommandeLivres.Controls.Add(nudNbExemplaires);
            gbNouvelleCommandeLivres.Controls.Add(btnCommander1);
            gbNouvelleCommandeLivres.Location = new Point(7, 666);
            gbNouvelleCommandeLivres.Name = "gbNouvelleCommandeLivres";
            gbNouvelleCommandeLivres.Size = new Size(393, 275);
            gbNouvelleCommandeLivres.TabIndex = 5;
            gbNouvelleCommandeLivres.TabStop = false;
            gbNouvelleCommandeLivres.Text = "Nouvelle commande";
            // 
            // lblID
            // 
            lblID.AutoSize = true;
            lblID.ForeColor = Color.Blue;
            lblID.Location = new Point(156, 49);
            lblID.Name = "lblID";
            lblID.Size = new Size(79, 32);
            lblID.TabIndex = 6;
            lblID.Text = "00000";
            // 
            // lblIdDuLivre
            // 
            lblIdDuLivre.AutoSize = true;
            lblIdDuLivre.Location = new Point(6, 49);
            lblIdDuLivre.Name = "lblIdDuLivre";
            lblIdDuLivre.Size = new Size(124, 32);
            lblIdDuLivre.TabIndex = 5;
            lblIdDuLivre.Text = "ID du livre";
            // 
            // lblMontant
            // 
            lblMontant.Location = new Point(6, 100);
            lblMontant.Name = "lblMontant";
            lblMontant.Size = new Size(110, 30);
            lblMontant.TabIndex = 0;
            lblMontant.Text = "Montant :";
            // 
            // txtMontant
            // 
            txtMontant.Location = new Point(156, 100);
            txtMontant.Name = "txtMontant";
            txtMontant.Size = new Size(200, 39);
            txtMontant.TabIndex = 1;
            // 
            // lblExemplaires
            // 
            lblExemplaires.Location = new Point(0, 159);
            lblExemplaires.Name = "lblExemplaires";
            lblExemplaires.Size = new Size(144, 30);
            lblExemplaires.TabIndex = 2;
            lblExemplaires.Text = "Exemplaires";
            // 
            // nudNbExemplaires
            // 
            nudNbExemplaires.Location = new Point(156, 157);
            nudNbExemplaires.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudNbExemplaires.Name = "nudNbExemplaires";
            nudNbExemplaires.Size = new Size(200, 39);
            nudNbExemplaires.TabIndex = 3;
            nudNbExemplaires.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnCommander1
            // 
            btnCommander1.Location = new Point(6, 227);
            btnCommander1.Name = "btnCommander1";
            btnCommander1.Size = new Size(350, 40);
            btnCommander1.TabIndex = 4;
            btnCommander1.Text = "Commander";
            btnCommander1.UseVisualStyleBackColor = true;
            btnCommander1.Click += btnCommander1_Click;
            // 
            // tabCommandesDVD
            // 
            tabCommandesDVD.Controls.Add(txtRecherche3);
            tabCommandesDVD.Controls.Add(btnClear3);
            tabCommandesDVD.Controls.Add(dgvCommandesDVD);
            tabCommandesDVD.Controls.Add(lblNouvelleCommande3);
            tabCommandesDVD.Controls.Add(gbNouvelleCommandeDVD);
            tabCommandesDVD.Location = new Point(8, 46);
            tabCommandesDVD.Name = "tabCommandesDVD";
            tabCommandesDVD.Padding = new Padding(5);
            tabCommandesDVD.Size = new Size(1446, 986);
            tabCommandesDVD.TabIndex = 2;
            tabCommandesDVD.Text = "Commandes DVD";
            tabCommandesDVD.UseVisualStyleBackColor = true;
            // 
            // txtRecherche3
            // 
            txtRecherche3.ForeColor = Color.Gray;
            txtRecherche3.Location = new Point(32, 31);
            txtRecherche3.Name = "txtRecherche3";
            txtRecherche3.Size = new Size(1277, 39);
            txtRecherche3.TabIndex = 1;
            txtRecherche3.Text = "Rechercher";
            txtRecherche3.TextChanged += txtRecherche3_TextChanged;
            txtRecherche3.Enter += txtRecherche3_Enter;
            txtRecherche3.Leave += txtRecherche3_Leave;
            // 
            // btnClear3
            // 
            btnClear3.Location = new Point(1318, 29);
            btnClear3.Name = "btnClear3";
            btnClear3.Size = new Size(108, 41);
            btnClear3.TabIndex = 2;
            btnClear3.Text = "Effacer";
            btnClear3.UseVisualStyleBackColor = true;
            btnClear3.Click += btnClear3_Click;
            // 
            // dgvCommandesDVD
            // 
            dgvCommandesDVD.AllowUserToAddRows = false;
            dgvCommandesDVD.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCommandesDVD.ColumnHeadersHeight = 46;
            dgvCommandesDVD.Location = new Point(32, 89);
            dgvCommandesDVD.Name = "dgvCommandesDVD";
            dgvCommandesDVD.ReadOnly = true;
            dgvCommandesDVD.RowHeadersWidth = 82;
            dgvCommandesDVD.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCommandesDVD.Size = new Size(1394, 562);
            dgvCommandesDVD.TabIndex = 3;
            dgvCommandesDVD.CellClick += dgvCommandesDVD_CellClick;
            dgvCommandesDVD.ColumnHeaderMouseClick += dgvCommandesDVD_ColumnHeaderMouseClick;
            // 
            // lblNouvelleCommande3
            // 
            lblNouvelleCommande3.AutoSize = true;
            lblNouvelleCommande3.Location = new Point(32, 680);
            lblNouvelleCommande3.Name = "lblNouvelleCommande3";
            lblNouvelleCommande3.Size = new Size(0, 32);
            lblNouvelleCommande3.TabIndex = 4;
            // 
            // gbNouvelleCommandeDVD
            // 
            gbNouvelleCommandeDVD.Controls.Add(lblID_DVD);
            gbNouvelleCommandeDVD.Controls.Add(lblIdDuDVD);
            gbNouvelleCommandeDVD.Controls.Add(lblMontant_DVD);
            gbNouvelleCommandeDVD.Controls.Add(txtMontantDVD);
            gbNouvelleCommandeDVD.Controls.Add(lblExemplaires_DVD);
            gbNouvelleCommandeDVD.Controls.Add(nudNbExemplairesDVD);
            gbNouvelleCommandeDVD.Controls.Add(btnCommanderDVD);
            gbNouvelleCommandeDVD.Location = new Point(7, 666);
            gbNouvelleCommandeDVD.Name = "gbNouvelleCommandeDVD";
            gbNouvelleCommandeDVD.Size = new Size(393, 281);
            gbNouvelleCommandeDVD.TabIndex = 5;
            gbNouvelleCommandeDVD.TabStop = false;
            gbNouvelleCommandeDVD.Text = "Nouvelle commande";
            // 
            // lblID_DVD
            // 
            lblID_DVD.AutoSize = true;
            lblID_DVD.ForeColor = Color.Blue;
            lblID_DVD.Location = new Point(156, 49);
            lblID_DVD.Name = "lblID_DVD";
            lblID_DVD.Size = new Size(79, 32);
            lblID_DVD.TabIndex = 6;
            lblID_DVD.Text = "00000";
            // 
            // lblIdDuDVD
            // 
            lblIdDuDVD.AutoSize = true;
            lblIdDuDVD.Location = new Point(6, 49);
            lblIdDuDVD.Name = "lblIdDuDVD";
            lblIdDuDVD.Size = new Size(127, 32);
            lblIdDuDVD.TabIndex = 5;
            lblIdDuDVD.Text = "ID du DVD";
            // 
            // lblMontant_DVD
            // 
            lblMontant_DVD.Location = new Point(6, 100);
            lblMontant_DVD.Name = "lblMontant_DVD";
            lblMontant_DVD.Size = new Size(110, 30);
            lblMontant_DVD.TabIndex = 0;
            lblMontant_DVD.Text = "Montant :";
            // 
            // txtMontantDVD
            // 
            txtMontantDVD.Location = new Point(156, 100);
            txtMontantDVD.Name = "txtMontantDVD";
            txtMontantDVD.Size = new Size(200, 39);
            txtMontantDVD.TabIndex = 1;
            // 
            // lblExemplaires_DVD
            // 
            lblExemplaires_DVD.Location = new Point(0, 159);
            lblExemplaires_DVD.Name = "lblExemplaires_DVD";
            lblExemplaires_DVD.Size = new Size(144, 30);
            lblExemplaires_DVD.TabIndex = 2;
            lblExemplaires_DVD.Text = "Exemplaires";
            // 
            // nudNbExemplairesDVD
            // 
            nudNbExemplairesDVD.Location = new Point(156, 157);
            nudNbExemplairesDVD.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudNbExemplairesDVD.Name = "nudNbExemplairesDVD";
            nudNbExemplairesDVD.Size = new Size(200, 39);
            nudNbExemplairesDVD.TabIndex = 3;
            nudNbExemplairesDVD.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnCommanderDVD
            // 
            btnCommanderDVD.Location = new Point(6, 227);
            btnCommanderDVD.Name = "btnCommanderDVD";
            btnCommanderDVD.Size = new Size(350, 40);
            btnCommanderDVD.TabIndex = 4;
            btnCommanderDVD.Text = "Commander";
            btnCommanderDVD.UseVisualStyleBackColor = true;
            btnCommanderDVD.Click += btnCommanderDVD_Click;
            // 
            // FrmSuiviCommandes
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1462, 991);
            Controls.Add(tabCommandes);
            Margin = new Padding(5);
            Name = "FrmSuiviCommandes";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Suivi des commandes";
            Load += FrmSuiviCommandes_Load;
            tabCommandes.ResumeLayout(false);
            tabToutesCommandes.ResumeLayout(false);
            tabToutesCommandes.PerformLayout();
            gbSupprimerCommande.ResumeLayout(false);
            gbModifierCommande.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvToutesCommandes).EndInit();
            tabCommandesLivres.ResumeLayout(false);
            tabCommandesLivres.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCommandesLivre).EndInit();
            gbNouvelleCommandeLivres.ResumeLayout(false);
            gbNouvelleCommandeLivres.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudNbExemplaires).EndInit();
            tabCommandesDVD.ResumeLayout(false);
            tabCommandesDVD.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCommandesDVD).EndInit();
            gbNouvelleCommandeDVD.ResumeLayout(false);
            gbNouvelleCommandeDVD.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudNbExemplairesDVD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabCommandes;
        private System.Windows.Forms.TabPage tabToutesCommandes;
        private System.Windows.Forms.TabPage tabCommandesLivres;
        private System.Windows.Forms.TabPage tabCommandesDVD;
        private System.Windows.Forms.TextBox txtRecherche1;
        private System.Windows.Forms.Button btnClear1;
        private System.Windows.Forms.DataGridView dgvToutesCommandes;
        private System.Windows.Forms.TextBox txtRecherche2;
        private System.Windows.Forms.Button btnClear2;
        private System.Windows.Forms.DataGridView dgvCommandesLivre;
        private System.Windows.Forms.Label lblResultat;
        private System.Windows.Forms.Label lblNouvelleCommande2;
        private System.Windows.Forms.GroupBox gbNouvelleCommandeLivres;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Label lblIdDuLivre;
        private System.Windows.Forms.Label lblMontant;
        private System.Windows.Forms.TextBox txtMontant;
        private System.Windows.Forms.Label lblExemplaires;
        private System.Windows.Forms.NumericUpDown nudNbExemplaires;
        private System.Windows.Forms.Button btnCommander1;
        private System.Windows.Forms.TextBox txtRecherche3;
        private System.Windows.Forms.Button btnClear3;
        private System.Windows.Forms.DataGridView dgvCommandesDVD;
        private System.Windows.Forms.Label lblNouvelleCommande3;
        private System.Windows.Forms.GroupBox gbNouvelleCommandeDVD;
        private System.Windows.Forms.Label lblID_DVD;
        private System.Windows.Forms.Label lblIdDuDVD;
        private System.Windows.Forms.Label lblMontant_DVD;
        private System.Windows.Forms.TextBox txtMontantDVD;
        private System.Windows.Forms.Label lblExemplaires_DVD;
        private System.Windows.Forms.NumericUpDown nudNbExemplairesDVD;
        private System.Windows.Forms.Button btnCommanderDVD;
        private Button btnSupprimerCommande;
        private GroupBox gbModifierCommande;
        private Button btnModifierStatut;
        private GroupBox gbSupprimerCommande;
        private ComboBox cbStatutCommande;
        private Label lblCommandeSelectionne;
        private Label lblIDCommandeSelectionne;
    }
}
