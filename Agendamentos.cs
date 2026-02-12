using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Museu_do_Expedicionário
{
    public partial class Agendamentos : Form
    {
        private DataGridView dgv;

        public Agendamentos()
        {
            InitializeComponent();
            this.ClientSize = new Size(1800, 700);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Load += Agendamentos_Load;
        }

        private void Agendamentos_Load(object sender, EventArgs e)
        {
            if (this.Controls.OfType<DataGridView>().Count() == 0)
            {
                CriarDataGridView();
            }
            CarregarAgendamentos();
        }

        private void CriarDataGridView()
        {
            dgv = new DataGridView
            {
                Dock = DockStyle.Top,
                Height = 550,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AllowUserToAddRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Name = "dgvAgendamentos"
            };

            // ✅ Cores alternadas nas linhas
            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkBlue;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            // ✅ Botões de ação
            var editCol = new DataGridViewButtonColumn
            {
                Name = "Editar",
                Text = "✏️ Editar",
                UseColumnTextForButtonValue = true,
                Width = 80
            };
            dgv.Columns.Add(editCol);

            var delCol = new DataGridViewButtonColumn
            {
                Name = "Excluir",
                Text = "🗑️ Excluir",
                UseColumnTextForButtonValue = true,
                Width = 80
            };
            dgv.Columns.Add(delCol);

            var pdfCol = new DataGridViewButtonColumn
            {
                Name = "PDF",
                Text = "📄 PDF",
                UseColumnTextForButtonValue = true,
                Width = 80
            };
            dgv.Columns.Add(pdfCol);

            dgv.CellClick += Dgv_CellClick;

            // ✅ Painel de controle (filtros e botões)
            Panel panelControles = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 100,
                BackColor = Color.LightGray
            };

            // Filtro por CPF
            Label lblCPF = new Label { Text = "Filtrar por CPF:", Location = new Point(10, 10), AutoSize = true };
            TextBox txtFiltro = new TextBox { Location = new Point(120, 10), Width = 200 };
            Button btnFiltrar = new Button { Text = "Filtrar", Location = new Point(330, 10), Width = 80 };
            Button btnLimpar = new Button { Text = "Limpar Filtro", Location = new Point(420, 10), Width = 100 };
            Button btnExportarTodos = new Button { Text = "📄 Exportar Todos em PDF", Location = new Point(530, 10), Width = 180 };
            Button btnFechar = new Button { Text = "❌ Fechar", Location = new Point(720, 10), Width = 80 };

            btnFiltrar.Click += (s, e) =>
            {
                FiltrarAgendamentos(txtFiltro.Text);
            };

            btnLimpar.Click += (s, e) =>
            {
                txtFiltro.Clear();
                CarregarAgendamentos();
            };

            btnExportarTodos.Click += (s, e) =>
            {
                ExportarTodosPDF();
            };

            // ✅ CORREÇÃO: Botão Fechar com a mesma lógica do Brasileiro.cs
            btnFechar.Click += (s, e) =>
            {
                FecharFormulario();
            };

            panelControles.Controls.Add(lblCPF);
            panelControles.Controls.Add(txtFiltro);
            panelControles.Controls.Add(btnFiltrar);
            panelControles.Controls.Add(btnLimpar);
            panelControles.Controls.Add(btnExportarTodos);
            panelControles.Controls.Add(btnFechar);

            this.Controls.Add(dgv);
            this.Controls.Add(panelControles);
            dgv.BringToFront();
        }

        // ✅ NOVO: Método para fechar corretamente
        private void FecharFormulario()
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public void CarregarAgendamentos()
        {
            if (dgv == null) return;
            try
            {
                string query = @"
                    SELECT
                        Data AS DataAgendamento,
                        CPFResponsavel,
                        FoneResponsavel,
                        FoneInstituicao,
                        Hora AS HorarioAgendamento,
                        TipoVisitante,
                        PaisGrupo,
                        UFGrupo,
                        CidadeGrupo,
                        TurmaFaixaEtaria,
                        GrauEscolaridade,
                        NumeroVisitantes,
                        FinalidadeVisita
                    FROM Agendamentos
                    ORDER BY Data DESC, Hora ASC";

                DataTable dt = DatabaseHelper.ExecuteQuery(query);
                dgv.DataSource = dt;

                ConfigurarColunas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar agendamentos: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ NOVO: Filtrar por CPF
        private void FiltrarAgendamentos(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
            {
                CarregarAgendamentos();
                return;
            }

            try
            {
                string query = @"
                    SELECT
                        Data AS DataAgendamento,
                        CPFResponsavel,
                        FoneResponsavel,
                        FoneInstituicao,
                        Hora AS HorarioAgendamento,
                        TipoVisitante,
                        PaisGrupo,
                        UFGrupo,
                        CidadeGrupo,
                        TurmaFaixaEtaria,
                        GrauEscolaridade,
                        NumeroVisitantes,
                        FinalidadeVisita
                    FROM Agendamentos
                    WHERE CPFResponsavel LIKE @CPF
                    ORDER BY Data DESC, Hora ASC";

                SQLiteParameter[] parameters = { new SQLiteParameter("@CPF", "%" + cpf + "%") };
                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
                dgv.DataSource = dt;

                ConfigurarColunas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao filtrar: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarColunas()
        {
            if (dgv.Columns.Count > 0)
            {
                if (dgv.Columns.Contains("DataAgendamento"))
                    dgv.Columns["DataAgendamento"].HeaderText = "Data";
                if (dgv.Columns.Contains("CPFResponsavel"))
                    dgv.Columns["CPFResponsavel"].HeaderText = "CPF";
                if (dgv.Columns.Contains("FoneResponsavel"))
                    dgv.Columns["FoneResponsavel"].HeaderText = "Fone Responsável";
                if (dgv.Columns.Contains("FoneInstituicao"))
                    dgv.Columns["FoneInstituicao"].HeaderText = "Fone Instituição";
                if (dgv.Columns.Contains("HorarioAgendamento"))
                    dgv.Columns["HorarioAgendamento"].HeaderText = "Horário";
                if (dgv.Columns.Contains("TipoVisitante"))
                    dgv.Columns["TipoVisitante"].HeaderText = "Tipo";
                if (dgv.Columns.Contains("PaisGrupo"))
                    dgv.Columns["PaisGrupo"].HeaderText = "País";
                if (dgv.Columns.Contains("UFGrupo"))
                    dgv.Columns["UFGrupo"].HeaderText = "UF";
                if (dgv.Columns.Contains("CidadeGrupo"))
                    dgv.Columns["CidadeGrupo"].HeaderText = "Cidade";
                if (dgv.Columns.Contains("TurmaFaixaEtaria"))
                    dgv.Columns["TurmaFaixaEtaria"].HeaderText = "Faixa Etária";
                if (dgv.Columns.Contains("GrauEscolaridade"))
                    dgv.Columns["GrauEscolaridade"].HeaderText = "Escolaridade";
                if (dgv.Columns.Contains("NumeroVisitantes"))
                    dgv.Columns["NumeroVisitantes"].HeaderText = "Visitantes";
                if (dgv.Columns.Contains("FinalidadeVisita"))
                    dgv.Columns["FinalidadeVisita"].HeaderText = "Finalidade";
            }
        }

        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgv == null) return;
            if (!dgv.Columns.Contains("CPFResponsavel") || dgv.Rows[e.RowIndex].Cells["CPFResponsavel"].Value == null)
                return;

            string cpf = dgv.Rows[e.RowIndex].Cells["CPFResponsavel"].Value.ToString();

            // ✅ Botão Editar
            if (dgv.Columns[e.ColumnIndex].Name == "Editar")
            {
                if (!dgv.Columns.Contains("TipoVisitante") || dgv.Rows[e.RowIndex].Cells["TipoVisitante"].Value == null)
                    return;

                string tipo = dgv.Rows[e.RowIndex].Cells["TipoVisitante"].Value.ToString();
                cpf = ValidationHelper.LimparCPF(cpf);

                if (tipo == "Estrangeiro")
                {
                    using (Estrangeiro form = new Estrangeiro())
                    {
                        form.CarregarAgendamento(cpf);
                        form.ShowDialog();
                    }
                }
                else if (tipo == "Brasileiro")
                {
                    using (Brasileiro form = new Brasileiro())
                    {
                        form.CarregarAgendamento(cpf);
                        form.ShowDialog();
                    }
                }
                CarregarAgendamentos();
            }
            // ✅ Botão Excluir
            else if (dgv.Columns[e.ColumnIndex].Name == "Excluir")
            {
                DialogResult dr = MessageBox.Show($"Deseja realmente excluir o agendamento do CPF: {cpf}?",
                    "Confirmar Exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dr == DialogResult.Yes)
                {
                    if (AgendamentoDAO.ExcluirAgendamento(cpf))
                    {
                        MessageBox.Show("Agendamento excluído com sucesso!", "Sucesso",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    CarregarAgendamentos();
                }
            }
            // ✅ Botão PDF
            else if (dgv.Columns[e.ColumnIndex].Name == "PDF")
            {
                ExportarUmAgendamentoPDF(cpf, e.RowIndex);
            }
        }

        // ✅ NOVO: Exportar um agendamento em PDF
        private void ExportarUmAgendamentoPDF(string cpf, int rowIndex)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "PDF Files (*.pdf)|*.pdf",
                    FileName = $"Agendamento_{cpf.Replace(".", "").Replace("-", "")}",
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    GerarPDFAgendamento(dgv.Rows[rowIndex], saveDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao exportar PDF: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ NOVO: Exportar todos os agendamentos em PDF
        private void ExportarTodosPDF()
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "PDF Files (*.pdf)|*.pdf",
                    FileName = $"Agendamentos_{DateTime.Now:dd-MM-yyyy_HHmmss}",
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    GerarPDFTodos(saveDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao exportar PDF: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ NOVO: Gerar PDF de um agendamento
        private void GerarPDFAgendamento(DataGridViewRow row, string caminhoArquivo)
        {
            PdfHelper.GerarPDFAgendamento(row, caminhoArquivo);
            MessageBox.Show($"PDF exportado com sucesso em:\n{caminhoArquivo}", "Sucesso",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ✅ NOVO: Gerar PDF de todos os agendamentos
        private void GerarPDFTodos(string caminhoArquivo)
        {
            DataTable dt = (DataTable)dgv.DataSource;
            PdfHelper.GerarPDFTodos(dt, caminhoArquivo);
            MessageBox.Show($"PDF exportado com sucesso em:\n{caminhoArquivo}", "Sucesso",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}