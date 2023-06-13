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
        private const string UserExistAlert = "User with this name already exists, change the username";
        private const string ToManyUsersAlert = "Too many users. To create a new user, remove one of the existing ones.";
        private const string DeleteAllUsersAlert = "Are you sure you want to delete all users?";
        private const string DeleteUserAlert = "Are you sure you want to delete this users?";
        private const string Alert = "Attention";
        private const string Yes = "Yes";
        private const string No = "No";

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
            var userPath = Directory.GetFiles(_dataDir, "*.json");
            foreach (var path in userPath)
            {
                if (path.Contains(name))
                {
                    return JsonConvert.DeserializeObject<UserModel>(File.ReadAllText(path));
                }
            }

            return new UserModel();
        }

        public UserModel UpdateUser(UserModel user, FlashCardModel card)
        {
            if (user.KnownCards.Contains(card))
            {
                return user;
            }
            user.KnownCards.Add(card);
            return user;
        }

        public void SaveUser(UserModel user)
        {
            var fileName = $"{user.Name}{Extension}";
            var localPath = Path.Combine(_dataDir, fileName);
            var serializedUser = JsonConvert.SerializeObject(user);

            File.WriteAllText(localPath, serializedUser);
        }

        public async Task RemoveUser(string name)
        {
            var result = await App.Current.MainPage.DisplayAlert(Alert, DeleteUserAlert, Yes, No);
            if (result)
            {
                var fileName = $"{name}.json";
                var localPath = Path.Combine(_dataDir, fileName);

                File.Delete(localPath);
            }
        }

        public async Task RemoveAllUsers()
        {
            var result = await App.Current.MainPage.DisplayAlert(Alert, DeleteAllUsersAlert, Yes, No);
            if (result)
            {
                var jsonFilesPaths = Directory.GetFiles(_dataDir, "*.json").ToList();
                foreach (var path in jsonFilesPaths)
                {
                    File.Delete(path);
                }
            }
        }
    }
}
