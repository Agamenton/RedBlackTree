using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBlackTrees_v2
{
    public class Printer2
    {

        public static void Print(Tree T)
        {
            Node root = T.Root;
            List<List<string>> lines = new List<List<string>>();

            List<Node> level = new List<Node>();
            List<Node> next = new List<Node>();

            level.Add(root);
            int nn = 1;

            int widest = 0;

            while (nn != 0)
            {
                List<string> line = new List<string>();

                nn = 0;

                foreach (Node n in level)
                {
                    if (n == null)
                    {
                        line.Add(null);

                        next.Add(null);
                        next.Add(null);
                    }
                    else
                    {
                        string aa = n.Key.ToString();
                        line.Add(aa);
                        if (aa.Length > widest) widest = aa.Length;

                        next.Add(n.Left);
                        next.Add(n.Right);

                        if (n.Left != null) nn++;
                        if (n.Right != null) nn++;
                    }
                }

                if (widest % 2 == 1) widest++;

                lines.Add(line);

                List<Node> tmp = level;
                level = next;
                next = tmp;
                next.RemoveAll(n => true);
            }

            List<string> tmpLine = lines[lines.Count - 1];
            int perpiece = tmpLine.Count * (widest + 4);

            for (int i = 0; i < lines.Count; i++)
            {
                List<string> line = lines[i];
                int hpw = (int)Math.Floor(perpiece / 2f) - 1;

                if (i > 0)
                {
                    for (int j = 0; j < line.Count; j++)
                    {

                        // split node
                        char c = ' ';
                        if (j % 2 == 1)
                        {
                            if (line[j-1] != null)
                            {
                                c = (line[j] != null) ? '┴' : '┘';
                            }
                            else
                            {
                                if (j < line.Count && line[j] != null) c = '└';
                            }
                        }
                        Console.Write(c);

                        // lines and spaces
                        if (line[j] == null)
                        {
                            for (int k = 0; k < perpiece - 1; k++)
                            {
                                Console.Write(" ");
                            }
                        }
                        else
                        {

                            for (int k = 0; k < hpw; k++)
                            {
                                Console.Write(j % 2 == 0 ? " " : "─");
                            }
                            Console.Write(j % 2 == 0 ? "┌" : "┐");
                            for (int k = 0; k < hpw; k++)
                            {
                                Console.Write(j % 2 == 0 ? "─" : " ");
                            }
                        }
                    }
                    Console.WriteLine();
                }

                // print line of numbers
                for (int j = 0; j < line.Count; j++)
                {

                    string f = line[j];
                    if (f == null) f = "";
                    int gap1 = (int)Math.Ceiling(perpiece / 2f - f.Length / 2f);
                    int gap2 = (int)Math.Floor(perpiece / 2f - f.Length / 2f);

                    // a number
                    for (int k = 0; k < gap1; k++)
                    {
                        Console.Write(" ");
                    }

                    // -1 is NIL
                    if (f.Equals("-1"))
                    {
                        Console.Write(" N");
                    }

                    // other nodes also have color
                    else if (!f.Equals(""))
                    {
                        SwitchConsoleColor(T, Int32.Parse(f));
                        Console.Write(f);
                    }
                    Console.ForegroundColor = ConsoleColor.White;

                    for (int k = 0; k < gap2; k++)
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();

                perpiece /= 2;
            }
        }

        private static void SwitchConsoleColor(Tree T, int key)
        {
            Node n = T.Search(key);
            if(n != null)
            {
                if (n.Color == Color.RED)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

       
    }
}
