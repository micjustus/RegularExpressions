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

            var randP = new Random();
            var randS = new Random();

            foreach (string input in testInputs)
            {
                var idxP = randP.Next(prefixes.Length - 1);
                var idxS = randS.Next(suffixes.Length - 1);

                var test = $"{prefixes[idxP]}{input}{suffixes[idxS]}.csv";

                Match match = Regex.Match(test, pattern, RegexOptions.IgnoreCase);
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
