namespace MediaTekDocuments.view
{
    partial class FrmConnexion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConnexion));
            txtIdentifiant = new TextBox();
            txtMotDePasse = new TextBox();
            btnSeConnecter = new Button();
            lblMessage = new Label();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // txtIdentifiant
            // 
            txtIdentifiant.ForeColor = Color.Gray;
            txtIdentifiant.Location = new Point(155, 254);
            txtIdentifiant.Name = "txtIdentifiant";
            txtIdentifiant.Size = new Size(455, 39);
            txtIdentifiant.TabIndex = 0;
            txtIdentifiant.Text = "Identifiant";
            txtIdentifiant.Enter += txtIdentifiant_Enter;
            txtIdentifiant.Leave += txtIdentifiant_Leave;
            // 
            // txtMotDePasse
            // 
            txtMotDePasse.ForeColor = Color.Gray;
            txtMotDePasse.Location = new Point(155, 314);
            txtMotDePasse.Name = "txtMotDePasse";
            txtMotDePasse.Size = new Size(455, 39);
            txtMotDePasse.TabIndex = 1;
            txtMotDePasse.Text = "Mot de passe";
            txtMotDePasse.Enter += txtMotDePasse_Enter;
            txtMotDePasse.Leave += txtMotDePasse_Leave;
            // 
            // btnSeConnecter
            // 
            btnSeConnecter.Location = new Point(155, 372);
            btnSeConnecter.Name = "btnSeConnecter";
            btnSeConnecter.Size = new Size(455, 43);
            btnSeConnecter.TabIndex = 2;
            btnSeConnecter.Text = "Se connecter";
            btnSeConnecter.UseVisualStyleBackColor = true;
            btnSeConnecter.Click += btnSeConnecter_Click;
            // 
            // lblMessage
            // 
            lblMessage.AutoSize = true;
            lblMessage.Font = new Font("Segoe UI", 7F);
            lblMessage.ForeColor = Color.Red;
            lblMessage.Location = new Point(233, 418);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(0, 25);
            lblMessage.TabIndex = 3;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(195, -38);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(363, 331);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // FrmConnexion
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(761, 460);
            Controls.Add(txtIdentifiant);
            Controls.Add(pictureBox1);
            Controls.Add(lblMessage);
            Controls.Add(btnSeConnecter);
            Controls.Add(txtMotDePasse);
            Name = "FrmConnexion";
            Text = "Authentification";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtIdentifiant;
        private TextBox txtMotDePasse;
        private Button btnSeConnecter;
        private Label lblMessage;
        private PictureBox pictureBox1;
    }
}