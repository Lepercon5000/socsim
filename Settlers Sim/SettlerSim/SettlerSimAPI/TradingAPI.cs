using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SettlerSimAPI.TradeTypes
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

        private HarborTrade harborTradeHandler;
        private BankTrade bankTradeHandler;
        private PlayerTrade playerTradeHandler;
        

        //Constructor
        public TradingAPI()
        {
            harborTradeHandler = new HarborTrade();
            bankTradeHandler = new BankTrade();
            playerTradeHandler = new PlayerTrade();
        }

        public Boolean handleTrade(Stack<string> instructions)
        {
            //Console.WriteLine(instructions);
            if (instructions.Count() == 0)
            {
                
            }
            else 
            {
                string tradeInstr = instructions.Pop();
                APITradeType tradeType = getTradeType(tradeInstr);
                if (tradeType == APITradeType.Error)
                {
                    
                }
                else
                {
                    switch (tradeType)
                    {
                        case APITradeType.Bank:
                            Console.WriteLine("Case Bank");
                            return bankTradeHandler.handleBankTrade(instructions);
                        case APITradeType.Harbor:
                            Console.WriteLine("Case Harbor");
                            return harborTradeHandler.handleHarborTrade(instructions);
                        case APITradeType.Player:
                            Console.WriteLine("Case Player");
                            return playerTradeHandler.handlePlayerTrade(instructions);
                    }
                }
            }
            Console.WriteLine("INVALID TRADE TYPE");
            return false;


        }

        private APITradeType getTradeType(string TradeLevel)
        {
            if (TradeLevel.CompareTo("Bank") == 0){
                return APITradeType.Bank;
            }else if(TradeLevel.CompareTo("Harbor") == 0){
                return APITradeType.Harbor;
            }else if(TradeLevel.CompareTo("Player") == 0){
                return APITradeType.Player;
            }else{
                return APITradeType.Error;
            }
        }
    }


}
