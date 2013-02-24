using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SettlerSimLib
{
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
                if (i > 7)
                    gameArea[i].BottomLeft = gameArea[i + 4];
                if(i < 11)
                {
                    gameArea[i].Bottom = gameArea[i + 5];
                    gameArea[i].BottomRight = gameArea[i + 1];
                }
            }

            for(int i = 12; i < 16; ++i)
            {
                if (i > 12)
                    gameArea[i].BottomLeft = gameArea[i + 3];
                if(i < 15)
                {
                    gameArea[i].Bottom = gameArea[i + 4];
                    gameArea[i].BottomRight = gameArea[i + 1];
                }
            }

            for (int i = 16; i < 18; ++i)
            {
                gameArea[i].BottomRight = gameArea[i + 1];
            }

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

            int randomHarbor = rand.Next(seaHarbors.Count);
            gameArea[0].Points[(int)LocationPoints.TopLeft].Harbor = seaHarbors[randomHarbor];
            gameArea[0].Points[(int)LocationPoints.TopRight].Harbor = seaHarbors[randomHarbor];
            seaHarbors.RemoveAt(randomHarbor);

            randomHarbor = rand.Next(seaHarbors.Count);
            gameArea[1].Points[(int)LocationPoints.TopRight].Harbor = seaHarbors[randomHarbor];
            gameArea[1].Points[(int)LocationPoints.Right].Harbor = seaHarbors[randomHarbor];
            seaHarbors.RemoveAt(randomHarbor);

            randomHarbor = rand.Next(seaHarbors.Count);
            gameArea[6].Points[(int)LocationPoints.TopRight].Harbor = seaHarbors[randomHarbor];
            gameArea[6].Points[(int)LocationPoints.Right].Harbor = seaHarbors[randomHarbor];
            seaHarbors.RemoveAt(randomHarbor);

            randomHarbor = rand.Next(seaHarbors.Count);
            gameArea[11].Points[(int)LocationPoints.Right].Harbor = seaHarbors[randomHarbor];
            gameArea[11].Points[(int)LocationPoints.BottomRight].Harbor = seaHarbors[randomHarbor];
            seaHarbors.RemoveAt(randomHarbor);

            randomHarbor = rand.Next(seaHarbors.Count);
            gameArea[15].Points[(int)LocationPoints.BottomRight].Harbor = seaHarbors[randomHarbor];
            gameArea[15].Points[(int)LocationPoints.BottomLeft].Harbor = seaHarbors[randomHarbor];
            seaHarbors.RemoveAt(randomHarbor);

            randomHarbor = rand.Next(seaHarbors.Count);
            gameArea[17].Points[(int)LocationPoints.BottomRight].Harbor = seaHarbors[randomHarbor];
            gameArea[17].Points[(int)LocationPoints.BottomLeft].Harbor = seaHarbors[randomHarbor];
            seaHarbors.RemoveAt(randomHarbor);

            randomHarbor = rand.Next(seaHarbors.Count);
            gameArea[16].Points[(int)LocationPoints.BottomLeft].Harbor = seaHarbors[randomHarbor];
            gameArea[16].Points[(int)LocationPoints.Left].Harbor = seaHarbors[randomHarbor];
            seaHarbors.RemoveAt(randomHarbor);

            randomHarbor = rand.Next(seaHarbors.Count);
            gameArea[12].Points[(int)LocationPoints.Left].Harbor = seaHarbors[randomHarbor];
            gameArea[12].Points[(int)LocationPoints.TopLeft].Harbor = seaHarbors[randomHarbor];
            seaHarbors.RemoveAt(randomHarbor);

            randomHarbor = rand.Next(seaHarbors.Count);
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
                    hex.Points[i].AddToEdgeList(edge);
                    hex.Points[toConnect].AddToEdgeList(edge);
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

        private void HighProbabilityDiceValuesSetup()
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
                Hex hex = gameArea[rand.Next(gameArea.Count)];

                // Check if that hexes neighboring hexes have a high probability number.
                bool neighboringHexWithHighProb = false;
                foreach (Hex neighboringHex in hex.NeighboringHexes)
                {
                    if ((hex.DiceRollValue == 6) || (hex.DiceRollValue == 8))
                        neighboringHexWithHighProb = true;
                }

                // If no neighboring hexes have a high probability number.
                if (!neighboringHexWithHighProb && hex.LandType != LandType.Sand)
                {
                    hex.DiceRollValue = values.First();
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
                Hex hex = gameArea[rand.Next(gameArea.Count)];

                // If not sand and the Dice Value was not already assigned
                if ((hex.LandType != LandType.Sand) && hex.DiceRollValue == -1)
                {
                    int randValue = rand.Next(values.Count);
                    hex.DiceRollValue = values[randValue];
                    values.RemoveAt(randValue);
                }
            }
        }

        private void SetDiceValuesForHexes()
        {
            HighProbabilityDiceValuesSetup();
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
                int randValue = (rand.Next(landTypes.Count));
                LandType currentType = landTypes[randValue];
                gameArea.Add(new Hex(currentType));
                landTypes.RemoveAt(randValue);
            }

            ConnectBoard();
            ConnectPoints();
            SetDiceValuesForHexes();
        }

        public List<IHex> GameBoard
        {
            get
            {
                List<IHex> gameBoard = new List<IHex>();
                foreach (Hex hex in gameArea)
                    gameBoard.Add(hex);
                return gameBoard;
            }
        }

        private SettlerBoard()
        {
            SetupGameBoard();
        }

        private static SettlerBoard instance;
        public static SettlerBoard Instance
        {
            get
            {
                if (instance == null)
                    instance = new SettlerBoard();
                return instance;
            }
        }
    }
}