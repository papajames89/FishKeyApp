using FishKeyApp.Models;
using Newtonsoft.Json;
using System.Reflection;

namespace FishKeyApp.Controllers
{
    class CardCategoryController
    {
        private readonly DatabaseController _databaseController;
        private const string Alert = "Uwaga";
        private const string CategoryReset = "Kategoria została przywrócona do stanu początkowego";
        private const string Ok = "Ok";
        public CardCategoryController()
        {
            _databaseController = new DatabaseController();
        }
        public List<FlashCardModel> GetCategory(string categoryName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"FishKeyApp.Resources.CardCategories.{categoryName}.json";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<List<FlashCardModel>>(result).Where(x => x.ImgUrl != "nophoto.jpg").ToList();
            }
        }

        public List<FlashCardModel> SelectedCategory(string category)
        {
            switch (category)
            {
                case "A1":
                    return GetCategory("a1");
                case "A2":
                    return GetCategory("a2");
                case "B1":
                    return GetCategory("b1");
                case "B2":
                    return GetCategory("b2");
                case "Czesci ciala":
                    return GetCategory("Czesci ciala");
                case "Na lotnisku":
                    return GetCategory("Na lotnisku");
                case "Jedzenie i picie":
                    return GetCategory("Jedzenie i picie");
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

        public double GetCategoryProgress(UserModel user, string category)
        {
            return (double)user.KnownCards.Where(c => c.Category.ToLower() == category.ToLower()).Count() / (double)SelectedCategory(category).Count();
        }

        public void ResetCategoryProgress(string userName, string category)
        {
            var user = _databaseController.GetUser(userName);
            if (user.KnownCards.Count == 0)
            {
                return; // user has no known cards
            }
            user.KnownCards.RemoveAll(card => card.Category.ToLower() == category.ToLower());

            _databaseController.SaveUser(user);
            Application.Current.MainPage.DisplayAlert(Alert, CategoryReset, Ok);
        }
    }
}
