using practika;
using praktika.generators;

namespace inverseProblem.Generators;

public class BC2Generator : IGenerator
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
            
        //left top
        int n = (grid.dischargeFactor.NElemZ) + (grid.dischargeFactor.NElemR);
            
        File.WriteAllText(Config.bc2Path, n.ToString() + "\n");
        for (int i = 0; i < grid.dischargeFactor.NElemR; i++)
        {
            //down
            //string str = string.Format("{0} {1} {2}\n", i, i + 1, down);
            //File.AppendAllText(Config.bc2Path, str);

            //top
            string str2 = string.Format("{0} {1} {2} {3}\n", (grid.dischargeFactor.NElemR + 1) * grid.dischargeFactor.NElemZ + i, (grid.dischargeFactor.NElemR + 1) * grid.dischargeFactor.NElemZ + i + 1, top, top);
            File.AppendAllText(Config.bc2Path, str2);
        }

        for (int i = 0; i < grid.dischargeFactor.NElemZ; i++)
        {
            //left
            string str = string.Format("{0} {1} {2} {3}\n", i * (grid.dischargeFactor.NElemR + 1), (i + 1) * (grid.dischargeFactor.NElemR + 1), left, left);
            File.AppendAllText(Config.bc2Path, str);

            //right
            //string str2 = string.Format("{0} {1} {2}\n", i * (grid.dischargeFactor.NElemR + 1) + grid.dischargeFactor.NElemR, (i + 1) * (grid.dischargeFactor.NElemR + 1) + grid.dischargeFactor.NElemR, right);
            //File.AppendAllText(Config.bc2Path, str2);
        }
    }
}