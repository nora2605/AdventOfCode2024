using System.Collections.Concurrent;

namespace AdventOfCode2024;

internal class Day19
{
    HashSet<string> available;
    string[] patterns;

    public Day19()
    {
        string[] inputs = new StreamReader("inputs/day19.txt").ReadToEnd().Replace("\r", "").Split("\n\n");
        available = [];
        foreach (var a in inputs[0].Split(", ")) available.Add(a);
        patterns = inputs[1].Split('\n');
    }

    Dictionary<string, long> arrangements = [];
    long Arrangements(string pattern)
    {
        if (arrangements.TryGetValue(pattern, out long p)) return p;
        if (pattern.Length == 0) return 1;
        long agg = 0;
        for (int i = 1; i <= pattern.Length; i++) {
            string fst = pattern[..i];
            string snd = pattern[i..];
            if (available.Contains(fst))
                agg += Arrangements(snd);
        }
        arrangements.Add(pattern, agg);
        return agg;
    }

    public int Part1()
    {
        return patterns.Where(p => Arrangements(p) > 0).Count();
    }

    public long Part2() => patterns.Sum(Arrangements);
}
