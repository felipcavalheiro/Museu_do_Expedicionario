using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Data;
using System.Data.SQLite;

namespace Museu_do_Expedicionário
{
    public partial class Principal : Form
    {
        private Action _limparCamposPrincipal;
        public string DataAgendamento { get; set; }
        public string CPFResponsavel { get; set; }
        public string FoneResponsavel { get; set; }
        public string FoneInstituicao { get; set; }
        public string HorarioAgendamento { get; set; }

        public Principal()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            _limparCamposPrincipal = LimparCampos;

            InicializarControles();
        }

        private void InicializarControles()
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            TxtBoxCPF.Enter += maskedTextBox_Enter;
            TxtBoxNumResponsavel.Enter += maskedTextBox_Enter;
            maskedTextBox3.Enter += maskedTextBox_Enter;
            TxtBoxCPF.Leave += TxtBoxCPF_Leave;

            btnBrasileiro.Click += btnBrasileiro_Click;
            btnEstrangeiro.Click += btnEstrangeiro_Click;
            button4.Click += button4_Click;
            btnAgendamentos.Click += btnAgendamentos_Click;

            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;

            dateTimePicker1.Value = DateTime.Today;

            AdicionarEnterComoTab(this);
        }

        private void maskedTextBox_Enter(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() =>
            {
                MaskedTextBox mtb = sender as MaskedTextBox;
                if (mtb != null)
                {
                    mtb.SelectionStart = 0;
                    mtb.SelectionLength = 0;
                }
            }));
        }

        private void TxtBoxCPF_Leave(object sender, EventArgs e)
        {
            if (!ValidationHelper.ValidarCPF(TxtBoxCPF.Text))
            {
                MessageBox.Show("CPF inválido!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtBoxCPF.Focus();
            }
        }

        // ✅ ADICIONAR: Método vazio que o Designer procura
        private void TxtBoxCPF_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            // Deixe vazio
        }

        // ✅ ADICIONAR: Método vazio que o Designer procura
        private void label7_Click(object sender, EventArgs e)
        {
            // Deixe vazio
        }

        private void btnBrasileiro_Click(object sender, EventArgs e)
        {
            SalvarCampos();

            // ✅ Validar antes de abrir
            if (!ValidarCamposPrincipais())
                return;

            // ✅ Criar e mostrar formulário
            using (Brasileiro formBrasileiro = new Brasileiro(
                DataAgendamento, CPFResponsavel, FoneResponsavel,
                FoneInstituicao, HorarioAgendamento, _limparCamposPrincipal))
            {
                // ✅ ShowDialog() bloqueia até o formulário fechar
                DialogResult result = formBrasileiro.ShowDialog();

                // ✅ Se fechou com sucesso
                if (result == DialogResult.OK)
                {
                    AtualizarHorariosDisponiveis();
                }
            }
            // ✅ Aqui o formulário foi destruído pelo using
        }

        private void btnEstrangeiro_Click(object sender, EventArgs e)
        {
            SalvarCampos();

            // ✅ Validar antes de abrir
            if (!ValidarCamposPrincipais())
                return;

            // ✅ Criar e mostrar formulário
            using (Estrangeiro formEstrangeiro = new Estrangeiro(
                DataAgendamento, CPFResponsavel, FoneResponsavel,
                FoneInstituicao, HorarioAgendamento))
            {
                // ✅ ShowDialog() bloqueia até o formulário fechar
                DialogResult result = formEstrangeiro.ShowDialog();

                // ✅ Se fechou com sucesso
                if (result == DialogResult.OK)
                {
                    AtualizarHorariosDisponiveis();
                }
            }
            // ✅ Aqui o formulário foi destruído pelo using
        }

        private bool ValidarCamposPrincipais()
        {
            if (string.IsNullOrWhiteSpace(DataAgendamento))
            {
                MessageBox.Show("Selecione a data do agendamento.", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(HorarioAgendamento))
            {
                MessageBox.Show("Selecione o horário do agendamento.", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!ValidationHelper.ValidarCPF(CPFResponsavel))
            {
                MessageBox.Show("CPF inválido.", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // ✅ Verificar se horário está disponível
            if (!AgendamentoDAO.HorarioDisponivel(DataAgendamento, HorarioAgendamento))
            {
                MessageBox.Show("Horário não está disponível. Escolha outro.", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAgendamentos_Click(object sender, EventArgs e)
        {
            // ✅ CORREÇÃO: Usar using e ShowDialog() como os outros formulários
            using (Agendamentos formAgendamentos = new Agendamentos())
            {
                // ✅ ShowDialog() bloqueia até o formulário fechar
                DialogResult result = formAgendamentos.ShowDialog();
            }
            // ✅ Aqui o formulário foi destruído pelo using
        }

        private void SalvarCampos()
        {
            DataAgendamento = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            CPFResponsavel = ValidationHelper.LimparCPF(TxtBoxCPF.Text);
            FoneResponsavel = TxtBoxNumResponsavel.Text;
            FoneInstituicao = maskedTextBox3.Text;
            HorarioAgendamento = comboBox1.SelectedItem?.ToString() ?? "";
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            AtualizarHorariosDisponiveis();
        }

        private void AtualizarHorariosDisponiveis()
        {
            comboBox1.Items.Clear();
            DayOfWeek diaSemana = dateTimePicker1.Value.DayOfWeek;

            string[] horariosDisponiveis;
            if (diaSemana >= DayOfWeek.Monday && diaSemana <= DayOfWeek.Thursday)
                horariosDisponiveis = new string[] { "09:00", "10:30", "13:30" };
            else if (diaSemana == DayOfWeek.Friday)
                horariosDisponiveis = new string[] { "09:00", "10:30" };
            else
                horariosDisponiveis = new string[] { };

            // ✅ Filtrar apenas horários disponíveis
            DataTable dt = DatabaseHelper.ExecuteQuery(
                "SELECT Hora FROM Agendamentos WHERE Data = @Data",
                new SQLiteParameter("@Data", dateTimePicker1.Value.ToString("yyyy-MM-dd"))
            );

            var horariosOcupados = new System.Collections.Generic.List<string>();
            foreach (DataRow row in dt.Rows)
                horariosOcupados.Add(row["Hora"].ToString());

            foreach (var horario in horariosDisponiveis)
                if (!horariosOcupados.Contains(horario))
                    comboBox1.Items.Add(horario);

            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;
        }

        private void AdicionarEnterComoTab(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is TextBox || ctrl is MaskedTextBox)
                {
                    ctrl.KeyDown += (s, e) =>
                    {
                        if (e.KeyCode == Keys.Enter)
                        {
                            e.SuppressKeyPress = true;
                            this.SelectNextControl((Control)s, true, true, true, true);
                        }
                    };
                }
                if (ctrl.HasChildren) AdicionarEnterComoTab(ctrl);
            }
        }

        /// <summary>
        /// ✅ Insere agendamento com validações
        /// </summary>
        public static void InserirNoBanco(
    string dataAgendamento, string cpfResponsavel, string foneResponsavel,
    string foneInstituicao, string horarioAgendamento, string tipoVisitante,
    string ufGrupo, string cidadeGrupo, string turmaFaixaEtaria,
    string grauEscolaridade, string numeroVisitantes, string finalidadeVisita,
    string paisGrupo)
        {
            try
            {
                // ✅ Validar CPF
                string cpfLimpo = ValidationHelper.LimparCPF(cpfResponsavel);
                if (!ValidationHelper.ValidarCPF(cpfLimpo))
                {
                    throw new Exception("CPF inválido.");
                }

                // ✅ Verificar disponibilidade (DATA + HORA)
                if (!AgendamentoDAO.HorarioDisponivel(dataAgendamento, horarioAgendamento))
                {
                    throw new Exception("Horário não está disponível. Escolha outro.");
                }

                string query = @"
            INSERT INTO Agendamentos (
                Data, Hora, CPFResponsavel, FoneResponsavel, FoneInstituicao,
                TipoVisitante, UFGrupo, CidadeGrupo, TurmaFaixaEtaria,
                GrauEscolaridade, NumeroVisitantes, FinalidadeVisita, PaisGrupo
            ) VALUES (
                @DataAgendamento, @HorarioAgendamento, @CPFResponsavel,
                @FoneResponsavel, @FoneInstituicao, @TipoVisitante, @UFGrupo,
                @CidadeGrupo, @TurmaFaixaEtaria, @GrauEscolaridade,
                @NumeroVisitantes, @FinalidadeVisita, @PaisGrupo
            )";

                SQLiteParameter[] parameters = {
            new SQLiteParameter("@DataAgendamento", dataAgendamento ?? ""),
            new SQLiteParameter("@HorarioAgendamento", horarioAgendamento ?? ""),
            new SQLiteParameter("@CPFResponsavel", cpfLimpo),
            new SQLiteParameter("@FoneResponsavel", foneResponsavel ?? ""),
            new SQLiteParameter("@FoneInstituicao", foneInstituicao ?? ""),
            new SQLiteParameter("@TipoVisitante", tipoVisitante ?? ""),
            new SQLiteParameter("@UFGrupo", ufGrupo ?? ""),
            new SQLiteParameter("@CidadeGrupo", cidadeGrupo ?? ""),
            new SQLiteParameter("@TurmaFaixaEtaria", turmaFaixaEtaria ?? ""),
            new SQLiteParameter("@GrauEscolaridade", grauEscolaridade ?? ""),
            new SQLiteParameter("@NumeroVisitantes", numeroVisitantes ?? ""),
            new SQLiteParameter("@FinalidadeVisita", finalidadeVisita ?? ""),
            new SQLiteParameter("@PaisGrupo", paisGrupo ?? "")
        };

                DatabaseHelper.ExecuteNonQuery(query, parameters);
            }
            catch (SQLiteException ex)
            {
                throw new Exception($"Erro ao inserir agendamento no banco: {ex.Message}", ex);
            }
        }

        private void LimparCampos()
        {
            TxtBoxCPF.Clear();
            TxtBoxNumResponsavel.Clear();
            maskedTextBox3.Clear();
            comboBox1.Items.Clear();
            dateTimePicker1.Value = DateTime.Today;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            HorarioAgendamento = comboBox1.SelectedItem?.ToString() ?? "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Today;
            AtualizarHorariosDisponiveis();
        }
    }
}