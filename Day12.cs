using System.Data;

namespace AdventOfCode2024;

internal class Day12
{
    string[] lines;
    int w, h;

    public Day12()
    {
        lines = new StreamReader("inputs/day12.txt").ReadToEnd().Replace("\r", "").Split('\n');
        w = lines[0].Length;
        h = lines.Length;
    }

    public int Part1()
    {
        bool[,] patches = new bool[w, h];
        int agg = 0;
        for (int y = 0; y < h; y++)
        {
            for (int x = 0; x < w; x++)
            {
                if (!patches[x, y])
                {
                    char c = lines[y][x];
                    (int p, int a, bool edge) Crawl(int x, int y)
                    {
                        if (x < 0 || x >= w || y < 0 || y >= h || lines[y][x] != c)
                            return (0, 0, true); // edge
                        if (patches[x, y])
                            return (0, 0, false); // backtracked
                        patches[x, y] = true;
                        (int p, int a, bool edge)[] n = [
                            Crawl(x, y - 1),
                            Crawl(x + 1, y),
                            Crawl(x, y + 1),
                            Crawl(x - 1, y),
                        ];
                        return (
                            n.Sum(x => x.edge ? 1 : x.p),
                            1 + n.Sum(x => x.a),
                            false
                        );
                    }
                    (int p, int a, _) = Crawl(x, y);
                    agg += p * a;
                }
            }
        }

        return agg;
    }

    public int Part2()
    {
        bool[,] patches = new bool[w, h];
        int agg = 0;
        for (int y = 0; y < h; y++)
        {
            for (int x = 0; x < w; x++)
            {
                if (!patches[x, y])
                {
                    char c = lines[y][x];
                    (int e, int a, bool edge) Crawl(int x, int y)
                    {
                        if (x < 0 || x >= w || y < 0 || y >= h || lines[y][x] != c)
                            return (0, 0, true); // edge
                        if (patches[x, y])
                            return (0, 0, false); // backtracked
                        patches[x, y] = true;
                        (int e, int a, bool edge)[] n = [
                            Crawl(x, y - 1),
                            Crawl(x + 1, y),
                            Crawl(x, y + 1),
                            Crawl(x - 1, y),
                        ];

                        bool[] diags = [
                            y - 1 >= 0 && x + 1 < w && lines[y - 1][x + 1] != c,
                            y + 1 < h && x + 1 < w && lines[y + 1][x + 1] != c,
                            y + 1 < h && x - 1 >= 0 && lines[y + 1][x - 1] != c,
                            y - 1 >= 0 && x - 1 >= 0 && lines[y - 1][x - 1] != c,
                        ];
                        int corners = 0;
                        for (int i = 0; i < 4; i++)
                        {
                            bool both = n[i].edge == n[(i + 1) % 4].edge;
                            if (both && (n[i].edge || diags[i])) corners++;
                        }

                        return (
                            n.Sum(x => x.e) + corners,
                            1 + n.Sum(x => x.a),
                            false
                        );
                    }
                    (int e, int a, _) = Crawl(x, y);

                    agg += e * a;
                }
            }
        }

        return agg;
    }
}
