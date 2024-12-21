using practika;
using practika.Input;
using practika.Input.Inputers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace praktika.Input
{
    internal class Inputer
    {
        ElemInputer elemInputer = new();
        NodeInputer nodeInputer = new();
        MatInputer  matInputer  = new();

        public void Input(Grid grid)
        {
            elemInputer.Input(grid);
            nodeInputer.Input(grid);
            matInputer.Input(grid);
        }
    }
}
