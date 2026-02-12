namespace Museu_do_Expedicionário
{
    partial class NovaSenha
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBoxNovaSenha = new System.Windows.Forms.TextBox();
            this.txtBoxConfirmacao = new System.Windows.Forms.TextBox();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(81, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Digite a nova senha:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(81, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Confirme a nova senha:";
            // 
            // txtBoxNovaSenha
            // 
            this.txtBoxNovaSenha.Location = new System.Drawing.Point(236, 70);
            this.txtBoxNovaSenha.Name = "txtBoxNovaSenha";
            this.txtBoxNovaSenha.PasswordChar = '*';
            this.txtBoxNovaSenha.Size = new System.Drawing.Size(100, 20);
            this.txtBoxNovaSenha.TabIndex = 2;
            this.txtBoxNovaSenha.TextChanged += new System.EventHandler(this.txtBoxNovaSenha_TextChanged);
            // 
            // txtBoxConfirmacao
            // 
            this.txtBoxConfirmacao.Location = new System.Drawing.Point(236, 118);
            this.txtBoxConfirmacao.Name = "txtBoxConfirmacao";
            this.txtBoxConfirmacao.PasswordChar = '*';
            this.txtBoxConfirmacao.Size = new System.Drawing.Size(100, 20);
            this.txtBoxConfirmacao.TabIndex = 3;
            this.txtBoxConfirmacao.TextChanged += new System.EventHandler(this.txtBoxConfirmacao_TextChanged);
            // 
            // btnSalvar
            // 
            this.btnSalvar.Location = new System.Drawing.Point(246, 160);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(75, 23);
            this.btnSalvar.TabIndex = 4;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // NovaSenha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 311);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.txtBoxConfirmacao);
            this.Controls.Add(this.txtBoxNovaSenha);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "NovaSenha";
            this.Text = "NovaSenha";
            this.Load += new System.EventHandler(this.NovaSenha_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBoxNovaSenha;
        private System.Windows.Forms.TextBox txtBoxConfirmacao;
        private System.Windows.Forms.Button btnSalvar;
    }
}