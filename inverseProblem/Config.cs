using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practika
{
    public struct Config
    {
        public const double x1 = 1e-4, x2 = 1000, y1 = -1000, y2 = 0;
        /*public const int NTimeInterval = 20;*/

        //public static readonly double[,] Points = new double[0, 2] {};
        //public static readonly double[,] Points = new double[6, 2] { { 15.5, 8.75 }, { 15.5, 7.5 }, { 15.5, 6.25 }, { 10, 4.5 }, { 4.5, 7.5 }, { 19.5, 12.5 } };

        //public static readonly double[,] Points = new double[4, 2] {  { 15.5, 7.5 }, { 4.5, 7.5 }, { 10, 4.2 }, { 12.5, 4.5 } };

        //public static readonly double[,] Points = new double[39, 2] {   { 0.5, 4.5 }, { 1, 4.5 }, { 1.5, 4.5 }, { 2, 4.5 }, { 2.5, 4.5 }, { 3, 4.5 },
        //{ 3.5, 4.5 },{ 4, 4.5 },{ 4.5, 4.5 },{ 5, 4.5 },{ 5.5, 4.5 },{ 6, 4.5 },{ 6.5, 4.5 },{ 7, 4.5 },{ 7.5, 4.5 },{ 8, 4.5 },{ 8.5, 4.5 },{ 9, 4.5 },{ 9.5, 4.5 },{ 10, 4.5 }, { 10.5, 4.5 }, { 11, 4.5 }, { 11.5, 4.5 },{ 12, 4.5 }, { 12.5, 4.5 }, { 13, 4.5 }, { 13.5, 4.5 }, { 14, 4.5 }, { 14.5, 4.5 },
        //{ 15, 4.5 },{ 15.5, 4.5 },{ 16, 4.5 },{ 16.5, 4.5 },{ 17, 4.5 },{ 17.5, 4.5 },{ 18, 4.5 },{ 18.5, 4.5 },{ 19, 4.5 },{ 19.5, 4.5 }};

        public static readonly double[,] MainSpring = new double[1, 2] { { 1e-4, 0 } };
        //public static readonly double[,] MainSpring = new double[1, 2] { { 10, 10 } };

        //public static readonly double[,] Points = new double[1, 2] { { 6, 0 } };
        
        //public static readonly double[,] Points = new double[2, 2] { { 100, 0 }, { 150, 0 } };
        public static readonly double[,] Points = new double[3, 2] { { 100, 0 }, { 150, 0 }, { 200, 0 } };
        
        //public static readonly double[,] Points = new double[5, 2] { { 100, 0 }, { 150, 0 }, { 200, 0 }, { 250, 0 }, { 300, 0 } };
        //public static readonly double[,] Points = new double[6, 2] { { 4.5, 7.5 }, { 7.5, 4.5 }, { 10, 4.5 }, { 12.5, 4.5 }, { 15.5, 6 }, { 15.5, 7.5 } };
        //public static readonly double[,] MainSpring = new double[1, 2] { { 10, 15.1953125 } };

        public const string elemPath = @"C:\Users\dimap\source\repos\inverseProblem\inverseProblem\Generated\Elem.txt";
        public const string nodePath = @"C:\Users\dimap\source\repos\inverseProblem\inverseProblem\Generated\Node.txt";
        public const string elemMatPath = @"C:\Users\dimap\source\repos\inverseProblem\inverseProblem\Generated\ElemMat.txt";
        public const string outPath = @"C:\Users\dimap\source\repos\inverseProblem\inverseProblem\Generated\Out.txt";
        public const string Path = @"C:\Users\dimap\source\repos\inverseProblem\inverseProblem\Generated\";
        public const string outTimePath = @"C:\Users\dimap\source\repos\inverseProblem\inverseProblem\Generated\OutTime.txt";

        public const string matPath = @"C:\Users\dimap\source\repos\inverseProblem\inverseProblem\Input\File\Mat.txt";
        public const string timePath = @"C:\Users\dimap\source\repos\inverseProblem\inverseProblem\Input\File\Time.txt";
        public const string sredaPath = @"C:\Users\dimap\source\repos\inverseProblem\inverseProblem\Input\File\Sreda.txt";

        public const string bc1Path = @"C:\Users\dimap\source\repos\inverseProblem\inverseProblem\InputBC\Bc1.txt";
        public const string bc2Path = @"C:\Users\dimap\source\repos\inverseProblem\inverseProblem\InputBC\Bc2.txt";
        public const string bc3Path = @"C:\Users\dimap\source\repos\inverseProblem\inverseProblem\InputBC\Bc3.txt";
    }
}
