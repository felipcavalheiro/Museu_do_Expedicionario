using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Museu_do_Expedicionário
{
    public partial class NovaSenha : Form
    {
        private string emailUsuario;  // Email passado do formulário anterior

        // Construtor padrão (se precisar)
        public NovaSenha()
        {
            InitializeComponent();
            this.ClientSize = new Size(560, 350);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        // Construtor que recebe o email do usuário
        public NovaSenha(string email) : this()
        {
            emailUsuario = email;
        }

        private void NovaSenha_Load(object sender, EventArgs e)
        {
            // Nada a fazer aqui por enquanto
        }

        private void txtBoxNovaSenha_TextChanged(object sender, EventArgs e)
        {
            // Nada a fazer aqui
        }

        private void txtBoxConfirmacao_TextChanged(object sender, EventArgs e)
        {
            // Nada a fazer aqui
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            string novaSenha = txtBoxNovaSenha.Text.Trim();
            string confirmacaoSenha = txtBoxConfirmacao.Text.Trim();

            // Validações
            if (string.IsNullOrEmpty(novaSenha) || novaSenha.Length < 6)
            {
                MessageBox.Show("A nova senha deve ter pelo menos 6 caracteres.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(confirmacaoSenha))
            {
                MessageBox.Show("Por favor, confirme a nova senha.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (novaSenha != confirmacaoSenha)
            {
                MessageBox.Show("As senhas não coincidem.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Atualizar senha no banco de dados
            try
            {
                UsuarioDAO.AtualizarSenha(emailUsuario, novaSenha);  // Assumindo que UsuarioDAO faz o hash
                MessageBox.Show("Senha alterada com sucesso! Você pode fazer login agora.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();  // Fechar o formulário após sucesso
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao alterar senha: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}