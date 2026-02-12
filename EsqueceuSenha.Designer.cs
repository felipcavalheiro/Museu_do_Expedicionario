namespace Museu_do_Expedicionário
{
    partial class EsqueceuSenha
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EsqueceuSenha));
            this.btnFechar = new System.Windows.Forms.Button();
            this.Email = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textboxemailEsqueceu = new System.Windows.Forms.TextBox();
            this.btnEsqueceu = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxVerificacao = new System.Windows.Forms.TextBox();
            this.btnVerificacao = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnFechar
            // 
            this.btnFechar.Location = new System.Drawing.Point(251, 468);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(64, 25);
            this.btnFechar.TabIndex = 8;
            this.btnFechar.Text = "Fechar";
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // Email
            // 
            this.Email.AutoSize = true;
            this.Email.Location = new System.Drawing.Point(21, 80);
            this.Email.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Email.Name = "Email";
            this.Email.Size = new System.Drawing.Size(84, 13);
            this.Email.TabIndex = 9;
            this.Email.Text = "Digite seu email:";
            this.Email.Click += new System.EventHandler(this.Email_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(119, 118);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(119, 105);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 11;
            // 
            // textboxemailEsqueceu
            // 
            this.textboxemailEsqueceu.Location = new System.Drawing.Point(139, 77);
            this.textboxemailEsqueceu.Margin = new System.Windows.Forms.Padding(2);
            this.textboxemailEsqueceu.Name = "textboxemailEsqueceu";
            this.textboxemailEsqueceu.Size = new System.Drawing.Size(163, 20);
            this.textboxemailEsqueceu.TabIndex = 12;
            this.textboxemailEsqueceu.TextChanged += new System.EventHandler(this.textboxemailEsqueceu_TextChanged);
            // 
            // btnEsqueceu
            // 
            this.btnEsqueceu.Location = new System.Drawing.Point(186, 118);
            this.btnEsqueceu.Name = "btnEsqueceu";
            this.btnEsqueceu.Size = new System.Drawing.Size(75, 23);
            this.btnEsqueceu.TabIndex = 13;
            this.btnEsqueceu.Text = "Confirmar";
            this.btnEsqueceu.UseVisualStyleBackColor = true;
            this.btnEsqueceu.Click += new System.EventHandler(this.btnEsqueceu_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 207);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Código de Verificação:";
            // 
            // textBoxVerificacao
            // 
            this.textBoxVerificacao.Location = new System.Drawing.Point(152, 204);
            this.textBoxVerificacao.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxVerificacao.Name = "textBoxVerificacao";
            this.textBoxVerificacao.Size = new System.Drawing.Size(163, 20);
            this.textBoxVerificacao.TabIndex = 15;
            this.textBoxVerificacao.TextChanged += new System.EventHandler(this.textBoxVerificacao_TextChanged);
            // 
            // btnVerificacao
            // 
            this.btnVerificacao.Location = new System.Drawing.Point(186, 246);
            this.btnVerificacao.Name = "btnVerificacao";
            this.btnVerificacao.Size = new System.Drawing.Size(75, 23);
            this.btnVerificacao.TabIndex = 16;
            this.btnVerificacao.Text = "Verificar";
            this.btnVerificacao.UseVisualStyleBackColor = true;
            this.btnVerificacao.Click += new System.EventHandler(this.btnVerificacao_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textboxemailEsqueceu);
            this.panel1.Controls.Add(this.btnVerificacao);
            this.panel1.Controls.Add(this.Email);
            this.panel1.Controls.Add(this.btnFechar);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.textBoxVerificacao);
            this.panel1.Controls.Add(this.btnEsqueceu);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(506, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(327, 505);
            this.panel1.TabIndex = 17;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Location = new System.Drawing.Point(1, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(499, 503);
            this.panel2.TabIndex = 18;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Museu_do_Expedicionário.Properties.Resources.Fundo_5;
            this.pictureBox1.Location = new System.Drawing.Point(0, -5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(821, 557);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // EsqueceuSenha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(833, 505);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EsqueceuSenha";
            this.Text = "EsqueceuSenha";
            this.Load += new System.EventHandler(this.EsqueceuSenha_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Label Email;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textboxemailEsqueceu;
        private System.Windows.Forms.Button btnEsqueceu;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxVerificacao;
        private System.Windows.Forms.Button btnVerificacao;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}