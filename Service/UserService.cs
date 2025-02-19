using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace GreetingApp.Services
{
    public class UserService
    {
        private const string FilePath = "users.json";

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
