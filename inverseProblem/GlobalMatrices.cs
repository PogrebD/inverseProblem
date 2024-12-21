namespace practika
{
    public class GlobalMatrices
    {
        public double[] _globaleATriangle;
        public double[] _globaleAdiag;

        public double[] _globaleMTriangle;
        public double[] _globaleMdiag;

        public double[] _globaleGTriangle;
        public double[] _globaleGdiag;

        public double[] _globalVectorB;
        public List<int> ig;
        private List<int> ig2;
        public List<int> jg;
        private List<List<int>> versh;
        private Grid _grid;
        public GlobalMatrices(Grid grid)
        {
            ig = new List<int>();
            jg = new List<int>();
            versh = new();
            versh.Init(grid.nodes.Count);
            _grid = grid;
            Portrait();
            _globaleAdiag = new double[ig.Count];
            _globaleATriangle = new double[jg.Count];

            _globaleMdiag = new double[ig.Count];
            _globaleMTriangle = new double[jg.Count];

            _globaleGdiag = new double[ig.Count];
            _globaleGTriangle = new double[jg.Count];

            _globalVectorB = new double[ig.Count];
            LocalMatricesInsertion();
        }
        public void Portrait()
        {
            for (int i = 0; i < _grid.elems.Count; i++)
            {
                for (int ji = 0; ji < 4; ji++)
                {
                    for (int j = 0; j < ji; j++)
                    {
                        versh[_grid.elems[i].index[ji]].Add(_grid.elems[i].index[j]);
                    }
                }
            }
            for (int i = 0; i < versh.Count; i++)
            {
                versh[i] = versh[i].Distinct().ToList();
                versh[i].Sort();
            }

            ig.Add(0);
            for (int i = 1; i < _grid.nodes.Count; i++)
            {
                ig.Add(ig[i - 1] + versh[i].Count);
            }

            foreach (var vershI in versh)
            {
                foreach (var vershJ in vershI)
                {
                    jg.Add(vershJ);
                }
            }
            ig2 = new List<int>();
            ig2.Add(0);
            ig2.AddRange(ig);
        }

        public void LocalMatricesInsertion()
        {
            for (int k = 0; k < _grid.elems.Count; k++)
            {
                //A
                for (int j = 0; j < 4; j++)
                {
                    _globaleAdiag[_grid.elems[k].index[j]] += _grid.elems[k].matrixA[j, j];
                }

                for (int i = 0; i < 4; i++)
                {
                    var ibeg = ig2[_grid.elems[k].index[i]];
                    for (int j = 0; j < i; j++)
                    {
                        var iend = ig2[_grid.elems[k].index[i] + 1] - 1;
                        while (jg[ibeg] != _grid.elems[k].index[j])
                        {
                            ibeg++;
                        }
                        _globaleATriangle[ibeg] += _grid.elems[k].matrixA[i, j];
                        ibeg++;
                    }
                }

                //M
                for (int j = 0; j < 4; j++)
                {
                    _globaleMdiag[_grid.elems[k].index[j]] += _grid.elems[k].matrixM[j, j];
                }

                for (int i = 0; i < 4; i++)
                {
                    var ibeg = ig2[_grid.elems[k].index[i]];
                    for (int j = 0; j < i; j++)
                    {
                        var iend = ig2[_grid.elems[k].index[i] + 1] - 1;
                        while (jg[ibeg] != _grid.elems[k].index[j])
                        {
                            ibeg++;
                        }
                        _globaleMTriangle[ibeg] += _grid.elems[k].matrixM[i, j];
                        ibeg++;
                    }
                }

                //G
                for (int j = 0; j < 4; j++)
                {
                    _globaleGdiag[_grid.elems[k].index[j]] += _grid.elems[k].matrixG[j, j];
                }

                for (int i = 0; i < 4; i++)
                {
                    var ibeg = ig2[_grid.elems[k].index[i]];
                    for (int j = 0; j < i; j++)
                    {
                        var iend = ig2[_grid.elems[k].index[i] + 1] - 1;
                        while (jg[ibeg] != _grid.elems[k].index[j])
                        {
                            ibeg++;
                        }
                        _globaleGTriangle[ibeg] += _grid.elems[k].matrixG[i, j];
                        ibeg++;
                    }
                }

                //B
                for (int j = 0; j < 4; j++)
                {
                    _globalVectorB[_grid.elems[k].index[j]] += _grid.elems[k].vectorB[j];
                }
            }
        }
    }
}
