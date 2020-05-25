using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using KursachV2.Models;
using KursachV2.Services.PlayerService;
using System.Text.RegularExpressions;
namespace KursachV2.Services.Validator
{
    public class MyUserValidator : IUserValidator<User>
    {
        private IPlayer _playerService;
        public MyUserValidator(IPlayer playerService)
        {
            _playerService = playerService;
        }
        public  async Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
        {
            bool Isvalid = await _playerService.IdIsValid(user.Account_id);
            List<IdentityError> errors = new List<IdentityError>();
           
            string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
           
            if (!Regex.IsMatch(user.Email, pattern, RegexOptions.IgnoreCase))
            {
                errors.Add(new IdentityError { Description = "Некоректний email" });
            }
            if (user.Email.ToLower().Contains("Hello")|| user.Email.ToLower().Contains("Admin"))
            {
                errors.Add(new IdentityError { Description = "Данна пошта недоступна." });
            }
            
            if (user.Account_id > Int32.MaxValue && user.Account_id > 0)
            {
                errors.Add(new IdentityError { Description = "Некоректний SteamID32." });
            }
            if (Isvalid == false)
            {
                errors.Add(new IdentityError { Description = "Некоректний SteamID32, перевірте чи це дійсно ваш акккаунт, корекність введених даних." });
            }
            return await Task.FromResult(errors.Count==0?
                IdentityResult.Success :
                IdentityResult.Failed(errors.ToArray()));
        }
    }
}
