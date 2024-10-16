using System.Text.Json.Serialization;

namespace LojaSeuManoel.Domain.Models
{
    public class CaixaSaida
    {
        [JsonPropertyName("caixa_id")]
        public string CaixaId { get; set; }


        [JsonPropertyName("produtos")]
        public List<string> Produtos { get; set; }

        
        [JsonPropertyName("observacao")]
        public string Observacao { get; set; } // Esto puede ser null si no hay observaciones
    }
}