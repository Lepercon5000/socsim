using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SettlerSimAPI
{
    class SocSimTopAPI
    {
        
        static void Main(string[] args)
        {         
            //Mock AI calls
            APIListener test = new APIListener();
            test.getInstruction("Trade Harbor Wood Sheep");
            test.getInstruction("Place Road Coord");

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

        //Constructor
        public APIListener()
        {
            tradeHandler = new TradingAPI();
        }

        public Boolean getInstruction(string input)
        {
            //For now, directly called by Mock AI
            

            //Split instruction into substrings
            string[] instructions = splitInput(input);

            //Determine top level instruction type
            APITopLevelType instrType = getType(instructions);

            if (instrType != null)
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
                return false;
            }

            return true;
        }//end getInstruction

        private string[] splitInput(string input)
        {
            char[] delimeters = new char[] { ' ' };

            return input.Split(delimeters);
        }

        private APITopLevelType getType(String []instructions)
        {

            if (instructions != null)
            {
                string topLevelCommand = instructions[0];

                //messy -> string constants
                if (topLevelCommand.CompareTo("Buy") == 0)
                {
                    return APITopLevelType.Buy;
                }
                else if (topLevelCommand.CompareTo("Place") == 0)
                {
                    return APITopLevelType.Place;
                }
                else if (topLevelCommand.CompareTo("Trade") == 0)
                {
                    return APITopLevelType.Trade;
                }
                else if (topLevelCommand.CompareTo("DevCard") == 0)
                {
                    return APITopLevelType.DevCard;
                }
            }
            
            //Invalid Call
            return APITopLevelType.Error;
        }
    }
}
