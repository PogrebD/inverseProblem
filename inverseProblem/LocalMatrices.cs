using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace practika
{
    internal class LocalMatrices
    {
        private Grid _grid;
        private double[,] _stiffnessR;
        private double[,] _stiffnessZ;
        private double[,] _stiffness;
        private double[,] _massR1;
        private double[,] _massR2;
        private double[,] _massZ;
        private double[,] _mass;
        public LocalMatrices(Grid grid)
        {
            _grid = grid;
            _stiffnessR = new double[2, 2];
            _stiffnessZ = new double[2, 2];
            _stiffness = new double[4, 4];
            _massR1 = new double[2, 2];
            _massR2 = new double[2, 2];
            _massZ = new double[2, 2];
            _mass = new double[4, 4];
            CalcLocalMatrices(grid);
        }
        public void CalcLocalMatrices(Grid grid)
        {
            for (int i = 0; i < _grid.elems.Count; i++)
            {
                grid.elems[i] = new Elem(new double[4, 4], 
                                        CalcMMatrix(_grid.elems[i], _grid.FindMatSigma(_grid.elems[i].mat)), 
                                        CalcStiffnessMatrix(_grid.elems[i], _grid.FindMatLambdaR(_grid.elems[i].mat),  _grid.FindMatLambdaZ(_grid.elems[i].mat)), 
                                    _grid.elems[i]);
                grid.elems[i] = new Elem(CalcBVector(_grid.elems[i], 
                                                    _grid.FindMatSigma(_grid.elems[i].mat), 
                                                    _grid.FindMatLambdaR(_grid.elems[i].mat), 
                                                    _grid.FindMatLambdaZ(_grid.elems[i].mat)), 
                                         _grid.elems[i]);
            }
        }

        public double[,] CalcMMatrix(Elem elem, double sigma)
        {
            var mass = CalcMassMatrix(elem);
            mass.Multiply(sigma, mass);
            return mass;
        }

        public double[,] CalcMassMatrix(Elem elem)
        {
            var _masss = new double[4, 4];
            var massR = CalcLocalMassRMatrix(elem);
            var massZ = CalcLocalMassZMatrix(elem);

            for (var i = 0; i < elem.index.Length; i++)
            {
                for (var j = 0; j <= i; j++)
                {
                    _masss[i, j] = massR[GetMuIndex(i), GetMuIndex(j)] * massZ[GetNuIndex(i), GetNuIndex(j)];
                    _masss[j, i] = _masss[i, j];
                }
            }
            return _masss;
        }

        public double[] CalcBVector(Elem elem, double sigma, double lambdaR, double lambdaZ)
        {
            var mas = CalcMassMatrix(elem);
            var b = new double[4];
            for (var i = 0; i < _mass.GetUpperBound(0) + 1; i++)
            {
                for (var j = 0; j < _mass.Length / (_mass.GetUpperBound(0) + 1); j++)
                {
                    b[j] += mas[j, i] * Func.FunctionB(_grid.nodes[elem.index[i]].r, _grid.nodes[elem.index[i]].z, sigma, lambdaR, lambdaZ);
                }
            }
            return b;
        }

        public double[,] CalcStiffnessMatrix(Elem elem, double lambdaR, double lambdaZ)
        {

            var _stiffnessgg = new double[4, 4];
            var massR = CalcLocalMassRMatrix(elem);
            var massZ = CalcLocalMassZMatrix(elem);

            var stiffnessR = new double[2, 2];
            CalcLocalStiffnessRMatrix(elem).Multiply(lambdaZ, stiffnessR);
            var stiffnessZ = new double[2, 2];
            CalcLocalStiffnessZMatrix(elem).Multiply(lambdaZ, stiffnessZ);
            for (var i = 0; i < elem.index.Length; i++)
            {
                for (var j = 0; j <= i; j++)
                {
                    _stiffnessgg[i, j] = stiffnessR[GetMuIndex(i), GetMuIndex(j)] * massZ[GetNuIndex(i), GetNuIndex(j)] +
                                       massR[GetMuIndex(i), GetMuIndex(j)] * stiffnessZ[GetNuIndex(i), GetNuIndex(j)] ;
                    _stiffnessgg[j, i] = _stiffnessgg[i, j];
                }
            }

            var pox = _stiffnessgg;
            return _stiffnessgg;
        }

       /* public double[,] CalcStiffnessMatrixCustom(double r1, double r2, double z1, double z2, double lambdaR, double lambdaZ)
        {

            var _stiffnessgg = new double[4, 4];
            var massR = CalcLocalMassRMatrixCustom(r1,r2-r1);
            var massZ = CalcLocalMassZMatrixCustom(z2-z1);

            var stiffnessR = new double[2, 2];
            CalcLocalStiffnessRMatrixCustom(r1,r2-r1).Multiply(lambdaR, stiffnessR);
            var stiffnessZ = new double[2, 2];
            CalcLocalStiffnessZMatrixCustom(z2-z1).Multiply(lambdaZ, stiffnessZ);
            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j <= i; j++)
                {
                    _stiffnessgg[i, j] = stiffnessR[GetMuIndex(i), GetMuIndex(j)] * massZ[GetNuIndex(i), GetNuIndex(j)] +
                                       massR[GetMuIndex(i), GetMuIndex(j)] * stiffnessZ[GetNuIndex(i), GetNuIndex(j)];
                    _stiffnessgg[j, i] = _stiffnessgg[i, j];
                }
            }
            return _stiffnessgg;
        }*/

        public double[,] CalcLocalStiffnessRMatrix(Elem elem)
        {
            GetStiffnessMatrix().Multiply((2 * _grid.nodes[elem.index[0]].r + (_grid.nodes[elem.index[1]].r - _grid.nodes[elem.index[0]].r)) / (2 * (_grid.nodes[elem.index[1]].r - _grid.nodes[elem.index[0]].r)), _stiffnessR);

            return _stiffnessR;
        }

        /*public double[,] CalcLocalStiffnessRMatrixCustom(double r, double hr)
        {
            GetStiffnessMatrix().Multiply((2 * r + hr) / (2 * hr), _stiffnessR);

            return _stiffnessR;
        }*/

        public double[,] CalcLocalMassRMatrix(Elem elem)
        {
            GetMassRMatrix().Multiply((_grid.nodes[elem.index[1]].r - _grid.nodes[elem.index[0]].r) * (_grid.nodes[elem.index[1]].r - _grid.nodes[elem.index[0]].r) / 12d, _massR1);

            GetMassZMatrix().Multiply((_grid.nodes[elem.index[1]].r - _grid.nodes[elem.index[0]].r) * _grid.nodes[elem.index[0]].r / 6d, _massR2);

            _massR1.Sum(_massR2, _massR1);

            return _massR1;
        }

        /*public double[,] CalcLocalMassRMatrixCustom(double r, double hr)
        {
            GetMassRMatrix().Multiply(hr * hr / 12d, _massR1);

            GetMassZMatrix().Multiply(hr * r / 6d, _massR2);

            _massR1.Sum(_massR2, _massR1);

            return _massR1;
        }*/

        public double[,] CalcLocalStiffnessZMatrix(Elem elem)
        {
            GetStiffnessMatrix().Multiply(1d / (_grid.nodes[elem.index[2]].z - _grid.nodes[elem.index[0]].z), _stiffnessZ);

            return _stiffnessZ;
        }

        /*public double[,] CalcLocalStiffnessZMatrixCustom(double hz)
        {
            GetStiffnessMatrix().Multiply(1d / hz, _stiffnessZ);

            return _stiffnessZ;
        }*/
        public double[,] CalcLocalMassZMatrix(Elem elem)
        {
            GetMassZMatrix().Multiply((_grid.nodes[elem.index[2]].z - _grid.nodes[elem.index[0]].z) / 6d, _massZ);

            return _massZ;
        }

        /*public double[,] CalcLocalMassZMatrixCustom(double hz)
        {
            GetMassZMatrix().Multiply(hz / 6d, _massZ);

            return _massZ;
        }*/

        public double[,] GetStiffnessMatrix() => new double[2, 2] { { 1d, -1d }, { -1d, 1d } };
        public double[,] GetMassZMatrix() => new double[2, 2] { { 2d, 1d }, { 1d, 2d } };
        public double[,] GetMassRMatrix() => new double[2, 2] { { 1d, 1d }, { 1d, 3d } };

        private static int GetMuIndex(int i)
        {
            return i % 2;
        }
        private static int GetNuIndex(int i)
        {
            return i / 2;
        }
    }
}


