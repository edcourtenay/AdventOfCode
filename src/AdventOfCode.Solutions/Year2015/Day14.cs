using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2015;

[Description("Reindeer Olympics")]
[SuppressMessage("ReSharper", "UnusedType.Global")]
public partial class Day14 : IPuzzle
{
    private readonly Regex _parseRegex = ParseRegex();
    
    public object Part1(string input)
    {
        Reindeer[] reindeer = Data(input).ToArray();

        foreach (Reindeer r in reindeer)
        {
            r.Tick(2503);
        }

        Reindeer winner = reindeer.OrderBy(r => r.DistanceTravelled).Last();
        return winner.DistanceTravelled;
    }

    public object Part2(string input)
    {
        Reindeer[] reindeer = Data(input).ToArray();

        for (int i = 0; i < 2503; i++)
        {
            foreach (Reindeer r in reindeer)
            {
                r.Tick();
            }

            int max = reindeer.Max(r => r.DistanceTravelled);

            foreach (Reindeer r in reindeer.Where(r => r.DistanceTravelled == max))
            {
                r.Points++;
            }
        }

        Reindeer winner = reindeer.OrderBy(r => r.Points).Last();
        return winner.Points;
    }
    
    private IEnumerable<Reindeer> Data(string input)
    {
        var reader = new StringReader(input);
        while (reader.ReadLine() is { } line)
        {
            if (_parseRegex.Match(line) is { Success: true } match)
            {
                var name = match.Groups["reindeer"].Value;
                var speed = int.Parse(match.Groups["speed"].Value);
                var flyTime = int.Parse(match.Groups["flyTime"].Value);
                var restTime = int.Parse(match.Groups["restTime"].Value);

                yield return new Reindeer(name, speed, flyTime, restTime);
            }
        }
    }

    public class Reindeer
    {
        private readonly Queue<State> _queue;
        public string Name { get; }
        public int Speed { get; }
        public int FlyTime { get; }
        public int RestTime { get; }
        public int DistanceTravelled { get; private set; }
        public int Points { get; set; }
        public Reindeer(string name, int speed, int flyTime, int restTime)
        {
            Name = name;
            Speed = speed;
            FlyTime = flyTime;
            RestTime = restTime;

            var flying = Enumerable.Range(1, flyTime)
                .Select(_ => State.Flying);

            var resting = Enumerable.Range(1, restTime)
                .Select(_ => State.Resting);
            
            _queue = new Queue<State>(flying.Concat(resting));
        }

        public void Tick(int iterations = 1)
        {
            for (int i = 0; i < iterations; i++)
            {
                State state = _queue.Dequeue();
                _queue.Enqueue(state);

                if (state == State.Flying)
                {
                    DistanceTravelled += Speed;
                }
            }
        }
    }

    public enum State
    {
        Flying,
        Resting
    }

    [GeneratedRegex(@"^(?<reindeer>\w+) can fly (?<speed>\d+) km\/s for (?<flyTime>\d+) seconds, but then must rest for (?<restTime>\d+) seconds\.$", RegexOptions.Compiled)]
    private static partial Regex ParseRegex();
}