using System;
using System.IO;
namespace N_Puzzle
{
    class Program
    {
        public static char Method;
        static void Main(string[] args)
        {
            Console.WriteLine("N Puzzle Problem:\n[1] Sample test cases\n[2] Complete testing");
            Console.Write("\nEnter your choice [1-2]: ");
            char choice = (char)Console.ReadLine()[0];
            Console.WriteLine("Please Enter The Solving Method 1- Manhatten Method else- Hamming Method");
            Method = (char)Console.ReadLine()[0];
            switch (choice)
            {
                case '1':
                    bool succeed = ReadAndCheck("Sample Test.txt");
                    if (succeed)
                    {
                        Console.Write("Do you want to run Complete Testing now (y/n)? ");
                        choice = (char)Console.Read();
                        if (choice == 'n' || choice == 'N')
                            break;
                        else if (choice == 'y' || choice == 'Y')
                            goto CompleteTest;
                        else
                        {
                            Console.WriteLine("Invalid Choice!");
                            break;
                        }
                    }
                    else
                    {
                        return;
                    }
                case '2':
                CompleteTest:
                    Console.WriteLine("Complete Testing is running now...");
                    succeed = ReadAndCheck("Complete Test.txt");
                    if (succeed)
                    {
                        Console.WriteLine("\nCongratulations... your program runs successfully");
                    }
                    break;
            }
        }
        static bool ReadAndCheck(string fileName)
        {
            FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(file);
            int cases = int.Parse(sr.ReadLine());
            int wrongAnswer = 0;
            for (int a = 0; a < cases; a++)
            {
                string sizeString = sr.ReadLine();
                int size = int.Parse(sizeString);
                int[,] n_Puzzle = new int[size, size];
                for (int i = 0; i < size; i++)
                {
                    string[] line = sr.ReadLine().Split();
                    for (int j = 0; j < size; j++)
                    {
                        int currentNumber = int.Parse(line[j]);
                        n_Puzzle[i, j] = currentNumber;
                    }
                }
                int expectedResult = int.Parse(sr.ReadLine());
                int receivedResult = N_Puzzle.NumberOfMOvements(size, n_Puzzle);
                if (receivedResult != expectedResult)
                {
                    wrongAnswer++;
                    Console.WriteLine("wrong answer at case " + (a + 1) + " expected = " + expectedResult + " received = " + receivedResult);
                }
            }
            sr.Close();
            file.Close();
            if (wrongAnswer == 0)
            {
                Console.WriteLine("Congratulations... :)");
                return true;
            }
            else
            {
                Console.WriteLine(wrongAnswer + " wrong answer out of " + cases);
                return false;
            }
        }
    }
}
