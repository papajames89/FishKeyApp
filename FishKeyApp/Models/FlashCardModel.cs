using System.Text.Json.Serialization;

namespace FishKeyApp.Models
{
    public class FlashCardModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("polish")]
        public string Polish { get; set; }

        [JsonPropertyName("english")]
        public string English { get; set; }

        [JsonPropertyName("mp3Url")]
        public string Mp3Url { get; set; }

        [JsonPropertyName("imgUrl")]
        public string ImgUrl { get; set; }
    }
}
