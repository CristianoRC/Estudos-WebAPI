using System;
using System.Collections.Generic;
using System.Net;

namespace Cotacoes.Model
{
    internal static class CotacaoCSV
    {
        internal static IEnumerable<Cotacao> ListarCotacoes()
        {
            try
            {
                var listaCotacoes = new List<Cotacao>();

                var linhasCSV = ObterCSVCotacoes().Trim().Split('\n');

                if (linhasCSV.Length != 0)
                {
                    foreach (var linha in linhasCSV)
                    {
                        listaCotacoes.Add(PreencherValoresCotacao(linha));
                    }
                }


                return listaCotacoes;
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao converter o CSV de cotações em lista: {e.Message}");
            }

        }

        private static string ObterCSVCotacoes()
        {
            var data = DateTime.Now.ToString("yyyyMMdd");
            var enderecoArquivo = $"http://www4.bcb.gov.br/Download/fechamento/{data}.csv";

            try
            {
                using (var webClient = new WebClient())
                {
                    return webClient.DownloadString(enderecoArquivo);
                }
            }
            catch
            {
                return String.Empty;
            }
        }

        private static Cotacao PreencherValoresCotacao(String Linha)
        {
            string[] informacoes = Linha.Split(';'); //Colocando cada valor separado por ';' no array

            var CotacaoBase = new Cotacao();

            CotacaoBase.Data = DateTime.Now;
            CotacaoBase.CodigoMoeda = Convert.ToInt16(informacoes[1]);
            //2 E 3 --> Eram os códigos e o Tipo, mas a busca é feita via banco

            CotacaoBase.TaxaCompra = Convert.ToDecimal(informacoes[4]);
            CotacaoBase.TaxaVenda = Convert.ToDecimal(informacoes[5]);
            CotacaoBase.ParidadeCompra = Convert.ToDecimal(informacoes[6]);
            CotacaoBase.ParidadeVenda = Convert.ToDecimal(informacoes[7]);

            return CotacaoBase;
        }
    }
}