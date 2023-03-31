using FishKeyApp.Models;
using FishKeyApp.Views;
using Newtonsoft.Json;

namespace FishKeyApp.Controllers
{
    public class DatabaseController
    {
        private readonly string _dataDir = FileSystem.Current.AppDataDirectory;
        private const int MaxUsers = 10;
        private const string Extension = ".json";
        private const string Ok = "Ok";
        private const string UserExistAlert = "Wprowadzona nazwa użytwonika już istnieje, podaj inną nazwę użytkownika";
        private const string ToManyUsersAlert = "Zbyt duża liczba użytkowników.Aby utworzyć kolejnego użytkownika, usuń jednego z instniejących.";
        private const string Alert = "Uwaga";

        public Task CreateDatabase(string name)
        {
            var totalUsers = Directory.GetFiles(_dataDir, "*.json").ToList().Count();
            var fileName = $"{name}{Extension}";
            var localPath = Path.Combine(_dataDir, fileName);

            if (File.Exists(localPath) || string.IsNullOrEmpty(name))
            {
                return Application.Current.MainPage.DisplayAlert(Alert, UserExistAlert, Ok);
            }
            else if (totalUsers >= MaxUsers)
            {
                return Application.Current.MainPage.DisplayAlert(Alert, ToManyUsersAlert, Ok);
            }

            var user = new UserModel()
            {
                Name = name,
                Color = $"{name}.jpg",
                KnownCards = new List<FlashCardModel>()
            };

            string jsonString = JsonConvert.SerializeObject(user);

            File.WriteAllText(localPath, jsonString);

            return Shell.Current.GoToAsync($"{nameof(DashboardPage)}?User={name}");
        }

        public List<UserModel> GetListOfUsers()
        {
            var jsonFilesPaths = Directory.GetFiles(_dataDir, "*.json").ToList();
            var users = new List<UserModel>();
            foreach (var path in jsonFilesPaths)
            {
                users.Add(JsonConvert.DeserializeObject<UserModel>(File.ReadAllText(path)));
            }
            return users;
        }

        public UserModel GetUser(string name)
        {
            var userPath = Directory.GetFiles(_dataDir, "*.json")
                .Where(path => Path.GetFileName(path)
                .Equals(name, StringComparison.OrdinalIgnoreCase))
                .First();

            return JsonConvert.DeserializeObject<UserModel>(userPath);
        }

        public void RemoveUser(string name)
        {
            var fileName = $"{name}.json";
            var localPath = Path.Combine(_dataDir, fileName);

            File.Delete(localPath);
        }

        public void RemoveAllUsers()
        {
            var jsonFilesPaths = Directory.GetFiles(_dataDir, "*.json").ToList();
            foreach (var path in jsonFilesPaths)
            {
                File.Delete(path);
            }
        }
    }
}
