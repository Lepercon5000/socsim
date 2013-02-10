using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SettlerSimLib
{
    internal class LocationPoint : ILocationPoint
    {
        private List<Hex> attachedHex;
        private List<Edge> edges;

        public bool PointIsNeighboring(LocationPoint point)
        {
            foreach (Edge edge in Edges)
            {
                if (edge.ConnectingPoints.Contains(point))
                    return true;
            }
            return false;
        }

        public void AddToEdgeList(Edge edge)
        {
            edges.Add(edge);
        }

        public void AddToAttachedHexList(Hex hex)
        {
            attachedHex.Add(hex);
        }

        public SeaHarbor Harbor
        {
            get;
            set;
        }

        public LocationPoint(Hex attachingHex)
        {
            attachedHex = new List<Hex>();
            edges = new List<Edge>();
            attachedHex.Add(attachingHex);
            Harbor = SeaHarbor.NotHarbor;
        }

        public IEnumerable<IHex> AttachedHex
        {
            get
            {
                return attachedHex;
            }
        }

        public IEnumerable<IEdge> Edges
        {
            get
            {
                return edges;
            }
        }
    }

    public interface ILocationPoint
    {
        IEnumerable<IHex> AttachedHex
        {
            get;
        }
        IEnumerable<IEdge> Edges
        {
            get;
        }
        SeaHarbor Harbor
        {
            get;
        }
    }
}