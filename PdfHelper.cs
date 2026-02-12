using System;
using System.Data;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Drawing.Printing;
using System.Xml.Linq;

namespace Museu_do_Expedicionário
{
    /// <summary>
    /// ✅ NOVO: Classe para gerar PDFs dos agendamentos
    /// </summary>
    public static class PdfHelper
    {
        public static void GerarPDFAgendamento(DataGridViewRow row, string caminhoArquivo)
        {
            try
            {
                Document doc = new Document(PageSize.A4);
                PdfWriter.GetInstance(doc, new FileStream(caminhoArquivo, FileMode.Create));
                doc.Open();

                // ✅ Cabeçalho
                Paragraph titulo = new Paragraph("AGENDAMENTO - MUSEU DO EXPEDICIONÁRIO",
                    FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16));
                titulo.Alignment = Element.ALIGN_CENTER;
                doc.Add(titulo);

                doc.Add(new Paragraph("\n"));

                // ✅ Linha separadora
                doc.Add(new Paragraph("_________________________________________________________________________________\n"));

                // ✅ Dados do agendamento
                PdfPTable table = new PdfPTable(2);
                table.WidthPercentage = 100;

                AdicionarLinhaTabela(table, "Data do Agendamento:",
                    row.Cells["DataAgendamento"].Value?.ToString() ?? "");
                AdicionarLinhaTabela(table, "Horário:",
                    row.Cells["HorarioAgendamento"].Value?.ToString() ?? "");
                AdicionarLinhaTabela(table, "CPF Responsável:",
                    row.Cells["CPFResponsavel"].Value?.ToString() ?? "");
                AdicionarLinhaTabela(table, "Tipo de Visitante:",
                    row.Cells["TipoVisitante"].Value?.ToString() ?? "");
                AdicionarLinhaTabela(table, "Telefone Responsável:",
                    row.Cells["FoneResponsavel"].Value?.ToString() ?? "");
                AdicionarLinhaTabela(table, "Telefone Instituição:",
                    row.Cells["FoneInstituicao"].Value?.ToString() ?? "");
                AdicionarLinhaTabela(table, "UF/País:",
                    row.Cells["UFGrupo"].Value?.ToString() ?? "" + " / " + row.Cells["PaisGrupo"].Value?.ToString() ?? "");
                AdicionarLinhaTabela(table, "Cidade:",
                    row.Cells["CidadeGrupo"].Value?.ToString() ?? "");
                AdicionarLinhaTabela(table, "Faixa Etária:",
                    row.Cells["TurmaFaixaEtaria"].Value?.ToString() ?? "");
                AdicionarLinhaTabela(table, "Grau de Escolaridade:",
                    row.Cells["GrauEscolaridade"].Value?.ToString() ?? "");
                AdicionarLinhaTabela(table, "Número de Visitantes:",
                    row.Cells["NumeroVisitantes"].Value?.ToString() ?? "");
                AdicionarLinhaTabela(table, "Finalidade da Visita:",
                    row.Cells["FinalidadeVisita"].Value?.ToString() ?? "");

                doc.Add(table);

                // ✅ Rodapé
                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph("_________________________________________________________________________________\n"));
                Paragraph rodape = new Paragraph($"Gerado em: {DateTime.Now:dd/MM/yyyy HH:mm:ss}",
                    FontFactory.GetFont(FontFactory.HELVETICA, 8));
                rodape.Alignment = Element.ALIGN_RIGHT;
                doc.Add(rodape);

                doc.Close();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao gerar PDF: {ex.Message}", ex);
            }
        }

        public static void GerarPDFTodos(DataTable dt, string caminhoArquivo)
        {
            try
            {
                Document doc = new Document(PageSize.A4.Rotate()); // Paisagem
                PdfWriter.GetInstance(doc, new FileStream(caminhoArquivo, FileMode.Create));
                doc.Open();

                // ✅ Cabeçalho
                Paragraph titulo = new Paragraph("RELATÓRIO DE AGENDAMENTOS - MUSEU DO EXPEDICIONÁRIO",
                    FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16));
                titulo.Alignment = Element.ALIGN_CENTER;
                doc.Add(titulo);

                Paragraph data = new Paragraph($"Gerado em: {DateTime.Now:dd/MM/yyyy HH:mm:ss}",
                    FontFactory.GetFont(FontFactory.HELVETICA, 10));
                data.Alignment = Element.ALIGN_RIGHT;
                doc.Add(data);

                doc.Add(new Paragraph("\n"));

                // ✅ Tabela com todos os agendamentos
                PdfPTable table = new PdfPTable(dt.Columns.Count);
                table.WidthPercentage = 100;

                // ✅ Cabeçalho da tabela
                foreach (DataColumn col in dt.Columns)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(col.ColumnName,
                        FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8)));
                    cell.BackgroundColor = new BaseColor(200, 200, 200);
                    table.AddCell(cell);
                }

                // ✅ Linhas da tabela
                foreach (DataRow row in dt.Rows)
                {
                    foreach (DataColumn col in dt.Columns)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(row[col.ColumnName]?.ToString() ?? "",
                            FontFactory.GetFont(FontFactory.HELVETICA, 7)));
                        table.AddCell(cell);
                    }
                }

                doc.Add(table);

                // ✅ Resumo
                doc.Add(new Paragraph("\n"));
                Paragraph resumo = new Paragraph($"Total de Agendamentos: {dt.Rows.Count}",
                    FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10));
                doc.Add(resumo);

                doc.Close();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao gerar PDF: {ex.Message}", ex);
            }
        }

        // ✅ Helper: Adicionar linha na tabela
        private static void AdicionarLinhaTabela(PdfPTable table, string chave, string valor)
        {
            PdfPCell cellChave = new PdfPCell(new Phrase(chave,
                FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10)));
            cellChave.BackgroundColor = new BaseColor(230, 230, 230);
            table.AddCell(cellChave);

            PdfPCell cellValor = new PdfPCell(new Phrase(valor,
                FontFactory.GetFont(FontFactory.HELVETICA, 10)));
            table.AddCell(cellValor);
        }
    }
}