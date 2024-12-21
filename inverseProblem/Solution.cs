namespace practika
{
    public class Solution
    {
        public Grid grid;
        private double div = 0.1;

        public Solution(Grid grid)
        {
            this.grid = grid;
        }

        public void Run()
        {
            string str = string.Format("\n NNode = {0}\n", grid.nodes.Count);
    int count = 0;
            Console.WriteLine(str);

            //double trueU = 10;
            //var trueV = DirectTask(trueU);
            double trueU = grid.mats[1].lambdaZ;
            var trueV = DirectTask();
            var trueRes = resultFunction(trueV);

            trueRes[2] *= 1.1;

            double[] w = new double[Config.Points.Length/2];
            for (int i = 0; i < Config.Points.Length/2; i++)
            {
                w[i] = 1 / trueRes[i];
            }

            double startU = 0.05;

            var maxIter = 100;
            double U = startU;
            var eps = 1e-15;
            var funEps = 1e-11;
            double prevFunctional = 2;
            for (int iter = 0; iter < maxIter; iter++)
            {
                grid.mats[1].lambdaZ = U;
                var V = DirectTask();
                //var V = DirectTask(U);
                var res = resultFunction(V);

                grid.mats[1].lambdaZ = U * div + U; //U*1.1
                var deltaV = DirectTask();
                //var deltaV = DirectTask(U * div + U);
                var resDelta = resultFunction(deltaV);

                var A = CalcAMatrix(res, resDelta, U, w);
                var F = CalcFVector(res, resDelta, U, trueRes, w);
                var deltaU = Slau(A, F);

                var newU = U + deltaU;

                grid.mats[1].lambdaZ = newU;
                var newV = DirectTask();
                //var newV = DirectTask(newU);
                var newRes = resultFunction(newV);

                double functional = 0;
                for (int i = 0; i < Config.Points.Length/2; i++)
                {
                    functional += Math.Pow(w[i] * (trueRes[i] - newRes[i]), 2);
                }

                U = grid.mats[1].lambdaZ;
                count++;
                Console.WriteLine(grid.mats[1].lambdaZ);
                //U = newU;
                //Console.WriteLine(newU);

                if (Math.Abs(prevFunctional - functional) < eps)
                {
                    Console.WriteLine(count);
                    break;
                }

                prevFunctional = functional;
                if (functional < eps)
                {
                    Console.WriteLine(count);
                    break;
                }
            }
            Console.WriteLine("точка остановки");
        }

        double result_xyz(double[] res, double x, double y)
        {
            double xA = 0;
            double yA = 0;
            double xB = 0;
            double yB = 100;
            double r1 = Math.Sqrt((x - xA) * (x - xA) + (y - yA) * (y - yA));
            double r2 = Math.Sqrt((x - xB) * (x - xB) + (y - yB) * (y - yB));
            return ResultInPoint(res, r1, 0) - ResultInPoint(res, r2, 0);
        }

        double[] resultFunction(double[] res)
        {
            double[] result = new double[Config.Points.Length / 2];
            for (int i = 0; i < Config.Points.Length / 2; i++)
            {
                result[i] = result_xyz(res, Config.Points[i, 0], 0);
            }

            return result;
        }

        double derivative(double Vdelta, double V, double U)
        {
            return (Vdelta - V) / (div * U); //0.1*U
        }

        public double Slau(double A, double F)
        {
            return (F) / (A);
        }

        public double CalcAMatrix(double[] res, double[] resDelta, double u, double[] w)
        {
            double A = 0;
            for (int i = 0; i < Config.Points.Length/2; i++)
            {
                A += w[i] * w[i] * Math.Pow(derivative(resDelta[i], res[i], u), 2);
            }

            return A;
        }

        public double CalcFVector(double[] res, double[] resDelta, double u, double[] trueRes, double[] w)
        {
            double f = 0;
            for (int i = 0; i < Config.Points.Length/2; i++)
            {
                f -= w[i] * w[i] * derivative(resDelta[i], res[i], u) *
                     (res[i] - trueRes[i]); // чмо (res[i] - trueRes[i])
            }

            return f;
        }

        public double[] DirectTask(double source)
        {
            string Path = string.Format("{0}OutST{1}.txt", Config.Path,
                grid.objects[0].mat * 2 + grid.objects[0].mode + grid.objects[0].mat2);
            File.WriteAllText(Path, "");
            LocalMatrices localMatrices = new(grid);
            GlobalMatrices globalMatrices = new(grid);

            for (int i = 0; i < Config.MainSpring.Length / 2; i++)
            {
                int springIndex = grid.nodes.FindIndex((Node node) =>
                {
                    return node.r == Config.MainSpring[i, 0] && node.z == Config.MainSpring[i, 1];
                });
                globalMatrices._globalVectorB[springIndex] = source * 1e-4;
            }

            globalMatrices._globaleATriangle = globalMatrices._globaleGTriangle;
            globalMatrices._globaleAdiag = globalMatrices._globaleGdiag;

            // краевые и слау
            BoundaryConditions boundaryConditions = new(globalMatrices, grid);
            Slau slau = new(globalMatrices, grid.nodes.Count);
            return slau.q;
            //PrintFileResultPointST(slau.q, Path);
        }

        public double[] DirectTask()
        {
            string Path = string.Format("{0}OutST{1}.txt", Config.Path,
                grid.objects[0].mat * 2 + grid.objects[0].mode + grid.objects[0].mat2);
            File.WriteAllText(Path, "");
            LocalMatrices localMatrices = new(grid);
            GlobalMatrices globalMatrices = new(grid);

            for (int i = 0; i < Config.MainSpring.Length / 2; i++)
            {
                int springIndex = grid.nodes.FindIndex((Node node) =>
                {
                    return node.r == Config.MainSpring[i, 0] && node.z == Config.MainSpring[i, 1];
                });
                globalMatrices._globalVectorB[springIndex] = 10 * 1e-4;
            }

            globalMatrices._globaleATriangle = globalMatrices._globaleGTriangle;
            globalMatrices._globaleAdiag = globalMatrices._globaleGdiag;

            // краевые и слау
            BoundaryConditions boundaryConditions = new(globalMatrices, grid);
            Slau slau = new(globalMatrices, grid.nodes.Count);
            return slau.q;
            //PrintFileResultPointST(slau.q, Path);
        }

        /*public double RZtuXY(double[] result, double targetX, double targetY, double Vrz, Node A, Node B)
        {
            var res = ResultInPoint(result, Math.Sqrt(Math.Pow(targetX - A.r, 2) - Math.Pow(targetY - A.z, 2)), 0) - ResultInPoint(result, Math.Sqrt(Math.Pow(targetX - B.r, 2) - Math.Pow(targetY - B.z, 2)), 0);
            return res;
        }
        double P = 0.1; // мощность источника
        int sourceIndex = -1; //  посчитать!!!!!!!!!!!!!!!!!
                              //globalMatrices._globalVectorB[sourceIndex] = P;*/

        public double ResultInPoint(double[] result, double r, double z)
        {
            int IndexNode = grid.nodes.FindIndex((Node node) => { return node.r >= r && node.z >= z; });
            int index = grid.elems.FindIndex((Elem elem) => { return elem.index[3] == IndexNode; });

            double r1 = grid.nodes[grid.elems[index].index[0]].r;
            double r2 = grid.nodes[grid.elems[index].index[3]].r;
            double z1 = grid.nodes[grid.elems[index].index[0]].z;
            double z2 = grid.nodes[grid.elems[index].index[3]].z;


            double psi0 = ((r2 - r) / (r2 - r1)) * ((z2 - z) / (z2 - z1));
            double psi1 = ((r - r1) / (r2 - r1)) * ((z2 - z) / (z2 - z1));
            double psi2 = ((r2 - r) / (r2 - r1)) * ((z - z1) / (z2 - z1));
            double psi3 = ((r - r1) / (r2 - r1)) * ((z - z1) / (z2 - z1));

            double res = psi0 * result[grid.elems[index].index[0]] + psi1 * result[grid.elems[index].index[1]] +
                         psi2 * result[grid.elems[index].index[2]] + psi3 * result[grid.elems[index].index[3]];

            return res;
        }

        //Принты

        public void PrintFileResult(double[] result, double time)
        {
            File.AppendAllText(Config.outPath, string.Format("\n time {0} \n", time));
            foreach (var it in result)
            {
                File.AppendAllText(Config.outPath, it.ToString());
                File.AppendAllText(Config.outPath, "\n");
            }
        }

        public void PrintFileResultPoint(double[] result, string Path)
        {
            for (int i = 0; i < Config.Points.Length / 2; i++)
            {
                var res = ResultInPoint(result, Config.Points[i, 0], Config.Points[i, 1]);
                string str = string.Format("{0}\t", res);

                File.AppendAllText(Path, str);
            }

            File.AppendAllText(Path, "\n");
        }

        public void PrintFileResultPointTime(double[] result, string Path)
        {
            double r = 0;
            double z = 4.5;
            double hr = 0.1;
            double hz = 1;
            while (r < 20 && z < 20)
            {
                var res = ResultInPoint(result, r, z);
                string str = string.Format("{0}\t", res);
                r += hr;
                File.AppendAllText(Path, str);
            }

            File.AppendAllText(Path, "\n");
        }

        /*        public void PrintFileResultPoint(double[] result, string Path)
                {

                    for (int i = 0; i < Config.Points.Length / 2; i++)
                    {
                        var index = grid.nodes.FindIndex((Node node) => { return node.r == Config.Points[i, 0] && node.z == Config.Points[i, 1]; });
                        string str = string.Format("{0}\t", result[index]);

                        File.AppendAllText(Path, str);
                    }
                    File.AppendAllText(Path, "\n");
                }*/

        /*        public void PrintFileResultPointST(double[] result, string Path)
                {
                    for (int i = 0; i < Config.Points.Length / 2; i++)
                    {
                        var index = grid.nodes.FindIndex((Node node) => { return node.r == Config.Points[i, 0] && node.z == Config.Points[i, 1]; });
                        string str = string.Format("{0}\t", result[index]);

                        File.AppendAllText(Path, str);
                    }
                }*/

        public void PrintFileResultPointST(double[] result, string Path)
        {
            for (int i = 0; i < Config.Points.Length / 2; i++)
            {
                var res = ResultInPoint(result, Config.Points[i, 0], Config.Points[i, 1]);
                string str = string.Format("{0}\t", res);

                File.AppendAllText(Path, str);
            }
        }

        /*        public void PrintFileResultPointST(double[] result, string Path)
                {
                    double r = 0;
                    double z = 4.5;
                    double hr = 1;
                    double hz = 1;
                    while (r < 20 && z < 20)
                    {
                        var res = ResultInPoint(result, r, z);
                        string str = string.Format("{0}\n", res);
                        r += hr;
                        File.AppendAllText(Path, str);
                    }
                }*/

        /*public void PrintFileTime()
        {
            File.Delete(Config.outTimePath);
            for (var i = 0; i <= grid.time.nTime - 1; i += grid.time.nTime / Config.NTimeInterval)
            {
                string str = string.Format("{0}\n", grid.time.timeSloy[i]);

                File.AppendAllText(Config.outTimePath, str);
            }
        }*/

        public void PrintResult(double[] result, double time)
        {
            Console.WriteLine("\n");
            Console.WriteLine("time");
            Console.WriteLine(time);
            foreach (var it in result)
            {
                Console.WriteLine(it.ToString());
            }
        }
        /*        public void PrintResultPoint(Grid grid, double[] result, double time, double r, double z)
                {
                    Console.WriteLine("\n");
                    string str = string.Format("\n time = {0}; r = {1}; z={2} \n", time, r, z);

                    Console.WriteLine(str);
                    for (int i = 0; i < grid.nodes.Count; i++)
                    {
                        if (grid.nodes[i].r == r && grid.nodes[i].z == z)
                        {
                            Console.WriteLine(result[i]);
                        }
                    }
                    Console.WriteLine("\nAN");
                    Console.WriteLine(Func.u(r, z, time));
                }*/


        //Операции
        double[] multiplication_matrix_on_vector(GlobalMatrices globalMatrices, double[] tri, double[] diag, double[] a,
            double[] b, int nNode)
        {
            for (int i = 0; i < nNode; i++)
                b[i] = diag[i] * a[i];

            for (int i = 1; i < nNode; i++)
            {
                int i0 = globalMatrices.ig[i - 1];
                int i1 = globalMatrices.ig[i];
                for (int j = 0; j < (i1 - i0); j++)
                {
                    b[i] += tri[i0 + j] * a[globalMatrices.jg[i0 + j]];
                    b[globalMatrices.jg[i0 + j]] += tri[i0 + j] * a[i];
                }
            }

            return b;
        }

        double[] Culcq(Grid grid, int t)
        {
            double[] q = new double[grid.nodes.Count];
            for (int i = 0; i < grid.nodes.Count; i++)
            {
                q[i] = Func.u(grid.nodes[i].r, grid.nodes[i].z);
            }

            return q;
        }

        double[] Culcq0(Grid grid, int t)
        {
            double[] q = new double[grid.nodes.Count];
            return q;
        }

        /*double CulcTLeft(int t)
        {
            return ((grid.time.timeSloy[t] - grid.time.timeSloy[t - 2]) + (grid.time.timeSloy[t] - grid.time.timeSloy[t - 1]))
                / ((grid.time.timeSloy[t] - grid.time.timeSloy[t - 2]) * (grid.time.timeSloy[t] - grid.time.timeSloy[t - 1]));
        }

        double CulcTRightJ_2(int t)
        {
            return (grid.time.timeSloy[t] - grid.time.timeSloy[t - 1])
                / ((grid.time.timeSloy[t - 1] - grid.time.timeSloy[t - 2]) * (grid.time.timeSloy[t] - grid.time.timeSloy[t - 2]));
        }

        double CulcTRightJ_1(int t)
        {
            return (grid.time.timeSloy[t] - grid.time.timeSloy[t - 2])
                / ((grid.time.timeSloy[t - 1] - grid.time.timeSloy[t - 2]) * (grid.time.timeSloy[t] - grid.time.timeSloy[t - 1]));
        }*/
    }
}