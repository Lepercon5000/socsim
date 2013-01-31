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
            SettlerSimLib.SettlerSim testing = new SettlerSimLib.SettlerSim();
            testing.SetupGameBoard();
        }
    }
}
