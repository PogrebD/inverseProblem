using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practika.Input.Inputers
{
    internal class SredaInputer
    {
        public void Input(Grid grid)
        {
            using (StreamReader reader = new(Config.sredaPath))
            {
                int nObjects = int.Parse(reader.ReadLine()); ///??????
                while (!reader.EndOfStream)
                {
                    for (int i = 0; i < nObjects; i++)
                    {
                        var line = reader.ReadLine();
                        var Array = line.Split(' ').ToArray();
                        if (int.Parse(Array[4]) == 0)
                        {
                            grid.objects.Add(
                            new Object(
                                double.Parse(Array[0]),
                                double.Parse(Array[1]),
                                double.Parse(Array[2]),
                                double.Parse(Array[3]),
                                int.Parse(Array[4]),
                                int.Parse(Array[5])
                                ));
                        }
                        else
                        {
                            grid.objects.Add(
                            new Object(
                                double.Parse(Array[0]),
                                double.Parse(Array[1]),
                                double.Parse(Array[2]),
                                double.Parse(Array[3]),
                                int.Parse(Array[4]),
                                int.Parse(Array[5]),
                                int.Parse(Array[6])
                                ));
                        }
                    }

                    int nAreasR = int.Parse(reader.ReadLine());
                    var lineR = reader.ReadLine();
                    var ArrayR = lineR.Split(' ').ToArray();
                    var lineRi = reader.ReadLine();
                    var ArrayRi = lineRi.Split(' ').ToArray();

                    int nAreasZ = int.Parse(reader.ReadLine());
                    var lineZ = reader.ReadLine();
                    var ArrayZ = lineZ.Split(' ').ToArray();
                    var lineZi = reader.ReadLine();
                    var ArrayZi = lineZi.Split(' ').ToArray();

                    double[] rDischarge = new double[nAreasR + 1];
                    double[] zDischarge = new double[nAreasZ + 1];
                    double[] rInterval = new double[nAreasR + 1];
                    double[] zInterval = new double[nAreasZ + 1];

                    for (int i = 0; i < nAreasR + 1; i++)
                    {
                        rDischarge[i] = double.Parse(ArrayR[i]);
                    }

                    for (int i = 0; i < nAreasZ + 1; i++)
                    {
                        zDischarge[i] = double.Parse(ArrayZ[i]);
                    }

                    for (int i = 0; i < nAreasR + 1; i++)
                    {
                        rInterval[i] = double.Parse(ArrayRi[i]);
                    }

                    for (int i = 0; i < nAreasZ + 1; i++)
                    {
                        zInterval[i] = double.Parse(ArrayZi[i]);
                    }

                    grid.dischargeFactor.rDischarge = rDischarge;
                    grid.dischargeFactor.zDischarge = zDischarge;

                    grid.dischargeFactor.rInterval = rInterval;
                    grid.dischargeFactor.zInterval = zInterval;
                }
            }
        }
    }
}
