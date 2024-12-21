using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practika.InputBC
{
    internal class BcInputer
    {
        public BcInputer(BoundaryConditions boundaryConditions) 
        {
            Input(boundaryConditions);
        }
        public void Input(BoundaryConditions boundaryConditions)
        {
            using (StreamReader reader = new(Config.bc1Path))
            {
                int nBc1 = int.Parse(reader.ReadLine()); ///??????
                List<int[]> ints = new();
                List<double> doubles = new();
                for (int i = 0; i < nBc1; i++)
                {
                    var line = reader.ReadLine();
                    var elemArray = line.Split(' ').ToArray();
                    ints.Add(new int[]
                            { int.Parse(elemArray[0]),
                            int.Parse(elemArray[1]),
                            });

                    doubles.Add(double.Parse(elemArray[2]));
                }
                boundaryConditions.bc1 = new Bc1(ints, doubles, nBc1);
            }

            using (StreamReader reader = new(Config.bc2Path))
            {
                int nBc2 = int.Parse(reader.ReadLine()); ///??????
                List<int[]> ints = new();
                List<double[]> doubles = new();
                for (int i = 0; i < nBc2; i++)
                {
                    var line = reader.ReadLine();
                    var elemArray = line.Split(' ').ToArray();
                    ints.Add(new int[] { int.Parse(elemArray[0]),
                        int.Parse(elemArray[1]) });

                    doubles.Add(new double[] { double.Parse(elemArray[2]),
                        double.Parse(elemArray[3]) });
                }
                boundaryConditions.bc2 = new Bc2(ints, doubles, nBc2);
            }

            using (StreamReader reader = new(Config.bc3Path))
            {
                int nBc3 = int.Parse(reader.ReadLine()); ///??????
                List<int[]> ints = new();
                List<double[]> doubles = new();
                List<double> doubles2 = new();
                for (int i = 0; i < nBc3; i++)
                {
                    var line = reader.ReadLine();
                    var elemArray = line.Split(' ').ToArray();
                    ints.Add(new int[] { int.Parse(elemArray[0]),
                        int.Parse(elemArray[1]) });

                    doubles.Add(new double[] { double.Parse(elemArray[2]),
                        double.Parse(elemArray[3]) });

                    doubles2.Add(double.Parse(elemArray[4]));
                }
                boundaryConditions.bc3 = new Bc3(ints, doubles, doubles2, nBc3);
            }
        }
    }
}
