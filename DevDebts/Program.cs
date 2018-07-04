using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoLibrary;
using Newtonsoft.Json;

namespace DevDebts
{
    public class Program
    {
        static Bank theBank = Bank.Load();

        public static void Main(string[] args)
        {
            //"llwyd owes alan £4.99 for subway"
            //"llwyd gave alan £2"
            List<string> names;
            

            string data = File.ReadAllText("names.txt");
            names = JsonConvert.DeserializeObject<List<string>>(data);

            List<string> actions = new List<string>();
            actions.Add("owes");
            actions.Add("gave");

            Console.WriteLine("Welcome");

            string input;
            do
            {

                input = Console.ReadLine();

                if (input == "clear")
                {
                    Console.Clear();
                    continue;
                }
                if (input == "hello world")
                {
                    Console.WriteLine("nerd");
                    continue;
                }
                if (input == "UwU")
                {
                    Console.WriteLine("Get Out");
                    continue;
                }
                if (input == "printnames")
                {
                    PrintNames(names);
                    continue;
                }


                if (input == "help")
                {
                    Console.WriteLine("commands:");
                    Console.WriteLine("printnames - Print Avalible Names");
                    Console.WriteLine("hello world - :)");
                    Console.WriteLine("show all- show all debts");
                    Console.WriteLine("clear- clears the console");
                    continue;
                }

                if (input == "show all")
                {
                    PrintAll();
                    continue;
                }

                string[] parameters = input.Split(' ');

                if (parameters.Length == 2)
                {
                    if (parameters[0] == "adduser")
                    {
                        names.Add(parameters[1]);
                        continue;
                    }
                }

                if (parameters.Length != 4)
                {
                    Console.WriteLine("Use: [Name] [Action] [Name] [Amount] or type help for  other commands");
                    continue;
                }

                string[] payerNames = parameters[0].Split(',');

                bool validNames = true;
                foreach (string payer in payerNames)
                {
                    if (IsValidName(payer, names) != true)
                    {
                        Console.WriteLine("Invaild First Name.Valid Names Are :");
                        validNames = false;
                        PrintNames(names);
                    }
                }

                if (!validNames)
                {
                    continue;
                }

                if (IsValidName(parameters[2], names) != true)
                {
                    Console.WriteLine("Invaild Second Name.Valid Names Are :");
                    PrintNames(names);
                    continue;
                }
                if (IsValidAction(parameters[1], actions) != true)
                {
                    Console.WriteLine("Invaild Action.Valid Actions Are :");
                    Printactions(actions);
                    continue;

                }
                if (parameters[0] == parameters[2])
                {
                    Console.WriteLine("First Name Cannot Be Second Name");
                    continue;
                }

                decimal amount = 0;
                if (!decimal.TryParse(parameters[3], out amount) || amount <= 0)
                {
                    Console.WriteLine("Invaild Number");
                    continue;
                }

                TheThing(payerNames, parameters[2], parameters[1], amount);

            } while (input != "exit");
            theBank.Save();
            string namedata = JsonConvert.SerializeObject(names);
            File.WriteAllText("names.txt", namedata);
        }

        public static bool IsValidName(string nameToCheck, List<string> validNames)
        {
            foreach (string name in validNames)
            {
                if (name == nameToCheck)
                {
                    return true;
                }
            }
            return false;
        }

        public static void PrintNames(List<string> validNames)
        {
            foreach (string name in validNames)
            {
                Console.WriteLine(name);
            }
        }

        public static bool IsValidAction(string actionToCheck, List<string> validActions)
        {
            foreach (string action in validActions)
            {
                if (action == actionToCheck)
                {
                    return true;
                }
            }
            return false;
        }
        public static void Printactions(List<string> validActions)
        {
            foreach (string action in validActions)
            {
                Console.WriteLine(action);
            }
        }

        public static void TheThing(string[] payers, string recipiant, string action, decimal amount)
        {
            foreach (string payer in payers)
            {
                Account account = GetOrAddAccount(payer, theBank.Accounts);
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
                        Console.WriteLine("{0} owes less than that to {1}", payer, recipiant);
                    }
                    else
                    {
                        account.Debts[recipiant] -= amount;
                    }
                }

                Console.WriteLine("{0} now owes {1} {2}", payer, recipiant, account.Debts[recipiant]);
            }
        }

        public static Account GetOrAddAccount(string name, List<Account> accounts)
        {
            Account resultAccount = null;

            foreach (Account account in accounts)
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
                accounts.Add(resultAccount);
            }

            return resultAccount;
        }

        public static void PrintAll()
        {

            foreach (Account account in theBank.Accounts)
            {
                Console.WriteLine(account.Owner);
                foreach (var debt in account.Debts)
                {
                    if (debt.Value != 0)
                    {
                        Console.WriteLine("   owes {0} {1}", debt.Key, debt.Value);
                    }
                }
            }
        }
    }

}
