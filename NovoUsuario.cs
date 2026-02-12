using System;
using System.Windows.Forms;
using System.Drawing;

namespace Museu_do_Expedicionário
{
    public partial class NovoUsuario : Form
    {
        public NovoUsuario()
        {
            InitializeComponent();
            //this.ClientSize = new Size(510, 350);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        // ✅ ADICIONAR: Método vazio que o Designer procura
        private void NovoUsuario_Load(object sender, EventArgs e)
        {
            // Deixe vazio
        }

        // ✅ ADICIONAR: Método vazio que o Designer procura
        private void label1_Click(object sender, EventArgs e)
        {
            // Deixe vazio
        }

        // ✅ ADICIONAR: Método vazio que o Designer procura
        private void emailNovo_TextChanged(object sender, EventArgs e)
        {
            // Código opcional para validação em tempo real
        }

        // ✅ ADICIONAR: Método vazio que o Designer procura
        private void novoNome_TextChanged(object sender, EventArgs e)
        {
            // Código opcional para validação em tempo real
        }

        // ✅ ADICIONAR: Método vazio que o Designer procura
        private void novaSenha_TextChanged(object sender, EventArgs e)
        {
            // Código opcional para validação em tempo real
        }

        // ✅ ADICIONAR: Método vazio que o Designer procura
        private void novaSenhaConfirm_TextChanged(object sender, EventArgs e)
        {
            // Código opcional para validação em tempo real
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            // ✅ VALIDAÇÕES COMPLETAS
            if (string.IsNullOrWhiteSpace(novoNome.Text) ||
                string.IsNullOrWhiteSpace(emailNovo.Text) ||
                string.IsNullOrWhiteSpace(novaSenha.Text) ||
                string.IsNullOrWhiteSpace(novaSenhaConfirm.Text))
            {
                MessageBox.Show("Todos os campos são obrigatórios.", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // ✅ Validar email
            if (!ValidationHelper.ValidarEmail(emailNovo.Text))
            {
                MessageBox.Show("Email inválido.", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                emailNovo.Focus();
                return;
            }

            // ✅ Validar senha (mínimo 6 chars, letra e número)
            if (!ValidationHelper.ValidarSenha(novaSenha.Text))
            {
                MessageBox.Show("Senha deve ter no mínimo 6 caracteres, com letras e números.", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                novaSenha.Clear();
                novaSenha.Focus();
                return;
            }

            // ✅ Confirmar senha
            if (novaSenha.Text != novaSenhaConfirm.Text)
            {
                MessageBox.Show("As senhas não coincidem.", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                novaSenhaConfirm.Clear();
                novaSenhaConfirm.Focus();
                return;
            }

            try
            {
                UsuarioDAO.CriarUsuario(novoNome.Text.Trim(), emailNovo.Text.Trim(), novaSenha.Text);
                MessageBox.Show("Usuário cadastrado com sucesso!", "Sucesso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao cadastrar: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}