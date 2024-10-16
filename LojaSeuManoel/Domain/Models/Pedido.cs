using System.Text.Json.Serialization;

namespace LojaSeuManoel.Domain.Models
{
    public class Pedido
    {
        [JsonPropertyName("pedido_id")]
        public int PedidoId { get; set; }

        [JsonPropertyName("produtos")]
        public List<ProdutoEntrada> Produtos { get; set; }
        // public List<Produto> Produtos { get; set; } = new List<Produto>();
    }
}