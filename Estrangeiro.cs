using System;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace Museu_do_Expedicionário
{
    public partial class Estrangeiro : Form
    {
        private bool _estaSalvando = false; // ✅ NOVO: Flag para prevenir dupla execução

        public string DataAgendamento { get; set; }
        public string CPFResponsavel { get; set; }
        public string FoneResponsavel { get; set; }
        public string FoneInstituicao { get; set; }
        public string HorarioAgendamento { get; set; }

        public Estrangeiro()
        {
            InitializeComponent();

            this.ClientSize = new Size(1189, 693);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            // ComboBoxes
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox6.DropDownStyle = ComboBoxStyle.DropDownList;
            if (comboBox7 != null) comboBox7.DropDownStyle = ComboBoxStyle.DropDownList;

            // Eventos
            button1.Click += buttonSalvar_Click;
            button2.Click += button2_Click;
            this.Load += Estrangeiro_Load;

            // Enter como Tab
            AdicionarEnterComoTab(this);
        }

        public Estrangeiro(string dataAgendamento, string cpfResponsavel, string foneResponsavel, string foneInstituicao, string horarioAgendamento)
            : this()
        {
            this.DataAgendamento = dataAgendamento;
            this.CPFResponsavel = cpfResponsavel;
            this.FoneResponsavel = foneResponsavel;
            this.FoneInstituicao = foneInstituicao;
            this.HorarioAgendamento = horarioAgendamento;
        }

        public void CarregarAgendamento(string cpf)
        {
            string cpfLimpo = ValidationHelper.LimparCPF(cpf);
            if (string.IsNullOrEmpty(cpfLimpo))
            {
                MessageBox.Show("CPF inválido.", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            CPFResponsavel = cpfLimpo;

            try
            {
                DataTable dt = DatabaseHelper.ExecuteQuery(
                    "SELECT Data, CPFResponsavel, FoneResponsavel, FoneInstituicao, Hora, UFGrupo, CidadeGrupo, TurmaFaixaEtaria, GrauEscolaridade, NumeroVisitantes, FinalidadeVisita, PaisGrupo FROM Agendamentos WHERE CPFResponsavel = @CPF",
                    new SQLiteParameter("@CPF", cpfLimpo)
                );

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    string dataStr = row["Data"].ToString();
                    if (!DateTime.TryParse(dataStr, out DateTime dataParsed))
                        DataAgendamento = DateTime.Today.ToString("yyyy-MM-dd");
                    else
                        DataAgendamento = dataParsed.ToString("yyyy-MM-dd");

                    CPFResponsavel = row["CPFResponsavel"].ToString();
                    FoneResponsavel = row["FoneResponsavel"].ToString();
                    FoneInstituicao = row["FoneInstituicao"].ToString();
                    HorarioAgendamento = row["Hora"].ToString();

                    comboBox4.Text = row["FinalidadeVisita"].ToString();
                    textBox2.Text = row["CidadeGrupo"].ToString();
                    comboBox2.Text = row["TurmaFaixaEtaria"].ToString();
                    comboBox6.Text = row["GrauEscolaridade"].ToString();
                    comboBox7.Text = row["NumeroVisitantes"].ToString();
                    textBox1.Text = row["PaisGrupo"].ToString();
                }
                else
                {
                    MessageBox.Show($"Agendamento não encontrado para CPF: {cpfLimpo}", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar agendamento: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSalvar_Click(object sender, EventArgs e)
        {
            // ✅ NOVO: Prevenir dupla execução
            if (_estaSalvando)
                return;

            _estaSalvando = true;

            try
            {
                if (string.IsNullOrWhiteSpace(CPFResponsavel))
                {
                    MessageBox.Show("O CPF do responsável deve ser informado.", "Atenção",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _estaSalvando = false;
                    return;
                }

                // ✅ Validações
                if (string.IsNullOrWhiteSpace(DataAgendamento))
                {
                    MessageBox.Show("Data é obrigatória.", "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _estaSalvando = false;
                    return;
                }

                if (string.IsNullOrWhiteSpace(HorarioAgendamento))
                {
                    MessageBox.Show("Horário é obrigatório.", "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _estaSalvando = false;
                    return;
                }

                string cpfLimpo = ValidationHelper.LimparCPF(CPFResponsavel);
                if (!ValidationHelper.ValidarCPF(cpfLimpo))
                {
                    MessageBox.Show("CPF inválido.", "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _estaSalvando = false;
                    return;
                }

                CPFResponsavel = cpfLimpo;

                try
                {
                    // ✅ NOVO: Sempre inserir novo agendamento (sem verificar CPF)
                    // Permite múltiplos agendamentos do mesmo CPF
                    Principal.InserirNoBanco(
                        DataAgendamento ?? DateTime.Today.ToString("yyyy-MM-dd"),
                        CPFResponsavel,
                        FoneResponsavel,
                        FoneInstituicao,
                        HorarioAgendamento,
                        "Estrangeiro",
                        null,  // UF não aplicável para estrangeiro
                        textBox2.Text,
                        comboBox2.Text,
                        comboBox6.Text,
                        comboBox7.Text,
                        comboBox4.Text,
                        textBox1.Text
                    );

                    MessageBox.Show("Dados salvos com sucesso!", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // ✅ CORREÇÃO: Setar DialogResult e fechar
                    this.DialogResult = DialogResult.OK;
                    this.Close();

                    // ✅ Saída segura
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao salvar: {ex.Message}", "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _estaSalvando = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro interno: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                _estaSalvando = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // ✅ CORREÇÃO: Fechar corretamente
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Deixe vazio
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // Deixe vazio
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Deixe vazio
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Deixe vazio
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Deixe vazio
        }

        private void Estrangeiro_Load(object sender, EventArgs e) { }

        private void AdicionarEnterComoTab(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is TextBox || ctrl is MaskedTextBox || ctrl is ComboBox)
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}