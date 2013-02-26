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

        public static List<CardType> RoadCost
        {
            get
            {
                return new List<CardType>()
                {
                    CardType.Clay,
                    CardType.Wood
                };
            }
        }

        public static List<CardType> SettlementCost
        {
            get
            {
                return new List<CardType>()
                {
                    CardType.Clay,
                    CardType.Wood,
                    CardType.Wheat,
                    CardType.Sheep
                };
            }
        }

        public static List<CardType> CityCost
        {
            get
            {
                return new List<CardType>()
                {
                    CardType.Wheat,
                    CardType.Wheat,
                    CardType.Rock,
                    CardType.Rock,
                    CardType.Rock
                };
            }
        }

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
            if (player.TakeResource(RoadCost))
            {
                ((Edge)roadEdge).PlayerOwner = player.PlayerNumber;
                this.RoadWasBuilt(this, new RoadBuiltArgs(roadEdge));
                return true;
            }
            return false;
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
            if (player.TakeResource(SettlementCost))
            {
                ((LocationPoint)locationPoint).PlayerOwner = player.PlayerNumber;
                this.SettlementWasBuilt(this, new SettlementBuiltArgs(locationPoint));
                return true;
            }
            return false;
        }

        public bool BuildCity(ILocationPoint locationPoint, Player player)
        {
            // if the spot is owned by another player
            if (locationPoint.PlayerOwner != player.PlayerNumber)
                return false;
            // if the spot is already a city, you can't build another city
            if (locationPoint.IsACity)
                return false;
            if (player.TakeResource(CityCost))
            {
                ((LocationPoint)locationPoint).IsACity = true;
                this.CityWasBuilt(this, new CityBuiltArgs(locationPoint));
                return true;
            }
            return false;
        }

        public event RoadBuiltHandler RoadWasBuilt;

        public event SettlementBuiltHandler SettlementWasBuilt;

        public event CityBuiltHandler CityWasBuilt;
    }

    public delegate void RoadBuiltHandler(object sender, RoadBuiltArgs args);

    public delegate void SettlementBuiltHandler(object sender, SettlementBuiltArgs args);

    public delegate void CityBuiltHandler(object sender, CityBuiltArgs args);

    public class RoadBuiltArgs : System.EventArgs
    {
        private IEdge road;
        public IEdge Road
        {
            get
            {
                return road;
            }
        }

        public RoadBuiltArgs(IEdge roadBuiltOn)
        {
            road = roadBuiltOn;
        }
    }

    public class SettlementBuiltArgs : System.EventArgs
    {
        private ILocationPoint settlement;
        public ILocationPoint Settlement
        {
            get
            {
                return settlement;
            }
        }

        public SettlementBuiltArgs(ILocationPoint settlementBuildPoint)
        {
            settlement = settlementBuildPoint;
        }
    }

    public class CityBuiltArgs : System.EventArgs
    {
        private ILocationPoint city;
        public ILocationPoint City
        {
            get
            {
                return city;
            }
        }

        public CityBuiltArgs(ILocationPoint cityBuildPoint)
        {
            city = cityBuildPoint;
        }
    }
}
