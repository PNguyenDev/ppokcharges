/**
 * Method to convert a float value to a currency string
 * @param {number} amount - a float value to be converted
 * @return {string} - the amount value as a string with parentheses for negative values and a dollar sign
 */
function getCurrencyString (amount) {
	 var amountString = amount,
		isNegative = (amount < 0) ? true : false
	amountString = Math.abs(amount)
	amountString = amountString.toFixed(2).toString()
	amountString = (isNegative) ? '(' +  amountString + ')' : amountString
	amountString = '$' + amountString
	return amountString
}

/**
 * Method to convert a date object to a string
 * @param {date} d - date object to be converted
 * @return {string} - the date as a string of "M/D/YYYY h:mm a"
 */
function getDateString (d) {
	var dateString = moment(d).format('M/D/YYYY h:mm a')
	return dateString
}
	
/**
 * Returns the string for the charge type of the charge
 * @param {number} cType - the number value of the charge type
 * @return {string} the charge type as a string 1: Deposit, 2: Expense, 3: Fee
 */
function getChargeTypeString (cType) {
	var chargeTypeString = '';
	switch(cType) {
		case 1:
			chargeTypeString = 'Deposit'
			break
		case 2:
			chargeTypeString = 'Expense'
			break
		case 3:
			chargeTypeString = 'Fee'
			break
	}
	return chargeTypeString
}

Vue.component('charge-list-item', {
	props: {
		charge: Object
	},
	template: `
		<tr>
			<td>{{ dt }}</td>
			<td>{{ cType }}</td>
			<td>{{ amt }}</td>
		</tr>
	`,
	computed: {
		dt: function () {
			return getDateString(this.charge.TransactionDate)
		},
		cType: function () {
			return getChargeTypeString(this.charge.ChargeType)
		},
		amt: function () {
			return getCurrencyString(this.charge.Amount)
		},
	},
})

Vue.component('charge-total-item', {
	props: {
		charge: Object
	},
	template: `
		<tr>
			<td>{{ this.charge.Name }}</td>
			<td>{{ amt }}</td>
		</tr>
	`,
	computed: {
		amt: function () {
			return getCurrencyString(this.charge.Amount)
		},
	},
})

var cList = new ChargeList([
	new Charge(1, '11/4/2018 12:11:09 PM', 1, 2500.34),
	new Charge(2, '10/31/2018 2:11:09 PM', 3, 59.18),
	new Charge(3, '10/29/2018 5:14:25 PM', 2, 33.64),
	new Charge(4, '10/29/2018 6:14:25 AM', 2, 16.43),
	new Charge(5, '10/26/2018 6:14:25 PM', 2, 325.00),
	new Charge(6, '10/23/2018 12:14:25 AM', 3, 5.00),
	new Charge(7, '10/16/2018 5:14:25 PM', 2, 120.00),
	new Charge(8, '10/5/2018 11:14:25 PM', 1, 1456.39),
	])

var app = new Vue({
	el: '#charge-listing',
    data () {
        return {
			chargelist: cList,
			newChargeDate: new Date(),
			newChargeType: 1,
			newAmount: 0,
        }
    },
	computed: {
		amt: function () {
			return getCurrencyString(this.chargelist.Total)
		},
	},
	methods: {
		addNewCharge: function () {
			var id = this.chargelist.Charges.length + 1
			var momentDate = moment(this.newChargeDate, 'YYYYMMDDTHHmmssZ')
			this.chargelist.Charges.push(new Charge(id, momentDate, this.newChargeType, this.newAmount))
			this.chargelist.getTotals()
		}
	}
})