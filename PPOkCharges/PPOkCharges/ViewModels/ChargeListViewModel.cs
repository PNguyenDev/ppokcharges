using PPOkCharges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace PPOkCharges.ViewModels
{
    public class ChargeListViewModel
    {
        public List<Charge> ChargeList { get; set; }
        public decimal Total { get { return this.ChargeList.Sum(c => c.Amount); } }

        // Read only property that gets our types and gives sums. Will definitely need empty list handling if we went further with this
        public List<KeyValuePair<string, decimal>> ChargeTypeTotals
        {
            get
            {
                Array cTypes = Enum.GetValues(typeof(ChargeTypes));
                List<KeyValuePair<string, decimal>> chargeTypes = new List<KeyValuePair<string, decimal>>();
                foreach (int i in cTypes)
                {
                    chargeTypes.Add(new KeyValuePair<string, decimal>(
                        Enum.GetName(typeof(ChargeTypes), i),
                        ChargeList.Where(c => c.ChargeType == i).ToList().Sum(c => c.Amount)
                        ));
                }

                return chargeTypes.OrderByDescending(c => c.Value).ToList();
            }
        }
    }
}