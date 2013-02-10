using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SettlerSimLib;

namespace SettlerSim
{
    class Program
    {
        static void Main(string[] args)
        {
            SettlerSimLib.SettlerBoard testing = new SettlerSimLib.SettlerBoard();

            List<IHex> testBoard = testing.GameBoard;

            foreach (IHex hex in testBoard)
            {
                Console.WriteLine("Hex with value " + hex.DiceRollValue);
                Console.WriteLine("\tLand Type : " + Enum.GetName(typeof(LandType), hex.LandType));
            }
        }
    }
}
