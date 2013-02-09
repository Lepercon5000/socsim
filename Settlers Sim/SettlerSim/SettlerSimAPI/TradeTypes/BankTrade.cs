using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SettlerSimLib;

namespace SettlerSimAPI.TradeTypes
{
    class BankTrade
    {
        private const int EXPECTED_NUM_ARG = 2;
        private APIHelper apiHelper;

        public BankTrade()
        {
            apiHelper = new APIHelper();
        }

        public Boolean handleBankTrade(Stack<string> instructions)
        {
            string strFourKind;     //the kind of resource you are paying 4 of.
            string strResourceType; //the kind of resource you want in return
            CardType fourKind;
            CardType resourceType;

            if (instructions.Count() != EXPECTED_NUM_ARG)
            {
                Console.WriteLine("INVALID BANK TRADE ARGUMENTS");
                return false;
            }

            strFourKind = instructions.Pop();
            strResourceType = instructions.Pop();

            if (apiHelper.getResourceTypeFromString(strFourKind, out fourKind)
                && (apiHelper.getResourceTypeFromString(strResourceType, out resourceType)))
            {
                Console.WriteLine("Trade will be completed " + strFourKind + " " + strResourceType + ".");
                //ARGUMENTS GOOD, Call Harbor Trading Logic
            }
            else
            {
                Console.WriteLine("INVALID BANK TRADE ARGUMENTS");
                return false;
            }

            return true;
        }

    }
}
