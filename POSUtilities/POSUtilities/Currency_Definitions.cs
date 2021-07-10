using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSUtilities
{
    public class Currency_Definitions
    {
        //Here we can define the valid denominations for the different currency we have available; if more currency need to be added you need to set them up
        //in this class and also in the local_currency where the initializacion occurrs.
        protected List<string> MXNNodeValues = new List<string> { "100", "50", "20", "10", "5", "2", "1", "0.50", "0.20", "0.10", "0.05" };
        protected List<string> USDNodeValues = new List<string> { "100", "50", "20", "10", "5", "2", "1", "0.50", "0.25", "0.10", "0.05", "0.01" };
    }
}
