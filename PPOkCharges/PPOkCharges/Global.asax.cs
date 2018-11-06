using PPOkCharges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PPOkCharges
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        // In order to keep this light weight we'll use a memory store of data. In a real application we'd use a database
        public static List<Charge> chargeList = new List<Charge>
        {
            new Charge() { Amount = 2500.34m, ChargeType = (int)ChargeTypes.Deposit, TransactionDate = DateTime.Now.AddDays(-1).AddHours(-13) },
            new Charge() { Amount = 59.18m, ChargeType = (int)ChargeTypes.Fee, TransactionDate = DateTime.Now.AddDays(-6).AddHours(13) },
            new Charge() { Amount = 33.64m, ChargeType = (int)ChargeTypes.Expense, TransactionDate = DateTime.Now.AddDays(-7).AddHours(-8) },
            new Charge() { Amount = 16.43m, ChargeType = (int)ChargeTypes.Expense, TransactionDate = DateTime.Now.AddDays(-7).AddHours(-19) },
            new Charge() { Amount = 325.00m, ChargeType = (int)ChargeTypes.Expense, TransactionDate = DateTime.Now.AddDays(-10).AddHours(-7) },
            new Charge() { Amount = 5.00m, ChargeType = (int)ChargeTypes.Fee, TransactionDate = DateTime.Now.AddDays(-14).AddHours(-1) },
            new Charge() { Amount = 120.00m, ChargeType = (int)ChargeTypes.Expense, TransactionDate = DateTime.Now.AddDays(-20).AddHours(-8) },
            new Charge() { Amount = 1456.39m, ChargeType = (int)ChargeTypes.Deposit, TransactionDate = DateTime.Now.AddDays(-31).AddHours(-2) }
        };

        public List<Charge> ChargeList 
        {
            get { return chargeList; }
            set { chargeList = value; }  
        }
    }
}