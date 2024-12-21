using practika;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace praktika.generators
{
    internal class NodeGenerator : IGenerator
    {
        public void Generate(Grid grid)
        {
            //выделяю контрольные точки по r
            List<double> rNodesList = new() { Config.x1 };
            for (int i = 0; i < grid.objects.Count; i++)
            {
                rNodesList.Add(grid.objects[i].r1);
                rNodesList.Add(grid.objects[i].r2);
            }
            rNodesList.Add(Config.x2);
            rNodesList.Sort();
            double[] rNodes = rNodesList.ToArray();
            rNodes = rNodes.Distinct().ToArray();

            //выделяю контрольные точки по z
            List<double> zNodesList = new() { Config.y1 };
            for (int i = 0; i < grid.objects.Count; i++)
            {
                zNodesList.Add(grid.objects[i].z1);
                zNodesList.Add(grid.objects[i].z2);
            }
            zNodesList.Add(Config.y2);
            zNodesList.Sort();
            double[] zNodes = zNodesList.ToArray();
            zNodes = zNodes.Distinct().ToArray();


            //создаю лист со всеми точками по r
            List<double> rNodesAllList = new();
            for (int i = 0; i < rNodes.Length - 1; i++)
            {
                double discharge = grid.dischargeFactor.rInterval[i];
                if (grid.dischargeFactor.rDischarge[i] < 0)
                {
                    double buf = rNodes[i + 1] - grid.dischargeFactor.rInterval[i];
                    while (rNodes[i] < buf)
                    {
                        if (buf - (discharge / 2) < rNodes[i])
                        {
                            break;
                        }

                        rNodesAllList.Add(buf);
                        discharge *= (-1) * grid.dischargeFactor.rDischarge[i];
                        buf -= discharge;
                    }
                }
                else
                {
                    double buf = rNodes[i] + grid.dischargeFactor.rInterval[i];

                    while (rNodes[i + 1] > buf)
                    {
                        if (buf + (discharge / 2) > rNodes[i + 1])
                        {
                            break;
                        }

                        rNodesAllList.Add(buf);
                        discharge *= grid.dischargeFactor.rDischarge[i];
                        buf += discharge;

                    }
                }
            }
            rNodesAllList.AddRange(rNodes);
            rNodesAllList.Sort();


            //создаю лист со всеми точками по z
            List<double> zNodesAllList = new();
            for (int i = 0; i < zNodes.Length - 1; i++)
            {
                double discharge = grid.dischargeFactor.zInterval[i];
                if (grid.dischargeFactor.zDischarge[i] < 0)
                {
                    double buf = zNodes[i + 1] - grid.dischargeFactor.zInterval[i];
                    while (zNodes[i] < buf)
                    {
                        if (zNodes[i] == grid.objects[i].z1)
                        {
                            zNodesAllList.Add(buf);
                            zNodesAllList.Add(buf - discharge);
                            discharge *= (-1) * grid.dischargeFactor.zDischarge[i];
                            buf -= discharge;
                        }
                        else
                        {
                            if (buf - (discharge / 2) < zNodes[i])
                            {
                                break;
                            }

                            zNodesAllList.Add(buf);
                            discharge *= (-1) * grid.dischargeFactor.zDischarge[i];
                            buf -= discharge;
                        }
                    }
                }
                else
                {
                    double buf = zNodes[i] + grid.dischargeFactor.zInterval[i];
                    while (zNodes[i + 1] > buf)
                    {
                        if (zNodes[i] == grid.objects[0].z1 && grid.dischargeFactor.zDischarge[i] != 1)
                        {
                            if (buf + (discharge) > zNodes[i + 1])
                            {
                                buf -= discharge;
                                double buff = (zNodes[i + 1] - buf) / 2;
                                zNodesAllList.Add(buf + buff);
                                break;
                            }
                            zNodesAllList.Add(buf);
                            buf += discharge;
                            zNodesAllList.Add(buf);
                            discharge *= grid.dischargeFactor.zDischarge[i];
                            buf += discharge;
                        }
                        else
                        {
                            if (buf + (discharge / 2) > zNodes[i + 1])
                            {
                                break;
                            }
                            zNodesAllList.Add(buf);
                            discharge *= grid.dischargeFactor.zDischarge[i];
                            buf += discharge;
                        }
                    }
                }
            }
            zNodesAllList.AddRange(zNodes);
            zNodesAllList.Sort();

            //определяю основные величины
            int NNodeR = rNodesAllList.Count;
            int NNodeZ = zNodesAllList.Count;
            int NNode = NNodeR * NNodeZ;
            int NElemR = NNodeR - 1;
            int NElemZ = NNodeZ - 1;
            int NElem = NElemR * NElemZ;

            grid.dischargeFactor.NElemR = NElemR;
            grid.dischargeFactor.NElemZ = NElemZ;
            grid.dischargeFactor.NNodeR = NNodeR;
            grid.dischargeFactor.NNodeZ = NNodeZ;
            grid.dischargeFactor.pointsAllR = rNodesAllList;
            grid.dischargeFactor.pointsAllZ = zNodesAllList;


            //создаю файл точек
            File.WriteAllText(Config.nodePath, NNode.ToString() + "\n");

            for (int i = 0; i < NNodeZ; i++)
            {
                for (int j = 0; j < NNodeR; j++)
                {
                    string str = string.Format("{0} {1}\n", rNodesAllList[j], zNodesAllList[i]);
                    File.AppendAllText(Config.nodePath, str);
                }
            }


            //создаю файл элементов
            File.WriteAllText(Config.elemPath, NElem.ToString() + "\n");

            for (int i = 0; i < NElemZ; i++)
            {
                for (int j = 0; j < NElemR; j++)
                {
                    double x1 = i * (NElemR + 1) + j;
                    double y1 = (i + 1) * (NElemR + 1) + j;
                    string str = string.Format("{0} {1} {2} {3} {4}\n", x1, x1 + 1, y1, y1 + 1, 3);
                    File.AppendAllText(Config.elemPath, str);
                }
            }
        }
    }
}
