namespace AdventOfCode2024;

#pragma warning disable CS8524
internal class Day16
{
    char[,] map;
    int w, h;

    Dictionary<State, int>? distances;

    State start;
    State end;

    public Day16()
    {
        string input = new StreamReader("inputs/day16.txt").ReadToEnd().Replace("\r", "");
        string[] lines = input.Split('\n');
        string cInput = input.Replace("\n", "");
        w = lines[0].Length;
        h = lines.Length;
        map = new char[w, h];
        for (int y = 0; y < h; y++)
            for (int x = 0; x < w; x++)
                map[x, y] = lines[y][x];
        (int ey, int ex) = Math.DivRem(cInput.IndexOf('E'), w);
        (int sy, int sx) = Math.DivRem(cInput.IndexOf('S'), w);
        start = new State(sx, sy, Direction.Right);
        end = new State(ex, ey, Direction.Up);
    }

    Dictionary<State, int> RevDijkstra(State endState)
    {
        PriorityQueue<State, int> q = new();
        Dictionary<State, int> dists = [];
        for (byte i = 0; i < 4; i++)
        {
            q.Enqueue(endState with { dir = (Direction)i }, 0);
            dists[endState with { dir = (Direction)i }] = 0;
        }

        while (q.TryDequeue(out State s, out int p))
        {
            foreach (var t in s.NextStates(map, false))
            {
                int d = p + t.d;
                if (d < dists.GetValueOrDefault(t.s, int.MaxValue))
                {
                    q.Remove(t.s, out _, out _);
                    dists[t.s] = d;
                    q.Enqueue(t.s, d);
                }
            }
        }

        return dists;
    }

    public int Part1()
    {
        distances = RevDijkstra(end);
        return distances[start];
    }

    public int Part2()
    {
        PriorityQueue<State, int> q = new();
        HashSet<State> spots = [start];
        q.Enqueue(start, distances![start]);
        while (q.TryDequeue(out State s, out int p))
        {
            foreach (var t in s.NextStates(map, true))
            {
                var d = p - t.d;
                if (!spots.Contains(t.s) && d == distances[t.s])
                {
                    spots.Add(t.s);
                    q.Enqueue(t.s, d);
                }
            }
        }
        return spots.DistinctBy(s => (s.x, s.y)).Count();
    }

    struct State(int x, int y, Direction dir)
    {
        public int x = x;
        public int y = y;
        public Direction dir = dir;

        State MoveBack() => new State(
            dir switch { Direction.Up or Direction.Down => x, Direction.Left => x + 1, Direction.Right => x - 1 },
            dir switch { Direction.Left or Direction.Right => y, Direction.Up => y + 1, Direction.Down => y - 1 },
            dir
        );
        State Move() => new State(
            dir switch { Direction.Up or Direction.Down => x, Direction.Left => x - 1, Direction.Right => x + 1 },
            dir switch { Direction.Left or Direction.Right => y, Direction.Up => y - 1, Direction.Down => y + 1 },
            dir
        );

        State Rotate(bool counterclockwise) => new State(
            x,
            y,
            counterclockwise ?
                (Direction)((byte)(dir + 3) % 4) :
                (Direction)((byte)(dir + 1) % 4)
        );

        bool Occupied(char[,] map) => map[x, y] == '#';

        public IEnumerable<(State s, int d)> NextStates(char[,] map, bool forward)
        {
            (State s, int d)[] n = [(forward ? MoveBack() : Move(), 1), (Rotate(true), 1000), (Rotate(false), 1000)];
            return n.Where(i => i.s.x < map.GetLength(0) && i.s.x >= 0 && i.s.y < map.GetLength(1) && i.s.y >= 0 && !i.s.Occupied(map));
        }

        public override string ToString()
        {
            return $"{x}, {y}, {dir}";
        }
    }

    internal enum Direction : byte
    {
        Right = 0,
        Down = 1,
        Left = 2,
        Up = 3
    }
}
