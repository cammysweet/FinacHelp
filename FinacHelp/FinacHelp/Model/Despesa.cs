using System.Collections.Generic;

namespace FinacHelp.Model
{
    public class Despesa
    {
        public int Id { get; set; }
        List<DespesaTipo> Despesas { get; set; }
        public double TotalProjetado { get; set; }
        public double TotalReal { get; set; }
        public double Variacao { get; set; }
    }
}
