using System.Reflection;

namespace AdventOfCode2024;

internal class Day8
{
    string cInput;
    int w, h;
    static readonly char[] letters = [
        '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
    ];
    Dictionary<char, List<(int x, int y)>> antennae = [];

    public Day8()
    {
        string input = new StreamReader("inputs/day8.txt").ReadToEnd();
        cInput = input.Replace("\r\n", "");
        var lines = input.Replace("\r", "").Split('\n');
        w = lines[0].Length;
        h = lines.Length;

        for (int i = 0; i < cInput.Length; i++)
        {
            if (cInput[i] != '.')
            {
                if (!antennae.ContainsKey(cInput[i]))
                    antennae[cInput[i]] = [];
                antennae[cInput[i]].Add(Math.DivRem(i, w));
            }
        }
    }

    public int Part1()
    {
        bool[,] antinodes = new bool[w, h];
        
        foreach (var letter in letters)
        {
            var antennae = this.antennae[letter];
            for (int i = 0; i < antennae.Count - 1; i++)
            {
                for (int j = i + 1; j < antennae.Count; j++)
                {
                    (int x1, int y1) = (
                        2 * antennae[i].x - antennae[j].x,
                        2 * antennae[i].y - antennae[j].y
                    );
                    (int x2, int y2) = (
                        2 * antennae[j].x - antennae[i].x,
                        2 * antennae[j].y - antennae[i].y
                    );
                    if (x1 >= 0 && x1 < w && y1 >= 0 && y1 < h)
                        antinodes[x1, y1] = true;
                    if (x2 >= 0 && x2 < w && y2 >= 0 && y2 < h)
                        antinodes[x2, y2] = true;
                }
            }
        }
        return antinodes.Cast<bool>().Where(x => x).Count();
    }

    public int Part2()
    {
        bool[,] antinodes = new bool[w, h];
        int m = Math.Max(w, h);
        foreach (var letter in letters)
        {
            var antennae = this.antennae[letter];
            for (int i = 0; i < antennae.Count - 1; i++)
            {
                for (int j = i + 1; j < antennae.Count; j++)
                {
                    (int dx, int dy) = (
                        antennae[i].x - antennae[j].x,
                        antennae[i].y - antennae[j].y
                    );
                    for (int d = 0; d < m; d++)
                    {
                        (int x1, int y1) = (
                            antennae[i].x + d * dx,
                            antennae[i].y + d * dy
                        );
                        (int x2, int y2) = (
                            antennae[j].x - d * dx,
                            antennae[j].y - d * dy
                        );
                        bool ib1 = x1 >= 0 && x1 < w && y1 >= 0 && y1 < h;
                        bool ib2 = x2 >= 0 && x2 < w && y2 >= 0 && y2 < h;
                        if (ib1) antinodes[x1, y1] = true;
                        if (ib2) antinodes[x2, y2] = true;
                        if (!ib1 && !ib2) break;
                    }
                }
            }
        }
        return antinodes.Cast<bool>().Where(x => x).Count();
    }
}
