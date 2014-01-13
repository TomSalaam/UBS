using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WordInstanceCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            //I have opted for the manual split implemented in the CountWordsUsingManualSplit() method.
            //There are faster algorithms such as the CountWordsUsingLinq() which i have included because it is very concise as well as fast.
            //But looking at time complexity
            //and the ease with which code can be read and maintained, it is often better to opt for the
            //most straightforward solution.
            //The manual split implementation loops only once but has more writes to the dictionary. Writes to the dictionary are pretty quick anyway.
            //The Linq implementation has potentially fewer writes to the dictionary but has to split and group thereby giving a higher time complexity
            CountWordsUsingManualSplit(Console.ReadLine());
            Console.ReadLine();

            //CountWordsUsingLinq(Console.ReadLine();
            //Console.ReadLine();
        }
        public static void CountWordsUsingManualSplit(String str)
        {
            DateTime start = DateTime.Now;
            int length = str.Length;
            int wordStart = 0;
            Dictionary<string, int> dict = new Dictionary<string, int>();

            //Using manual split rather than string.split(' ')
            for (int i = 0; i < length; i++)
            {
                if (str[i] == ' ')
                {
                    //Remove commas etc
                    string word = Regex.Replace(str.Substring(wordStart, i - wordStart), "[^a-zA-Z0-9]", "");
                    AddToDictionary(dict, word);
                    wordStart = i + 1;
                }
            }

            //Add last word in sentence
            AddToDictionary(dict, Regex.Replace(str.Substring(wordStart), "[^a-zA-Z0-9]", ""));

            foreach (KeyValuePair<string, int> item in dict)
            {
                Console.WriteLine("Word : " + item.Key + " - Instances : " + item.Value);
            }
            
        }

        private static void AddToDictionary(Dictionary<string, int> dict, string word)
        {
            if (!string.IsNullOrWhiteSpace(word))
            {
                if (!dict.ContainsKey(word))
                {
                    dict.Add(word, 1);
                }
                else
                {
                    dict[word] = ++dict[word];
                }
            }
        }

        public static TimeSpan CountWordsUsingLinq(String sentence)
        {
            DateTime start = DateTime.Now;

            //add some exceptions
            IList<string> exceptions = new List<string>();
            exceptions.Add("");

            //Remove full stop
            sentence = sentence.Replace(".", "");
            Dictionary<string, int> dict = sentence.Split(' ').Where(i => !exceptions.Contains(i))
            .GroupBy(s => s)
            .ToDictionary(g => Regex.Replace(g.Key, "[^a-zA-Z0-9]", ""), g => g.Count());

            foreach (KeyValuePair<string, int> item in dict)
            {
                Console.WriteLine("Word : " + item.Key + " - Instances : " + item.Value);
            }
            return DateTime.Now.Subtract(start);
        }
    }
}
