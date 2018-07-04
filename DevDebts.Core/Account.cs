using System.Collections.Generic;

namespace DevDebts.Core
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
}
