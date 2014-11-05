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
        private Random rand;

        public EnemyAI(List<PlayerUnit> cList, List<EnemyUnit> eList)
        {
            characterList = cList;
            enemyList = eList;
            rand = new Random();
        }

        //determine if the computer should attack a player
        public bool AttackPlayer(EnemyUnit enemy, PlayerUnit player)
        {
            //is the player next to the enemy
            if(IsNextTo(enemy, player))
            {
                //is the players life twice the enemies
                if( (enemy.Health * 2) < player.Health )
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsNextTo(EnemyUnit enemy, PlayerUnit player)
        {
            //if the player is right next to the enemy unit return true
            if (((player.X - 1) == enemy.X || player.X + 1 == enemy.X || player.X == enemy.X) &&
                ((player.Y - 1) == enemy.Y || player.Y + 1 == enemy.Y || player.Y == enemy.Y))
            {
                return true;
            }
            return false;
        }

        //determine if the computer should move towards a player
        public bool MoveTowardsPlayer(EnemyUnit enemy, PlayerUnit player)
        {
            int num = rand.Next(1, 101);

            if( !RunAway(enemy, player) && ( (num % 2) == 0))
            {
                return true;
            }
            return false;
        }

        //determine if the computer should chase after a retreating player
        public bool ChasePlayer(EnemyUnit enemy, PlayerUnit player)
        {
            //if players life is less then half of enemies they will pursue the player
            if(player.Health < (enemy.Health / 2))
            {
                return true;
            }
            return false;
        }

        //run away from the player
        public bool RunAway(EnemyUnit enemy, PlayerUnit player)
        {
            if (player.Health > (enemy.Health * 1.5))
            {
                return true;
            }
            return false;
        }

        //determine if the computer should use an item ( if they even have items to use)
        public bool UseItem(MovableGridOccupant enemy)
        {
            return false;
        }

        //can the enemy see or hear a player nearby
        public bool IsPlayerVisible(EnemyUnit enemy, PlayerUnit player)
        {
            //if player is within 4 spaces of the enemy it is considered seen or heard
            if ((Math.Abs((player.X - enemy.X))) < 5 && (Math.Abs((player.Y - enemy.Y))) < 5)
            {
                return true;
            }
            return false;
        }

        
        /// <summary> 
        /// /// which side is the player on in relation to the enemy
        /// 0 --> above, 1 --> below, 2 --> left, 3 --> right
        /// </summary>
        ///
        /// <param name="enemy"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        public int WhichSide(EnemyUnit enemy, PlayerUnit player)
        {
            if( player.X == enemy.X )
            {
                if( player.Y < enemy.Y)
                {
                    return 0;
                }
                else if( player.Y > enemy.Y )
                {
                    return 1;
                }
            }
            else if( player.Y == enemy.Y )
            {
                if (player.X < enemy.X )
                {
                    return 2;
                }
                else if (player.X > enemy.X )
                {
                    return 3;
                }
            }

            //default return
            return 4;
        }

        //find closest player
        public PlayerUnit ClosestPlayer(EnemyUnit enemy)
        {
            //set x, y coordinates to some number that could never be on the map
            int x = 1000;
            int y = 1000;
            int pNum = 0;

            //check how close the player is to the enemy and store new x,y coords
            if( (Math.Abs((characterList[0].X - enemy.X))) < x && (Math.Abs((characterList[0].Y - enemy.Y))) < y)
            {
                x = Math.Abs((characterList[0].X - enemy.X));
                y = Math.Abs((characterList[0].Y - enemy.Y));
                pNum = 0;
            }
            if ((Math.Abs((characterList[1].X - enemy.X))) < x && (Math.Abs((characterList[1].Y - enemy.Y))) < y)
            {
                x = Math.Abs((characterList[1].X - enemy.X));
                y = Math.Abs((characterList[1].Y - enemy.Y));
                pNum = 1;
            }
            if ((Math.Abs((characterList[2].X - enemy.X))) < x && (Math.Abs((characterList[2].Y - enemy.Y))) < y)
            {
                x = Math.Abs((characterList[2].X - enemy.X));
                y = Math.Abs((characterList[2].Y - enemy.Y));
                pNum = 2;
            }

            //return character closest to the enemy
            return characterList[pNum];
        }//end ClosestPlayer

    }//end class
}
