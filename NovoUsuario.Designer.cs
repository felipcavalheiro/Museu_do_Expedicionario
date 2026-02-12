namespace Museu_do_Expedicionário
{
    partial class NovoUsuario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NovoUsuario));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.novoNome = new System.Windows.Forms.TextBox();
            this.novaSenha = new System.Windows.Forms.TextBox();
            this.novaSenhaConfirm = new System.Windows.Forms.TextBox();
            this.btnContinuar = new System.Windows.Forms.Button();
            this.btnFechar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.emailNovo = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Digite seu nome:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Digite sua senha";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 173);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Confirme sua senha:";
            // 
            // novoNome
            // 
            this.novoNome.Location = new System.Drawing.Point(143, 67);
            this.novoNome.Name = "novoNome";
            this.novoNome.Size = new System.Drawing.Size(128, 20);
            this.novoNome.TabIndex = 3;
            this.novoNome.TextChanged += new System.EventHandler(this.novoNome_TextChanged);
            // 
            // novaSenha
            // 
            this.novaSenha.Location = new System.Drawing.Point(143, 131);
            this.novaSenha.Name = "novaSenha";
            this.novaSenha.PasswordChar = '*';
            this.novaSenha.Size = new System.Drawing.Size(128, 20);
            this.novaSenha.TabIndex = 4;
            this.novaSenha.TextChanged += new System.EventHandler(this.novaSenha_TextChanged);
            // 
            // novaSenhaConfirm
            // 
            this.novaSenhaConfirm.Location = new System.Drawing.Point(143, 170);
            this.novaSenhaConfirm.Name = "novaSenhaConfirm";
            this.novaSenhaConfirm.PasswordChar = '*';
            this.novaSenhaConfirm.Size = new System.Drawing.Size(128, 20);
            this.novaSenhaConfirm.TabIndex = 5;
            this.novaSenhaConfirm.TextChanged += new System.EventHandler(this.novaSenhaConfirm_TextChanged);
            // 
            // btnContinuar
            // 
            this.btnContinuar.Location = new System.Drawing.Point(103, 239);
            this.btnContinuar.Name = "btnContinuar";
            this.btnContinuar.Size = new System.Drawing.Size(75, 23);
            this.btnContinuar.TabIndex = 6;
            this.btnContinuar.Text = "Continuar";
            this.btnContinuar.UseVisualStyleBackColor = true;
            this.btnContinuar.Click += new System.EventHandler(this.btnContinuar_Click);
            // 
            // btnFechar
            // 
            this.btnFechar.Location = new System.Drawing.Point(226, 484);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(64, 25);
            this.btnFechar.TabIndex = 7;
            this.btnFechar.Text = "Fechar";
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Digite seu email:";
            // 
            // emailNovo
            // 
            this.emailNovo.Location = new System.Drawing.Point(143, 98);
            this.emailNovo.Name = "emailNovo";
            this.emailNovo.Size = new System.Drawing.Size(128, 20);
            this.emailNovo.TabIndex = 9;
            this.emailNovo.TextChanged += new System.EventHandler(this.emailNovo_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.novoNome);
            this.panel1.Controls.Add(this.btnFechar);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btnContinuar);
            this.panel1.Controls.Add(this.emailNovo);
            this.panel1.Controls.Add(this.novaSenhaConfirm);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.novaSenha);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(561, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(325, 519);
            this.panel1.TabIndex = 10;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(562, 509);
            this.panel2.TabIndex = 11;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Museu_do_Expedicionário.Properties.Resources.Fundo_5;
            this.pictureBox1.Location = new System.Drawing.Point(0, -31);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(970, 685);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // NovoUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 519);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NovoUsuario";
            this.Text = "NovoUsuario";
            this.Load += new System.EventHandler(this.NovoUsuario_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox novoNome;
        private System.Windows.Forms.TextBox novaSenha;
        private System.Windows.Forms.TextBox novaSenhaConfirm;
        private System.Windows.Forms.Button btnContinuar;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox emailNovo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}