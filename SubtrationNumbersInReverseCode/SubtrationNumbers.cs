using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtrationNumbersInReverseCode
{
    class SubtrationNumbers
    {
        private const string _examplesPath = "..\\..\\..\\..\\Examples\\input.txt" ;
        private const string _resultFilePath = "..\\..\\..\\..\\Result\\output.txt";



        private List<string> GetExamples()
        {
            List<string> exapmles = new List<string>();
            StreamReader reader = new StreamReader(_examplesPath);
            while (!reader.EndOfStream)
            {
                exapmles.Add(reader.ReadLine());
            }
            reader.Close();

            return exapmles;
        }

        private string Calculate(string exapmle)
        {
            List<string> result = new List<string>();
            int additionalUnits = 0;
            string[] operands = exapmle.Split('\t');
            char[] op1 = operands[0].ToCharArray();
            char[] op2 = operands[1].ToCharArray();
            for (int i = op1.Length-1; i >= 0; i--)
            {
                if (op1[i] == '0' && op2[i] == '0')
                {
                    if (additionalUnits == 0)
                    {
                        result.Insert(0,"0");
                    }
                    else
                    {
                        result.Insert(0, "1");
                        additionalUnits--;
                    }
                    continue;
                }

                if (op1[i] == '1' && op2[i] == '1')
                {

                    result.Insert(0, "0");
                    additionalUnits++;
                    continue;
                    
                }

                if (op1[i] == '1' || op2[i] == '1' && op1[i] == '0' || op2[i] == '0' )
                {
                    if (additionalUnits == 0)
                    {
                        result.Insert(0, "1");

                    }
                    else
                    {
                        result.Insert(0, "0");
                    }

                }
            }

            if (additionalUnits != 0)
            {
                result.Insert(0, "1");
                operands[0] = "";
                operands[1] = "";
            }

            string readyAnswer = "";
            foreach (var el in result)
            {
                readyAnswer += el;
            }

            return readyAnswer;
        }

        private void PrintResults(List<string> exapmples, List<string> results)
        {
            string[] examplesArray = exapmples.ToArray();
            string[] resultsArray = results.ToArray();
            for (int i = 0; i < resultsArray.Length; i++)
            {
                string result = resultsArray[i];
                if (result[0] == '1')
                {
                    string examplePart = "";
                    for (int j = 0; j < result.Length; j++)
                    {
                        if (j == result.Length - 1)
                        {
                            examplePart += "1";
                        }
                        else
                        {
                            examplePart += "0";
                        }
                    }
                    string example = result + "\t" + examplePart;
                    resultsArray[i] = Calculate(example);
                }
                
            }
            StreamWriter writer = new StreamWriter(_resultFilePath);
            for (int i = 0; i < exapmples.Count; i++)
            {
                writer.WriteLine(exapmples[i]+" = "+resultsArray[i]);
            }
            writer.Close();
        }

        public void GetReuslt()
        {
            List<string> examples = GetExamples();
            List<string> results = new List<string>();
            foreach (var el in examples)
            {
                results.Add(Calculate(el));
            }
            PrintResults(examples,results);

        }

    }
}
