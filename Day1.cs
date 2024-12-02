namespace AdventOfCode2024;
public class Day1
{
    int[] list_left;
    int[] list_right;

    public Day1()
    {
        var input = new StreamReader("inputs/day1.txt")!.ReadToEnd();
        var data = input.Split('\n').Select(l => l.Split("   ").Select(int.Parse).ToArray());
        list_left = data.Select(x => x[0]).OrderBy(x => x).ToArray();
        list_right = data.Select(x => x[1]).OrderBy(x => x).ToArray();
    }

    public int Part1() => list_left
            .Zip(list_right)
            .Select(x => Math.Abs(x.First - x.Second))
            .Sum();

    public int Part2()
    {
        int[] counts = new int[Math.Max(list_right.Max(), list_left.Max())+1];
        foreach (var v in list_right) counts[v]++;
        return list_left.Select(l => l * counts[l]).Sum();
    }
}
