namespace MediaTekDocuments.view
{
    partial class FrmAbonnementRevues
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
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            tabGestionAbonnements = new TabPage();
            gbSupprimerAbonnement = new GroupBox();
            btnSupprimerAbonnement = new Button();
            gbRenouvelerAbonnement = new GroupBox();
            dtpDateDebutRenouvellement = new DateTimePicker();
            lblDateDebutRenouvellement = new Label();
            lblAbonnementID = new Label();
            lbNAbonnement = new Label();
            lblDateFinRenouvellement = new Label();
            dtpDateFinRenouvellement = new DateTimePicker();
            btnRenouvelerAbonnement = new Button();
            txtRecherche = new TextBox();
            btnClearRecherche = new Button();
            dgvAbonnements = new DataGridView();
            tabAbonnements = new TabControl();
            tabGestionRevues = new TabPage();
            gbSouscrireAbonnement = new GroupBox();
            lblRevue2 = new Label();
            cbRevue = new ComboBox();
            lblDateDebutSouscription = new Label();
            dtpDateDebutSouscription = new DateTimePicker();
            lblDateFinSouscription = new Label();
            dtpDateFinSouscription = new DateTimePicker();
            lblMontant = new Label();
            txtMontant = new TextBox();
            btnSouscrireAbonnement = new Button();
            dgvRevues = new DataGridView();
            btnClearRecherche2 = new Button();
            txtRechercheRevues = new TextBox();
            tabGestionAbonnements.SuspendLayout();
            gbSupprimerAbonnement.SuspendLayout();
            gbRenouvelerAbonnement.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAbonnements).BeginInit();
            tabAbonnements.SuspendLayout();
            tabGestionRevues.SuspendLayout();
            gbSouscrireAbonnement.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRevues).BeginInit();
            SuspendLayout();
            // 
            // tabGestionAbonnements
            // 
            tabGestionAbonnements.Controls.Add(gbSupprimerAbonnement);
            tabGestionAbonnements.Controls.Add(gbRenouvelerAbonnement);
            tabGestionAbonnements.Controls.Add(txtRecherche);
            tabGestionAbonnements.Controls.Add(btnClearRecherche);
            tabGestionAbonnements.Controls.Add(dgvAbonnements);
            tabGestionAbonnements.Location = new Point(8, 46);
            tabGestionAbonnements.Margin = new Padding(4);
            tabGestionAbonnements.Name = "tabGestionAbonnements";
            tabGestionAbonnements.Padding = new Padding(4);
            tabGestionAbonnements.Size = new Size(1284, 880);
            tabGestionAbonnements.TabIndex = 0;
            tabGestionAbonnements.Text = "Gestion des abonnements";
            tabGestionAbonnements.UseVisualStyleBackColor = true;
            // 
            // gbSupprimerAbonnement
            // 
            gbSupprimerAbonnement.Controls.Add(btnSupprimerAbonnement);
            gbSupprimerAbonnement.Location = new Point(939, 774);
            gbSupprimerAbonnement.Name = "gbSupprimerAbonnement";
            gbSupprimerAbonnement.Size = new Size(338, 105);
            gbSupprimerAbonnement.TabIndex = 11;
            gbSupprimerAbonnement.TabStop = false;
            gbSupprimerAbonnement.Text = "Supprimer l'abonnement";
            // 
            // btnSupprimerAbonnement
            // 
            btnSupprimerAbonnement.Location = new Point(18, 50);
            btnSupprimerAbonnement.Margin = new Padding(4);
            btnSupprimerAbonnement.Name = "btnSupprimerAbonnement";
            btnSupprimerAbonnement.Size = new Size(304, 39);
            btnSupprimerAbonnement.TabIndex = 10;
            btnSupprimerAbonnement.Text = "Supprimer";
            btnSupprimerAbonnement.UseVisualStyleBackColor = true;
            btnSupprimerAbonnement.Click += btnSupprimerAbonnement_Click;
            // 
            // gbRenouvelerAbonnement
            // 
            gbRenouvelerAbonnement.Controls.Add(dtpDateDebutRenouvellement);
            gbRenouvelerAbonnement.Controls.Add(lblDateDebutRenouvellement);
            gbRenouvelerAbonnement.Controls.Add(lblAbonnementID);
            gbRenouvelerAbonnement.Controls.Add(lbNAbonnement);
            gbRenouvelerAbonnement.Controls.Add(lblDateFinRenouvellement);
            gbRenouvelerAbonnement.Controls.Add(dtpDateFinRenouvellement);
            gbRenouvelerAbonnement.Controls.Add(btnRenouvelerAbonnement);
            gbRenouvelerAbonnement.Location = new Point(26, 636);
            gbRenouvelerAbonnement.Margin = new Padding(4);
            gbRenouvelerAbonnement.Name = "gbRenouvelerAbonnement";
            gbRenouvelerAbonnement.Padding = new Padding(4);
            gbRenouvelerAbonnement.Size = new Size(559, 243);
            gbRenouvelerAbonnement.TabIndex = 3;
            gbRenouvelerAbonnement.TabStop = false;
            gbRenouvelerAbonnement.Text = "Renouveler l'abonnement";
            // 
            // dtpDateDebutRenouvellement
            // 
            dtpDateDebutRenouvellement.Format = DateTimePickerFormat.Short;
            dtpDateDebutRenouvellement.Location = new Point(260, 87);
            dtpDateDebutRenouvellement.Margin = new Padding(4);
            dtpDateDebutRenouvellement.Name = "dtpDateDebutRenouvellement";
            dtpDateDebutRenouvellement.Size = new Size(275, 39);
            dtpDateDebutRenouvellement.TabIndex = 13;
            // 
            // lblDateDebutRenouvellement
            // 
            lblDateDebutRenouvellement.AutoSize = true;
            lblDateDebutRenouvellement.Location = new Point(27, 92);
            lblDateDebutRenouvellement.Margin = new Padding(4, 0, 4, 0);
            lblDateDebutRenouvellement.Name = "lblDateDebutRenouvellement";
            lblDateDebutRenouvellement.Size = new Size(146, 32);
            lblDateDebutRenouvellement.TabIndex = 12;
            lblDateDebutRenouvellement.Text = "Date début :";
            // 
            // lblAbonnementID
            // 
            lblAbonnementID.AutoSize = true;
            lblAbonnementID.Location = new Point(260, 41);
            lblAbonnementID.Margin = new Padding(4, 0, 4, 0);
            lblAbonnementID.Name = "lblAbonnementID";
            lblAbonnementID.Size = new Size(0, 32);
            lblAbonnementID.TabIndex = 11;
            // 
            // lbNAbonnement
            // 
            lbNAbonnement.AutoSize = true;
            lbNAbonnement.Location = new Point(27, 41);
            lbNAbonnement.Margin = new Padding(4, 0, 4, 0);
            lbNAbonnement.Name = "lbNAbonnement";
            lbNAbonnement.Size = new Size(214, 32);
            lbNAbonnement.TabIndex = 0;
            lbNAbonnement.Text = "N° de commande :";
            // 
            // lblDateFinRenouvellement
            // 
            lblDateFinRenouvellement.AutoSize = true;
            lblDateFinRenouvellement.Location = new Point(28, 142);
            lblDateFinRenouvellement.Margin = new Padding(4, 0, 4, 0);
            lblDateFinRenouvellement.Name = "lblDateFinRenouvellement";
            lblDateFinRenouvellement.Size = new Size(145, 32);
            lblDateFinRenouvellement.TabIndex = 4;
            lblDateFinRenouvellement.Text = "Date de fin :";
            // 
            // dtpDateFinRenouvellement
            // 
            dtpDateFinRenouvellement.Format = DateTimePickerFormat.Short;
            dtpDateFinRenouvellement.Location = new Point(260, 142);
            dtpDateFinRenouvellement.Margin = new Padding(4);
            dtpDateFinRenouvellement.Name = "dtpDateFinRenouvellement";
            dtpDateFinRenouvellement.Size = new Size(275, 39);
            dtpDateFinRenouvellement.TabIndex = 5;
            // 
            // btnRenouvelerAbonnement
            // 
            btnRenouvelerAbonnement.Location = new Point(27, 191);
            btnRenouvelerAbonnement.Margin = new Padding(4);
            btnRenouvelerAbonnement.Name = "btnRenouvelerAbonnement";
            btnRenouvelerAbonnement.Size = new Size(508, 39);
            btnRenouvelerAbonnement.TabIndex = 9;
            btnRenouvelerAbonnement.Text = "Renouveler";
            btnRenouvelerAbonnement.UseVisualStyleBackColor = true;
            btnRenouvelerAbonnement.Click += btnRenouvelerAbonnement_Click;
            // 
            // txtRecherche
            // 
            txtRecherche.ForeColor = Color.Gray;
            txtRecherche.Location = new Point(26, 25);
            txtRecherche.Margin = new Padding(4);
            txtRecherche.Name = "txtRecherche";
            txtRecherche.Size = new Size(1084, 39);
            txtRecherche.TabIndex = 0;
            txtRecherche.Text = "Rechercher";
            txtRecherche.TextChanged += txtRecherche_TextChanged;
            txtRecherche.Enter += txtRecherche_Enter;
            txtRecherche.Leave += txtRecherche_Leave;
            // 
            // btnClearRecherche
            // 
            btnClearRecherche.Location = new Point(1118, 25);
            btnClearRecherche.Margin = new Padding(4);
            btnClearRecherche.Name = "btnClearRecherche";
            btnClearRecherche.Size = new Size(143, 39);
            btnClearRecherche.TabIndex = 1;
            btnClearRecherche.Text = "Effacer";
            btnClearRecherche.UseVisualStyleBackColor = true;
            btnClearRecherche.Click += btnClearRecherche_Click;
            // 
            // dgvAbonnements
            // 
            dgvAbonnements.AllowUserToAddRows = false;
            dgvAbonnements.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAbonnements.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAbonnements.Location = new Point(26, 85);
            dgvAbonnements.Margin = new Padding(4);
            dgvAbonnements.Name = "dgvAbonnements";
            dgvAbonnements.ReadOnly = true;
            dgvAbonnements.RowHeadersWidth = 82;
            dgvAbonnements.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAbonnements.Size = new Size(1235, 538);
            dgvAbonnements.TabIndex = 2;
            dgvAbonnements.CellClick += dgvAbonnements_CellClick;
            dgvAbonnements.RowPrePaint += dgvAbonnements_RowPrePaint;
            // 
            // tabAbonnements
            // 
            tabAbonnements.Controls.Add(tabGestionAbonnements);
            tabAbonnements.Controls.Add(tabGestionRevues);
            tabAbonnements.Location = new Point(16, 15);
            tabAbonnements.Margin = new Padding(4);
            tabAbonnements.Name = "tabAbonnements";
            tabAbonnements.SelectedIndex = 0;
            tabAbonnements.Size = new Size(1300, 934);
            tabAbonnements.TabIndex = 0;
            tabAbonnements.SelectedIndexChanged += tabAbonnements_SelectedIndexChanged;
            // 
            // tabGestionRevues
            // 
            tabGestionRevues.Controls.Add(gbSouscrireAbonnement);
            tabGestionRevues.Controls.Add(dgvRevues);
            tabGestionRevues.Controls.Add(btnClearRecherche2);
            tabGestionRevues.Controls.Add(txtRechercheRevues);
            tabGestionRevues.Location = new Point(8, 46);
            tabGestionRevues.Name = "tabGestionRevues";
            tabGestionRevues.Padding = new Padding(3);
            tabGestionRevues.Size = new Size(1284, 880);
            tabGestionRevues.TabIndex = 1;
            tabGestionRevues.Text = "Gestion des revues";
            tabGestionRevues.UseVisualStyleBackColor = true;
            // 
            // gbSouscrireAbonnement
            // 
            gbSouscrireAbonnement.Controls.Add(lblRevue2);
            gbSouscrireAbonnement.Controls.Add(cbRevue);
            gbSouscrireAbonnement.Controls.Add(lblDateDebutSouscription);
            gbSouscrireAbonnement.Controls.Add(dtpDateDebutSouscription);
            gbSouscrireAbonnement.Controls.Add(lblDateFinSouscription);
            gbSouscrireAbonnement.Controls.Add(dtpDateFinSouscription);
            gbSouscrireAbonnement.Controls.Add(lblMontant);
            gbSouscrireAbonnement.Controls.Add(txtMontant);
            gbSouscrireAbonnement.Controls.Add(btnSouscrireAbonnement);
            gbSouscrireAbonnement.Location = new Point(23, 545);
            gbSouscrireAbonnement.Margin = new Padding(4);
            gbSouscrireAbonnement.Name = "gbSouscrireAbonnement";
            gbSouscrireAbonnement.Padding = new Padding(4);
            gbSouscrireAbonnement.Size = new Size(559, 331);
            gbSouscrireAbonnement.TabIndex = 4;
            gbSouscrireAbonnement.TabStop = false;
            gbSouscrireAbonnement.Text = "Souscrire à un nouvel abonnement";
            // 
            // lblRevue2
            // 
            lblRevue2.AutoSize = true;
            lblRevue2.Location = new Point(28, 53);
            lblRevue2.Margin = new Padding(4, 0, 4, 0);
            lblRevue2.Name = "lblRevue2";
            lblRevue2.Size = new Size(91, 32);
            lblRevue2.TabIndex = 0;
            lblRevue2.Text = "Revue :";
            // 
            // cbRevue
            // 
            cbRevue.FormattingEnabled = true;
            cbRevue.Location = new Point(260, 43);
            cbRevue.Margin = new Padding(4);
            cbRevue.Name = "cbRevue";
            cbRevue.Size = new Size(275, 40);
            cbRevue.TabIndex = 1;
            // 
            // lblDateDebutSouscription
            // 
            lblDateDebutSouscription.AutoSize = true;
            lblDateDebutSouscription.Location = new Point(28, 107);
            lblDateDebutSouscription.Margin = new Padding(4, 0, 4, 0);
            lblDateDebutSouscription.Name = "lblDateDebutSouscription";
            lblDateDebutSouscription.Size = new Size(212, 32);
            lblDateDebutSouscription.TabIndex = 2;
            lblDateDebutSouscription.Text = "Date souscription :";
            // 
            // dtpDateDebutSouscription
            // 
            dtpDateDebutSouscription.Format = DateTimePickerFormat.Short;
            dtpDateDebutSouscription.Location = new Point(260, 100);
            dtpDateDebutSouscription.Margin = new Padding(4);
            dtpDateDebutSouscription.Name = "dtpDateDebutSouscription";
            dtpDateDebutSouscription.Size = new Size(275, 39);
            dtpDateDebutSouscription.TabIndex = 3;
            // 
            // lblDateFinSouscription
            // 
            lblDateFinSouscription.AutoSize = true;
            lblDateFinSouscription.Location = new Point(28, 162);
            lblDateFinSouscription.Margin = new Padding(4, 0, 4, 0);
            lblDateFinSouscription.Name = "lblDateFinSouscription";
            lblDateFinSouscription.Size = new Size(145, 32);
            lblDateFinSouscription.TabIndex = 4;
            lblDateFinSouscription.Text = "Date de fin :";
            // 
            // dtpDateFinSouscription
            // 
            dtpDateFinSouscription.Format = DateTimePickerFormat.Short;
            dtpDateFinSouscription.Location = new Point(260, 160);
            dtpDateFinSouscription.Margin = new Padding(4);
            dtpDateFinSouscription.Name = "dtpDateFinSouscription";
            dtpDateFinSouscription.Size = new Size(275, 39);
            dtpDateFinSouscription.TabIndex = 5;
            // 
            // lblMontant
            // 
            lblMontant.AutoSize = true;
            lblMontant.Location = new Point(28, 218);
            lblMontant.Margin = new Padding(4, 0, 4, 0);
            lblMontant.Name = "lblMontant";
            lblMontant.Size = new Size(118, 32);
            lblMontant.TabIndex = 6;
            lblMontant.Text = "Montant :";
            // 
            // txtMontant
            // 
            txtMontant.Location = new Point(260, 220);
            txtMontant.Margin = new Padding(4);
            txtMontant.Name = "txtMontant";
            txtMontant.Size = new Size(275, 39);
            txtMontant.TabIndex = 7;
            // 
            // btnSouscrireAbonnement
            // 
            btnSouscrireAbonnement.Location = new Point(28, 284);
            btnSouscrireAbonnement.Margin = new Padding(4);
            btnSouscrireAbonnement.Name = "btnSouscrireAbonnement";
            btnSouscrireAbonnement.Size = new Size(507, 39);
            btnSouscrireAbonnement.TabIndex = 8;
            btnSouscrireAbonnement.Text = "Souscrire";
            btnSouscrireAbonnement.UseVisualStyleBackColor = true;
            btnSouscrireAbonnement.Click += btnSouscrireAbonnement_Click;
            // 
            // dgvRevues
            // 
            dgvRevues.AllowUserToAddRows = false;
            dgvRevues.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRevues.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRevues.Location = new Point(23, 91);
            dgvRevues.Margin = new Padding(4);
            dgvRevues.Name = "dgvRevues";
            dgvRevues.ReadOnly = true;
            dgvRevues.RowHeadersWidth = 82;
            dgvRevues.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRevues.Size = new Size(1235, 434);
            dgvRevues.TabIndex = 3;
            dgvRevues.CellClick += dgvRevues_CellClick;
            // 
            // btnClearRecherche2
            // 
            btnClearRecherche2.Location = new Point(1115, 24);
            btnClearRecherche2.Margin = new Padding(4);
            btnClearRecherche2.Name = "btnClearRecherche2";
            btnClearRecherche2.Size = new Size(143, 39);
            btnClearRecherche2.TabIndex = 2;
            btnClearRecherche2.Text = "Effacer";
            btnClearRecherche2.UseVisualStyleBackColor = true;
            btnClearRecherche2.Click += btnClearRecherche2_Click;
            // 
            // txtRechercheRevues
            // 
            txtRechercheRevues.ForeColor = Color.Gray;
            txtRechercheRevues.Location = new Point(23, 24);
            txtRechercheRevues.Margin = new Padding(4);
            txtRechercheRevues.Name = "txtRechercheRevues";
            txtRechercheRevues.Size = new Size(1084, 39);
            txtRechercheRevues.TabIndex = 1;
            txtRechercheRevues.Text = "Rechercher";
            txtRechercheRevues.TextChanged += txtRechercheRevues_TextChanged;
            txtRechercheRevues.Enter += txtRechercheRevues_Enter;
            txtRechercheRevues.Leave += txtRechercheRevues_Leave;
            // 
            // FrmAbonnementRevues
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1331, 953);
            Controls.Add(tabAbonnements);
            Margin = new Padding(4);
            Name = "FrmAbonnementRevues";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestion des abonnements de revues";
            Load += FrmAbonnementRevues_Load;
            tabGestionAbonnements.ResumeLayout(false);
            tabGestionAbonnements.PerformLayout();
            gbSupprimerAbonnement.ResumeLayout(false);
            gbRenouvelerAbonnement.ResumeLayout(false);
            gbRenouvelerAbonnement.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAbonnements).EndInit();
            tabAbonnements.ResumeLayout(false);
            tabGestionRevues.ResumeLayout(false);
            tabGestionRevues.PerformLayout();
            gbSouscrireAbonnement.ResumeLayout(false);
            gbSouscrireAbonnement.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRevues).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabPage tabGestionAbonnements;
        private GroupBox gbRenouvelerAbonnement;
        private Label lbNAbonnement;
        private Label lblDateFinRenouvellement;
        private DateTimePicker dtpDateFinRenouvellement;
        private Button btnRenouvelerAbonnement;
        private Button btnSupprimerAbonnement;
        private TextBox txtRecherche;
        private Button btnClearRecherche;
        private DataGridView dgvAbonnements;
        private TabControl tabAbonnements;
        private TabPage tabGestionRevues;
        private GroupBox gbSouscrireAbonnement;
        private Label lblRevue2;
        private Label lblDateDebutSouscription;
        private DateTimePicker dtpDateDebutSouscription;
        private Label lblDateFinSouscription;
        private DateTimePicker dtpDateFinSouscription;
        private Label lblMontant;
        private TextBox txtMontant;
        private Button btnSouscrireAbonnement;
        private DataGridView dgvRevues;
        private Button btnClearRecherche2;
        private TextBox txtRechercheRevues;
        private Label lblAbonnementID;
        private ComboBox cbRevue;
        private GroupBox gbSupprimerAbonnement;
        private Label lblDateDebutRenouvellement;
        private DateTimePicker dtpDateDebutRenouvellement;
    }
}
