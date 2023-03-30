namespace FishKeyApp.Models
{
    public class UserModel
    {
        public string Name { get; set; }
        public string? Color { get; set; }
        public List<FlashCardModel>? KnownCards { get; set; }
    }
}