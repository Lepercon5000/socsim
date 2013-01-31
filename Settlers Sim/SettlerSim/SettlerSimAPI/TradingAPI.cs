using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SettlerSimAPI
{


    class TradingAPI
    {
        private enum APITradeType
        {
            Error   =-1,
            Bank    =0,
            Harbor  =1,
            Player  =2
        }
        

        //Constructor
        public TradingAPI()
        {
        }

        public void handleTrade(string []instructions)
        {
            //Console.WriteLine(instructions);
        }
    }


}
