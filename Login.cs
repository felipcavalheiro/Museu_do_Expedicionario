using System;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data;
using BCrypt.Net;

namespace Museu_do_Expedicionário
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void Login_Load(object sender, EventArgs e)
        {
            // ✅ O banco real já foi inicializado no Program.Main()
        }

        // ✅ ADICIONAR: Método vazio que o Designer procura
        private void textBoxUsuário_TextChanged(object sender, EventArgs e)
        {
            // Deixe vazio ou adicione lógica de validação em tempo real
        }

        // ✅ ADICIONAR: Método vazio que o Designer procura
        private void textBoxSenha_TextChanged(object sender, EventArgs e)
        {
            // Deixe vazio ou adicione lógica de validação em tempo real
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string email = textBoxUsuário.Text.Trim();
            string senha = textBoxSenha.Text.Trim();

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
            {
                MessageBox.Show("Preencha todos os campos.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!ValidationHelper.ValidarEmail(email))
            {
                MessageBox.Show("Email inválido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string query = "SELECT Senha FROM Usuarios WHERE Email = @Email";
                SQLiteParameter[] parameters = { new SQLiteParameter("@Email", email) };
                DataTable result = DatabaseHelper.ExecuteQuery(query, parameters);

                if (result.Rows.Count > 0)
                {
                    string hashedSenha = result.Rows[0]["Senha"].ToString();

                    if (BCrypt.Net.BCrypt.Verify(senha, hashedSenha))
                    {
                        Principal principalForm = new Principal();
                        principalForm.FormClosed += (s, args) => Application.Exit();
                        principalForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Senha incorreta!", "Erro de Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxSenha.Clear();
                        textBoxSenha.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Usuário não encontrado!", "Erro de Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBoxUsuário.Clear();
                    textBoxUsuário.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro de banco de dados: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabelEsqSen_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            EsqueceuSenha esqueceuSenha = new EsqueceuSenha();
            esqueceuSenha.ShowDialog();
        }

        private void linkLabelNovUsu_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            NovoUsuario novoUsuario = new NovoUsuario();
            novoUsuario.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}