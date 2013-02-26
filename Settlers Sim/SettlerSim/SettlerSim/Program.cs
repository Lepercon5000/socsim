using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SettlerSimLib;
using SettlerSimLib.Building;

namespace SettlerSim
{
    class Program
    {
        static void Main(string[] args)
        {
            SettlerSimLib.SettlerBoard testing = SettlerSimLib.SettlerBoard.Instance;
            BuildingInterface buildTest = BuildingInterface.Instance;
            Player testPlayer = new Player();
            System.Threading.Thread.Sleep(25 * 1000);
            ILocationPoint locPoint = testing.GameBoard[0].LocationPointsEnum.First();
            IEdge edge = locPoint.Edges.First();
            locPoint = edge.GetOppositePoint(locPoint);
            edge = locPoint.Edges.First((e) => e != edge);
            locPoint = edge.GetOppositePoint(locPoint);
            buildTest.BuildSettlement(locPoint, testPlayer);
            Console.WriteLine("Done building Settlment!");
        }
    }
}
