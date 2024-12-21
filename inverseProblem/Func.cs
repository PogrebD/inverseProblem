using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practika
{
    internal class Func
    {
        public static double FunctionB(double r, double z, double sigma, double lambdaR, double lambdaZ)
        {
            double componentSigma = 2;
            componentSigma *= sigma;
            double componentLambdaR = 4;
            componentLambdaR *= lambdaR;
            double componentLambdaZ = 2;
            componentLambdaZ *= lambdaZ;

            return 0;
            //return componentSigma-componentLambdaR-componentLambdaZ;
            //return -4*lambda;
            //return 1*sigma;
            //return lambda * (Math.Pow(double.E, r) + Math.Pow(double.E, r) * r)*(-1/r);
            //return (0)*sigma+lambda*(-9*r);

        }
        public static double u(double r, double z)
        {
            //return r * r + t + z;
            return 0;
            //return r*r;
            //return t;
            //return Math.Pow(double.E, r);
            //return  r*r*r;

        }
    }
}
