using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SettlerSimLib;
using SettlerSimAPI.TradeTypes;

namespace SettlerSimAPI
{
    class SocSimTopAPI
    {
        
        static void Main(string[] args)
        {         
            //Mock AI calls
            APIListener test = new APIListener();
            Console.WriteLine("***************** Test 1");
            test.getInstruction("Trade Bank Wood Sheep");
            Console.WriteLine("***************** Test 2");
            test.getInstruction("Cheat ahah");
            Console.WriteLine("***************** Test 3");
            test.getInstruction("");
            Console.WriteLine("***************** Test 4");
            test.getInstruction("Trade ");
            Console.WriteLine("***************** Test 5");
            test.getInstruction("Trade");
            Console.WriteLine("***************** Test 6");
            test.getInstruction("Place Road Coord");
            Console.WriteLine("***************** Test 7");
            test.getInstruction("Trade Bank Three Wood");
            Console.WriteLine("***************** Test 8");
            test.getInstruction("Trade Bank Wood Sheep Clay");
            Console.WriteLine("***************** Test 9");
            test.getInstruction("Trade nothing");

            Console.ReadLine();
        }//end Main
    }//end SocSimTopAPI



    class APIListener
    {

        public enum APITopLevelType
        {
            Error   = -1,   //Invalid Call
            Buy     = 0,    //Road, Settlement, City, DevCard
            Place   = 1,    //Road, Settlement, City
            Trade   = 2,    //Bank, Harbor, Player
            DevCard = 3,    //RoadBuild, YearOfPlenty, Soldier, Monopoly, VictoryPoint
        }

        private TradingAPI tradeHandler;
        //private PlacingAPI placeHandler;
        //private BuyingAPI buyHandler;
        //private DevCardAPI devCardHandler;

        //Constructor
        public APIListener()
        {
            tradeHandler = new TradingAPI();
        }

        public Boolean getInstruction(string input)
        {
            //For now, directly called by Mock AI
            

            //Split instruction into substrings
            Stack<string> instructions = splitInput(input);

            //Determine top level instruction type
            APITopLevelType instrType = getCallType(instructions.Pop());

            if (instrType != APITopLevelType.Error)
            {
                
                switch (instrType)
                {
                    case APITopLevelType.Buy:
                        Console.WriteLine("Case Buy");
                        return true;
                    case APITopLevelType.Place:
                        Console.WriteLine("Case Place");
                        return true;
                    case APITopLevelType.Trade:
                        Console.WriteLine("Case Trade");
                        tradeHandler.handleTrade(instructions);
                        break;
                    case APITopLevelType.DevCard:
                        Console.WriteLine("Case DevCard");
                        break;
                    case APITopLevelType.Error:
                        Console.WriteLine("Case Not Determined");
                        //Bad Call - Handle
                        break;
                }
            }
            else
            {
                //Handle Bad Call
                Console.WriteLine("BadCall");
                return false;
            }

            return true;
        }//end getInstruction

        private Stack<string> splitInput(string input)
        {
            Stack<string> temp = new Stack<string>(8);
            char[] delimeters = new char[] { ' ' };
            string[] instrStrings = input.Split(delimeters);

            for (int i = instrStrings.Length - 1; i >= 0; i--)
            {
                temp.Push(instrStrings[i]);
            }

            return temp;
        }

        private APITopLevelType getCallType(String topLevel)
        {

            if (topLevel != null)
            {

                //messy -> string constants
                if (topLevel.CompareTo("Buy") == 0)
                {
                    return APITopLevelType.Buy;
                }
                else if (topLevel.CompareTo("Place") == 0)
                {
                    return APITopLevelType.Place;
                }
                else if (topLevel.CompareTo("Trade") == 0)
                {
                    return APITopLevelType.Trade;
                }
                else if (topLevel.CompareTo("DevCard") == 0)
                {
                    return APITopLevelType.DevCard;
                }
            }
            
            //Invalid Call
            return APITopLevelType.Error;
        }
    }
}
