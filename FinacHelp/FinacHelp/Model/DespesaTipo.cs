namespace FinacHelp.Model
{
    public class DespesaTipo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Projetado { get; set; }
        public double Real { get; set; }
        public double Variacao { get; set; }
        public string Observacao { get; set; }
    }
}
