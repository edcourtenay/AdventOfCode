namespace AdventOfCode.Year2023;

[Description("Pulse Propagation")]
public sealed class Day20 : IPuzzle
{
    private static readonly Signal ButtonTrigger = new("", "button", false);
    private static readonly string[] LineSeparator = {"->", ","};

    public object Part1(string input)
    {
        var modules = ParseInput(input);
        (long low, long high) = CountPulses(modules, 1000);
        return low * high;
    }

    public object Part2(string input)
    {
        var modules = ParseInput(input);
        return CalculateButtonPresses(modules, "rx");
    }

    private static Dictionary<string, Module> ParseInput(string input)
    {
        List<Module> modules = [..input.ToLines(ParseModule), new Button(), new RxModule()];

        var map = modules.ToDictionary(m => m.Name);

        foreach (var (key, module) in map)
        {
            foreach (var target in module.Targets)
            {
                if (map.TryGetValue(target, out var value))
                {
                    value.RegisterSource(key);
                }
            }
        }

        return map;
    }

    private static Module ParseModule(string line) =>
        line.Split(LineSeparator, StringSplitOptions.TrimEntries) switch
        {
            [['%', .. { } name], .. var targets] => new FlipFlop(name, targets),
            [['&', .. { } name], .. var targets] => new Conjunction(name, targets),
            ["broadcaster", .. var targets] => new Broadcaster(targets),
            _ => throw new ArgumentOutOfRangeException(nameof(line))
        };

    private static (long low, long high) CountPulses(Dictionary<string, Module> modules, int iterations)
    {
        long low = 0;
        long high = 0;

        while (iterations-- > 0)
        {
            Queue<Signal> signals = new([ButtonTrigger]);

            while (signals.Count > 0)
            {
                var next = signals.Dequeue();
                if (!modules.TryGetValue(next.Target, out var module))
                {
                    continue;
                }

                foreach (var signal in module.Pulse(next.Source, next.Pulse))
                {
                    high += signal.Pulse ? 1 : 0;
                    low += signal.Pulse ? 0 : 1;
                    signals.Enqueue(signal);
                }
            }
        }

        return (low, high);
    }

    private static long CalculateButtonPresses(IReadOnlyDictionary<string, Module> modules, string target)
    {
        var targetModule = modules[modules[target].Sources[0]];
        Dictionary<string, long> watchlist = targetModule.Sources.ToDictionary(s => s, _ => 0L);

        var iteration = 0;
        while (watchlist.Values.Any(l => l == 0))
        {
            iteration++;
            Queue<Signal> signals = new([ButtonTrigger]);

            while (signals.TryDequeue(out var signal))
            {
                if (!modules.TryGetValue(signal.Target, out var module))
                {
                    continue;
                }

                foreach (var triggered in module.Pulse(signal.Source, signal.Pulse))
                {
                    signals.Enqueue(triggered);

                    if (signal.Pulse && watchlist.ContainsKey(signal.Source) && signal.Target == targetModule.Name)
                    {
                        watchlist[signal.Source] = iteration;
                    }
                }
            }
        }

        return watchlist.Values.LeastCommonMultiple();
    }

    private record struct Signal(string Source, string Target, bool Pulse);

    private abstract record Module(string Name, string[] Targets)
    {
        private readonly List<string> _sources = [];

        public IReadOnlyList<string> Sources => _sources;

        public virtual IEnumerable<Signal> Pulse(string source, bool pulse) =>
            Targets.Select(target => new Signal(Name, target, pulse));

        public virtual void RegisterSource(string source)
        {
            _sources.Add(source);
        }
    }

    private sealed record RxModule() : Module("rx", []);

    private sealed record Button() : Module("button", ["broadcaster"]);

    private sealed record FlipFlop(string Name, string[] Targets) : Module(Name, Targets)
    {
        private bool _lastIsLow = true;

        public override IEnumerable<Signal> Pulse(string source, bool pulse) =>
            pulse switch
            {
                false => base.Pulse(source, !(_lastIsLow = !_lastIsLow)),
                _ => []
            };
    }

    private sealed record Conjunction(string Name, string[] Targets) : Module(Name, Targets)
    {
        private readonly Dictionary<string, bool> _sourceStates = [];

        public override void RegisterSource(string name)
        {
            _sourceStates.Add(name, false);
            base.RegisterSource(name);
        }

        public override IEnumerable<Signal> Pulse(string source, bool pulse)
        {
            _sourceStates[source] = pulse;
            return base.Pulse(source, _sourceStates.Values.Any(lastPulse => !lastPulse));
        }
    }

    private sealed record Broadcaster(string[] Targets) : Module("broadcaster", Targets);
}