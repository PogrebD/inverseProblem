using praktika.generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practika.Generators
{
    internal class BC1Generator : IGenerator
    {
        public void Generate(Grid grid)
        {
            double left = 0;
            double right = 0;
            double top = 0;
            double down = 0;

            //all
            //int n = (grid.dischargeFactor.NElemZ) * 2 + (grid.dischargeFactor.NElemR) * 2;

            //left right
            //int n = (grid.dischargeFactor.NElemZ) * 2;
            
            //right down
            int n = (grid.dischargeFactor.NElemZ) + (grid.dischargeFactor.NElemR);
            
            File.WriteAllText(Config.bc1Path, n.ToString() + "\n");
            for (int i = 0; i < grid.dischargeFactor.NElemR; i++)
            {
                //down
                string str = string.Format("{0} {1} {2}\n", i, i + 1, down);
                File.AppendAllText(Config.bc1Path, str);

                //top
                //string str2 = string.Format("{0} {1} {2}\n", (grid.dischargeFactor.NElemR + 1) * grid.dischargeFactor.NElemZ + i, (grid.dischargeFactor.NElemR + 1) * grid.dischargeFactor.NElemZ + i + 1, top);
                //File.AppendAllText(Config.bc1Path, str2);
            }

            for (int i = 0; i < grid.dischargeFactor.NElemZ; i++)
            {
                //left
                //string str = string.Format("{0} {1} {2}\n", i * (grid.dischargeFactor.NElemR + 1), (i + 1) * (grid.dischargeFactor.NElemR + 1), left);
                //File.AppendAllText(Config.bc1Path, str);

                //right
                string str2 = string.Format("{0} {1} {2}\n", i * (grid.dischargeFactor.NElemR + 1) + grid.dischargeFactor.NElemR, (i + 1) * (grid.dischargeFactor.NElemR + 1) + grid.dischargeFactor.NElemR, right);
                File.AppendAllText(Config.bc1Path, str2);
            }
        }
    }
}
