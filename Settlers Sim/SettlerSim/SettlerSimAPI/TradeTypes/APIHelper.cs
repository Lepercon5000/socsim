using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SettlerSimLib;

namespace SettlerSimAPI
{
    class APIHelper
    {
        public Boolean getResourceTypeFromString(string resourceType, out CardType type)
        {
            switch (resourceType)
            {
                case "Wood":
                    type = CardType.Wood;
                    break;
                case "Sheep":
                    type = CardType.Sheep;
                    break;
                case "Wheat":
                    type = CardType.Wheat;
                    break;
                case "Rock":
                    type = CardType.Rock;
                    break;
                case "Clay":
                    type = CardType.Clay;
                    break;
                default:
                    type = CardType.Wood;
                    return false;
            }
            return true;
        }

        public Boolean getHarborTypeFromString(string harborType, out SeaHarbor type)
        {
            switch (harborType)
            {
                case "Clay":
                    type = SeaHarbor.ClayHarbor;
                    break;
                case "Rock":
                    type = SeaHarbor.RockHarbor;
                    break;
                case "Wheat":
                    type = SeaHarbor.WheatHarbor;
                    break;
                case "Sheep":
                    type = SeaHarbor.SheepHarbor;
                    break;
                case "Wood":
                    type = SeaHarbor.WoodHarbor;
                    break;
                case "Three":
                    type = SeaHarbor.ThreeHarbor;
                    break;
                default:
                    type = SeaHarbor.NotHarbor;
                    return false;
            }

            return true;
        }

    }
}
