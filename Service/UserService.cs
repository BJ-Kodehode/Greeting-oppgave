using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace GreetingApp.Services
{
    public class UserService
    {
        private static readonly string FilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "GreetingApp",
            "users.json"
        );

        public UserService()
        {
            var directory = Path.GetDirectoryName(FilePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        public List<User> LoadUsers()
        {
            if (!File.Exists(FilePath))
                return new List<User>();
            var json = File.ReadAllText(FilePath);
            return JsonConvert.DeserializeObject<List<User>>(json) ?? new List<User>();
        }

        public void SaveUsers(List<User> users)
        {
            var json = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }
    }
}