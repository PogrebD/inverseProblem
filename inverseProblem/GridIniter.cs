using practika.Input.Inputers;
using praktika.generators;
using praktika.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practika
{
    public class GridIniter
    {
        public Grid Init(int mode, int mat1, int mat2)
        {
            Grid grid = new();
            SredaInputer sredaInputer = new();
            sredaInputer.Input(grid);
            Generator generator = new(grid);
            generator.Generate(grid);
            Inputer inputer = new();
            inputer.Input(grid);
            return grid;
        }
    }
}
