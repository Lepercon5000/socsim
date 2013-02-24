using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SettlerSimLib.Building
{
    /// <summary>
    /// a class that handles building actions
    /// </summary>
    public class BuildingInterface
    {
        private SettlerBoard settlerBoard;

        private BuildingInterface()
        {
            settlerBoard = SettlerBoard.Instance;
        }

        private static BuildingInterface instance;
        public static BuildingInterface Instance
        {
            get
            {
                if (instance == null)
                    instance = new BuildingInterface();
                return instance;
            }
        }

        // TODO: need to take the resources from the player still.

        public bool BuildRoad(IEdge roadEdge, Player player)
        {
            // A road is already built there, so we can not build there.
            if(roadEdge.PlayerOwner != 0)
                return false;
            // Need to check if the connecting edges or points have roads any roads/settlements/cities owned by the same player
            if(!roadEdge.Point1.Edges.Where((edge) => edge.PlayerOwner == player.PlayerNumber).Any() &&
                !roadEdge.Point2.Edges.Where((edge) => edge.PlayerOwner == player.PlayerNumber).Any())
            {
                if(roadEdge.Point1.PlayerOwner != player.PlayerNumber && roadEdge.Point2.PlayerOwner != player.PlayerNumber)
                    return false;
            }

            ((Edge)roadEdge).PlayerOwner = player.PlayerNumber;
            return true;
        }

        public bool BuildSettlement(ILocationPoint locationPoint, Player player)
        {
            // if the point is already owned by someone
            if (locationPoint.PlayerOwner != 0)
                return false;
            // verify that at least one of our neighboring edges is the players
            if (!locationPoint.Edges.Any((edge) => edge.PlayerOwner == player.PlayerNumber))
                return false;
            // check if the point is two edges away from all settlements players
            if (locationPoint.Edges.Any((edge) => edge.GetOppositePoint(locationPoint).PlayerOwner != 0))
                return false;
            ((LocationPoint)locationPoint).PlayerOwner = player.PlayerNumber;
            return true;
        }

        public bool BuildCity(ILocationPoint locationPoint, Player player)
        {
            // if the spot is owned by another player
            if (locationPoint.PlayerOwner != player.PlayerNumber)
                return false;
            // if the spot is already a city, you can't build another city
            if (locationPoint.IsACity)
                return false;
            ((LocationPoint)locationPoint).IsACity = true;
            return true;
        }
    }
}
