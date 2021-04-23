using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class toGrid
    {
        private PNode pos;
        private bool wallCheck;
        private List<Path> paths = new List<Path>();


        public bool WallCheck
        {
            get
            {
                return wallCheck;
            }

            set
            {
                wallCheck = value;
            }
        }

        public List<Path> Paths
        {
            get
            {
                return paths;
            }

            set
            {
                paths = value;
            }
        }

        public PNode Pos
        {
            get
            {
                return pos;
            }
        }

        public toGrid(PNode ppos, bool wall)
        {
            pos = ppos;
            wallCheck = wall;
        }

       

        
    }
}