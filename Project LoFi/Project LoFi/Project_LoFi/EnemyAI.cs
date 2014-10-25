using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_LoFi
{
    public class EnemyAI
    {
        //determine if the computer should attack a player
        public bool AttackPlayer(MovableGridOccupant player1, MovableGridOccupant player2,
            MovableGridOccupant player3, MovableGridOccupant enemy)
        {
            return false;
        }

        //determine if the computer should move towards a player
        public bool MoveTowardsPlayer(MovableGridOccupant player1, MovableGridOccupant player2,
            MovableGridOccupant player3, MovableGridOccupant enemy)
        {
            return false;
        }

        //determine if the computer should chase after a retreating player
        public bool ChasePlayer(MovableGridOccupant player1, MovableGridOccupant player2,
            MovableGridOccupant player3, MovableGridOccupant enemy)
        {
            return false;
        }

        //determine if the computer should use an item ( if they even have items to use)
        public bool UseItem(MovableGridOccupant enemy)
        {
            return false;
        }

        //can the enemy see a player
        public bool IsPlayerVisible(MovableGridOccupant player1, MovableGridOccupant player2,
            MovableGridOccupant player3, MovableGridOccupant enemy)
        {
            return false;
        }

    }//end class
}
