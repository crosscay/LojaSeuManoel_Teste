using System.Text.Json.Serialization;

namespace LojaSeuManoel.Domain.Models
{
    public class Dimensoes
    {
        [JsonPropertyName("altura")]
        public double Altura { get; set; }

        [JsonPropertyName("largura")]
        public double Largura { get; set; }

        [JsonPropertyName("comprimento")]
        public double Comprimento { get; set; }
    }
}