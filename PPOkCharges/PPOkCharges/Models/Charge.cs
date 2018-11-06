using System;
using System.ComponentModel.DataAnnotations;

namespace PPOkCharges.Models
{
    public class Charge
    {
        [Display(Name = "Transaction Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy h:mm tt}")]
        [DataType(DataType.DateTime)]
        public DateTime TransactionDate { get; set; }
        private decimal _amount;
        [DataType(DataType.Currency)]
        public decimal Amount
        {
            get
            {
                decimal returnVal = _amount;
                if (this.ChargeType == (int)ChargeTypes.Deposit)
                    returnVal = _amount * -1;
                return returnVal;
            }
            set { _amount = value; }
        }
        [Display(Name = "Type")]
        public int ChargeType { get; set; }
        
        // If we ever had a more complicated type we might need to add some additional handling
        public string GetChargeTypeString()
        {
            ChargeTypes cType = (ChargeTypes)ChargeType;
            return cType.ToString();
        }
    }

    public enum ChargeTypes
    {
        Deposit = 1,
        Expense = 2,
        Fee = 3
    }
}