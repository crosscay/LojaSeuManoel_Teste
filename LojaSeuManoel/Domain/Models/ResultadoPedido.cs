using System.Text.Json.Serialization;
using LojaSeuManoel.Domain.Models;

namespace LojaSeuManoel.Domain.Models
{
    public class ResultadoPedido
    {
        [JsonPropertyName("pedido_id")]
        public int PedidoId { get; set; }

        [JsonPropertyName("caixas")]
        public List<CaixaSaida> Caixas { get; set; }
    }
}