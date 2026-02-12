using System;
using System.Windows.Forms;

namespace Museu_do_Expedicionário
{
    internal static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // ✅ CORREÇÃO: Inicializa banco antes de abrir qualquer formulário
            try
            {
                DatabaseHelper.EnsureDatabaseExists();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao inicializar banco de dados: {ex.Message}", "Erro Fatal",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                Application.Run(new Login());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro na aplicação: {ex.Message}", "Erro Fatal",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}