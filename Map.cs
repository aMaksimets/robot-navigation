using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ConsoleApp1
{
    class Map
    {
        List<toGrid> grids = new List<toGrid>();
        private int width;
        private int length;
        private List<string> wall;
        private List<toGrid> wallList = new List<toGrid>();

        public List<toGrid> Grids
        {
            get
            {
                return grids;
            }
        }

        public int Width
        {
            get
            {
                return width;
            }
        }

        public int Length
        {
            get
            {
                return length;
            }
        }

        public List<toGrid> WallList
        {
            get
            {
                return wallList;
            }
        }
        public Map(string mapSize, List<string> mapWall)
        {
            stringInt ifs = new stringInt(mapSize);

            List<int> coordinate = ifs.getIntFromString();

            width = coordinate[0];
            length = coordinate[1];
            wall = mapWall;
            drawMap();
        }

        public void drawMap()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    grids.Add(new toGrid(new PNode(j, i), false));
                }
            }          

            for (int i = 0; i < wall.Count; i++)
            {
                drawWall(wall[i]);
            }

            drawPath();
        }
        public void drawPath()
        {
            for (int i = 0; i < grids.Count; i++)
            {
                if (!grids[i].WallCheck)
                {
                    for (int j = 0; j < width; j++)
                    {
                        if ((i >= j * length) && (i < (j + 1) * length - 1))
                        {
                            grids[i].Paths.Add(new Path(grids[i + 1]));
                        }
                    }

                    if (i < length * width - length)
                    {
                        if (!grids[i + length].WallCheck)
                        {
                            grids[i].Paths.Add(new Path(grids[i + length]));
                        }
                    }

                    for (int j = 0; j < width; j++)
                    {
                        if ((i > j * length) && (i < (j + 1) * length))
                        {
                            grids[i].Paths.Add(new Path(grids[i - 1]));
                        }
                    }

                    if (i > length - 1)
                    {
                        if (!grids[i - length].WallCheck)
                        {
                            grids[i].Paths.Add(new Path(grids[i - length]));
                        }
                    }
                }
            }

            foreach (toGrid g in grids)
            {
                for (int i = 0; i < g.Paths.Count; i++)
                {
                    if (g.Paths[i].Location.WallCheck == true)
                    {
                        g.Paths.Remove(g.Paths[i]);
                    }
                }
            }
        }
        public void drawWall(string oneWall)
        {
            stringInt ifs = new stringInt(oneWall);
            List<int> coordinate = ifs.getIntFromString();

            for (int j = coordinate[1]; j < coordinate[1] + coordinate[3]; j++)
            {
                for (int i = coordinate[0]; i < coordinate[0] + coordinate[2]; i++)
                {
                    int index = grids.FindIndex(x => (x.Pos.X == i) && (x.Pos.Y == j));
                    grids[index].WallCheck = true;
                }
            }

            foreach (toGrid g in grids)
            {
                if (g.WallCheck == true)
                {
                    wallList.Add(g);
                }
            }
        }

        public void printMap()
        {
            foreach (toGrid g in grids)
            {
                Console.WriteLine("Grid: ({0},{1}), wall: {2}", g.Pos.X, g.Pos.Y, g.WallCheck);
                Console.WriteLine("Containing: ");
                foreach (Path p in g.Paths)
                {
                    Console.WriteLine(p.Location.Pos.Coordinate);
                }
                Console.WriteLine("");
            }
        }

    }
}
