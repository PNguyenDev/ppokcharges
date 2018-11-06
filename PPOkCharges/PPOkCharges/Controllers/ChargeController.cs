using PPOkCharges;
using PPOkCharges.Models;
using PPOkCharges.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PPOkCharges.Controllers
{
    public class ChargeController : Controller
    {
        // GET: Charge
        public ActionResult Index()
        {
            ChargeListViewModel vm = new ChargeListViewModel();
            vm.ChargeList = MvcApplication.chargeList.OrderByDescending(c => c.TransactionDate).ToList();
            return View(vm);
        }

        // Here we are leveraging application state to prevent the need for a database, so for concurrency purposes
        // We are doing application locks to prevent concurrency issues
        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            try
            {
                Charge c = new Charge();
                DateTime dateValue;
                decimal amountValue;
                int chargeType;
                if (DateTime.TryParse(form["transactionDate"], out dateValue))
                {
                    c.TransactionDate = dateValue;
                }
                else
                {
                    throw new ArgumentException("Value is not a date", form["transactionDate"]);
                }
                if (decimal.TryParse(form["amount"], out amountValue))
                {
                    c.Amount = amountValue;
                }
                else
                {
                    throw new ArgumentException("Value is not a valid amount", form["amount"]);
                }
                if (int.TryParse(form["chargeType"], out chargeType))
                {
                    c.ChargeType = chargeType;
                }
                else
                {
                    throw new ArgumentException("Value is not a valid charge type", form["chargeType"]);
                }
                List<Charge> chargeList = MvcApplication.chargeList;
                chargeList.Add(c);
                MvcApplication.chargeList = chargeList;
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
