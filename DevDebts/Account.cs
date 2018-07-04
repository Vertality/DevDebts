using System;
using System.Collections.Generic;
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
}
