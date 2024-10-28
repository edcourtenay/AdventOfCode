namespace AdventOfCode.Year2015;

[Description("Like a GIF For Your Yard")]
public class Day18 : IPuzzle
{
    public object Part1(string input) => CountLights(input, grid => grid.PartTick());

    public object Part2(string input) => CountLights(input, grid => grid.Part2Tick());

    private object CountLights(string input, Action<Grid> action)
    {
        var grid = Parse(input);

        for (int i = 0; i < 100; i++)
        {
            action(grid);
        }

        return grid.CountLights();
    }

    public Grid Parse(string input)
    {
        var grid = new Grid(100, 100);

        using IEnumerator<char> enumerator = Data(input).GetEnumerator();
        for (int y = 0; y < 100; y++)
        {
            for (int x = 0; x < 100; x++)
            {
                enumerator.MoveNext();
                grid.SetAt(x, y, false, enumerator.Current == '#');
            }
        }

        return grid;
    }
    
    private IEnumerable<char> Data(string input)
    {
        IEnumerable<string> ReadData()
        {
            var reader = new StringReader(input);
            while (reader.ReadLine() is { } line)
            {
                yield return line;
            }
        }
        return ReadData().SelectMany(s => s.ToCharArray());
    }

    public class Grid
    {
        private readonly int _width;
        private readonly int _height;
        private readonly bool[][] _grid;
        private int _current;
        private int _buffer;

        public Grid(int width, int height)
        {
            _width = width;
            _height = height;

            _current = 0;
            _buffer = 1;

            _grid =
            [
                new bool[_width * _height],
                new bool[_width * _height]
            ];
        }

        public int Index(int x, int y) => y * _width + x;
    
        public IEnumerable<int> Neighbours(int x, int y)
        {
            for (int cy = y - 1; cy <= y + 1; cy++)
            {
                for (int cx = x - 1; cx <= x + 1; cx++)
                {
                    if (cx < 0 || cy < 0 || cx >= _width || cy >= _height || (cx == x && cy == y))
                    {
                        continue;
                    }

                    yield return Index(cx, cy);
                }
            }
        }

        public void Switch()
        {
            (_current, _buffer) = (_buffer, _current);
        }

        public void SetAt(int x, int y, bool buffer, bool value)
        {
            _grid[buffer ? _buffer : _current][Index(x, y)] = value;
        }

        public bool GetAt(int x, int y, bool buffer)
        {
            return _grid[buffer ? _buffer : _current][Index(x, y)];
        }

        public void PartTick()
        {
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    var neighbours = Neighbours(x, y).Select(i => _grid[_current][i]).ToArray();
                    var cur = _grid[_current][Index(x, y)];
                    _grid[_buffer][Index(x, y)] = cur switch
                    {
                        true => neighbours.Count(b => b) is 2 or 3,
                        false => neighbours.Count(b => b) is 3
                    };
                }
            }
            
            Switch();
        }

        public void Part2Tick()
        {
            void SetCorners()
            {
                _grid[_current][Index(0, 0)] = true;
                _grid[_current][Index(_width - 1, 0)] = true;
                _grid[_current][Index(0, _height - 1)] = true;
                _grid[_current][Index(_width - 1, _height - 1)] = true;
            }

            SetCorners();
            PartTick();
            SetCorners();
        }

        public int CountLights()
        {
            return _grid[_current].Count(b => b);
        }
    }
}