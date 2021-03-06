using System;

namespace Cotacoes.Model
{
    public class Cotacao
    {
        public DateTime Data { get; set; }
        public int CodigoMoeda { get; set; }
        public decimal TaxaCompra { get; set; }
        public decimal TaxaVenda { get; set; }
        public decimal ParidadeCompra { get; set; }
        public decimal ParidadeVenda { get; set; }


        public Cotacao(int CodigoMoeda)
        {
            var cotacaoSaida = CotacaoService.ObterUltima(CodigoMoeda);
            atualizarPropriedades(cotacaoSaida);
        }

        public Cotacao(string siglaMoeda)
        {
            var cotacaoSaida = CotacaoService.ObterUltima(siglaMoeda);

            if (cotacaoSaida.CodigoMoeda != 0)
            {
                atualizarPropriedades(cotacaoSaida);
            }
        }
        public Cotacao()
        {
        }

        private void atualizarPropriedades(Cotacao cotacaoConsulta)
        {
            this.CodigoMoeda = cotacaoConsulta.CodigoMoeda;
            this.Data = cotacaoConsulta.Data;
            this.ParidadeCompra = cotacaoConsulta.ParidadeCompra;
            this.ParidadeVenda = cotacaoConsulta.ParidadeVenda;
            this.TaxaCompra = cotacaoConsulta.TaxaCompra;
            this.TaxaVenda = cotacaoConsulta.TaxaVenda;
        }
    }
}