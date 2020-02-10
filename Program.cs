using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralClassLibrary;

namespace NeuralConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Network n = new Network();
            Console.WriteLine("Enter network in form: x y z");
            string size = Console.ReadLine();
            string [] networksize = size.Split();
            // Network n = new Network(networksize);
            Net n = new Net(networksize);
            //n.Learning_rate = 0.5;
            //for (int i = 0; i < networksize.Length; i++)
            //{
            //    Console.WriteLine(networksize[i]);
            //}
            int count = 0;
            int test_input_1, test_input_2;
            int result;
            Random rnd = new Random();
            string terminate = "";
            Console.WriteLine("Learning in progress");
            while (count <= /*20*/100000 /*&& terminate != "."*/)
            {
                //Console.WriteLine("Learning in progress");
                test_input_1 = rnd.Next(1, 3) % 2;
                //Console.WriteLine("test input 1 is: "+test_input_1);
                test_input_2 = rnd.Next(1, 3) % 2;
               // Console.WriteLine("test input 2 is: "+test_input_2);
                if (((test_input_1 == 0) && (test_input_2 == 1)) || ((test_input_1 == 1) && (test_input_2 == 0)))
                    result = 1;
                else
                    result = 0;
                List<double> test_input = new List<double> { test_input_1, test_input_2 };
                List<double> result_list = new List<double> { result };
                n.Learn(test_input, result_list);
                /* List<double> ans = n.Feedforward(test_input);
                 Console.WriteLine();
                 Console.WriteLine("Ans is:");
                 foreach (double d in ans)
                     Console.Write("\t " + d);
                 List<double> result_list = new List<double> { result };
                 List<double> CostVector = new List<double>();
                 if (CostVector.Count == 0)
                 {
                     for (int i = 0; i < ans.Count; i++)
                         CostVector.Add(ans[i] - result_list[i]);
                 }
                 else
                 {
                     for (int i = 0; i < CostVector.Count; i++)
                         CostVector[i] = (ans[i] - result_list[i]);
                 }
                     n.BackProp(CostVector);
                 //n.BackProp(ans, result_list);
                */

                //Console.WriteLine();
                //Console.WriteLine("Ans is:");
                //foreach (double d in ans)
                //    Console.Write("\t " + d);
                count++;
                if (count % 10000 == 0)
                {
                    Console.WriteLine("Enter '.' to skip to test or press enter to continue training");
                    terminate = Console.ReadLine();
                    if (terminate != ".")
                        count++;
                    else
                        break;
                }
                //count++;
            }
            Console.WriteLine();
            Console.WriteLine("Begin Test:");
            string repeat = "";
            string [] input = Console.ReadLine().Split(' ');
            List<double> test_inputs = new List<double>() {int.Parse(input[0]),int.Parse(input[1])};
            //test_inputs.Add((double)int.Parse(input[0]));
            //test_inputs.Add((double)int.Parse(input[1]));
            List<double>OutputVector = n.Test(test_inputs);
            Console.WriteLine("Guess is ");
                foreach (double d in OutputVector)
            Console.WriteLine(d.ToString());
            Console.WriteLine("Press Enter to continue or . to stop:");
            repeat = Console.ReadLine();
            while (repeat != ".")
            {
                input = Console.ReadLine().Split(' ');
                test_inputs = new List<double>() { int.Parse(input[0]), int.Parse(input[1]) };
                //test_inputs.Add((double)int.Parse(input[0]));
                //test_inputs.Add((double)int.Parse(input[1]));
                OutputVector = n.Test(test_inputs);
                Console.WriteLine("Guess is ");
                foreach (double d in OutputVector)
                    Console.WriteLine(d.ToString());
            }
            Console.ReadKey();
        }
    }
}
