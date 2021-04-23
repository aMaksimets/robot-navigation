using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class main
    {
        static void Main(string[] args)
        {
            //get file from dir
            initRes _ = new initRes(@"RobotNav-test.txt");
            _.populateData();
            //const map and robot objects
            Map Grid = new Map(_.Grid, _.Edge);
            RobotAI Robot = new RobotAI(_.InitS, _.GoalState, Grid);


            switch (args[0].ToLower())
            {
                case "dfs":
                    Console.WriteLine(Robot.depthFirst());
                    break;
                case "bfs":
                    Console.WriteLine(Robot.breadthFirst());
                    break;
                case "gbfs":
                    Console.WriteLine(Robot.greedyBest());
                    break;
                case "astar":
                    Console.WriteLine(Robot.AStar());
                    break;
                default:
                    //errocase
                    Console.WriteLine("Error: No search method " + args[1]);
                    break;
            }
            Console.ReadLine();
        }
    }
}
