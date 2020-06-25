using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;



namespace Left_factoring_and_recursion
{
    class Program
    { 
        // Removal of Left Recursion and Factoring
        // submitted to: Dr Talha Waheed
        // submitted by : Komal Shehzadi 2016-CS-178
        // main program
        static void Main(string[] args)
        {
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~Automatic Removal of Left Factoring and Recursion~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("\n");
            while (!inputfileReader.EndOfStream)
            {
                bool isLeftFactoringOrRecursive = false;
                string line = inputfileReader.ReadLine();
                Console.WriteLine("*****************************************************************");
                Console.WriteLine("Given Grammar:");
                Console.WriteLine(line);
                writeToFile("---------------------------------------------------------------------------------------------------------------------------------------------");
                writeToFile("~~~~~~~~~~~~~~~~~~~~~Given Grammar~~~~~~~~~~~~~~~~~~~~~");
                writeToFile(line);
                LeftFactoring(line, ref isLeftFactoringOrRecursive);
                LeftRecursion(line, ref isLeftFactoringOrRecursive);
                if(!isLeftFactoringOrRecursive)
                {
                    writeToFile("NOTHING TO REMOVE");
                    
                    Console.WriteLine("\n");
                    Console.WriteLine("NOTHING TO REMOVE :)");
                }
                writeToFile("--------------------------------------------------------------------------------------------------------------------------------------------");
                writeToFile("\n");
                Console.WriteLine("*****************************************************************");
                Console.WriteLine("\n");
            }
            Console.WriteLine("All Finished !!! :D");
            System.Console.ReadKey();
        }
        // shared vairiables
        const string Path = @"D:\1A Semesters\8th semester\CC\Left Recursion and Factoring\code\input and output directory\input.txt";
        private static StreamReader inputfileReader = new StreamReader(Path);
        private static FileStream outputfilepath = new FileStream(@"D:\1A Semesters\8th semester\CC\Left Recursion and Factoring\code\input and output directory\output.txt", FileMode.OpenOrCreate, FileAccess.Write);
        private static StreamWriter outfile = new StreamWriter(outputfilepath);

        // function to write to a file at outputfilepath
        public static void writeToFile(string str)
        {
            outfile.WriteLine(str);
            outfile.Flush();
        }
        // function to print grammar after removing left factoring on console and file
        public static void LeftFactoring(string inputline, ref bool isLeftFactoringOrRecursive)
        {
            string firstproductionafterremoval = "";
            string secondproductionafterremoval = "";
                if (PerformLeftFactoring(inputline,ref firstproductionafterremoval, ref secondproductionafterremoval))
                {
                isLeftFactoringOrRecursive = true;
                
                    Console.Write("\n" + "After Removing Left Factoring:\n");
                    Console.WriteLine(firstproductionafterremoval + "\n" + secondproductionafterremoval);
                
                writeToFile("~~~~~~~~~~~~~~~~~After Removing Left Factoring~~~~~~~~~~~~~~~");
                    writeToFile(firstproductionafterremoval);
                    writeToFile(secondproductionafterremoval);
                }

        }
        // function to remove left factoring 
    public static bool PerformLeftFactoring(string line, ref string firstproductionafterremoval, ref string secondproductionafterremoval)
    {

            string[] splittedgrammar = Regex.Split(line, " -> ");
            firstproductionafterremoval = splittedgrammar[0] + " -> ";
            secondproductionafterremoval = splittedgrammar[0] + "' -> ";
            string[] productions = splittedgrammar[1].Split('|');
            int temp = productions.Length;
            bool isLeftFactoring = false;
            if (temp >= 2)
            {

                productions[0] = productions[0].Trim();
                productions[1] = productions[1].Trim();
                string[] splittedfirstproduction = Regex.Split(productions[0], " ");
                string[] splittedsecondproduction = Regex.Split(productions[1], " ");
            
                int checkgreater = 0;


                int smallerproductionlength = 0;
                int i = 0;

                    if (splittedfirstproduction.Length > splittedsecondproduction.Length)
                    {
                       smallerproductionlength = splittedsecondproduction.Length;
                        checkgreater = 1;
                    }
                    else if (splittedfirstproduction.Length < splittedsecondproduction.Length)
                    {
                        smallerproductionlength = splittedfirstproduction.Length;
                        checkgreater = 2;
                    }
                    for (i = 0; i < smallerproductionlength; i++)
                    {
                            if (splittedfirstproduction[i].Equals(splittedsecondproduction[i]))
                            {
                                firstproductionafterremoval = firstproductionafterremoval + " " + splittedfirstproduction[i];

                                isLeftFactoring = true;
                            }
                            
                    }

                    if (checkgreater == 1)
                    {
                        for (int j = i; j < splittedfirstproduction.Length; ++j)
                        {
                            secondproductionafterremoval = secondproductionafterremoval + " " + splittedfirstproduction[j];
                        }
                    }
                    else if (checkgreater == 2)
                    {
                        for (int j = i; j < splittedsecondproduction.Length; ++j)
                        {
                            secondproductionafterremoval = secondproductionafterremoval + " " + splittedsecondproduction[j];
                        }
                    }
                    firstproductionafterremoval = firstproductionafterremoval + " " + splittedgrammar[0] + "'";
                    secondproductionafterremoval = secondproductionafterremoval + " | absolon";
                    for (int j = 2; j < productions.Length; ++j)
                    {
                        firstproductionafterremoval = firstproductionafterremoval + " | " + productions[j];
                    }
                }
                return isLeftFactoring;
        }

        // function to print grammar after removing left recursion on console and file
        public static void LeftRecursion(string line, ref bool isLeftFactoringOrRecursive)
        {
            string[] inputstring = Regex.Split(line, " -> ");
            string firstgrammar = inputstring[0] + " -> ";
            string secondgrammar = inputstring[0] + "' -> ";
            string[] splittedstring = inputstring[1].Split('|');
            
            if (PerformLeftRecursion(inputstring,splittedstring,ref firstgrammar, ref secondgrammar))
            {
                isLeftFactoringOrRecursive = true;
                Console.WriteLine("\n");
                Console.WriteLine("After Removing Left Recursion:");
                Console.WriteLine(firstgrammar);
                Console.WriteLine(secondgrammar);

                writeToFile("~~~~~~~~~~~~~~~~~~~~~~~~~~After Removing Left Recursion~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                writeToFile(firstgrammar);
                writeToFile(secondgrammar);
            }
        }
        // function to perform left recursion
        public static bool PerformLeftRecursion(string[] inputstring, string[] splittedstring,ref string firstgrammar,ref string secondgrammar)
        {
            bool isRecursive = false;
            for (int i = 0; i < splittedstring.Length; i++)
            {
                string[] bufferstring = Regex.Split(splittedstring[i], " ");
                if (bufferstring[0].Equals(""))
                {
                    bufferstring[1] = bufferstring[1].Replace(" ", String.Empty);
                    if (inputstring[0].Equals(bufferstring[1]))
                    {
                        isRecursive = true;
                        firstgrammar = firstgrammar + inputstring[0] + "'";
                        for (int j = 2; j < bufferstring.Length; ++j)
                        {
                            secondgrammar = secondgrammar + " " + bufferstring[j];
                        }
                        secondgrammar = secondgrammar + " " + inputstring[0] + "' | absolon";
                    }
                    else if (i == 0)
                    {
                        firstgrammar = firstgrammar + splittedstring[i];
                    }
                    else
                    {
                        firstgrammar = firstgrammar + " | " + splittedstring[i];
                    }
                }
                else if (!bufferstring[0].Equals(""))
                {
                    bufferstring[0] = bufferstring[0].Replace(" ", String.Empty);
                    if (inputstring[0].Equals(bufferstring[0]))
                    {
                        isRecursive = true;
                        firstgrammar = firstgrammar + inputstring[0] + "'";
                        for (int j = 1; j < bufferstring.Length; ++j)
                        {
                            secondgrammar = secondgrammar + " " + bufferstring[j];
                        }
                        secondgrammar = secondgrammar + " " + inputstring[0] + "' | absolon";
                    }
                    else if (i == 0)
                    {
                        firstgrammar = firstgrammar + splittedstring[i];
                    }
                    else
                    {
                        firstgrammar = firstgrammar + " | " + splittedstring[i];
                    }
                }
            }
            return isRecursive;
        }
    }
}
