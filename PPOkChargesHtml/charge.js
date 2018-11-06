/**
 * @class
 */
 class Charge {
    /**
     * @param {number} id - the id number of this record
     * @param {string} transactionDate - the datetime of the transaction passed in as a string
     * @param {number} chargeType - int representing the charge type - 1: Deposit, 2: Expense, 3: Fee
     * @param {number} amount - the date passed in as a string
     * @constructor
     */
    constructor (id, transactionDate, chargeType, amount) {
		var momentDate = moment(transactionDate, 'M/D/YYYY h:mm a')
		this.id = id
		this.TransactionDate = momentDate.toDate()
		this.ChargeType = parseInt(chargeType)
		this.Amount = parseFloat(amount)
		if (this.ChargeType != 1) {
			this.Amount = this.Amount * -1
		}
        return this
    }
 }
 
 /**
 * @class
 */
 class ChargeList {
    /**
     * @param {Array} charges - the array of charges to be retained
     * @constructor
     */
    constructor (charges) {
		this.Charges = charges
		this.Total = 0
		this.DepositTotal = 0
		this.ExpenseTotal = 0
		this.FeeTotal = 0
		this.Totals = []
		this.getTotals()
        return this
    }
	
    /**
     * Method to sum charges
     * @return {void}
     */
	getTotals () {
		var self = this
		self.Total = 0
		self.DepositTotal = 0
		self.ExpenseTotal = 0
		self.FeeTotal = 0
		self.Charges.forEach(function(c) {
			self.Total += c.Amount
			switch(c.ChargeType) {
				case 1:
					self.DepositTotal += c.Amount
					break
				case 2:
					self.ExpenseTotal += c.Amount * -1
					break
				case 3:
					self.FeeTotal += c.Amount * -1
					break
			}
		})
		this.Total = self.Total
		this.DepositTotal = self.DepositTotal
		this.ExpenseTotal = self.ExpenseTotal
		this.FeeTotal = self.FeeTotal
		self.Totals =
		[
			{ Name: 'Deposit', Amount: this.DepositTotal },
			{ Name: 'Expense', Amount: this.ExpenseTotal },
			{ Name: 'Fee', Amount: this.FeeTotal },
		]
		self.sortValues()
		this.Charges = self.Charges
		this.Totals = self.Totals
	}
	
    /**
     * Method to sort both charges and totals
     * @return {void}
     */
	sortValues () {
		this.Charges.sort(function (a, b) {
			return b.TransactionDate-a.TransactionDate
		})
		this.Totals.sort(function (a, b) {
			return b.Amount-a.Amount
		})
	}
 }