using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DevDebts.Core
{
    public class Bank
    {
        public List<Account> Accounts { get; set; }

        public Bank()
        {
            Accounts = new List<Account>();
        }

        public void Save(string filename)
        {
            string data = JsonConvert.SerializeObject(Accounts);
            File.WriteAllText(filename, data);
        }

        public static Bank Load(string filename)
        {
            Bank bank = new Bank();
            if (File.Exists(filename))
            {
                string data = File.ReadAllText(filename);
                bank.Accounts = JsonConvert.DeserializeObject<List<Account>>(data);
            }
            return bank;
        }
        public string Print()
        {
            StringBuilder Sb = new StringBuilder();
            foreach (Account account in this.Accounts)
            {
                Sb.AppendLine(account.Owner);
                foreach (var debt in account.Debts)
                {
                    if (debt.Value != 0)
                    {
                        Sb.AppendLine($"    owes {debt.Key} {debt.Value}");
                    }
                }
            }
            return Sb.ToString();
        }
        private Account GetOrAddAccount(string name)
        {
            Account resultAccount = null;

            foreach (Account account in Accounts)
            {
                if (account.Owner == name)
                {
                    resultAccount = account;
                }
            }
            if (resultAccount == null)
            {
                resultAccount = new Account();
                resultAccount.Owner = name;
                Accounts.Add(resultAccount);
            }

            return resultAccount;
        }
        public void DoTransaction(string[] payers, string recipiant, string action, decimal amount)
        {
            foreach (string payer in payers)
            {
                Account account = GetOrAddAccount(payer);
                bool hasExistingDebt = account.Debts.ContainsKey(recipiant);

                if (!hasExistingDebt)
                {
                    account.Debts.Add(recipiant, 0);
                }

                if (action == "owes")
                {
                    account.Debts[recipiant] += amount;
                }

                if (action == "gave")
                {
                    if (account.Debts[recipiant] - amount < 0)
                    {
                       throw new Exception($"{payer} owes less than that to {recipiant}");
                    }
                    else
                    {
                        account.Debts[recipiant] -= amount;
                    }
                }

                //Console.WriteLine("{0} now owes {1} {2}", payer, recipiant, account.Debts[recipiant]);
            }
        }

    }
}
