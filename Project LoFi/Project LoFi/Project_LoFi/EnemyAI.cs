using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_LoFi
{
    public class EnemyAI
    {
        public List<PlayerUnit> characterList;
        public List<EnemyUnit> enemyList;

        public EnemyAI(List<PlayerUnit> cList, List<EnemyUnit> eList)
        {
            characterList = cList;
            enemyList = eList;
        }

        //determine if the computer should attack a player
        public bool AttackPlayer()
        {
            return false;
        }

        //determine if the computer should move towards a player
        public bool MoveTowardsPlayer()
        {
            return false;
        }

        //determine if the computer should chase after a retreating player
        public bool ChasePlayer()
        {
            return false;
        }

        //determine if the computer should use an item ( if they even have items to use)
        public bool UseItem(MovableGridOccupant enemy)
        {
            return false;
        }

        //can the enemy see a player
        public bool IsPlayerVisible(int enemyNum, PlayerUnit player)
        {
            if ( (Math.Abs((player.X - enemyList[enemyNum].X))) < 5 && (Math.Abs((player.Y - enemyList[enemyNum].Y))) < 5 )
            {
                return true;
            }
            return false;
        }

        //find closest player
        public PlayerUnit ClosestPlayer(int enemyNum)
        {
            int x = 100;
            int y;
            int pNum = 0;

            if( (Math.Abs((characterList[0].X - enemyList[enemyNum].X))) < x && (Math.Abs((characterList[0].Y - enemyList[enemyNum].Y))) < y)
            {
                x = Math.Abs((characterList[0].X - enemyList[enemyNum].X));
                y = Math.Abs((characterList[0].Y - enemyList[enemyNum].Y));
                pNum = 0;
            }
            if ((Math.Abs((characterList[1].X - enemyList[enemyNum].X))) < x && (Math.Abs((characterList[1].Y - enemyList[enemyNum].Y))) < y)
            {
                x = Math.Abs((characterList[1].X - enemyList[enemyNum].X));
                y = Math.Abs((characterList[1].Y - enemyList[enemyNum].Y));
                pNum = 1;
            }
            if ((Math.Abs((characterList[2].X - enemyList[enemyNum].X))) < x && (Math.Abs((characterList[2].Y - enemyList[enemyNum].Y))) < y)
            {
                x = Math.Abs((characterList[2].X - enemyList[enemyNum].X));
                y = Math.Abs((characterList[2].Y - enemyList[enemyNum].Y));
                pNum = 2;
            }

            return characterList[pNum];
        }//end ClosestPlayer

    }//end class
}
