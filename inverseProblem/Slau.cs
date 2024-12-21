using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace practika
{
    public class Slau
    {
        public Slau(GlobalMatrices globalMatrices, int nNode) 
        {
            _nNode = nNode;
            _globalMatrices = globalMatrices;
            q = new double[_nNode];
            MSG_no();
        }
        private GlobalMatrices _globalMatrices;
        public double[] q;
        private int _nNode;
        void MSG_no()
        {
            double[] z = new double[_nNode];
            double[] r = new double[_nNode];
            double[] y = new double[_nNode];
            int k_iter;
            int maxiter = 100000;
            double e = 0.00000000001;
            
            double alpha, betta;
            double rk_1_rk_1;
            multiplication_matrix_on_vector(q, y);
            summ(_globalMatrices._globalVectorB, -1, y, r);
            for (int i = 0; i < _nNode; i++)
                z[i] = r[i];

            for (int k = 1; k < maxiter; k++)
            {
                rk_1_rk_1 = vector_multiplication(r, r);
                multiplication_matrix_on_vector(z, y);
                alpha = rk_1_rk_1 / vector_multiplication(y, z);
                summ(q, alpha, z, q);
                multiplication_matrix_on_vector(z, y);
                summ(r, -alpha, y, r);
                betta = vector_multiplication(r, r) / rk_1_rk_1;
                summ(r, betta, z, z);
                var f = Norm(r) / Norm(_globalMatrices._globalVectorB);
                if (Norm(r) / Norm(_globalMatrices._globalVectorB) < e)
                {
                    k_iter = k;
                    return;
                }
            }
            k_iter = maxiter;
        }
        double[] multiplication_matrix_on_vector(double[] a, double[] b)
        {
            for (int i = 0; i < _nNode; i++)
                b[i] = _globalMatrices._globaleAdiag[i] * a[i];

            for (int i = 1; i < _nNode; i++)
            {
                int i0 = _globalMatrices.ig[i - 1];
                int i1 = _globalMatrices.ig[i];
                for (int j = 0; j < (i1 - i0); j++)
                {
                    b[i] += _globalMatrices._globaleATriangle[i0 + j] * a[_globalMatrices.jg[i0 + j]];
                    b[_globalMatrices.jg[i0 + j]] += _globalMatrices._globaleATriangle[i0 + j] * a[i];
                }
            }
            return b;
        }
        double Norm(double[] y)
        {
            double norma = 0;
            for (int i = 0; i < _nNode; i++)
                norma += y[i] * y[i];
            return Math.Sqrt(norma);
        }

        double vector_multiplication(double[] a, double[] b)
        {
            double s = 0;
            for (int i = 0; i < _nNode; i++)
                s += a[i] * b[i];
            return s;
        }
        double[] summ(double[] a, double b, double[] c, double[] d)
        {
            for (int i = 0; i < _nNode; i++)
                d[i] = a[i] + b * c[i];
            return d;
        }
    }
}
