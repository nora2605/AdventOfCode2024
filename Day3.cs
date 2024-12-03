using System.Text.RegularExpressions;

namespace AdventOfCode2024;
internal partial class Day3
{
    string instrs;
    public Day3()
    {
        instrs = new StreamReader("inputs/day3.txt").ReadToEnd();
    }

    [GeneratedRegex(@"mul\((\d+),(\d+)\)")]
    private static partial Regex MulInstruction();

    [GeneratedRegex(@"mul\((\d+),(\d+)\)|do\(\)|don't\(\)")]
    private static partial Regex AnyInstruction();

    public int Part1()
    {
        return MulInstruction()
            .Matches(instrs)
            .Select(m => m.Groups)
            .Select(e => int.Parse(e[1].Value) * int.Parse(e[2].Value))
            .Sum();
    }

    public int Part2()
    {
        IEnumerable<Match> matches = AnyInstruction().Matches(instrs);
        bool enabled = true;
        int aggregate = 0;
        foreach (var match in matches)
        {
            switch (match.Value)
            {
                case "don't()":
                    enabled = false;
                    break;
                case "do()":
                    enabled = true;
                    break;
                default:
                    if (!enabled) break;
                    aggregate += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
                    break;
            }
        }
        return aggregate;
    }
}