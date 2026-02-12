using System;
using System.Linq;
using System.Net.Mail;

namespace Museu_do_Expedicionário
{
    /// <summary>
    /// Classe estática que centraliza todas as validações do sistema
    /// </summary>
    public static class ValidationHelper
    {
        /// <summary>
        /// Valida formato de email usando MailAddress do .NET
        /// </summary>
        /// <param name="email">Email a validar</param>
        /// <returns>true se email é válido, false caso contrário</returns>
        public static bool ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // ✅ Usa a classe MailAddress que já valida formato de email
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                // Se lançar exceção, email é inválido
                return false;
            }
        }

        /// <summary>
        /// Valida senha com requisitos:
        /// - Mínimo 6 caracteres
        /// - Contém pelo menos uma letra
        /// - Contém pelo menos um número
        /// </summary>
        /// <param name="senha">Senha a validar</param>
        /// <returns>true se senha atende aos requisitos, false caso contrário</returns>
        public static bool ValidarSenha(string senha)
        {
            // Verificar se é nula ou vazia
            if (string.IsNullOrWhiteSpace(senha))
                return false;

            // Verificar mínimo de 6 caracteres
            if (senha.Length < 6)
                return false;

            // Verificar se contém letra (maiúscula ou minúscula)
            if (!senha.Any(char.IsLetter))
                return false;

            // Verificar se contém número
            if (!senha.Any(char.IsDigit))
                return false;

            return true;
        }

        /// <summary>
        /// Valida CPF verificando:
        /// - 11 dígitos
        /// - Não todos os dígitos iguais
        /// - Dígitos verificadores corretos
        /// </summary>
        /// <param name="cpf">CPF a validar (com ou sem pontuação)</param>
        /// <returns>true se CPF é válido, false caso contrário</returns>
        public static bool ValidarCPF(string cpf)
        {
            // Verificar se é nulo ou vazio
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            // Remove tudo que não é dígito
            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            // Deve ter exatamente 11 dígitos
            if (cpf.Length != 11)
                return false;

            // Não pode ter todos os dígitos iguais (ex: 111.111.111-11)
            if (cpf.Distinct().Count() == 1)
                return false;

            // Cálculo do primeiro dígito verificador
            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(cpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;
            string digito1 = resto.ToString();

            // Cálculo do segundo dígito verificador
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(cpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;
            string digito2 = resto.ToString();

            // Verifica se os dígitos calculados coincidem com os do CPF
            return cpf.EndsWith(digito1 + digito2);
        }

        /// <summary>
        /// Remove tudo que não é dígito do CPF
        /// </summary>
        /// <param name="cpf">CPF com pontuação</param>
        /// <returns>CPF apenas com dígitos</returns>
        public static string LimparCPF(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return string.Empty;

            return new string(cpf.Where(char.IsDigit).ToArray());
        }

        /// <summary>
        /// Valida se uma data é válida e não é no passado
        /// </summary>
        /// <param name="data">Data em formato string (yyyy-MM-dd)</param>
        /// <returns>true se data é válida e no futuro, false caso contrário</returns>
        public static bool ValidarData(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
                return false;

            if (!DateTime.TryParse(data, out DateTime dataParsed))
                return false;

            // Data não pode ser no passado
            if (dataParsed < DateTime.Today)
                return false;

            return true;
        }

        /// <summary>
        /// Valida formato de telefone brasileiro
        /// </summary>
        /// <param name="telefone">Telefone com ou sem formatação</param>
        /// <returns>true se telefone é válido, false caso contrário</returns>
        public static bool ValidarTelefone(string telefone)
        {
            if (string.IsNullOrWhiteSpace(telefone))
                return false;

            // Remove tudo que não é dígito
            string apenasDigitos = new string(telefone.Where(char.IsDigit).ToArray());

            // Telefone brasileiro tem 10 ou 11 dígitos
            // 10: (XX) XXXX-XXXX
            // 11: (XX) XXXXX-XXXX
            return apenasDigitos.Length == 10 || apenasDigitos.Length == 11;
        }

        /// <summary>
        /// Limpa um telefone removendo formatação
        /// </summary>
        /// <param name="telefone">Telefone formatado</param>
        /// <returns>Telefone apenas com dígitos</returns>
        public static string LimparTelefone(string telefone)
        {
            if (string.IsNullOrEmpty(telefone))
                return string.Empty;

            return new string(telefone.Where(char.IsDigit).ToArray());
        }

        /// <summary>
        /// Valida se uma string não está vazia ou nula
        /// </summary>
        /// <param name="texto">Texto a validar</param>
        /// <param name="nomeCampo">Nome do campo para mensagem de erro</param>
        /// <returns>true se texto é válido, false caso contrário</returns>
        public static bool ValidarCampoObrigatorio(string texto, string nomeCampo = "Campo")
        {
            if (string.IsNullOrWhiteSpace(texto))
            {
                return false;
            }
            return true;
        }
    }
}