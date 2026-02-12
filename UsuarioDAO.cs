using System.Data;  // ADICIONADO: Para DataTable
using System.Data.SQLite;  // Para SQLite
using System;  // Para exceções
using System.Windows.Forms;  // Para MessageBox

namespace Museu_do_Expedicionário
{
    public static class UsuarioDAO
    {
        // Método para verificar se o email existe (usando SQLite)
        public static bool VerificarEmail(string email)
        {
            string query = "SELECT COUNT(*) FROM Usuarios WHERE Email = @Email";
            SQLiteParameter[] parameters = { new SQLiteParameter("@Email", email) };

            DataTable result = DatabaseHelper.ExecuteQuery(query, parameters);
            if (result.Rows.Count > 0)
            {
                int count = Convert.ToInt32(result.Rows[0][0]);
                return count > 0;
            }
            return false;
        }

        // Método para criar um novo usuário (inclui Nome)
        public static void CriarUsuario(string nome, string email, string senha)
        {
            // Verifica se o email já existe
            if (VerificarEmail(email))
            {
                throw new Exception("Email já cadastrado.");
            }

            // Hash da senha para segurança
            string hashedSenha = BCrypt.Net.BCrypt.HashPassword(senha);

            // Query para inserir
            string query = "INSERT INTO Usuarios (Nome, Email, Senha) VALUES (@Nome, @Email, @Senha)";
            SQLiteParameter[] parameters = {
                new SQLiteParameter("@Nome", nome),
                new SQLiteParameter("@Email", email),
                new SQLiteParameter("@Senha", hashedSenha)
            };

            // Executa via DatabaseHelper
            DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        // Método para atualizar a senha
        public static void AtualizarSenha(string email, string novaSenha)
        {
            // Hash da nova senha
            string hashedSenha = BCrypt.Net.BCrypt.HashPassword(novaSenha);

            // Query para atualizar
            string query = "UPDATE Usuarios SET Senha = @Senha WHERE Email = @Email";
            SQLiteParameter[] parameters = {
                new SQLiteParameter("@Senha", hashedSenha),
                new SQLiteParameter("@Email", email)
            };

            // Executa via DatabaseHelper
            DatabaseHelper.ExecuteNonQuery(query, parameters);
        }
    }
}