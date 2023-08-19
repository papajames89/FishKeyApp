using System.Text.Json.Serialization;

namespace FishKeyApp.Models
{
    public class FlashCardModel : IEquatable<FlashCardModel>
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

        public bool Equals(FlashCardModel other)
        {
            if (other == null) return false;
            return this.Id == other.Id && this.Category == other.Category && this.Polish == other.Polish && this.English == other.English && this.Mp3Url == other.Mp3Url && this.ImgUrl == other.ImgUrl;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            FlashCardModel cardObj = obj as FlashCardModel;
            if (cardObj == null) return false;
            else return Equals(cardObj);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
