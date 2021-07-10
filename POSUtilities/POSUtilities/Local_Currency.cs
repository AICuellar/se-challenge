using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace POSUtilities
{    

    //This will be our main input and output parameter; while it will function as is for our currents need if in the future more information is need as input
    //or if more information is requested as output we can add it with out breaking the calling applications during the update.
    public class lcl_Currency : Currency_Definitions
    {        
        //The base structure of currencyStructure; it will allow us to save the value of the currency and how many items of that currency we have or need to give.
        public class currencyInfo
        {
            public string CurrencyValue { get; set; }
            public int Qty { get; set; }
        }

        public List<currencyInfo> currencyStructure = new List<currencyInfo>();

        //We are looking for our global setup previously set by the installlation of the POS application; if we do not found anything we need to send
        //a proper message to the user to contact the administrator.
        private string lclCurrencyConfig = ConfigurationManager.AppSettings["Local_Currency"];
       

        public lcl_Currency()
        {
            //Send a proper message to the user to contact the administrator if the setup was not found.
            if (String.IsNullOrEmpty(lclCurrencyConfig))
            {
                throw new ArgumentException("The local Currency setup has not being found; please contact system administrator", "Local_Currency");
            }

            //Below is the initialization for the currencyStrcuture which will contain the different valid values for the currency that was set during the POS installation
            //Please not that extra currency can be added; but you need to set them up on the Currency Definition class and then add them to the initialization part below.
            switch (lclCurrencyConfig.ToUpper())
            {
                case "MXN":
                    foreach (string billValue in MXNNodeValues)
                    {
                        currencyInfo myCurrencyInfo = new currencyInfo();
                        myCurrencyInfo.CurrencyValue = billValue.ToString();
                        myCurrencyInfo.Qty = 0;
                        currencyStructure.Add(myCurrencyInfo);
                    }
                    break;
                case "USD":
                    foreach (string billValue in USDNodeValues)
                    {
                        currencyInfo myCurrencyInfo = new currencyInfo();
                        myCurrencyInfo.CurrencyValue = billValue.ToString();
                        myCurrencyInfo.Qty = 0;
                        currencyStructure.Add(myCurrencyInfo);
                    }
                    break;
                default:
                    throw new ArgumentException("The currency set during the installation is not currently supported by GetChange; please contact your administrator for an upgraded version", lclCurrencyConfig.ToUpper());
            }
        }

    }   
}
