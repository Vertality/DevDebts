using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevDebts
{
    public class Account
    {
        public Account()
        {
            Owner = "not set";
            Debts = new Dictionary<string, decimal>();
        }

        public string Owner { get; set; }
        public Dictionary<string, decimal> Debts { get; set; }
    }

    public class Bank
    {
        public List<Account> Accounts { get; set; }

        public Bank()
        {
            Accounts = new List<Account>();
        }

        public void Save()
        {
            string data = JsonConvert.SerializeObject(Accounts);
            File.WriteAllText("Bank.txt", data);
        }
        
        public static Bank Load()
        {
            Bank bank = new Bank();
            if (File.Exists("Bank.txt"))
            {
                string data = File.ReadAllText("Bank.txt");
                bank.Accounts = JsonConvert.DeserializeObject<List<Account>>(data);
            }
            return bank;
        }
    }
}
