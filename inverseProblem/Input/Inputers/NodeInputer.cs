using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practika.Input
{
    internal class NodeInputer
    {
        public void Input(Grid grid)
        {
            using (StreamReader reader = new(Config.nodePath))
            {
                int nNode = int.Parse(reader.ReadLine()); ///??????
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var nodeArray = line.Split(' ').ToArray();
                    grid.nodes.Add(
                        new Node(
                             double.Parse(nodeArray[0]),
                            double.Parse(nodeArray[1])
                            ));
                }
            }
        }
    }
}
