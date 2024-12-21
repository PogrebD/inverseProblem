using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practika.Input
{
    internal class MatInputer
    {
        public void Input(Grid grid)
        {

            using (StreamReader reader = new(Config.matPath))
            {
                int nMat = int.Parse(reader.ReadLine()); ///??????
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var elemArray = line.Split(' ').ToArray();
                    grid.mats.Add(
                        new Mat(int.Parse(elemArray[0]),
                            double.Parse(elemArray[1]) * double.Parse(elemArray[2]),
                            double.Parse(elemArray[3]),
                            double.Parse(elemArray[4])
                            ));
                }
            }


            for (int i = 0; i < grid.objects.Count; i++)
            {
                int index1 = grid.nodes.FindIndex((Node node) => { return node.r == grid.objects[i].r1 && node.z == grid.objects[i].z1; });
                int index2 = grid.nodes.FindIndex((Node node) => { return node.r == grid.objects[i].r2 && node.z == grid.objects[i].z2; });
                int hr = (index2 - index1) % grid.dischargeFactor.NNodeR;
                int hz = (index2 - index1) / grid.dischargeFactor.NNodeR;

                int hrElem = (index1) % grid.dischargeFactor.NNodeR;
                int hzElem = (index1) / grid.dischargeFactor.NNodeR;
                int indexElem = hzElem * grid.dischargeFactor.NElemR + hrElem;

                for (int j = 0; j < hz; j++)
                {
                    for (int k = 0; k < hr; k++)
                    {
                        if (grid.objects[i].mode == 0)
                        {
                            grid.elems[indexElem + k + j * grid.dischargeFactor.NElemR] = new Elem(grid.objects[i].mat, grid.elems[indexElem + k + j * grid.dischargeFactor.NElemR]);
                        }
                        else
                        {
                            grid.elems[indexElem + k + j * grid.dischargeFactor.NElemR] = j % 2 == 0
                                ? new Elem(grid.objects[i].mat, grid.elems[indexElem + k + j * grid.dischargeFactor.NElemR])
                                : new Elem(grid.objects[i].mat2, grid.elems[indexElem + k + j * grid.dischargeFactor.NElemR]);
                        }
                    }
                }
            }
        }
    }
}
