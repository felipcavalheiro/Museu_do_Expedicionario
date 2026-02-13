using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Drawing;
using System.Linq;
using System.Data;
using System.Data.SQLite;

namespace Museu_do_Expedicionário
{
    public partial class Brasileiro : Form
    {
        private Action _limparCamposPrincipal;
        private string cidadeParaSelecao;
        private bool _estaSalvando = false; // ✅ NOVO: Flag para prevenir dupla execução

        public string DataAgendamento { get; set; }
        public string CPFResponsavel { get; set; }
        public string FoneResponsavel { get; set; }
        public string FoneInstituicao { get; set; }
        public string HorarioAgendamento { get; set; }

        public class Estado { public string nome { get; set; } public string sigla { get; set; } }
        public class Cidade { public string nome { get; set; } }

        public Brasileiro()
        {
            InitializeComponent();
            this.ClientSize = new Size(1340, 787);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            comboBoxUF.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxCidade.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;

            comboBoxUF.SelectedIndexChanged += comboBoxUF_SelectedIndexChanged;
            buttonSalvar.Click += buttonSalvar_Click_1;
            button1.Click += button1_Click;
            this.Load += Brasileiro_Load;

            // ✅ Carregar estados de forma async
            _ = CarregarEstadosAsync();
            AdicionarEnterComoTab(this);
        }

        public Brasileiro(string dataAgendamento, string cpfResponsavel, string foneResponsavel,
            string foneInstituicao, string horarioAgendamento, Action limparCamposPrincipal)
            : this()
        {
            this.DataAgendamento = dataAgendamento;
            this.CPFResponsavel = cpfResponsavel;
            this.FoneResponsavel = foneResponsavel;
            this.FoneInstituicao = foneInstituicao;
            this.HorarioAgendamento = horarioAgendamento;
            this._limparCamposPrincipal = limparCamposPrincipal;
        }

        // ✅ CORREÇÃO: Async Task em vez de async void
        private async Task CarregarEstadosAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = "https://servicodados.ibge.gov.br/api/v1/localidades/estados";
                    var response = await client.GetStringAsync(url);
                    var estados = JsonConvert.DeserializeObject<List<Estado>>(response);

                    if (estados == null || estados.Count == 0)
                    {
                        MessageBox.Show("Nenhum estado encontrado.", "Aviso",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    estados.Insert(0, new Estado { nome = "Selecione a UF", sigla = "" });
                    comboBoxUF.DisplayMember = "nome";
                    comboBoxUF.ValueMember = "sigla";
                    comboBoxUF.DataSource = estados.OrderBy(e => e.nome).ToList();
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Erro de conexão ao carregar estados: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (JsonSerializationException ex)
            {
                MessageBox.Show($"Erro ao processar dados de estados: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar estados: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void comboBoxUF_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ✅ Null check
            if (comboBoxUF?.SelectedValue == null ||
                string.IsNullOrEmpty(comboBoxUF.SelectedValue.ToString()))
            {
                comboBoxCidade.DataSource = null;
                return;
            }

            string ufSigla = comboBoxUF.SelectedValue.ToString();
            string cidadePreSelecionada = cidadeParaSelecao;
            await CarregarCidadesAsync(ufSigla);

            if (!string.IsNullOrEmpty(cidadePreSelecionada))
            {
                SelecionarComboBoxText(comboBoxCidade, cidadePreSelecionada);
                cidadeParaSelecao = null;
            }
        }

        // ✅ CORREÇÃO: Async Task em vez de async void
        private async Task CarregarCidadesAsync(string uf)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = $"https://servicodados.ibge.gov.br/api/v1/localidades/estados/{uf}/municipios";
                    var response = await client.GetStringAsync(url);
                    var cidades = JsonConvert.DeserializeObject<List<Cidade>>(response);

                    if (cidades == null || cidades.Count == 0)
                    {
                        MessageBox.Show("Nenhuma cidade encontrada.", "Aviso",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    cidades.Insert(0, new Cidade { nome = "Selecione a Cidade" });
                    comboBoxCidade.DataSource = cidades.OrderBy(c => c.nome).ToList();
                    comboBoxCidade.DisplayMember = "nome";
                    comboBoxCidade.ValueMember = "nome";
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Erro de conexão ao carregar cidades: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar cidades: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool SelecionarComboBoxText(ComboBox comboBox, string text)
        {
            if (comboBox?.DataSource == null || string.IsNullOrWhiteSpace(text))
                return false;

            if (comboBox.DataSource is List<Estado> estados)
            {
                Estado estadoEncontrado = estados.FirstOrDefault(e =>
                    e.sigla.Equals(text, StringComparison.OrdinalIgnoreCase)) ??
                    estados.FirstOrDefault(e => e.nome.Equals(text, StringComparison.OrdinalIgnoreCase));

                if (estadoEncontrado != null)
                {
                    comboBox.SelectedItem = estadoEncontrado;
                    return true;
                }
            }
            else if (comboBox.DataSource is List<Cidade> cidades)
            {
                Cidade cidadeEncontrada = cidades.FirstOrDefault(c =>
                    c.nome.Equals(text, StringComparison.OrdinalIgnoreCase));

                if (cidadeEncontrada != null)
                {
                    comboBox.SelectedItem = cidadeEncontrada;
                    return true;
                }
            }
            else
            {
                comboBox.Text = text;
                return true;
            }
            return false;
        }

        private void buttonSalvar_Click_1(object sender, EventArgs e)
        {
            
            if (_estaSalvando)
                return;

            _estaSalvando = true;

            try
            {
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

                if (comboBoxUF.SelectedValue == null || string.IsNullOrEmpty(comboBoxUF.SelectedValue.ToString()))
                {
                    MessageBox.Show("Selecione a UF.", "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _estaSalvando = false;
                    return;
                }

                if (comboBoxCidade.SelectedValue == null || string.IsNullOrEmpty(comboBoxCidade.Text))
                {
                    MessageBox.Show("Selecione a cidade.", "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _estaSalvando = false;
                    return;
                }

                string ufValor = comboBoxUF.SelectedValue.ToString();
                string cidadeTexto = comboBoxCidade.Text;

                try
                {
                    // ✅ NOVO: Sempre inserir novo agendamento (sem verificar CPF)
                    // Permite múltiplos agendamentos do mesmo CPF
                    Principal.InserirNoBanco(
                        DataAgendamento, CPFResponsavel, FoneResponsavel, FoneInstituicao, HorarioAgendamento,
                        "Brasileiro", ufValor, cidadeTexto, comboBox3.Text, comboBox4.Text,
                        comboBox1.Text, comboBox2.Text, "Brasil"
                    );

                    MessageBox.Show("Dados salvos com sucesso!", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // ✅ CORREÇÃO: Invocar limpeza ANTES de fechar
                    _limparCamposPrincipal?.Invoke();

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

        private void button1_Click(object sender, EventArgs e)
        {
            // ✅ CORREÇÃO: Cancelar também precisa fechar corretamente
            this.DialogResult = DialogResult.Cancel;
            this.Close();
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

            this.CPFResponsavel = cpfLimpo;

            try
            {
                DataTable dt = DatabaseHelper.ExecuteQuery(
                    "SELECT Data, CPFResponsavel, FoneResponsavel, FoneInstituicao, Hora, UFGrupo, CidadeGrupo, TurmaFaixaEtaria, GrauEscolaridade, NumeroVisitantes, FinalidadeVisita FROM Agendamentos WHERE CPFResponsavel = @CPF",
                    new SQLiteParameter("@CPF", cpfLimpo)
                );

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    DataAgendamento = row["Data"] != DBNull.Value ? row["Data"].ToString() : "";
                    FoneResponsavel = row["FoneResponsavel"] != DBNull.Value ? row["FoneResponsavel"].ToString() : "";
                    FoneInstituicao = row["FoneInstituicao"] != DBNull.Value ? row["FoneInstituicao"].ToString() : "";
                    HorarioAgendamento = row["Hora"] != DBNull.Value ? row["Hora"].ToString() : "";

                    string ufDoBanco = row["UFGrupo"] != DBNull.Value ? row["UFGrupo"].ToString() : "";
                    string cidadeDoBanco = row["CidadeGrupo"] != DBNull.Value ? row["CidadeGrupo"].ToString() : "";

                    SelecionarComboBoxText(comboBox3, row["TurmaFaixaEtaria"] != DBNull.Value ? row["TurmaFaixaEtaria"].ToString() : "");
                    SelecionarComboBoxText(comboBox4, row["GrauEscolaridade"] != DBNull.Value ? row["GrauEscolaridade"].ToString() : "");
                    SelecionarComboBoxText(comboBox1, row["NumeroVisitantes"] != DBNull.Value ? row["NumeroVisitantes"].ToString() : "");
                    SelecionarComboBoxText(comboBox2, row["FinalidadeVisita"] != DBNull.Value ? row["FinalidadeVisita"].ToString() : "");

                    cidadeParaSelecao = cidadeDoBanco;
                    SelecionarComboBoxText(comboBoxUF, ufDoBanco);
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
                            if (ctrl is Button)
                            {
                                ((Button)ctrl).PerformClick();
                            }
                            else
                            {
                                this.SelectNextControl((Control)s, true, true, true, true);
                            }
                        }
                    };
                }
                if (ctrl.HasChildren) AdicionarEnterComoTab(ctrl);
            }
        }

        private void Brasileiro_Load(object sender, EventArgs e) { }
    }
}