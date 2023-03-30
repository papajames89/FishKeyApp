using Bumptech.Glide.Load.Engine;
using FishKeyApp.Models;
using Newtonsoft.Json;

namespace FishKeyApp.Controllers
{
    public class DatabaseController
    {
        private readonly string _dataDir = FileSystem.Current.AppDataDirectory;
        private const string Extension = ".json";

        public void CreateDatabase(string name)
        {
            var fileName = $"{name}{Extension}";
            var localPath = Path.Combine(_dataDir, fileName);

            if (File.Exists(localPath) || string.IsNullOrEmpty(name))
            {
                return;
            }

            var user = new UserModel()
            {
                Name = name,
                Color = $"{name}.jpg",
                KnownCards= new List<FlashCardModel>()
            };

            string jsonString = JsonConvert.SerializeObject(user);

           File.WriteAllText(localPath, jsonString);
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
