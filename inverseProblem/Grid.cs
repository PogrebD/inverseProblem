using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practika
{
    public struct Grid
    {
        public List<Node> nodes = new();
        public List<Elem> elems = new();
        public List<Mat> mats = new();
        public List<Object> objects = new();
        public List<List<double>> receivers = new();
        public DischargeFactor dischargeFactor = new();

        public Grid()
        {
        }
        public double FindMatSigma(int n)
        {
            foreach (var mat in mats)
            {
                if (n == mat.number)
                    return mat.sigma;
            }
            return 1;
        }
        public double FindMatLambdaR(int n)
        {
            foreach (var mat in mats)
            {
                if (n == mat.number)
                    return mat.lambdaR;
            }
            return 1;
        }
        public double FindMatLambdaZ(int n)
        {
            foreach (var mat in mats)
            {
                if (n == mat.number)
                    return mat.lambdaZ;
            }
            return 1;
        }
        public void AddReceiver(double r, double z)
        {
            receivers.Add(new List<double> { r , z});
        }
        public void DeleteReceiver(double r, double z)
        {
            receivers.Remove(new List<double> { r ,z});
        }
    }

    public class Mat
    {
        public int number;
        public double sigma;
        public double lambdaR;
        public double lambdaZ;

        public Mat(int number, double sigma, double lambdaR, double lambdaZ)
        {
            this.number = number;
            this.sigma = sigma;
            this.lambdaR = lambdaR;
            this.lambdaZ = lambdaZ;
        }
    }
    public struct Node
    {
        public double r;
        public double z;

        public Node(double r, double z)
        {
            this.r = r;
            this.z = z;
        }
    }
    public struct Elem
    {
        public int[] index;
        public double[,] matrixA;
        public double[,] matrixM;
        public double[,] matrixG;
        public double[] vectorB;
        public int mat = 3;
        public Elem()
        {
            index = new int[4];
            matrixA = new double[4, 4];
            matrixM = new double[4, 4];
            matrixG = new double[4, 4];
            vectorB = new double[4];
        }
        public Elem(int[] ints)
        {
            index = ints;
            matrixA = new double[4, 4];
            matrixM = new double[4, 4];
            matrixG = new double[4, 4];
            vectorB = new double[4];
        }
        public Elem(double[,] matrix, double[,] matrix1, double[,] matrix2, Elem elem)
        {
            index = elem.index;
            matrixA = matrix;
            vectorB = elem.vectorB;
            matrixM = matrix1;
            matrixG = matrix2;
            mat = elem.mat;
        }
        public Elem(double[] list, Elem elem)
        {
            index = elem.index;
            matrixA = elem.matrixA;
            vectorB = list;
            matrixM = elem.matrixM;
            matrixG = elem.matrixG;
            mat = elem.mat;
        }
        public Elem(int matNumber, Elem elem)
        {
            index = elem.index;
            matrixA = elem.matrixA;
            vectorB = elem.vectorB;
            matrixM = elem.matrixM;
            matrixG = elem.matrixG;
            mat = matNumber;
        }
    }

    public struct Object
    {
        public double r1;
        public double z1;
        public double r2;
        public double z2;
        public int mat;
        public int mode;
        public int mat2;
        public Object(double r1, double r2, double z1, double z2, int mode, int mat)
        {
            this.r1 = r1;
            this.z1 = z1;
            this.r2 = r2;
            this.z2 = z2;
            this.mode = mode;
            this.mat = mat;
        }

        public Object( Object obj, int mode, int mat, int mat2)
        {
            this.r1 = obj.r1;
            this.z1 = obj.z1;
            this.r2 = obj.r2;
            this.z2 = obj.z2;
            this.mode = mode;
            this.mat = mat;
            this.mat2 = mat2;
        }

        public Object(double r1, double r2, double z1, double z2, int mode, int mat, int mat2)
        {
            this.r1 = r1;
            this.z1 = z1;
            this.r2 = r2;
            this.z2 = z2;
            this.mode = mode;
            this.mat = mat;
            this.mat2 = mat2;
        }
    }
    public class DischargeFactor
    {
        public int NNodeR;
        public int NNodeZ;
        public int NElemR;
        public int NElemZ;

        public double[] rDischarge;
        public double[] zDischarge;

        public List<double> pointsAllR;
        public List<double> pointsAllZ;

        public double[] rInterval;
        public double[] zInterval;

        public DischargeFactor() { }
    }
}
