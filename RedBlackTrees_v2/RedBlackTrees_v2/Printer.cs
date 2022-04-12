using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBlackTrees_v2
{
    internal class Printer
    {
        public static void PrintNode(Node root)
        {
            int maxLevel = Printer.MaxLevel(root);

            PrintNodeInternal(new List<Node>() { root }, 1, maxLevel);
        }

        private static void PrintNodeInternal(List<Node> nodes, int level, int maxLevel)
        {
            if (nodes.Count == 0 || Printer.IsAllElementsNull(nodes))
                return;

            int floor = maxLevel - level;
            int endgeLines = (int)Math.Pow(2, (Math.Max(floor - 1, 0)));
            int firstSpaces = (int)Math.Pow(2, (floor)) - 1;
            int betweenSpaces = (int)Math.Pow(2, (floor + 1)) - 1;

            Printer.PrintWhitespaces(firstSpaces);

            List<Node> newNodes = new List<Node>();
            foreach (Node node in nodes)
            {
                if (node != null)
                {
                    Console.BackgroundColor = node.Color == Color.RED ? ConsoleColor.DarkRed : ConsoleColor.Black;
                    if (node != Tree.NIL)
                    {
                        Console.Write(node.Key);
                    }
                    else
                    {
                        Console.Write('N');
                    }
                    Console.BackgroundColor = ConsoleColor.Black;
                    newNodes.Add(node.Left);
                    newNodes.Add(node.Right);
                }
                else
                {
                    newNodes.Add(null);
                    newNodes.Add(null);
                    Console.Write(" ");
                }

                Printer.PrintWhitespaces(betweenSpaces);
            }
            Console.WriteLine("");


            for (int i = 1; i <= endgeLines; i++)
            {
                for (int j = 0; j < nodes.Count; j++)
                {
                    Printer.PrintWhitespaces(firstSpaces - i);
                    if (nodes[j] == null)
                    {
                        Printer.PrintWhitespaces(endgeLines + endgeLines + i + 1);
                        continue;
                    }

                    if (nodes[j].Left != null)
                        Console.Write("/");
                    else
                        Printer.PrintWhitespaces(1);

                    Printer.PrintWhitespaces(i + i - 1);

                    if (nodes[j].Right != null)
                        Console.Write("\\");
                    else
                        Printer.PrintWhitespaces(1);

                    Printer.PrintWhitespaces(endgeLines + endgeLines - i);
                }

                Console.WriteLine("");
            }

            PrintNodeInternal(newNodes, level + 1, maxLevel);
        }

        private static void PrintWhitespaces(int count)
        {
            for (int i = 0; i < count; i++)
                Console.Write(" ");
        }

        private static int MaxLevel(Node node)
        {
            if (node == null)
                return 0;

            return Math.Max(Printer.MaxLevel(node.Left), Printer.MaxLevel(node.Right)) + 1;
        }

        private static bool IsAllElementsNull(List<Node> list)
        {
            foreach (Node node in list)
            {
                if (node != null)
                    return false;
            }

            return true;
        }


    }
}
