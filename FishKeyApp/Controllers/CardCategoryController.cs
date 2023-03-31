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

        public void SelectedCategory(string category)
        {
            switch (category)
            {
                case "A1":
                    GetCategory("a1");
                    break;
                case "A2":
                    GetCategory("a2");
                    break;
            }
        }
    }
}
