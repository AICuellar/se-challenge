using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
//In order to make use of the GetChange function a reference to the POSUtilities dll need to be added to the project, for the purpouse of this DEMO we will grab it
//from the debug folder inside this same solution
using POSUtilities;

namespace GetChangeDEMO
{
    class Program
    {
        //This demo will show a possible use of the GetChange function, will also illustrate how to make the call and how we have use the 
        //currencyStructure inside lcl_Currency (which function as input and output of the function) to display the valid values availables for the currency
        //Is expected that the developer is familiar with the scope of the main POS
        static void Main(string[] args)
        {
            try
            {
                lcl_Currency currencyObtained = new lcl_Currency();
                lcl_Currency currencyForChange;
                Console.WriteLine("Provide the total of the purchase:");
                List<lcl_Currency.currencyInfo> currencyOnHand = new List<lcl_Currency.currencyInfo>();
                lcl_Currency.currencyInfo tmpData;
                double totalPurchased = double.Parse(Console.ReadLine());
                string answer = "Y";
                bool isValidDenomination = false;
                double isAdouble;
                //From line 30 to line 58 is just the basic data gathering for before calling the function; is not necessary to you do the same for the function to work
                while (String.Equals(answer.ToUpper(), "Y"))
                {
                    Console.WriteLine("How many bills or coins are you going to provide:");
                    tmpData = new lcl_Currency.currencyInfo();
                    tmpData.Qty = Int32.Parse(Console.ReadLine());                    
                    Console.WriteLine("Which is the individual value on those bill or coins:");
                    Console.WriteLine("The valid values are:");
                    foreach (lcl_Currency.currencyInfo currObtained in currencyObtained.currencyStructure)
                    {
                        Console.Write("[" + currObtained .CurrencyValue + "] ");
                    }
                    tmpData.CurrencyValue = Console.ReadLine();
                    if(!Double.TryParse(tmpData.CurrencyValue, out isAdouble))
                        throw new InvalidOperationException("ERROR: Please use only numbers to define the denomination");
                    isValidDenomination = false;
                    foreach (lcl_Currency.currencyInfo currObtained in currencyObtained.currencyStructure)
                    {
                        if (tmpData.CurrencyValue.Equals(currObtained.CurrencyValue))
                        {
                            isValidDenomination = true;
                            break;
                        }
                    }
                    if(!isValidDenomination)
                        throw new InvalidOperationException("ERROR: The value of the bill or coin not supported");
                    currencyOnHand.Add(tmpData);
                    Console.WriteLine("Do you wish to capture more bills/coins? (Y/N)");
                    answer = Console.ReadLine();
                }
                //This is where we grab what ever was feed into our application and we apply a little order to it; basically the idea behind "currencyStrcuture" is that 
                //the first position will have the most great value available for the currency and will be decrementing on each position of the structure; that being said
                //we need to make sure we set the values on the proper place and in order to do it we apply the below nested loops or bubble order.
                foreach (lcl_Currency.currencyInfo currOnHand in currencyOnHand)
                {
                    foreach (lcl_Currency.currencyInfo currObtained in currencyObtained.currencyStructure)
                    {
                        if (currOnHand.CurrencyValue.Equals(currObtained.CurrencyValue))
                        {
                            currObtained.Qty += currOnHand.Qty;
                            break;
                        }
                    }
                }
                //With everything in place we call the function which in return will give us the same currencyStructure we originally fill but this time it will provide
                //the exact number of each denomaton that we need to deliver as the change for our buyer. You can take below representation as a sample on how to display it.
                currencyForChange = Transactional_Functions.GetChange(totalPurchased, currencyObtained);
                Console.WriteLine("Debes de entregar el siguiente cambio:");
                foreach (lcl_Currency.currencyInfo curr4Change in currencyForChange.currencyStructure)
                {
                    Console.WriteLine("[" + curr4Change.Qty + "] de [" + curr4Change.CurrencyValue + "]");
                }
                Console.ReadLine();
            }
            catch (Exception ex)
            {

                Console.Write(ex.Message);
                Console.ReadLine();
            }
        }
    }
}
