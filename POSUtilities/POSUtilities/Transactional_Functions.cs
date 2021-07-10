using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSUtilities
{
    public class Transactional_Functions
    {
        //This is main function; as per request it accept the total of the purchae and my suggested structure; which is an instance of lclCurrency 
        //that contain our currencyStructure that will allow us to know how many of each items we get from the customer and how many of each we need to give back
        //to the customer
        public static lcl_Currency GetChange(double totalPurchase, lcl_Currency currencyOnHand)
        {            
            double totalGiven = 0.0f;
            double totalChange = 0.0f;
            foreach (lcl_Currency.currencyInfo billValue in currencyOnHand.currencyStructure)
            {
                totalGiven += (double.Parse(billValue.CurrencyValue) * billValue.Qty);
            }

            //Making sure the customer give us suficient money to cover the purchase
            if(totalGiven < totalPurchase)
            {
                throw new InvalidOperationException("ERROR: The quantity provided is not enough to cover the purchase");
            }
            else
            {
                totalChange = Math.Round((totalGiven - totalPurchase),2);
            }
            
            double tmpReminder = totalChange;
            //We calculate the minimun number of each item that we need to give as a change.
            lcl_Currency YourChange = new lcl_Currency();
            foreach (lcl_Currency.currencyInfo billValue in YourChange.currencyStructure)
            {
                while (tmpReminder >= (double.Parse(billValue.CurrencyValue)))
                {                    
                    tmpReminder = Math.Round((tmpReminder - (double.Parse(billValue.CurrencyValue))),2);
                    billValue.Qty = billValue.Qty + 1;
                }
                if (tmpReminder == 0)
                    break;
            }
            return YourChange;
        }
    }
}
