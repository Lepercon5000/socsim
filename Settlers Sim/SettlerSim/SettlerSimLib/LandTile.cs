using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SettlerSimLib
{
    public enum CardType
    {
        Wood = 0,
        Sheep = 1,
        Wheat = 2,
        Rock = 3,
        Clay = 4,
    }

    public enum LandType
    {
        Wood = CardType.Wood,
        Sheep = CardType.Sheep,
        Wheat = CardType.Wheat,
        Rock = CardType.Rock,
        Clay = CardType.Clay,
        Sand = 5
    }

    public enum SeaType
    {
        WoodHarbor = CardType.Wood,
        SheepHarbor = CardType.Sheep,
        WheatHarbor = CardType.Wheat,
        RockHarbor = CardType.Rock,
        ClayHarbor = CardType.Clay,
        AnyTrade = 6,
        Sea = LandType.Sand
    }

    public enum NeighboringHex
    {
        TopLeft = 0,
        Top = 1,
        TopRight = 2,
        BottomRight = 3,
        Bottom = 4,
        BottomLeft = 5
    }

    public enum LocationPoints
    {
        Left = 0,
        TopLeft = 1,
        TopRight = 2,
        Right = 3,
        BottomRight = 4,
        BottomLeft = 5
    }

    public class LocationPoint
    {
        public List<Hex> attachedHex;

        public LocationPoint(Hex attachingHex)
        {
            attachedHex = new List<Hex>();
            attachedHex.Add(attachingHex);
        }
    }

    public abstract class Hex
    {
        private Hex[] neighboringHexes;
        public LocationPoint[] points;

        public Hex()
        {
            neighboringHexes = new Hex[6];
            for (int i = 0; i < 6; ++i)
                neighboringHexes[i] = null;

            points = new LocationPoint[6];
            for (int i = 0; i < 6; ++i)
                points[i] = null;
        }

        private void ConnectLocationPoints(NeighboringHex neighboringHex, LocationPoints thisLocation, LocationPoints neighboringLocation)
        {
            if (this.points[(int)thisLocation] == null)
            {
                if (this.neighboringHexes[(int)neighboringHex].points[(int)neighboringLocation] == null)
                {
                    this.neighboringHexes[(int)neighboringHex].points[(int)neighboringLocation] = new LocationPoint(this.neighboringHexes[(int)neighboringHex]);
                }
                this.points[(int)thisLocation] = this.neighboringHexes[(int)neighboringHex].points[(int)neighboringLocation];
                if (!this.neighboringHexes[(int)neighboringHex].points[(int)neighboringLocation].attachedHex.Contains(this))
                    this.neighboringHexes[(int)neighboringHex].points[(int)neighboringLocation].attachedHex.Add(this);
            }
            else
            {
                if (this.neighboringHexes[(int)neighboringHex].points[(int)neighboringLocation] == null)
                {
                    this.neighboringHexes[(int)neighboringHex].points[(int)neighboringLocation] = this.points[(int)thisLocation];
                    if (!this.points[(int)thisLocation].attachedHex.Contains(this.neighboringHexes[(int)neighboringHex]))
                        this.points[(int)thisLocation].attachedHex.Add(this.neighboringHexes[(int)neighboringHex]);
                }
                else
                {
                    this.points[(int)thisLocation] = this.neighboringHexes[(int)neighboringHex].points[(int)neighboringLocation];
                    if (!this.neighboringHexes[(int)neighboringHex].points[(int)neighboringLocation].attachedHex.Contains(this))
                        this.neighboringHexes[(int)neighboringHex].points[(int)neighboringLocation].attachedHex.Add(this);
                }
            }
        }

        private void ConnectPoints(NeighboringHex neighboringHex)
        {
            LocationPoints point1 = LocationPoints.BottomLeft;
            LocationPoints point2 = LocationPoints.BottomLeft;
            LocationPoints nPoint1 = LocationPoints.BottomLeft;
            LocationPoints nPoint2 = LocationPoints.BottomLeft;
            switch(neighboringHex)
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

        public Hex TopLeft
        {
            get
            {
                return neighboringHexes[(int)NeighboringHex.TopLeft];
            }
            set
            {
                if (neighboringHexes[(int)NeighboringHex.TopLeft] == null)
                {
                    neighboringHexes[(int)NeighboringHex.TopLeft] = value;
                    value.neighboringHexes[(int)NeighboringHex.BottomRight] = this;
                    ConnectPoints(NeighboringHex.TopLeft);
                }
            }
        }
        public Hex Top
        {
            get
            {
                return neighboringHexes[(int)NeighboringHex.Top];
            }
            set
            {
                if (neighboringHexes[(int)NeighboringHex.Top] == null)
                {
                    neighboringHexes[(int)NeighboringHex.Top] = value;
                    value.neighboringHexes[(int)NeighboringHex.Bottom] = this;
                    ConnectPoints(NeighboringHex.Top);
                }
            }
        }
        public Hex TopRight
        {
            get
            {
                return neighboringHexes[(int)NeighboringHex.TopRight];
            }
            set
            {
                if (neighboringHexes[(int)NeighboringHex.TopRight] == null)
                {
                    neighboringHexes[(int)NeighboringHex.TopRight] = value;
                    value.neighboringHexes[(int)NeighboringHex.BottomLeft] = this;
                    ConnectPoints(NeighboringHex.TopRight);
                }
            }
        }
        public Hex BottomRight
        {
            get
            {
                return neighboringHexes[(int)NeighboringHex.BottomRight];
            }
            set
            {
                if (neighboringHexes[(int)NeighboringHex.BottomRight] == null)
                {
                    neighboringHexes[(int)NeighboringHex.BottomRight] = value;
                    value.neighboringHexes[(int)NeighboringHex.TopLeft] = this;
                    ConnectPoints(NeighboringHex.BottomRight);
                }
            }
        }
        public Hex Bottom
        {
            get
            {
                return neighboringHexes[(int)NeighboringHex.Bottom];
            }
            set
            {
                if (neighboringHexes[(int)NeighboringHex.Bottom] == null)
                {
                    neighboringHexes[(int)NeighboringHex.Bottom] = value;
                    value.neighboringHexes[(int)NeighboringHex.Top] = this;
                    ConnectPoints(NeighboringHex.Bottom);
                }
            }
        }
        public Hex BottomLeft
        {
            get
            {
                return neighboringHexes[(int)NeighboringHex.BottomLeft];
            }
            set
            {
                if (neighboringHexes[(int)NeighboringHex.BottomLeft] == null)
                {
                    neighboringHexes[(int)NeighboringHex.BottomLeft] = value;
                    this.neighboringHexes[(int)NeighboringHex.TopRight] = this;
                    ConnectPoints(NeighboringHex.BottomLeft);
                }
            }
        }
    }

    public class SeaHex : Hex
    {
        private SeaType seaType;
        public SeaType SeaType
        {
            get
            {
                return seaType;
            }
        }

        public SeaHex(SeaType type)
            : base()
        {
            seaType = type;
        }
    }

    public class LandHex : Hex
    {
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

        public LandHex(LandType type)
            : base()
        {
            landType = type;
        }
    }

    public class SettlerSim
    {
        private List<Hex> gameArea;

        public void ConnectBoard()
        {
            for(int i = 0; i < 3; ++i)
            {
                gameArea[i].BottomLeft = gameArea[i + 3];
                gameArea[i].Bottom = gameArea[i + 4];
                if(i < 2)
                    gameArea[i].BottomRight = gameArea[i + 1];
            }

            for(int i = 3; i < 7; ++i)
            {
                gameArea[i].BottomLeft = gameArea[i + 4];
                gameArea[i].Bottom = gameArea[i + 5];
                if(i < 6)
                    gameArea[i].BottomRight = gameArea[i + 1];
            }

            for(int i = 7; i < 12; ++i)
            {
                if(i < 11)
                {
                    gameArea[i].Bottom = gameArea[i + 5];
                    gameArea[i].BottomRight = gameArea[i + 1];
                }
                if(i > 7)
                    gameArea[i].BottomLeft = gameArea[i + 4];
            }

            for(int i = 12; i < 16; ++i)
            {
                if(i < 15)
                {
                    gameArea[i].Bottom = gameArea[i + 4];
                    gameArea[i].BottomRight = gameArea[i + 1];
                }
                if(i > 12)
                    gameArea[i].BottomLeft = gameArea[i + 3];
            }

            for (int i = 16; i < 17; ++i)
            {
                gameArea[i].BottomRight = gameArea[i + 1];
            }

            gameArea[18].TopLeft = gameArea[17];

            for (int i = 0; i < 19; ++i)
            {
                for (int j = 0; j < 6; ++j)
                {
                    if (gameArea[i].points[j] == null)
                        gameArea[i].points[j] = new LocationPoint(gameArea[i]);
                }
            }
        }

        public void SetupGameBoard()
        {
            Random rand = new Random();
            gameArea = new List<Hex>();
            List<LandType> landTypes = new List<LandType>();
            for (int i = 0; i < 6; ++i)
            {
                int maxType = 0;
                if ((LandType.Clay == (LandType)i) || (LandType.Rock == (LandType)i))
                    maxType = 3;
                else if (LandType.Sand == (LandType)i)
                    maxType = 1;
                else
                    maxType = 4;
                for (int j = 0; j < maxType; ++j)
                    landTypes.Add((LandType)i);
            }

            List<SeaType> seaTypes = new List<SeaType>();
            seaTypes.Add(SeaType.WheatHarbor);
            seaTypes.Add(SeaType.WoodHarbor);
            seaTypes.Add(SeaType.SheepHarbor);
            seaTypes.Add(SeaType.RockHarbor);
            seaTypes.Add(SeaType.ClayHarbor);
            for (int i = 0; i < 4; ++i)
                seaTypes.Add(SeaType.AnyTrade);

            while (landTypes.Any())
            {
                int randValue = (rand.Next() % landTypes.Count);
                LandType currentType = landTypes[randValue];
                gameArea.Add(new LandHex(currentType));
                landTypes.RemoveAt(randValue);
            }


            for (int i = 0; i < 18; ++i)
            {
                SeaType currentType;
                int randValue = 0;
                if ((i % 2) == 1)
                    currentType = SeaType.Sea;
                else
                {
                    randValue = (rand.Next(seaTypes.Count - 1));
                    currentType = seaTypes[randValue];
                }
                gameArea.Add(new SeaHex(currentType));
                if (currentType != SeaType.Sea)
                    seaTypes.RemoveAt(randValue);
            }

            ConnectBoard();

            List<LocationPoint> points = new List<LocationPoint>();
            for (int i = 0; i < 19; ++i)
            {
                Console.WriteLine("Tile " + i + " has type " + Enum.GetName(typeof(LandType), ((LandHex)gameArea[i]).LandType));
            }

            Console.ReadLine();
        }
    }
}