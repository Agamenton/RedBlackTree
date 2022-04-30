using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBlackTrees_v2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int howManyNodes = 12;
            int keysRange    = 20;     // from 0
            
            while (true)
            {
                Console.WriteLine("Do you want to run Test?\n[Y / N]");
                var input = Console.ReadKey();
                if (input.Key == ConsoleKey.N)
                {
                    break;
                }
                else if(input.Key == ConsoleKey.Y)
                {
                    Test(howManyNodes, keysRange);
                }
            }

            Console.WriteLine("Testing Red-Black Trees finished\n(Press anything to end)");
            Console.ReadLine();
        }



        public static void Test(int howManyNodes, int keysRange)
        {

            int maxNodes = howManyNodes;
            int maxKey = keysRange;
            if (maxKey < maxNodes)
            {
                Console.WriteLine("Amount of nodes must be lower than keys-range");
                return;
            }

            Tree t = new Tree();

            List<Node> nodes = new List<Node>();

            Random random = new Random();

            try
            {
                while (nodes.Count < maxNodes)
                {
                    int newKey = random.Next(maxKey);

                    if (nodes.All(n => n.Key != newKey))
                    {
                        nodes.Add(new Node(newKey));
                    }
                }

                Console.WriteLine("===========================================================");
                Console.WriteLine("Inserting " + maxNodes + " nodes in random order, key range from 0 to " + maxKey);

                int insertCount = 0;
                foreach (Node n in nodes)
                {
                    Console.WriteLine("-----------------------");
                    Console.WriteLine("Inserted key: " + n.Key);
                    t.RedBlackInsert(t, n);
                    //Printer.PrintNode(t.Root);
                    Printer2.Print(t);
                    Console.WriteLine();
                    insertCount++;
                }
                Console.WriteLine("= = = = INSERTS DONE = = = =\n");
            }
            catch (Exception e)
            {
                Console.WriteLine("INSERT FAILED");
                Console.WriteLine(e);
            }

            try
            {
                Console.WriteLine("================================================");
                Console.WriteLine("Now Deleting nodes");
                int deleteCount = 0;
                foreach (Node n in nodes)
                {
                    Console.WriteLine("-----------------------");
                    Console.WriteLine("Deleted key: " + n.Key);
                    t.RedBlackDelete(t, n);
                    //Printer.PrintNode(t.Root);
                    Printer2.Print(t);
                    Console.WriteLine();
                    deleteCount++;
                }
                Console.WriteLine("= = = = DELETES DONE = = = =\n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
