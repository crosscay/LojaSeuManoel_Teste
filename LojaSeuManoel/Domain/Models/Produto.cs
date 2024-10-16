using LojaSeuManoel.Domain.Models;

namespace LojaSeuManoel.Domain.Models
{
    public class Produto
    {
        public string ProdutoId { get; set; }  // Cambiado de Nome a ProdutoId
        public double Altura { get; set; }
        public double Largura { get; set; }
        public double Comprimento { get; set; }
    }
}