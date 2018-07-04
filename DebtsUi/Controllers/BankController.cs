using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DebtsUi.Models.Bank;
using DevDebts.Core;
using Microsoft.AspNetCore.Mvc;

namespace DebtsUi.Controllers
{
    public class BankController : Controller
    {
        private static Bank _bank = Bank.Load("banc.txt");


        public IActionResult Index()
        {
            _bank.DoTransaction(new[] { "Tom", "Llwyd" }, "Bryn", "owes", 5.99m);
            var viewModel = new BankViewModel();
            viewModel.BalanceSummary = _bank.Print();
            return View(viewModel);
        }
    }
}