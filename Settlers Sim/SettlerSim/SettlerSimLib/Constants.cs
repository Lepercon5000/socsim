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
}
