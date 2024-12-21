using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practika.Input
{
    internal class ElemInputer
    {
        public void Input(Grid grid)
        {
            using (StreamReader reader = new(Config.elemPath))
            {
                int nElem = int.Parse(reader.ReadLine()); ///??????
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var elemArray = line.Split(' ').ToArray();
                    grid.elems.Add(
                        new Elem(
                            new int[]
                            { int.Parse(elemArray[0]),
                            int.Parse(elemArray[1]),
                            int.Parse(elemArray[2]),
                            int.Parse(elemArray[3]),
                            }));
                }
            }
        }
    }
}
