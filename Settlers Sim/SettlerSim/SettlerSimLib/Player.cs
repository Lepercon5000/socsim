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
        }

        public int PlayerNumber
        {
            get;
            set;
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
        // Can play Development carda
    }
}
