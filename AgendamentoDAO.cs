using System.Data;
using System.Data.SQLite;
using System;
using System.Windows.Forms;

namespace Museu_do_Expedicionário
{
    public static class AgendamentoDAO
    {
        /// <summary>
        /// ✅ Verifica se horário está disponível (não importa o CPF)
        /// </summary>
        public static bool HorarioDisponivel(string data, string hora)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM Agendamentos WHERE Data = @Data AND Hora = @Hora";
                SQLiteParameter[] parameters = {
                    new SQLiteParameter("@Data", data),
                    new SQLiteParameter("@Hora", hora)
                };

                DataTable result = DatabaseHelper.ExecuteQuery(query, parameters);
                int qtd = Convert.ToInt32(result.Rows[0][0]);
                return qtd == 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao verificar horário: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// ✅ REMOVIDO: ExisteCPF (não é mais necessário)
        /// Agora permite múltiplos agendamentos do mesmo CPF
        /// </summary>

        public static bool ExcluirAgendamento(string cpf)
        {
            try
            {
                string cpfLimpo = ValidationHelper.LimparCPF(cpf);

                // ✅ Delete apenas o PRIMEIRO agendamento com esse CPF
                // Para evitar deletar múltiplos de uma vez
                string query = "DELETE FROM Agendamentos WHERE CPFResponsavel = @CPF LIMIT 1";
                SQLiteParameter[] parameters = { new SQLiteParameter("@CPF", cpfLimpo) };

                int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);

                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show($"Agendamento não encontrado.", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao excluir agendamento: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}