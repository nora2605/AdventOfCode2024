using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    internal class Day4
    {
        string[] lines;
        string[] columns;
        string[] diag1;
        string[] diag2;

        public Day4()
        {
            lines = new StreamReader("inputs/day4.txt").ReadToEnd().Replace("\r", "").Split('\n');
            columns = lines[0].Select((_, i) => new string(lines.Select(l => l[i]).ToArray())).ToArray();
            diag1 = new string[lines.Length + columns.Length - 1];
            diag2 = new string[lines.Length + columns.Length - 1];
            for (int i = 0; i < lines.Length + columns.Length - 1; i++)
            {
                string diagonal1 = "";
                string diagonal2 = "";
                for (int j = 0; j <= i; j++)
                {
                    if (i - j < lines.Length && j < columns.Length)
                    {
                        diagonal1 += lines[i - j][j];
                        diagonal2 += lines[i - j][columns.Length - j - 1];
                    }
                }
                diag1[i] = diagonal1;
                diag2[i] = diagonal2;
            }
        }

        static int Occurences(string searchString, string input)
        {
            int c = 0;
            string reversed = new string(searchString.Reverse().ToArray());
            for (int i = 0; i < input.Length - searchString.Length + 1; i++)
            {
                string subs = input[i..(i+searchString.Length)];
                if (subs == searchString) c++;
                else if (subs == reversed) c++;
            }
            return c;
        }

        public int Part1()
        {
            return
                lines.Select(l => Occurences("XMAS", l)).Sum() +
                columns.Select(c => Occurences("XMAS", c)).Sum() + 
                diag1.Select(d => Occurences("XMAS", d)).Sum() + 
                diag2.Select(d => Occurences("XMAS", d)).Sum();
        }

        public int Part2()
        {
            int c = 0;
            // use a 3x3 kernel and scan
            for (int i = 0; i < lines.Length - 2; i++)
            {
                for (int j = 0; j < columns.Length - 2; j++)
                {
                    if (lines[i+1][j+1] == 'A')
                        if ((lines[i][j] == 'M' && lines[i + 2][j + 2] == 'S' ||
                            lines[i][j] == 'S' && lines[i + 2][j + 2] == 'M') &&
                            (lines[i + 2][j] == 'M' && lines[i][j + 2] == 'S' ||
                            lines[i + 2][j] == 'S' && lines[i][j + 2] == 'M'))
                            c++;
                }
            }
            return c;
        }
    }
}
