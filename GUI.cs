using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class GUI
    {
        public GUI()
        {

        }

        public void Draw(PNode initial, PNode goal, List<toGrid> wall, PNode visitedNode, int mapWidth, int mapLength)
        {
            Console.Clear();
            bool wallDrawn = false;

            for (int i = 0; i < mapWidth; i++)
            {
                for (int j = 0; j < mapLength; j++)
                {
                    if ((initial.X == j) && (initial.Y == i))
                    {
                        Console.Write("|i");
                        continue;
                    }

                    if ((goal.X == j) && (goal.Y == i))
                    {
                        Console.Write("|g");
                        continue;
                    }

                    if ((visitedNode.X == j) && (visitedNode.Y == i))
                    {
                        Console.Write("|x");
                        continue;
                    }

                    foreach (toGrid g in wall)
                    {
                        if ((g.WallCheck == true) && (g.Pos.X == j) && (g.Pos.Y == i))
                        {
                            Console.Write("|w");
                            wallDrawn = true;
                            break;
                        }
                        wallDrawn = false;
                    }
                    
                    if (wallDrawn == false)
                        Console.Write("| ");
                }
                Console.WriteLine("|");
            }

            if ((visitedNode.X == goal.X) && (visitedNode.Y == goal.Y))
            {
                Console.WriteLine("\nWas solved at: ({0},{1})", visitedNode.X, visitedNode.Y);
            }
            else Console.WriteLine("\nExpanding: ({0},{1})", visitedNode.X, visitedNode.Y);

        }

        public void DrawPath(PNode initial, PNode goal, List<toGrid> wall, int mapWidth, int mapLength, List<PNode> path)
        {
            Console.Clear();
            bool wallDrawn = false;
            for (int i = 0; i < mapWidth; i++)
            {
                for (int j = 0; j < mapLength; j++)
                {
                    if ((initial.X == j) && (initial.Y == i))
                    {
                        Console.Write("|i");
                        continue;
                    }

                    if ((goal.X == j) && (goal.Y == i))
                    {
                        Console.Write("|g");
                        continue;
                    }

                    if (path.Any(x => x.X == j && x.Y == i))
                    {
                        Console.Write("|x");
                        continue;
                    }

                    foreach (toGrid g in wall)
                    {
                        if ((g.WallCheck == true) && (g.Pos.X == j) && (g.Pos.Y == i))
                        {
                            Console.Write("|w");
                            wallDrawn = true;
                            break;
                        }
                        wallDrawn = false;
                    }

                    if (wallDrawn == false)
                        Console.Write("| ");
                }
                Console.WriteLine("|");
            }
            Console.WriteLine();
        }
    }
}
