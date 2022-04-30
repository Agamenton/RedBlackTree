using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBlackTrees_v2
{
    public class Tree
    {
        public Node Root { get; set; }

        public static Node NIL = new Node(-1) {Color = Color.BLACK, Parent = NIL, Left = NIL, Right = NIL };


        public Tree()
        {
            Root = NIL;
        }

        public Tree(int i)
        {
            Root = new Node(i);
        }

        public Tree(Node root)
        {
            Root = root;
        }



        public void LeftRotate(Tree T, Node x)
        {
            Node y = x.Right;
            x.Right = y.Left;
            if (y.Left != NIL)
            {
                y.Left.Parent = x;
            }

            y.Parent = x.Parent;
            if (x.Parent == NIL)
            {
                T.Root = y;
            }
            else if (x == x.Parent.Left)
            {
                x.Parent.Left = y;
            }
            else
            {
                x.Parent.Right = y;
            }

            y.Left = x;
            x.Parent = y;
        }


        public void RightRotate(Tree T, Node x)
        {
            Node y = x.Left;
            x.Left = y.Right;
            if (y.Right != NIL)
            {
                y.Right.Parent = x;
            }

            y.Parent = x.Parent;
            if (x.Parent == NIL)
            {
                T.Root = y;
            }
            else if (x == x.Parent.Right)
            {
                x.Parent.Right = y;
            }
            else
            {
                x.Parent.Left = y;
            }

            y.Right = x;
            x.Parent = y;
        }

        public void RedBlackInsert(Tree T, Node z)
        {
            Node y = NIL;
            Node x = T.Root;
            while (x != NIL)
            {
                y = x;
                if (z.Key < x.Key)
                {
                    x = x.Left;
                }
                else if(z.Key > x.Key)
                {
                    x = x.Right;
                }
                else // do not allow duplicate keys
                {
                    return;
                }
            }

            z.Parent = y;
            if (y == NIL)
            {
                T.Root = z;
            }
            else if (z.Key < y.Key)
            {
                y.Left = z;
            }
            else
            {
                y.Right = z;
            }

            z.Left = NIL;
            z.Right = NIL;
            z.Color = Color.RED;
            RedBlackFixup(T, z);
        }

        private void RedBlackFixup(Tree T, Node z)
        {
            while (z.Parent.Color == Color.RED)
            {
                if (z.Parent == z.Parent.Parent.Left)
                {
                    Node y = z.Parent.Parent.Right;
                    if (y.Color == Color.RED)
                    {
                        z.Parent.Color = Color.BLACK;
                        y.Color = Color.BLACK;
                        z.Parent.Parent.Color = Color.RED;
                        z = z.Parent.Parent;
                    }
                    else 
                    {
                        if (z == z.Parent.Right)
                        {
                            z = z.Parent;
                            LeftRotate(T, z);
                        }

                        z.Parent.Color = Color.BLACK;
                        z.Parent.Parent.Color = Color.RED;
                        RightRotate(T, z.Parent.Parent);
                    }
                }
                else
                {
                    Node y = z.Parent.Parent.Left;
                    if (y.Color == Color.RED)
                    {
                        z.Parent.Color = Color.BLACK;
                        y.Color = Color.BLACK;
                        z.Parent.Parent.Color = Color.RED;
                        z = z.Parent.Parent;
                    }
                    else
                    {
                        if (z == z.Parent.Left)
                        {
                            z = z.Parent;
                            RightRotate(T, z);
                        }

                        z.Parent.Color = Color.BLACK;
                        z.Parent.Parent.Color = Color.RED;
                        LeftRotate(T, z.Parent.Parent);
                    }
                }
            }

            T.Root.Color = Color.BLACK;
        }



        private void RedBlackTransplant(Tree T, Node u, Node v)
        {
            if (u.Parent == NIL)
            {
                T.Root = v;
            }
            else if (u == u.Parent.Left)
            {
                u.Parent.Left = v;
            }
            else
            {
                u.Parent.Right = v;
            }

            v.Parent = u.Parent;
        }

        
        public Node TreeMinimum(Node x)
        {
            Node min = x;
            while (min.Left != NIL)
            {
                min = min.Left;
            }
            return min;
        }

        public void RedBlackDelete(Tree T, Node z)
        {
            Node y = z;
            Node x;
            Color yOriginalColor = y.Color;
            if (z.Left == NIL)
            {
                x = z.Right;
                RedBlackTransplant(T, z, z.Right);
            }
            else if (z.Right == NIL)
            {
                x = z.Left;
                RedBlackTransplant(T, z, z.Left);
            }
            else
            {
                y = TreeMinimum(z.Right);
                yOriginalColor = y.Color;
                x = y.Right;
                if (y.Parent == z)
                {
                    x.Parent = y;
                }
                else
                {
                    RedBlackTransplant(T, y, y.Right);
                    y.Right = z.Right;
                    y.Right.Parent = y;
                }

                RedBlackTransplant(T, z, y);
                y.Left = z.Left;
                y.Left.Parent = y;
                y.Color = z.Color;
            }
            if (yOriginalColor == Color.BLACK)
            {
                RedBlackDeleteFixup(T, x);
            }
        }




        private void RedBlackDeleteFixup(Tree T, Node x)
        {
            while (x != Root && x.Color == Color.BLACK)
            {
                if (x == x.Parent.Left)
                {
                    Node w = x.Parent.Right;
                    if (w.Color == Color.RED)
                    {
                        w.Color = Color.BLACK;
                        x.Parent.Color = Color.RED;
                        LeftRotate(T,x.Parent);
                        w = x.Parent.Right;
                    }

                    if (w.Left.Color == Color.BLACK && w.Right.Color == Color.BLACK)   
                    {
                        w.Color = Color.RED;
                        x = x.Parent;
                    }

                    else
                    {
                        if (w.Right.Color == Color.BLACK)
                        {
                            w.Left.Color = Color.BLACK;
                            w.Color = Color.RED;
                            RightRotate(T,w);
                            w = x.Parent.Right;
                        }

                        w.Color = x.Parent.Color;
                        x.Parent.Color = Color.BLACK;
                        w.Right.Color = Color.BLACK;
                        LeftRotate(T, x.Parent);
                        x = T.Root;
                    }
                }
                else
                {
                    Node w = x.Parent.Left;
                    if (w.Color == Color.RED)
                    {
                        w.Color = Color.BLACK;
                        x.Parent.Color = Color.RED;
                        RightRotate(T,x.Parent);
                        w = x.Parent.Left;
                    }

                    if (w.Left.Color == Color.BLACK && w.Right.Color == Color.BLACK)    
                    {
                        w.Color = Color.RED;
                        x = x.Parent;
                    }

                    else
                    {
                        if (w.Left.Color == Color.BLACK)
                        {
                            w.Right.Color = Color.BLACK;
                            w.Color = Color.RED;
                            LeftRotate(T, w);
                            w = x.Parent.Left;
                        }

                        w.Color = x.Parent.Color;
                        x.Parent.Color = Color.BLACK;
                        w.Left.Color = Color.BLACK;
                        RightRotate(T, x.Parent);
                        x = T.Root;
                    }
                }
            }

            x.Color = Color.BLACK;
        }


        public Node Search(int i)
        {
            Node node = Root;
            while (node != NIL)
            {
                if (node.Key == i)
                {
                    return node;
                }
                else if (node.Key > i)
                {
                    node = node.Left;
                }
                else
                {
                    node = node.Right;
                }
            }
            return null;
        }

    }
}
