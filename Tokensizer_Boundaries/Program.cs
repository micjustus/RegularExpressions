using System;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace RegularExpressions
{
    class Program
    {
        static void Main()
        {
            const string pattern1 = "bss_subject";
            const string pattern2 = "bss_episode";
            const string pattern3 = "history";
            const string pattern4 = "historical";

            string[] testInputs = [
              $"{pattern1}"
            , $"{pattern1}_{pattern1}"
            , $"{pattern1}_{pattern2}"
            , $"{pattern1}_{pattern3}"
            , $"{pattern1}_{pattern4}"

            , $"{pattern2}"
            , $"{pattern2}_{pattern1}"
            , $"{pattern2}_{pattern2}"
            , $"{pattern2}_{pattern3}"
            , $"{pattern2}_{pattern4}"

            , $"{pattern3}"
            , $"{pattern3}_{pattern1}"
            , $"{pattern3}_{pattern2}"
            , $"{pattern3}_{pattern3}"
            , $"{pattern3}_{pattern4}"

            , $"{pattern4}"
            , $"{pattern4}_{pattern1}"
            , $"{pattern4}_{pattern2}"
            , $"{pattern4}_{pattern3}"
            , $"{pattern4}_{pattern4}"
        ];

            string[] prefixes = [ "junk_", "xhdgf_", "_xyz_", "_09023_", "" ];
            string[] suffixes = [ "_end", "_test", "_v2", "_final", "" ];

            string pattern = @$"(?=.*(?<subject>{pattern1}|{pattern2}))(?=.*(?<period>{pattern3}|{pattern4})).*\.csv$";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

            var rand = new Random();
            
            foreach (string input in testInputs)
            {
                var idxP = prefixes[rand.Next(prefixes.Length)];
                var idxS = suffixes[rand.Next(suffixes.Length)];

                var test = $"{idxP}{input}{idxS}.csv";

                var match = regex.Match(test);
                if (match.Success)
                {
                    var subject = match.Groups["subject"].Value;
                    var period = match.Groups["period"].Value;
                    Console.WriteLine($"Matched. Subject={subject}, Perdio={period}, Input={input}");
                }
                else
                {
                    Console.WriteLine($"No match: {test}");
                }
            }

            Console.ReadLine();
        }
    }

}
