using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Relic_Proto
{
    public class pathFinder
    {
        public List<MobComponent> mobs = new List<MobComponent>();
        public List<AllyComponent> allies = new List<AllyComponent>();
        public int[,] iMap;
        public bool[,] Walkable;
        public int[] playerposition = new int[2];
        string[] directions = new string[4];
        public String chosenDirection;
        int RightPriority;
        int LeftPriority;
        int UpPriority;
        int DownPriority;
        int[] MoveLeft = new int[2];
        int[] MoveRight = new int[2];
        int[] MoveUp = new int[2];
        int[] MoveDown = new int[2];
        List<int> PrioritiesX = new List<int>();
        List<int> PrioritiesY = new List<int>();

        public pathFinder(int[,] Map)
        {

            iMap = Map;
            Walkable = new bool[100, 100];
            playerposition[0] = 0;
            playerposition[1] = 0;
            MoveLeft[0] = -1;
            MoveLeft[1] = 0;
            MoveUp[0] = 0;
            MoveUp[1] = -1;
            MoveRight[0] = 1;
            MoveRight[1] = 0;
            MoveDown[0] = 0;
            MoveDown[1] = 1;
            PrioritiesX.Add(MoveLeft[1]);
            PrioritiesX.Add(MoveUp[0]);
            PrioritiesX.Add(MoveRight[0]);
            PrioritiesX.Add(MoveDown[1]);
            PrioritiesY.Add(MoveLeft[1]);
            PrioritiesY.Add(MoveUp[1]);
            PrioritiesY.Add(MoveRight[1]);
            PrioritiesY.Add(MoveDown[1]);

            for (int y = 0; y < 100; y++)
            {
                for (int x = 0; x < 100; x++)
                {
                    switch (iMap[y, x])
                    {
                        case 6:
                        case 8:
                        case 9:
                        case 11:
                        case 18:
                        case 19:
                        case 20:
                        case 21:
                        case 22:
                        case 24:
                        case 25:
                        case 26:
                        case 27:
                        case 38:
                        case 39:
                        case 40:
                        case 41:
                        case 49:
                        case 50:
                        case 51:
                        case 53:
                        case 57:
                        case 58:
                        case 59:
                        case 60:
                        case 61:
                        case 62:
                        case 63:
                        case 64:
                        case 65:
                        case 66:
                        case 68:
                        case 70:
                        case 71:
                        case 77:
                        case 81:
                            Walkable[y, x] = false;
                            break;
                        default:
                            Walkable[y, x] = true;
                            break;
                    }
                }
            }
        }
        public int[] DoSearch(int[] position)
        {
            int[] newPosition = new int[2];
            int[] start = new int[2];
            start = position;
            CheckPriorities(position);
            UpdateCollisions();
            newPosition = position;
            chosenDirection = "Stand";
            if (!finalPosition(position, playerposition))
            {
                if (Walkable[position[1] + PrioritiesY[0], position[0] + PrioritiesX[0]] == true)
                {
                    newPosition[0] = position[0] + PrioritiesX[0];
                    newPosition[1] = position[1] + PrioritiesY[0];
                    chosenDirection = directions[0];
                }

                else if (Walkable[position[1] + PrioritiesY[1], position[0] + PrioritiesX[1]] == true)
                {
                    newPosition[0] = position[0] + PrioritiesX[1];
                    newPosition[1] = position[1] + PrioritiesY[1];
                    chosenDirection = directions[1];
                }

                else if (Walkable[position[1] + PrioritiesY[2], position[0] + PrioritiesX[2]] == true)
                {
                    newPosition[0] = position[0] + PrioritiesX[2];
                    newPosition[1] = position[1] + PrioritiesY[2];

                    chosenDirection = directions[2];
                }

                else if (Walkable[position[1] + PrioritiesY[3], position[0] + PrioritiesX[3]] == true)
                {
                    newPosition[0] = position[0] + PrioritiesX[3];
                    newPosition[1] = position[1] + PrioritiesY[3];
                    chosenDirection = directions[3];
                }
            }
            else
            {
                chosenDirection = "Still";
            }
            return newPosition;
        }
        public int[] DoSearchOLD(int[] position)
        {
            int[] newPosition = new int[2];
            CheckPriorities(position);
            UpdateCollisions();
            newPosition = position;

            if (!finalPosition(position, playerposition))
            {
                if (Walkable[position[1] + PrioritiesY[0], position[0] + PrioritiesX[0]] == true)
                {
                    newPosition[0] = position[0] + PrioritiesX[0];
                    newPosition[1] = position[1] + PrioritiesY[0];
                }

                else if (Walkable[position[1] + PrioritiesY[1], position[0] + PrioritiesX[1]] == true)
                {
                    newPosition[0] = position[0] + PrioritiesX[1];
                    newPosition[1] = position[1] + PrioritiesY[1];
                }

                else if (Walkable[position[1] + PrioritiesY[2], position[0] + PrioritiesX[2]] == true)
                {
                    newPosition[0] = position[0] + PrioritiesX[2];
                    newPosition[1] = position[1] + PrioritiesY[2];
                }

                else if (Walkable[position[1] + PrioritiesY[3], position[0] + PrioritiesX[3]] == true)
                {
                    newPosition[0] = position[0] + PrioritiesX[3];
                    newPosition[1] = position[1] + PrioritiesY[3];
                }
            }
            return newPosition;
        }

        public void CheckPriorities(int[] position)
        {
            LeftPriority = -(playerposition[0] - position[0]);
            RightPriority = playerposition[0] - position[0];
            UpPriority = -(playerposition[1] - position[1]);
            DownPriority = (playerposition[1] - position[1]);

            if ((LeftPriority >= RightPriority) && (LeftPriority >= UpPriority) && (LeftPriority >= DownPriority))
            {
                PrioritiesX[0] = MoveLeft[0];
                PrioritiesY[0] = MoveLeft[1];
                directions[0] = "Left";
                if (UpPriority >= DownPriority)
                {
                    PrioritiesX[1] = MoveUp[0];
                    PrioritiesY[1] = MoveUp[1];
                    PrioritiesX[2] = MoveDown[0];
                    PrioritiesY[2] = MoveDown[1];
                    directions[1] = "Up";
                    directions[2] = "Down";
                }
                else
                {
                    PrioritiesX[1] = MoveDown[0];
                    PrioritiesY[1] = MoveDown[1];
                    PrioritiesX[2] = MoveUp[0];
                    PrioritiesY[2] = MoveUp[1];

                    directions[1] = "Down";
                    directions[2] = "Up";
                }
                PrioritiesX[3] = MoveRight[0];
                PrioritiesY[3] = MoveRight[1];
                directions[3] = "Right";
            }

            else if ((RightPriority >= LeftPriority) && (RightPriority >= UpPriority) && (RightPriority >= DownPriority))
            {
                PrioritiesX[0] = MoveRight[0];
                PrioritiesY[0] = MoveRight[1];
                directions[0] = "Right";
                if (UpPriority >= DownPriority)
                {
                    PrioritiesX[1] = MoveUp[0];
                    PrioritiesY[1] = MoveUp[1];
                    PrioritiesX[2] = MoveDown[0];
                    PrioritiesY[2] = MoveDown[1];
                    directions[1] = "Up";
                    directions[2] = "Down";
                }
                else
                {
                    PrioritiesX[1] = MoveDown[0];
                    PrioritiesY[1] = MoveDown[1];
                    PrioritiesX[2] = MoveUp[0];
                    PrioritiesY[2] = MoveUp[1];

                    directions[1] = "Down";
                    directions[2] = "Up";
                }
                PrioritiesX[3] = MoveLeft[0];
                PrioritiesY[3] = MoveLeft[1];
                directions[3] = "Left";
            }

            else if ((UpPriority >= LeftPriority) && (UpPriority >= RightPriority) && (UpPriority >= DownPriority))
            {
                PrioritiesX[0] = MoveUp[0];
                PrioritiesY[0] = MoveUp[1];
                directions[0] = "Up";
                if (RightPriority >= LeftPriority)
                {
                    PrioritiesX[1] = MoveRight[0];
                    PrioritiesY[1] = MoveRight[1];
                    PrioritiesX[2] = MoveLeft[0];
                    PrioritiesY[2] = MoveLeft[1];
                    directions[1] = "Right";
                    directions[2] = "Left";
                }
                else
                {
                    PrioritiesX[1] = MoveLeft[0];
                    PrioritiesY[1] = MoveLeft[1];
                    PrioritiesX[2] = MoveRight[0];
                    PrioritiesY[2] = MoveRight[1];
                    directions[1] = "Left";
                    directions[2] = "Right";
                }
                PrioritiesX[3] = MoveDown[0];
                PrioritiesY[3] = MoveDown[1];
                directions[3] = "Down";
            }

            else if ((DownPriority >= LeftPriority) && (DownPriority >= RightPriority) && (DownPriority >= UpPriority))
            {
                PrioritiesX[0] = MoveDown[0];
                PrioritiesY[0] = MoveDown[1];
                directions[0] = "Down";
                if (RightPriority >= LeftPriority)
                {
                    PrioritiesX[1] = MoveRight[0];
                    PrioritiesY[1] = MoveRight[1];
                    PrioritiesX[2] = MoveLeft[0];
                    PrioritiesY[2] = MoveLeft[1];
                    directions[1] = "Right";
                    directions[2] = "Left";
                }
                else
                {
                    PrioritiesX[1] = MoveLeft[0];
                    PrioritiesY[1] = MoveLeft[1];
                    PrioritiesX[2] = MoveRight[0];
                    PrioritiesY[2] = MoveRight[1];
                    directions[1] = "Left";
                    directions[2] = "Right";
                }
                PrioritiesX[3] = MoveUp[0];
                PrioritiesY[3] = MoveUp[1];
                directions[3] = "Up";
            }
        }

        public bool finalPosition(int[] position, int[] playerposition)
        {
            bool finalPosition = false;

            if (((position[0] == playerposition[0] - 1) || (position[0] == playerposition[0] + 1)) && position[1] == playerposition[1])
            {
                finalPosition = true;
            }
            if (((position[1] == playerposition[1] - 1) || (position[1] == playerposition[1] + 1)) && position[0] == playerposition[0])
            {
                finalPosition = true;
            }

            return finalPosition;
        }

        public void UpdateCollisions()
        {
            for (int y = 0; y < 100; y++)
            {
                for (int x = 0; x < 100; x++)
                {
                    switch (iMap[y, x])
                    {
                        case 6:
                        case 8:
                        case 9:
                        case 11:
                        case 18:
                        case 19:
                        case 20:
                        case 21:
                        case 22:
                        case 24:
                        case 25:
                        case 26:
                        case 27:
                        case 38:
                        case 39:
                        case 40:
                        case 41:
                        case 49:
                        case 50:
                        case 51:
                        case 53:
                        case 57:
                        case 58:
                        case 59:
                        case 60:
                        case 61:
                        case 62:
                        case 63:
                        case 64:
                        case 65:
                        case 66:
                        case 68:
                        case 70:
                        case 71:
                        case 77:
                        case 81:
                            Walkable[y, x] = false;
                            break;
                        default:
                            Walkable[y, x] = true;
                            break;
                    }
                }
            }

            foreach (MobComponent thisMob in mobs)
            {
                if (thisMob.alive == true)
                {
                    Walkable[thisMob.position[1], thisMob.position[0]] = false;
                }
            }

            foreach (AllyComponent thisMob in allies)
            {
                if (thisMob.alive == true)
                {
                    Walkable[thisMob.position[1], thisMob.position[0]] = false;
                }
            }
            Walkable[playerposition[1], playerposition[0]] = false;
        }
    }
}
