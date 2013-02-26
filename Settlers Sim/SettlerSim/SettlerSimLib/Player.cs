using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SettlerSimLib
{
    public abstract class Player
    {
        public Player()
        {
            resourceHand = new List<CardType>();
            ++playersNumber;
            playerNumber = playersNumber;
        }

        private static int playersNumber = 0;

        private int playerNumber;
        public int PlayerNumber
        {
            get
            {
                return playerNumber;
            }
        }

        private List<CardType> resourceHand;

        public bool TakeResource(List<CardType> cardsToTake)
        {
            foreach (CardType card in cardsToTake)
            {
                if (!resourceHand.Contains(card))
                    return false;
            }

            foreach (CardType cardToTake in cardsToTake)
                resourceHand.Remove(cardToTake);

            return true;
        }

        // What can players do on there turn

        // Building interface :
        // Can place roads
        // Can place settlements
        // Can place cities

        // Trading interface
        // Can trade with harbors (this includes the 4 to one trade)
        // Can trade with other players

        // Development card Interface :
        // Can buy Development cards
        // Can play Development cards
    }
}
