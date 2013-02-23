using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SettlerSimLib
{
    internal class Hex : IHex
    {
        private Hex[] neighboringHexes;
        public Hex[] NeighboringHexes
        {
            get
            {
                return neighboringHexes;
            }
        }
        private int diceRollValue;
        public int DiceRollValue
        {
            get
            {
                return diceRollValue;
            }
            set
            {
                if (diceRollValue == -1)
                    diceRollValue = value;
            }
        }

        public PointCollection Points;

        public Hex(LandType type)
        {
            landType = type;
            diceRollValue = -1;

            neighboringHexes = new Hex[6];
            Points = new PointCollection();
            for (int i = 0; i < 6; ++i)
            {
                neighboringHexes[i] = null;
            }
        }

        private void ConnectLocationPoints(NeighboringHex neighboringHex, LocationPoints thisLocation, LocationPoints neighboringLocation)
        {
            if (this.Points[thisLocation] == null)
            {
                if (this.neighboringHexes[(int)neighboringHex].Points[neighboringLocation] == null)
                {
                    this.neighboringHexes[(int)neighboringHex].Points[neighboringLocation] = new LocationPoint(this.neighboringHexes[(int)neighboringHex]);
                }
                this.Points[thisLocation] = this.neighboringHexes[(int)neighboringHex].Points[neighboringLocation];
                if (!this.neighboringHexes[(int)neighboringHex].Points[neighboringLocation].AttachedHex.Contains(this))
                    this.neighboringHexes[(int)neighboringHex].Points[neighboringLocation].AddToAttachedHexList(this);
            }
            else
            {
                if (this.neighboringHexes[(int)neighboringHex].Points[neighboringLocation] == null)
                {
                    this.neighboringHexes[(int)neighboringHex].Points[neighboringLocation] = this.Points[thisLocation];
                    if (!this.Points[thisLocation].AttachedHex.Contains(this.neighboringHexes[(int)neighboringHex]))
                        this.Points[thisLocation].AddToAttachedHexList(this.neighboringHexes[(int)neighboringHex]);
                }
                else
                {
                    this.Points[(int)thisLocation] = this.neighboringHexes[(int)neighboringHex].Points[(int)neighboringLocation];
                    if (!this.neighboringHexes[(int)neighboringHex].Points[(int)neighboringLocation].AttachedHex.Contains(this))
                        this.neighboringHexes[(int)neighboringHex].Points[(int)neighboringLocation].AddToAttachedHexList(this);
                }
            }
        }

        private void ConnectPoints(NeighboringHex neighboringHex)
        {
            LocationPoints point1 = LocationPoints.BottomLeft;
            LocationPoints point2 = LocationPoints.BottomLeft;
            LocationPoints nPoint1 = LocationPoints.BottomLeft;
            LocationPoints nPoint2 = LocationPoints.BottomLeft;
            switch (neighboringHex)
            {
                case NeighboringHex.Bottom:
                    point1 = LocationPoints.BottomLeft;
                    nPoint1 = LocationPoints.TopLeft;
                    point2 = LocationPoints.BottomRight;
                    nPoint2 = LocationPoints.TopRight;
                    break;
                case NeighboringHex.BottomLeft:
                    point1 = LocationPoints.Left;
                    nPoint1 = LocationPoints.TopRight;
                    point2 = LocationPoints.BottomLeft;
                    nPoint2 = LocationPoints.Right;
                    break;
                case NeighboringHex.BottomRight:
                    point1 = LocationPoints.Right;
                    nPoint1 = LocationPoints.TopLeft;
                    point2 = LocationPoints.BottomRight;
                    nPoint2 = LocationPoints.Left;
                    break;
                case NeighboringHex.Top:
                    point1 = LocationPoints.TopLeft;
                    nPoint1 = LocationPoints.BottomLeft;
                    point2 = LocationPoints.TopRight;
                    nPoint2 = LocationPoints.BottomRight;
                    break;
                case NeighboringHex.TopLeft:
                    point1 = LocationPoints.Left;
                    nPoint1 = LocationPoints.BottomRight;
                    point2 = LocationPoints.TopLeft;
                    nPoint2 = LocationPoints.Right;
                    break;
                case NeighboringHex.TopRight:
                    point1 = LocationPoints.TopRight;
                    nPoint1 = LocationPoints.Left;
                    point2 = LocationPoints.Right;
                    nPoint2 = LocationPoints.BottomLeft;
                    break;
            }

            ConnectLocationPoints(neighboringHex, point1, nPoint1);
            ConnectLocationPoints(neighboringHex, point2, nPoint2);
        }

        public IHex TopLeft
        {
            get
            {
                return neighboringHexes[(int)NeighboringHex.TopLeft];
            }
            set
            {
                if (neighboringHexes[(int)NeighboringHex.TopLeft] == null)
                {
                    neighboringHexes[(int)NeighboringHex.TopLeft] = (Hex)value;
                    ((Hex)value).neighboringHexes[(int)NeighboringHex.BottomRight] = this;
                    ConnectPoints(NeighboringHex.TopLeft);
                }
            }
        }
        public IHex Top
        {
            get
            {
                return neighboringHexes[(int)NeighboringHex.Top];
            }
            set
            {
                if (neighboringHexes[(int)NeighboringHex.Top] == null)
                {
                    neighboringHexes[(int)NeighboringHex.Top] = (Hex)value;
                    ((Hex)value).neighboringHexes[(int)NeighboringHex.Bottom] = this;
                    ConnectPoints(NeighboringHex.Top);
                }
            }
        }
        public IHex TopRight
        {
            get
            {
                return neighboringHexes[(int)NeighboringHex.TopRight];
            }
            set
            {
                if (neighboringHexes[(int)NeighboringHex.TopRight] == null)
                {
                    neighboringHexes[(int)NeighboringHex.TopRight] = (Hex)value;
                    ((Hex)value).neighboringHexes[(int)NeighboringHex.BottomLeft] = this;
                    ConnectPoints(NeighboringHex.TopRight);
                }
            }
        }
        public IHex BottomRight
        {
            get
            {
                return neighboringHexes[(int)NeighboringHex.BottomRight];
            }
            set
            {
                if (neighboringHexes[(int)NeighboringHex.BottomRight] == null)
                {
                    neighboringHexes[(int)NeighboringHex.BottomRight] = (Hex)value;
                    ((Hex)value).neighboringHexes[(int)NeighboringHex.TopLeft] = this;
                    ConnectPoints(NeighboringHex.BottomRight);
                }
            }
        }
        public IHex Bottom
        {
            get
            {
                return neighboringHexes[(int)NeighboringHex.Bottom];
            }
            set
            {
                if (neighboringHexes[(int)NeighboringHex.Bottom] == null)
                {
                    neighboringHexes[(int)NeighboringHex.Bottom] = (Hex)value;
                    ((Hex)value).neighboringHexes[(int)NeighboringHex.Top] = this;
                    ConnectPoints(NeighboringHex.Bottom);
                }
            }
        }
        public IHex BottomLeft
        {
            get
            {
                return neighboringHexes[(int)NeighboringHex.BottomLeft];
            }
            set
            {
                if (neighboringHexes[(int)NeighboringHex.BottomLeft] == null)
                {
                    neighboringHexes[(int)NeighboringHex.BottomLeft] = (Hex)value;
                    this.neighboringHexes[(int)NeighboringHex.TopRight] = this;
                    ConnectPoints(NeighboringHex.BottomLeft);
                }
            }
        }

        public IEnumerable<ILocationPoint> LocationPointsEnum
        {
            get
            {
                return Points;
            }
        }

        public IEnumerable<IHex> NeightboringHexesEnum
        {
            get
            {
                return neighboringHexes;
            }
        }

        private LandType landType;
        public LandType LandType
        {
            get
            {
                return landType;
            }
            set
            {
                landType = value;
            }
        }
    }

    public interface IHex
    {
        IHex TopLeft
        {
            get;
        }

        IHex Top
        {
            get;
        }

        IHex TopRight
        {
            get;
        }

        IHex BottomRight
        {
            get;
        }

        IHex Bottom
        {
            get;
        }

        IHex BottomLeft
        {
            get;
        }

        int DiceRollValue
        {
            get;
        }

        IEnumerable<ILocationPoint> LocationPointsEnum
        {
            get;
        }

        IEnumerable<IHex> NeightboringHexesEnum
        {
            get;
        }

        LandType LandType
        {
            get;
        }
    }
}
