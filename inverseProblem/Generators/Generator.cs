using practika;
using practika.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using inverseProblem.Generators;

namespace praktika.generators
{
    internal class Generator
    {
        public Generator(Grid grid)
        {
            nodeGenerator = new NodeGenerator();
            bc1Generator = new BC1Generator();
            bc2Generator = new BC2Generator();
        }

        public IGenerator nodeGenerator;
        public IGenerator bc1Generator;
        public IGenerator bc2Generator;

        public void Generate(Grid grid)
        {
            nodeGenerator.Generate(grid);
            bc1Generator.Generate(grid);
            bc2Generator.Generate(grid);
        }
    }
}
