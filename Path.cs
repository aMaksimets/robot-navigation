using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Path
    {
        toGrid _;

        public Path(toGrid location)
        {
            _ = location;
        }

        public toGrid Location
        {
            get
            {
                return _;
            }

            set
            {
                _ = value;
            }
        }
    }
}
