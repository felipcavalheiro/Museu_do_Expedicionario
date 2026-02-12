using System;
using System.Windows.Forms;
using System.Drawing;
using System.Net.Mail;
using System.Net;
using System.Configuration;

namespace Museu_do_Expedicionário
{
    public partial class EsqueceuSenha : Form
    {
        private string codigoVerificacao;
        private string emailUsuario;

        // ✅ SEGURANÇA: Carregar do arquivo de configuração (App.config)
        private string emailRemetente = ConfigurationManager.AppSettings["EmailRemetente"] ?? "seu-email@gmail.com";
        private string senhaRemetente = ConfigurationManager.AppSettings["EmailSenha"] ?? "";

        public EsqueceuSenha()
        {
            InitializeComponent();
            //this.ClientSize = new Size(560, 350);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        // ✅ ADICIONAR: Método vazio que o Designer procura
        private void EsqueceuSenha_Load(object sender, EventArgs e)
        {
            // Deixe vazio
        }

        // ✅ ADICIONAR: Método vazio que o Designer procura
        private void Email_Click(object sender, EventArgs e)
        {
            // Deixe vazio
        }

        // ✅ ADICIONAR: Método vazio que o Designer procura
        private void textboxemailEsqueceu_TextChanged(object sender, EventArgs e)
        {
            // Deixe vazio
        }

        // ✅ ADICIONAR: Método vazio que o Designer procura
        private void textBoxVerificacao_TextChanged(object sender, EventArgs e)
        {
            // Deixe vazio
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEsqueceu_Click(object sender, EventArgs e)
        {
            string email = textboxemailEsqueceu.Text.Trim();

            // ✅ Validar email
            if (!ValidationHelper.ValidarEmail(email))
            {
                MessageBox.Show("Por favor, insira um email válido.", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // ✅ Verificar se email existe
            if (!UsuarioDAO.VerificarEmail(email))
            {
                MessageBox.Show("Email não encontrado no sistema.", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Gerar código de verificação
                codigoVerificacao = new Random().Next(100000, 999999).ToString();
                emailUsuario = email;

                EnviarEmailVerificacao(email, codigoVerificacao);
                MessageBox.Show("Código enviado para seu email!", "Sucesso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                textBoxVerificacao.Enabled = true;
                btnVerificacao.Enabled = true;
            }
            catch (SmtpException ex)
            {
                MessageBox.Show($"Erro ao enviar email (SMTP): {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao enviar email: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVerificacao_Click(object sender, EventArgs e)
        {
            string codigoInserido = textBoxVerificacao.Text.Trim();

            if (string.IsNullOrEmpty(codigoInserido))
            {
                MessageBox.Show("Por favor, insira o código de verificação.", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (codigoInserido == codigoVerificacao)
            {
                MessageBox.Show("Código verificado! Você será redirecionado para alterar a senha.", "Sucesso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // ✅ Abrir formulário de nova senha
                NovaSenha novaSenhaForm = new NovaSenha(emailUsuario);
                novaSenhaForm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Código incorreto. Tente novamente.", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxVerificacao.Clear();
            }
        }

        private void EnviarEmailVerificacao(string emailDestino, string codigo)
        {
            // ✅ SEGURANÇA: Verificar se credenciais estão configuradas
            if (string.IsNullOrWhiteSpace(emailRemetente) || string.IsNullOrWhiteSpace(senhaRemetente))
            {
                throw new Exception("Credenciais de email não configuradas no App.config");
            }

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailRemetente);
                mail.To.Add(emailDestino);
                mail.Subject = "Código de Verificação - Museu do Expedicionário";
                mail.Body = $@"
                    <h2>Código de Verificação</h2>
                    <p>Seu código é: <strong>{codigo}</strong></p>
                    <p>Este código é válido por 10 minutos.</p>
                    <p>Se você não solicitou este código, ignore este email.</p>";
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential(emailRemetente, senhaRemetente);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }
    }
}