using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class initRes
    {
        private string curLin;
        private System.IO.StreamReader openFile;
        private List<string> edge = new List<string>();
        private string grid;
        private string initS;
        private string goalState;
        //get set
        public string GoalState
        { get { return goalState; } }

        public string Grid
        { get { return grid; } }

        public List<string> Edge
        { get { return edge; } }

        public string InitS
        { get { return initS; } }

        

        public initRes(string testfile)
        { openFile = new System.IO.StreamReader(testfile);}

        public void populateData()
        {
            int counter = 0;

            while((curLin = openFile.ReadLine()) != null)
            {
                if (counter == 0)
                {
                    grid = curLin;
                }

                if (counter == 1)
                {
                    initS = curLin;
                }

                if (counter == 2)
                {
                    goalState = curLin;
                }

                if (counter >= 3)
                {
                    edge.Add(curLin);
                }

                counter++;
            }
        }
        public void displayStats()
        {
            Console.WriteLine("Size: {0}", grid);
            Console.WriteLine("Initial: {0}", initS);
            Console.WriteLine("Goal: {0}", goalState);

            foreach (string w in edge)
            {
                Console.WriteLine("Edge: {0}", w);
            }
        }

        public void closeFile()
        {
            openFile.Close();
        }
    }
}
