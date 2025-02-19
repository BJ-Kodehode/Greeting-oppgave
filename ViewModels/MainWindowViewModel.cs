using System;
using System.Collections.Generic;
using System.Reactive;
using ReactiveUI;
using GreetingApp.Services;

namespace GreetingApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly UserService _userService = new UserService();
        private string _userName = string.Empty;  // Løser advarsel
        private string _greetingMessage = string.Empty;  // Løser advarsel
        private List<User> _users = new();  // Løser advarsel

        public MainWindowViewModel()
        {
            Users = _userService.LoadUsers();
            AddUserCommand = ReactiveCommand.Create(AddUser);
        }

        // UserName property (bindable)
        public string UserName
        {
            get => _userName;
            set => this.RaiseAndSetIfChanged(ref _userName, value);
        }

        // GreetingMessage property (bindable)
        public string GreetingMessage
        {
            get => _greetingMessage;
            set => this.RaiseAndSetIfChanged(ref _greetingMessage, value);
        }

        // Users property (bindable)
        public List<User> Users
        {
            get => _users;
            set => this.RaiseAndSetIfChanged(ref _users, value);
        }

        // Command for adding user
        public ReactiveCommand<Unit, Unit> AddUserCommand { get; }

        private void AddUser()
        {
            var trimmedName = UserName?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(trimmedName))
                return;

            var existingUser = Users.Find(u => u.Name.Equals(trimmedName, StringComparison.OrdinalIgnoreCase));
            var now = DateTime.Now;

            if (existingUser != null)
            {
                existingUser.LastGreetingTime = now;
            }
            else
            {
                Users.Add(new User { Name = trimmedName, LastGreetingTime = now });
            }

            UpdateGreeting(now, trimmedName); // Pass trimmedName to UpdateGreeting
            _userService.SaveUsers(Users);
            UserName = string.Empty;
        }

        private void UpdateGreeting(DateTime time, string userName)
        {
            var hour = time.Hour;
            var greeting = hour < 12 ? "Good morning" :
                           hour < 18 ? "Good afternoon" : "Good evening";
            GreetingMessage = $"{greeting}, {userName}!";
        }
    }
}
