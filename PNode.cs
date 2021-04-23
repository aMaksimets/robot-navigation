using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class PNode
    {
        private int x;
        private int y;
        private double goalDis;
        private double fScore;
        private double gScore;
        private PNode parentNode;

        public PNode(int parX, int parY)
        {
            x = parX;
            y = parY;
        }

        public PNode(PNode parent)
        {
            x = parent.X;
            y = parent.Y;
        }

        public double FScore
        {
            get
            {
                return fScore;
            }

            set
            {
                fScore = value;
            }
        }

        public double GScore
        {
            get
            {
                return gScore;
            }

            set
            {
                gScore = value;
            }
        }

        public int Y
        {
            get{return y;
            }

            set{y = value;}
        }

        public int X
        {
            get { return x; }

            set { x = value; }
        }

        public double GoalDis
        {
            get
            {
                return goalDis;
            }

            set
            {
                goalDis = value;
            }
        }

        public string Coordinate
        {
            get
            {
                return "(" + X + "," + Y + ")";
            }
        }

        public PNode ParentNode
        {
            get
            {
                return parentNode;
            }

            set
            {
                parentNode = value;
            }
        }
    }
}
