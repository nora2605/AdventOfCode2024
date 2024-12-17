namespace AdventOfCode2024;

#pragma warning disable CS8524
internal class Day15
{
    Tile[,] p1_map;
    WTile[,] p2_map;
    int w, h;
    int p1x, p1y;
    int p2x, p2y;
    Direction[] sequence;

    public Day15()
    {
        var p = new StreamReader("inputs/day15.txt").ReadToEnd().Replace("\r", "").Split("\n\n");
        var lines = p[0].Split('\n');

        w = lines[0].Length;
        h = lines.Length;

        (p1y, p1x) = Math.DivRem(p[0].Replace("\n", "").IndexOf('@'), w);
        p2x = 2 * p1x;
        p2y = p1y;

        p1_map = new Tile[w, h];
        p2_map = new WTile[2 * w, h];
        for (int y = 0; y < h; y++)
        {
            for (int x = 0; x < w; x++)
            {
                p1_map[x, y] = lines[y][x] switch
                {
                    '#' => Tile.Wall,
                    'O' => Tile.Box,
                    '@' => Tile.Robot,
                    _ => Tile.Empty,
                };
                (p2_map[2*x, y], p2_map[2*x + 1, y]) = lines[y][x] switch
                {
                    '#' => (WTile.Wall, WTile.Wall),
                    'O' => (WTile.LBox, WTile.RBox),
                    '@' => (WTile.Robot, WTile.Empty),
                    _ => (WTile.Empty, WTile.Empty),
                };
            }
        }

        sequence = p[1].Replace("\n", "").Select(c => c switch
        {
            '<' => Direction.Left,
            'v' => Direction.Down,
            '>' => Direction.Right,
            _ => Direction.Up,
        }).ToArray();
    }

    static bool ExecuteMove(Tile[,] map, int x, int y, Direction dir)
    {
        (int x, int y) n = dir switch
        {
            Direction.Up => (x, y - 1),
            Direction.Right => (x + 1, y),
            Direction.Down => (x, y + 1),
            Direction.Left => (x - 1, y)
        };
        if (map[n.x, n.y] switch
        {
            Tile.Wall => false,
            Tile.Empty => true,
            _ => ExecuteMove(map, n.x, n.y, dir)
        }) {
            map[n.x, n.y] = map[x, y];
            map[x, y] = Tile.Empty;
            return true;
        }
        return false;
    }

    static bool TileFreeableInDir(WTile[,] map, int x, int y, Direction dir, bool otherpart=false)
    {
        (int x, int y) n = dir switch
        {
            Direction.Up => (x, y - 1),
            Direction.Right => (x + 1, y),
            Direction.Down => (x, y + 1),
            Direction.Left => (x - 1, y)
        };

        WTile current = map[x, y];

        if (current == WTile.Empty) return true;
        if (current == WTile.Wall) return false;

        WTile next = map[n.x, n.y];

        if (current == WTile.Robot) return TileFreeableInDir(map, n.x, n.y, dir);

        // we're a box part
        // same logic for left/right as before
        if (y == n.y) return TileFreeableInDir(map, n.x, n.y, dir);
        // check for both box parts if up/down is possible, or just for ourselves if we're the otherpart
        int o = current == WTile.LBox ? 1 : -1;
        return TileFreeableInDir(map, n.x, n.y, dir) &&
            (otherpart || TileFreeableInDir(map, x + o, y, dir, true));
    }

    static bool ExecuteWMove(WTile[,] map, int x, int y, Direction dir, bool c = false)
    {
        (int x, int y) n = dir switch
        {
            Direction.Up => (x, y - 1),
            Direction.Right => (x + 1, y),
            Direction.Down => (x, y + 1),
            Direction.Left => (x - 1, y)
        };
        if (c || TileFreeableInDir(map, x, y, dir))
        {
            WTile current = map[x, y];
            if (current == WTile.Empty) return true;
            if (current == WTile.Wall) throw new NotImplementedException("Huh?");
            WTile next = map[n.x, n.y];
            if (next != WTile.Empty)
                ExecuteWMove(map, n.x, n.y, dir, true);
            if (current != WTile.Robot && y != n.y)
            {
                var o = current == WTile.LBox ? 1 : -1;
                ExecuteWMove(map, n.x + o, n.y, dir, true);
                map[n.x + o, n.y] = map[x + o, y];
                map[x + o, y] = WTile.Empty;
            }
            map[n.x, n.y] = map[x, y];
            map[x, y] = WTile.Empty;
            return true;
        }
        return false;
    }

    public int Part1()
    {
        foreach (Direction dir in sequence)
            (p1x, p1y) = ExecuteMove(p1_map, p1x, p1y, dir) ?
                dir switch {
                    Direction.Up => (p1x, p1y - 1),
                    Direction.Right => (p1x + 1, p1y),
                    Direction.Down => (p1x, p1y + 1),
                    Direction.Left => (p1x - 1, p1y)
                } :
                (p1x, p1y);
        int agg = 0;
        for (int y = 0; y < h; y++)
            for (int x = 0; x < w; x++)
                if (p1_map[x, y] == Tile.Box) agg += y * 100 + x;
        return agg;
    }

    public int Part2()
    {
        foreach (Direction dir in sequence)
        {
            (p2x, p2y) = ExecuteWMove(p2_map, p2x, p2y, dir) ?
                dir switch
                {
                    Direction.Up => (p2x, p2y - 1),
                    Direction.Right => (p2x + 1, p2y),
                    Direction.Down => (p2x, p2y + 1),
                    Direction.Left => (p2x - 1, p2y)
                } :
                (p2x, p2y);
        }
        int agg = 0;
        for (int y = 0; y < h; y++)
            for (int x = 0; x < 2*w; x++)
                if (p2_map[x, y] == WTile.LBox) agg += y * 100 + x;
        return agg;
    }
    internal enum Tile : byte
    {
        Empty,
        Wall,
        Box,
        Robot
    }

    internal enum WTile : byte
    {
        Empty,
        Wall,
        LBox,
        RBox,
        Robot
    }

    internal enum Direction : byte
    {
        Up,
        Down,
        Left,
        Right
    }
}

