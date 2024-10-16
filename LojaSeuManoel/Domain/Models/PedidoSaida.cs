using System.Text.Json.Serialization;
using LojaSeuManoel.Domain.Models;

namespace LojaSeuManoel.Domain.Models
{
    public class PedidoSaida
    {
        [JsonPropertyName("pedidos")]
        public List<ResultadoPedido> Pedidos { get; set; }
    }
}