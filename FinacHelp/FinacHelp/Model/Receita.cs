using System.Collections.Generic;

namespace FinacHelp.Model
{
    public class Receita
    {
        public int Id { get; set; }
        public List<ReceitaTipo> Receitas { get; set; }
        public double TotalProjetado { get; set; }
        public double TotalReal { get; set; }
        public double Variacao { get; set; }

    }
}
