using System.Text.Json.Serialization;

namespace LojaSeuManoel.Domain.Models
{
    public class PedidoEntrada
    {
        [JsonPropertyName("pedidos")]
        public List<Pedido> Pedidos { get; set; }
    }
}