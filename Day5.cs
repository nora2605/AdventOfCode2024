using System.Net;
using System.Xml.Serialization;

namespace AdventOfCode2024;

internal class Day5
{
    bool[,] rules;
    int[][] updates;
    IEnumerable<IGrouping<bool, int[]>>? groupedUpdates;

    public Day5()
    {
        string[] data = new StreamReader("inputs/day5.txt").ReadToEnd().Replace("\r", "").Split("\n\n");
        var preRules = data[0]
            .Split('\n')
            .Select(x => x.Split('|').Select(int.Parse).ToArray());

        //int max = pre_rules.Max(x => x.Max());
        rules = new bool[100, 100];

        foreach (var rule in preRules)
            rules[rule[0], rule[1]] = true;

        updates = data[1]
            .Split('\n')
            .Select(x => x.Split(',').Select(int.Parse).ToArray())
            .ToArray();
    }

    public int Part1()
    {
        return (groupedUpdates = updates.GroupBy(x =>
        {
            for (int i = 0; i < x.Length - 1; i++)
                for (int j = i + 1; j < x.Length; j++)
                    if (rules[x[j], x[i]])
                        return false;
            return true;
        })).First(x => x.Key).Select(x => x[x.Length / 2]).Sum();
    }

    public int Part2()
    {
        return groupedUpdates!
            .First(x => !x.Key)
            .Select(x => x.Order(new RuleComparer(rules)).ToArray())
            .Select(x => x[x.Length / 2])
            .Sum();
    }
}

internal class RuleComparer(bool[,] rules) : IComparer<int>
{
    public int Compare(int a, int b) => rules[a, b] ? -1 : rules[b, a] ? 1 : 0;
}
