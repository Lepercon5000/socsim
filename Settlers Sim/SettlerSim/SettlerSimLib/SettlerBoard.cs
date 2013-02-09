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

    public enum SeaHarbor
    {
        WoodHarbor = CardType.Wood,
        SheepHarbor = CardType.Sheep,
        WheatHarbor = CardType.Wheat,
        RockHarbor = CardType.Rock,
        ClayHarbor = CardType.Clay,
        ThreeHarbor = 5,
        NotHarbor = 6
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

    public class Edge
    {
        public Edge(LocationPoint point1, LocationPoint point2)
        {
            Occupied = false;
            connectingPoints = new LocationPoint[2];
            Point1 = point1;
            Point2 = point2;
        }
        private LocationPoint[] connectingPoints;
        public LocationPoint[] ConnectingPoints
        {
            get
            {
                return connectingPoints;
            }
        }

        public LocationPoint Point1
        {
            get
            {
                return connectingPoints[0];
            }
            private set
            {
                connectingPoints[0] = value;
            }
        }
        public LocationPoint Point2
        {
            get
            {
                return connectingPoints[1];
            }
            private set
            {
                connectingPoints[1] = value;
            }
        }
        public bool Occupied { get; set; }
    }

    public class LocationPoint
    {
        public List<Hex> AttachedHex;
        public List<Edge> Edges;
        public SeaHarbor Harbor { get; set; }

        public bool PointIsNeighboring(LocationPoint point)
        {
            foreach (Edge edge in Edges)
            {
                if (edge.ConnectingPoints.Contains(point))
                    return true;
            }
            return false;
        }

        public LocationPoint(Hex attachingHex)
        {
            AttachedHex = new List<Hex>();
            Edges = new List<Edge>();
            AttachedHex.Add(attachingHex);
            Harbor = SeaHarbor.NotHarbor;
        }
    }

    public class PointCollection
    {
        public PointCollection()
        {
            points = new LocationPoint[6];
            for (int i = 0; i < 6; ++i)
                points[i] = null;
        }
        private LocationPoint[] points;
        public LocationPoint this[int index]
        {
            get
            {
                return points[index];
            }
            set
            {
                points[index] = value;
            }
        }
        public LocationPoint this[LocationPoints index]
        {
            get
            {
                return this[(int)index];
            }
            set
            {
                this[(int)index] = value;
            }
        }

        public LocationPoint Left
        {
            get
            {
                return this[LocationPoints.Left];
            }
            set
            {
                this[LocationPoints.Left] = value;
            }
        }
        public LocationPoint TopLeft
        {
            get
            {
                return this[LocationPoints.TopLeft];
            }
            set
            {
                this[LocationPoints.TopLeft] = value;
            }
        }
        public LocationPoint TopRight
        {
            get
            {
                return this[LocationPoints.TopRight];
            }
            set
            {
                this[LocationPoints.TopRight] = value;
            }
        }
        public LocationPoint Right
        {
            get
            {
                return this[LocationPoints.Right];
            }
            set
            {
                this[LocationPoints.Right] = value;
            }
        }
        public LocationPoint BottomRight
        {
            get
            {
                return this[LocationPoints.BottomRight];
            }
            set
            {
                this[LocationPoints.BottomRight] = value;
            }
        }
        public LocationPoint BottomLeft
        {
            get
            {
                return this[LocationPoints.BottomLeft];
            }
            set
            {
                this[LocationPoints.BottomLeft] = value;
            }
        }
        
    }

    public class Hex
    {
        private Hex[] neighboringHexes;
        public Hex[] NeighboringHexes
        {
            get
            {
                return neighboringHexes;
            }
        }
        private int diceRoleValue;
        public int DiceRoleValue
        {
            get
            {
                return diceRoleValue;
            }
            set
            {
                if (diceRoleValue == -1)
                    diceRoleValue = value;
            }
        }

        public PointCollection Points;

        public Hex(LandType type)
        {
            landType = type;
            diceRoleValue = -1;

            neighboringHexes = new Hex[6];
            for (int i = 0; i < 6; ++i)
                neighboringHexes[i] = null;

            Points = new PointCollection();
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
                    this.neighboringHexes[(int)neighboringHex].Points[neighboringLocation].AttachedHex.Add(this);
            }
            else
            {
                if (this.neighboringHexes[(int)neighboringHex].Points[neighboringLocation] == null)
                {
                    this.neighboringHexes[(int)neighboringHex].Points[neighboringLocation] = this.Points[thisLocation];
                    if (!this.Points[thisLocation].AttachedHex.Contains(this.neighboringHexes[(int)neighboringHex]))
                        this.Points[thisLocation].AttachedHex.Add(this.neighboringHexes[(int)neighboringHex]);
                }
                else
                {
                    this.Points[(int)thisLocation] = this.neighboringHexes[(int)neighboringHex].Points[(int)neighboringLocation];
                    if (!this.neighboringHexes[(int)neighboringHex].Points[(int)neighboringLocation].AttachedHex.Contains(this))
                        this.neighboringHexes[(int)neighboringHex].Points[(int)neighboringLocation].AttachedHex.Add(this);
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

    public class SettlerBoard
    {
        private List<Hex> gameArea;

        private void ConnectBoard()
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
                    if (gameArea[i].Points[j] == null)
                        gameArea[i].Points[j] = new LocationPoint(gameArea[i]);
                }
            }

            //Connect the Sea Harbor Points
            Random rand = new Random();
            List<SeaHarbor> seaHarbors = new List<SeaHarbor>();
            for (int i = 0; i < 6; ++i)
            {
                if (SeaHarbor.ThreeHarbor == (SeaHarbor)i)
                {
                    for (int j = 0; j < 3; ++j)
                        seaHarbors.Add((SeaHarbor)i);
                }
                seaHarbors.Add((SeaHarbor)i);
            }

            int randomHarbor = rand.Next() % seaHarbors.Count;
            gameArea[0].Points[(int)LocationPoints.TopLeft].Harbor = seaHarbors[randomHarbor];
            gameArea[0].Points[(int)LocationPoints.TopRight].Harbor = seaHarbors[randomHarbor];
            seaHarbors.RemoveAt(randomHarbor);

            randomHarbor = rand.Next() % seaHarbors.Count;
            gameArea[1].Points[(int)LocationPoints.TopRight].Harbor = seaHarbors[randomHarbor];
            gameArea[1].Points[(int)LocationPoints.Right].Harbor = seaHarbors[randomHarbor];
            seaHarbors.RemoveAt(randomHarbor);

            randomHarbor = rand.Next() % seaHarbors.Count;
            gameArea[6].Points[(int)LocationPoints.TopRight].Harbor = seaHarbors[randomHarbor];
            gameArea[6].Points[(int)LocationPoints.Right].Harbor = seaHarbors[randomHarbor];
            seaHarbors.RemoveAt(randomHarbor);

            randomHarbor = rand.Next() % seaHarbors.Count;
            gameArea[11].Points[(int)LocationPoints.Right].Harbor = seaHarbors[randomHarbor];
            gameArea[11].Points[(int)LocationPoints.BottomRight].Harbor = seaHarbors[randomHarbor];
            seaHarbors.RemoveAt(randomHarbor);

            randomHarbor = rand.Next() % seaHarbors.Count;
            gameArea[15].Points[(int)LocationPoints.BottomRight].Harbor = seaHarbors[randomHarbor];
            gameArea[15].Points[(int)LocationPoints.BottomLeft].Harbor = seaHarbors[randomHarbor];
            seaHarbors.RemoveAt(randomHarbor);

            randomHarbor = rand.Next() % seaHarbors.Count;
            gameArea[17].Points[(int)LocationPoints.BottomRight].Harbor = seaHarbors[randomHarbor];
            gameArea[17].Points[(int)LocationPoints.BottomLeft].Harbor = seaHarbors[randomHarbor];
            seaHarbors.RemoveAt(randomHarbor);

            randomHarbor = rand.Next() % seaHarbors.Count;
            gameArea[16].Points[(int)LocationPoints.BottomLeft].Harbor = seaHarbors[randomHarbor];
            gameArea[16].Points[(int)LocationPoints.Left].Harbor = seaHarbors[randomHarbor];
            seaHarbors.RemoveAt(randomHarbor);

            randomHarbor = rand.Next() % seaHarbors.Count;
            gameArea[12].Points[(int)LocationPoints.Left].Harbor = seaHarbors[randomHarbor];
            gameArea[12].Points[(int)LocationPoints.TopLeft].Harbor = seaHarbors[randomHarbor];
            seaHarbors.RemoveAt(randomHarbor);

            randomHarbor = rand.Next() % seaHarbors.Count;
            gameArea[3].Points[(int)LocationPoints.Left].Harbor = seaHarbors[randomHarbor];
            gameArea[3].Points[(int)LocationPoints.TopLeft].Harbor = seaHarbors[randomHarbor];
            seaHarbors.RemoveAt(randomHarbor);
        }

        private void FindPointsToConenct(Hex hex)
        {
            for (int i = 0; i < 6; ++i)
            {
                int toConnect = (i + 1) % 6;
                if (!hex.Points[i].PointIsNeighboring(hex.Points[toConnect]))
                {
                    Edge edge = new Edge(hex.Points[i], hex.Points[toConnect]);
                    hex.Points[i].Edges.Add(edge);
                    hex.Points[toConnect].Edges.Add(edge);
                }
            }
        }

        private void ConnectPoints()
        {
            foreach (Hex hex in gameArea)
            {
                FindPointsToConenct(hex);
            }
        }

        private void HighProbabilyDiceValuesSetup()
        {
            // Pick an initial random point
            List<int> values = new List<int>();
            for(int i = 0; i < 4; ++i)
            {
                if ((i % 2) == 0)
                    values.Add(8);
                else
                    values.Add(6);
            }
            Random rand = new Random();
            while (values.Any())
            {
                // Pick a random hex
                Hex hex = gameArea[rand.Next() % gameArea.Count];

                // Check if that hexes neighboring hexes have a high probability number.
                bool neighboringHexWithHighProb = false;
                foreach (Hex neighboringHex in hex.NeighboringHexes)
                {
                    if ((hex.DiceRoleValue == 6) || (hex.DiceRoleValue == 8))
                        neighboringHexWithHighProb = true;
                }

                // If no neighboring hexes have a high probability number.
                if (!neighboringHexWithHighProb && hex.LandType != LandType.Sand)
                {
                    hex.DiceRoleValue = values.First();
                    values.RemoveAt(0);
                }
            }
        }

        private void RestOfDiceValuesSetup()
        {
            List<int> values = new List<int>();
            for (int i = 2; i <= 12; ++i)
            {
                if ((i != 6) && (i != 8) && (i != 7))
                {
                    values.Add(i);
                    if ((i != 2) && (i != 12))
                        values.Add(i);
                }
            }

            Random rand = new Random();
            while (values.Any())
            {
                // Pick a random hex
                Hex hex = gameArea[rand.Next() % gameArea.Count];

                // If not sand and the Dice Value was not already assigned
                if ((hex.LandType != LandType.Sand) && hex.DiceRoleValue == -1)
                {
                    int randValue = rand.Next() % values.Count;
                    hex.DiceRoleValue = values[randValue];
                    values.RemoveAt(randValue);
                }
            }
        }

        private void SetDiceValuesForHexes()
        {
            HighProbabilyDiceValuesSetup();
            RestOfDiceValuesSetup();
        }

        private void SetupGameBoard()
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

            while (landTypes.Any())
            {
                int randValue = (rand.Next() % landTypes.Count);
                LandType currentType = landTypes[randValue];
                gameArea.Add(new Hex(currentType));
                landTypes.RemoveAt(randValue);
            }

            ConnectBoard();
            ConnectPoints();
            SetDiceValuesForHexes();

            for (int i = 0; i < gameArea.Count; ++i)
            {
                Hex hex = gameArea[i];
                Console.WriteLine("Hex " + i + " has type " + Enum.GetName(typeof(LandType), hex.LandType) + " with dice value of " + hex.DiceRoleValue);
            }
            Console.ReadLine();
        }

        public SettlerBoard()
        {
            SetupGameBoard();
        }
    }
}