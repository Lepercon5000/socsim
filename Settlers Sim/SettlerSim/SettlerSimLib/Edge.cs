using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SettlerSimLib
{
    internal class Edge : IEdge
    {
        private LocationPoint[] connectingPoints;

        public Edge(LocationPoint point1, LocationPoint point2)
        {
            connectingPoints = new LocationPoint[2];
            connectingPoints[0] = point1;
            connectingPoints[1] = point2;
            playerOwner = 0;
        }

        public ILocationPoint[] ConnectingPoints
        {
            get
            {
                return connectingPoints;
            }
        }

        public ILocationPoint Point1
        {
            get
            {
                return connectingPoints[0];
            }
        }

        public ILocationPoint Point2
        {
            get
            {
                return connectingPoints[1];
            }
        }

        public ILocationPoint GetOppositePoint(ILocationPoint point)
        {
            if (point == null)
                return null;
            if (!ConnectingPoints.Contains(point))
                return null;
            if (ConnectingPoints[0] == point)
                return connectingPoints[1];
            else
                return connectingPoints[0];
        }

        private int playerOwner;
        public int PlayerOwner
        {
            get
            {
                return playerOwner;
            }
            set
            {
                playerOwner = value;
            }
        }
    }

    public interface IEdge
    {
        ILocationPoint[] ConnectingPoints
        {
            get;
        }
        ILocationPoint Point1
        {
            get;
        }
        ILocationPoint Point2
        {
            get;
        }
        int PlayerOwner
        {
            get;
        }

        ILocationPoint GetOppositePoint(ILocationPoint point);
    }
}
