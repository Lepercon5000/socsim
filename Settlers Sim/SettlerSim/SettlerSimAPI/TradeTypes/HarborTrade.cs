using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SettlerSimLib;

namespace SettlerSimAPI.TradeTypes
{
    class HarborTrade
    {
        private const int EXPECTED_NUM_ARG = 2;
        private APIHelper apiHelper;

        public HarborTrade()
        {
            apiHelper = new APIHelper();
        }//end constructor


        public Boolean handleHarborTrade(Stack<string> instructions)
        {
            //Format "Trade Harbor <HarborType> <ResourceWanted>
            string strHarborType;       //the type of harbor
            string strResourceType;     //the resource you want in return
            SeaHarbor harborType;
            CardType resourceType;

            if (instructions.Count() != EXPECTED_NUM_ARG)
            {
                Console.WriteLine("INVALID HARBOR TRADE ARGUMENTS");
                return false;
            }

            strHarborType = instructions.Pop();
            strResourceType = instructions.Pop();
            if (apiHelper.getHarborTypeFromString(strHarborType, out harborType)
                && (apiHelper.getResourceTypeFromString(strResourceType, out resourceType)))
            {
                Console.WriteLine("Trade will be completed " + strHarborType + " " + strResourceType + ".");
                //ARGUMENTS GOOD, Call Harbor Trading Logic
            }
            else
            {
                Console.WriteLine("INVALID HARBOR TRADE ARGUMENTS");
                return false;
            }

            return true;
        }
    }

}
