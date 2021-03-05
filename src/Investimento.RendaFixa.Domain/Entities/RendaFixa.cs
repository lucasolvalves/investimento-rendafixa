using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investimento.RendaFixa.Domain.Entities
{
    public class RendaFixa
    {
        public RendaFixa(double valorInvestido, double valorTotal, double quantidade, DateTime dataDeVencimento, double iof, double outrasTaxas, double taxas, string indice, 
                        string tipo, string nome, bool garantiaFGC, DateTime dataDeCompra, double precoUnitario, bool primario)
        {
            ValorInvestido = valorInvestido;
            ValorTotal = valorTotal;
            Quantidade = quantidade;
            DataDeVencimento = dataDeVencimento;
            IOF = iof;
            OutrasTaxas = outrasTaxas;
            Taxas = taxas;
            Indice = indice;
            Tipo = tipo;
            Nome = nome;
            GarantiaFGC = garantiaFGC;
            DataDeCompra = dataDeCompra;
            PrecoUnitario = precoUnitario;
            Primario = primario;

            CalcularIR();
            CalcularResgate();
        }

        public double ValorInvestido { get; private set; }
        public double ValorTotal { get; private set; }
        public double Quantidade { get; private set; }
        public DateTime DataDeVencimento { get; private set; }
        public double IOF { get; private set; }
        public double IR { get; private set; }
        public double ValorResgate { get; private set; }
        public double OutrasTaxas { get; private set; }
        public double Taxas { get; private set; }
        public string Indice { get; private set; }
        public string Tipo { get; private set; }
        public string Nome { get; private set; }
        public bool GarantiaFGC { get; private set; }
        public DateTime DataDeCompra { get; private set; }
        public double PrecoUnitario { get; private set; }
        public bool Primario { get; private set; }
        private double TaxaRentabilidade => 5;

        private double CalcularIR()
        {
            var rentabilidade = ValorTotal - ValorInvestido;

            if (rentabilidade <= 0)
                return IR = 0;

            return IR = (rentabilidade * (TaxaRentabilidade / 100));
        }

        private void CalcularResgate()
        {
            var mesesInicioVencimento = Math.Truncate(DataDeVencimento.Subtract(DataDeCompra).Days / (365.25 / 12));
            var mesesInicioAtual = Math.Truncate(DateTime.Now.Subtract(DataDeCompra).Days / (365.25 / 12));

            if ((mesesInicioVencimento - mesesInicioAtual) <= 3)
            {
                //Investimento com até 3 meses para vencer: Perde 6% do valor investido
                var desconto = (ValorTotal * ((double)6 / 100));
                ValorResgate = ValorTotal - desconto;
            }
            else if (mesesInicioAtual > (mesesInicioVencimento / 2))
            {
                //Investimento com mais da metade do tempo em custódia: Perde 15% do valor investido
                var desconto = (ValorTotal * ((double)15 / 100));
                ValorResgate = ValorTotal - desconto;
            }
            else
            {
                //Outros: Perde 30% do valor investido
                var desconto = (ValorTotal * ((double)30 / 100));
                ValorResgate = ValorTotal - desconto;
            }
        }
    }
}
