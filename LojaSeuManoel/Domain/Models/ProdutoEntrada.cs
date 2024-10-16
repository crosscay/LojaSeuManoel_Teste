using System.Text.Json.Serialization;

namespace LojaSeuManoel.Domain.Models
{
    public class ProdutoEntrada
    {
        [JsonPropertyName("produto_id")]
        public string ProdutoId { get; set; }

        [JsonPropertyName("dimensoes")]
        public Dimensoes Dimensoes { get; set; }
    }
}
