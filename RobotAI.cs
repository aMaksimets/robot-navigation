using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace ConsoleApp1
{
    class RobotAI
    {
        private PNode pos;
        private PNode goalPos;
        private Map mapRobot;
        private GUI ui = new GUI();

        public PNode Pos
        { get { return pos; } }


        public RobotAI(string initialState, string goalState, Map map)
        {
            stringInt ifs = new stringInt(initialState);
            List<int> coordinate = ifs.getIntFromString();
            pos = new PNode(coordinate[0], coordinate[1]);
            ifs = new stringInt(goalState);

            coordinate = ifs.getIntFromString();
            goalPos = new PNode(coordinate[0], coordinate[1]);
            mapRobot = map;
        }


        public void notify()
        {
            Console.WriteLine("Current pos: ({0},{1})", pos.X, pos.Y);
            foreach (toGrid g in mapRobot.Grids)
            {
                if ((pos.X == g.Pos.X) && (pos.Y == g.Pos.Y))
                {
                    Console.WriteLine("Possible path : ");
                    foreach (Path p in g.Paths)
                    {
                        Console.WriteLine("({0},{1})", p.Location.Pos.X, p.Location.Pos.Y);
                    }
                }
            }
            Console.WriteLine("Goal ; to find ({0},{1})", goalPos.X, goalPos.Y);
        }


        public string MoveUp()
        {
            return "up";
        }

        public string MoveDown()
        {
            return "down";
        }

        public string MoveRight()
        {
            return "right";
        }

        public string MoveLeft()
        {
            return "left";
        }


        //dfs
        public string depthFirst()
        {
            if ((pos.X == goalPos.X) && (pos.Y == goalPos.Y))
            {
                return "The solution is the initial positition, no movement required";
            }
            else
            {
                Stack<PNode> open = new Stack<PNode>();
                List<PNode> visited = new List<PNode>();

                PNode visitedNode;

                open.Push(pos);

                while (open.Count != 0)
                {
                    visitedNode = open.Pop();
                    visited.Add(visitedNode);

                    Debug.WriteLine("Expanding: " + visitedNode.Coordinate);
                    ui.Draw(pos, goalPos, mapRobot.WallList, visitedNode, mapRobot.Width, mapRobot.Length);
                    Thread.Sleep(200);

                    foreach (toGrid g in mapRobot.Grids)
                    {
                        if ((visitedNode.X == g.Pos.X) && (visitedNode.Y == g.Pos.Y))
                        {
                            if (g.Paths.Count != 0)
                            {
                                foreach (Path p in g.Paths)
                                {
                                    if ((!visited.Any(x => x.X == p.Location.Pos.X && x.Y == p.Location.Pos.Y)) && !open.Any(x => x.X == p.Location.Pos.X && x.Y == p.Location.Pos.Y))
                                    {
                                        p.Location.Pos.ParentNode = new PNode(visitedNode);
                                        Debug.WriteLine(p.Location.Pos.Coordinate);
                                        open.Push(p.Location.Pos);
                                    }
                                }
                            }
                            if ((visitedNode.X == goalPos.X) && (visitedNode.Y == goalPos.Y))
                            {
                                return produceSolution("DFS", Pos, goalPos, visited);
                            }
                        }
                    }
                }
                return "No solution";
            }
        }


        //bfs
        public string breadthFirst()
        {
            if ((pos.X == goalPos.X) && (pos.Y == goalPos.Y))
            {
                return "The solution is the initial positition, no movement required";
            }
            else
            {
                Queue<PNode> open = new Queue<PNode>();
                List<PNode> visited = new List<PNode>();

                PNode visitedNode;

                open.Enqueue(pos);

                while (open.Count != 0)
                {
                    visitedNode = open.Dequeue();
                    visited.Add(visitedNode);

                    ui.Draw(pos, goalPos, mapRobot.WallList, visitedNode, mapRobot.Width, mapRobot.Length);
                    Thread.Sleep(100);

                    foreach (toGrid g in mapRobot.Grids)
                    {
                        if ((visitedNode.X == g.Pos.X) && (visitedNode.Y == g.Pos.Y))
                        {
                            if (g.Paths.Count != 0)
                            {
                                foreach (Path p in g.Paths)
                                {
                                    if ((!visited.Any(x => x.X == p.Location.Pos.X && x.Y == p.Location.Pos.Y)) && !open.Any(x => x.X == p.Location.Pos.X && x.Y == p.Location.Pos.Y))
                                    {
                                       p.Location.Pos.ParentNode = new PNode(visitedNode);
                                  
                                       open.Enqueue(p.Location.Pos);
                                    }
                                }
                            }

                            if ((visitedNode.X == goalPos.X) && (visitedNode.Y == goalPos.Y))
                            {
                                return produceSolution("BFS", Pos, goalPos, visited);
                            }
                        }
                    }
                }

                return "No solution";
            }
        }
        //GBFS
        public string greedyBest()
        {
            if ((pos.X == goalPos.X) && (pos.Y == goalPos.Y))
            {
                return "The solution is the initial positition, no movement required";
            }
            else
            {
                //struct init
                List<PNode> open = new List<PNode>();
                List<PNode> visited = new List<PNode>();

                PNode visitedNode;
                open.Add(pos);

                while (open.Count != 0)
                {
                    open = open.OrderBy(s => s.GoalDis).ToList();

                    visitedNode = open.First();
                    open.Remove(open.First());
                    visited.Add(visitedNode);

                    ui.Draw(pos, goalPos, mapRobot.WallList, visitedNode, mapRobot.Width, mapRobot.Length);
                    Thread.Sleep(200);

                    foreach (toGrid g in mapRobot.Grids)
                    {
                        if ((visitedNode.X == g.Pos.X) && (visitedNode.Y == g.Pos.Y))
                        {
                            if (g.Paths.Count != 0)
                            {
                                foreach (Path p in g.Paths)
                                {
                                    if (!visited.Exists(x => x.X == p.Location.Pos.X && x.Y == p.Location.Pos.Y))
                                    {
                                        p.Location.Pos.ParentNode = new PNode(visitedNode);

                                        p.Location.Pos.GoalDis = Math.Sqrt(Math.Pow(goalPos.X - p.Location.Pos.X, 2) + Math.Pow(goalPos.Y - p.Location.Pos.Y, 2));

                                        open.Add(p.Location.Pos);
                                    }
                                }
                            }

                            if ((visitedNode.X == goalPos.X) && (visitedNode.Y == goalPos.Y))
                            {
                                return produceSolution("GBFS", Pos, goalPos, visited);
                            }
                        }
                    }
                }

                return "No solution";
            }
        }


        //astar
        public string AStar()
        {
            if ((pos.X == goalPos.X) && (pos.Y == goalPos.Y))
            {
                return "The solution is the initial positition, no movement required";
            }
            else
            {
                List<PNode> open = new List<PNode>();
                List<PNode> visited = new List<PNode>();

                PNode visitedNode;

                open.Add(pos);

                pos.GScore = 0;

                while (open.Count != 0)
                {
                    open = open.OrderBy(s => s.FScore).ToList();

                    visitedNode = open.First();
                    open.Remove(open.First());

                    visited.Add(visitedNode);

                    ui.Draw(pos, goalPos, mapRobot.WallList, visitedNode, mapRobot.Width, mapRobot.Length);
                    Thread.Sleep(100);

                    foreach (toGrid g in mapRobot.Grids)
                    {
                        if ((visitedNode.X == g.Pos.X) && (visitedNode.Y == g.Pos.Y))
                        {
                            if (g.Paths.Count != 0)
                            {
                                foreach (Path p in g.Paths)
                                {
                                    if ((!visited.Any(x => x.X == p.Location.Pos.X && x.Y == p.Location.Pos.Y)) && !open.Any(x => x.X == p.Location.Pos.X && x.Y == p.Location.Pos.Y))
                                    {
                                        p.Location.Pos.ParentNode = new PNode(visitedNode);
                                        p.Location.Pos.GScore = visitedNode.GScore + 1;

                                        p.Location.Pos.FScore = p.Location.Pos.GScore + Math.Sqrt(Math.Pow(goalPos.X - p.Location.Pos.X, 2) + Math.Pow(goalPos.Y - p.Location.Pos.Y, 2));
                                       
                                        open.Add(p.Location.Pos);
                                    }
                                }
                            }

                            if ((visitedNode.X == goalPos.X) && (visitedNode.Y == goalPos.Y))
                            {
                                return produceSolution("A*", Pos, goalPos, visited);
                            }
                        }
                    }
                }

                return "No solution";
            }
        }


        public string produceSolution(string method, PNode initial, PNode child, List<PNode> expanded)
        {
            string solution = "";
            List<PNode> path = new List<PNode>();
            List<string> action = new List<string>();

            expanded.Reverse();

            foreach (PNode p in expanded)
            {
                if ((p.X == child.X) && (p.Y == child.Y))
                    path.Add(p);

                if (path.Count() != 0)
                {
                    if ((path.Last().ParentNode.X == p.X) && (path.Last().ParentNode.Y == p.Y))
                    {
                        path.Add(p);
                    }
                }
            }

            path.Reverse();

            for (int i = 0; i < path.Count(); i++)
            {
                if (i == path.Count() - 1)
                {
                    break;
                }

                if (path[i + 1].X == path[i].X + 1)
                {
                    action.Add(MoveRight());
                }

                if (path[i + 1].X == path[i].X - 1)
                {
                    action.Add(MoveLeft());
                }

                if (path[i + 1].Y == path[i].Y + 1)
                {
                    action.Add(MoveDown());
                }

                if (path[i + 1].Y == path[i].Y - 1)
                {
                    action.Add(MoveUp());
                }
            }

            foreach (string a in action)
            {
                solution = solution + a + "; ";
            }

            ui.DrawPath(pos, goalPos, mapRobot.WallList, mapRobot.Width, mapRobot.Length, path);

            return method + " " + expanded.Count() + " " + solution;
        }
    }
}
