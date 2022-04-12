using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBlackTrees_v2
{
    public enum Color
    {
        RED,
        BLACK
    }


    public class Node
    {
        public int Key { get; set; }
        //public Generic Data { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public Node Parent { get; set; }
        public Color Color { get; set; }

        public Node(int key)
        {
            Key = key;
            //Data = data;
            Left = Tree.NIL;
            Right = Tree.NIL;
            Parent = Tree.NIL;
            Color = Color.BLACK;
        }
    }
}
