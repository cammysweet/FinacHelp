namespace FinacHelp.Model
{
    public class FluxoCaixa
    {
        public int Id { get; set; }
        public Receita ReceitaTotal { get; set; }
        public Despesa DespesaTotal { get; set; }
        public double Projetado { get; set; }
        public double Real { get; set; }
        public double Variacao { get; set; }
    }
}
