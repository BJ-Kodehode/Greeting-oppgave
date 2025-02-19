using System;

namespace GreetingApp
{
    public class User
    {
        public string Name { get; set; } = string.Empty;  // Sikrer at Name ikke er null
        public DateTime LastGreetingTime { get; set; }
    }
}
