using System.Runtime.InteropServices;

namespace AdventOfCode2024;

internal class Day18
{
    (int x, int y)[] corr;
    bool[,] map;

    public Day18()
    {
        corr = new StreamReader("inputs/day18.txt")
            .ReadToEnd()
            .Split('\n')
            .Select(l => (x: int.Parse(l.Split(',')[0]), y: int.Parse(l.Split(',')[1])))
            .ToArray();
        map = new bool[71, 71];
    }

    static int MinSteps(bool[,] obst, int cx, int cy, int tx, int ty)
    {
        PriorityQueue<(int x, int y), int> q = new();
        Dictionary<(int x, int y), int> steps = [];
        q.Enqueue((cx, cy), 0);
        steps[(cx, cy)] = 0;
        while (q.TryDequeue(out (int x, int y) d, out int p))
        {
            if (d == (tx, ty)) return p;
            (int x, int y)[] next = [
                (d.x + 1, d.y),
                (d.x - 1, d.y),
                (d.x, d.y + 1),
                (d.x, d.y - 1)
            ];
            foreach (var e in next.Where(f => f.x >= 0 && f.x < 71 && f.y >= 0 && f.y < 71 && !obst[f.x, f.y]))
            {
                if (!steps.TryGetValue(e, out int s) || s > p + 1)
                {
                    q.Enqueue(e, p + 1);
                    steps[e] = p + 1;
                }
            }
        }
        return -1;
    }

    public int Part1()
    {
        foreach (var (x, y) in corr[..^1024])
            map[x, y] = true;
        return MinSteps(map, 0, 0, 70, 70);
    }

    public string Part2()
    {
        for (int i = 1024; i < corr.Length; i++)
        {
            map[corr[i].x, corr[i].y] = true;
            if (MinSteps(map, 0, 0, 70, 70) == -1)
                return $"{corr[i].x},{corr[i].y}";
        }
        return "0,0";
    }
}
