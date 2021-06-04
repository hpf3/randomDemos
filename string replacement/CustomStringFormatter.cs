using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace string_replacement
{
    public static class CustomStringFormatter
    {
        public static string Format(string input, Dictionary<string, string> dictionary)
        {
            //need to modify the string a little to prevent seemingly fine keys from failing
            string[] textArray = input
                .Replace(Environment.NewLine, Environment.NewLine + " ")
                .Replace(@"$MATH{", @"$MATH{ ")
                .Replace(@"}", @" } ")
                .Split(' ');

            for (int i = 0; i < textArray.Length; i++)
            {
                if (textArray[i].StartsWith("$"))
                {
                    textArray[i] = GetValue(textArray[i], dictionary);
                }
            }

            //and then undo the previous edits to return it to how the user typed it
            string Output = String.Join(' ', textArray)
                .Replace(Environment.NewLine + " ",Environment.NewLine)
                .Replace(@"$MATH{ ",@"$MATH{")
                .Replace(@" } ",@"}");
            return MathFormat(Output);

            
        }

        /// <summary>
        /// Removes the leading $ from a key and performs a lookup
        /// </summary>
        /// <param name="Key">the key to lookup</param>
        /// <param name="dictionary">the dictionary to search</param>
        /// <returns>if key is not found returns "#INVALID_KEY:"+Key</returns>
        private static string GetValue(string Key,Dictionary<string,string> dictionary)
        {
            
            if (Key.StartsWith("$MATH{"))//we dont want to handle this yet, so we just ignore it
            {
                return Key;
            }
            if (Key.StartsWith("$"))//i like to store my keys without a marker, there probably isn't a case where this is skipped
            {
                Key = Key.Substring(1);
            }
            if (dictionary.ContainsKey(Key))
            {
                return dictionary.GetValueOrDefault(Key);
            }
            else
            {
                return $"#INVALID_KEY:{Key}";
            }
            
        }


        private static readonly string MathRegex = @"\$MATH\{(.*?)\}";
        private static string MathFormat(string input)
        {
            System.Text.RegularExpressions.MatchCollection matchCollection = System.Text.RegularExpressions.Regex.Matches(input, MathRegex);
            for (int i = 0; i < matchCollection.Count; i++)
            {
                string solution;
                try
                {
                    solution = new System.Data.DataTable().Compute(matchCollection[i].Groups[1].Value, null).ToString();
                }
                catch (Exception)
                {
                    solution = "#MATH_ERROR: " + matchCollection[i].Groups[1].Value;
                }
                
                input = input.Replace(matchCollection[i].Value, solution);
            }
            return input;
        }
    }
}
