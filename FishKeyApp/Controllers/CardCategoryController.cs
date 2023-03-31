using FishKeyApp.Models;
using Newtonsoft.Json;
using System.Reflection;

namespace FishKeyApp.Controllers
{
    class CardCategoryController
    {
        public List<FlashCardModel> GetCategory(string categoryName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"FishKeyApp.Resources.CardCategories.{categoryName}.json";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<List<FlashCardModel>>(result);
            }
        }

        public List<FlashCardModel> SelectedCategory(string category)
        {
            switch (category)
            {
                case "A1":
                    return GetCategory("a1");
                case "Kategoria testowa":
                    return GetCategory("Kategoria testowa");
                default:
                    return GetCategory("a1");
            }
        }

        public FlashCardModel GetRandomFlashCard(UserModel user, string category)
        {
            var flashcards = SelectedCategory(category);
            var uniqueCards = flashcards.Except(user.KnownCards).ToList();
            if (uniqueCards.Count > 0)
            {
                Random rnd = new Random();
                int index = rnd.Next(uniqueCards.Count);

                return uniqueCards[index];
            }
            else return new FlashCardModel()
            {
                Polish = "Gratulacje, poznałeś wszystkie hasła w tej kategorii",
                English = "Congratulations, you have learned all the entries in this category",
                ImgUrl = "congratulations.jpg",
                Mp3Url = string.Empty,
                Category = string.Empty
            };
        }
    }
}
