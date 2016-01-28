using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring
{
    [Serializable]
    public class User
    {
        [JsonProperty("Username")]
        public string Name;
        [JsonProperty("Password")]
        public string Password;
        [JsonProperty("Balance")]
        public double RemainingBalance;

        private List<User> ValidUsers;

        public User()
        {
            ValidUsers = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(@"Data\Users.json"));
        }

        public void getUserName()
        {
            UI.promptForUserName();
            Name = Console.ReadLine();
        }

        public void getUserPassword()
        {
            UI.promptForUserPassword();
            Password = Console.ReadLine();
        }

        public bool IsValidUserName()
        {
            return ValidUsers.Where(x => x.Name == Name).Count() > 0;
        }

        public bool IsValidUserPassword()
        {
            return ValidUsers.Where(x => x.Name == Name && x.Password == Password).Count() > 0;
        }

        public bool IsValidUser()
        {
            return IsValidUserName() && IsValidUserPassword();
        }

        public string GetRemainingBalanceFormatted()
        {
            return RemainingBalance.ToString("C");
        }

        public bool IsUserBalanceSufficient(Product currentProduct, int quantityRequested)
        {
            return RemainingBalance - currentProduct.getTotalCost(quantityRequested) >= 0;
        }

        public void SaveUser()
        {
            ValidUsers.Where(x => x.Name == Name && x.Password == Password).Single<User>().RemainingBalance = this.RemainingBalance;
            Program.SaveUsers(ValidUsers);
        }
    }
}
