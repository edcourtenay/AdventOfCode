using Microsoft.Z3;

namespace AdventOfCode.Solutions.Year2025;

public sealed class Day10 : IPuzzle
{
    public object Part1(string input)
    {
        var machines = input.ToLines(Machine.Parse);
        int totalSum = 0;

        foreach (var machine in machines)
        {
            var target = machine.LightMask;
            // Pre-allocate with reasonable capacity based on typical state space
            var visited = new HashSet<int>(capacity: 1024);
            var queue = new Queue<(int Mask, int Presses)>(capacity: 256);
            queue.Enqueue((0, 0));

            while (queue.Count > 0)
            {
                var (mask, presses) = queue.Dequeue();
                if (mask == target)
                {
                    totalSum += presses;
                    break;
                }

                foreach (var buttonMask in machine.ButtonMasks)
                {
                    var nextMask = mask ^ buttonMask;
                    if (visited.Add(nextMask))
                    {
                        queue.Enqueue((nextMask, presses + 1));
                    }
                }
            }
        }

        return totalSum;
    }
    
    public object Part2(string input)
    {
        var machines = input.ToLines(Machine.Parse);
        int totalSum = 0;

        foreach (var machine in machines)
        {
            using var ctx = new Context();
            var opt = ctx.MkOptimize();
            var buttonVars = new IntExpr[machine.Buttons.Count];

            // Add constraints: each button press count >= 0
            for (var b = 0; b < machine.Buttons.Count; b++)
            {
                buttonVars[b] = ctx.MkIntConst($"button_{b}");
                opt.Add(ctx.MkGe(buttonVars[b], ctx.MkInt(0)));
            }

            // Add constraints: sum of button effects on each joltage equals target
            for (var j = 0; j < machine.Joltage.Length; j++)
            {
                var terms = new List<ArithExpr>(machine.Buttons.Count);
                for (var b = 0; b < machine.Buttons.Count; b++)
                {
                    if (machine.ButtonAffectsJoltage[b].Contains(j))
                    {
                        terms.Add(buttonVars[b]);
                    }
                }

                if (terms.Count > 0)
                {
                    opt.Add(ctx.MkEq(ctx.MkAdd(terms.ToArray()), ctx.MkInt(machine.Joltage[j])));
                }
            }

            // Minimize total button presses
            opt.MkMinimize(ctx.MkAdd(buttonVars.Cast<ArithExpr>().ToArray()));
            opt.Check();

            // Calculate total presses from model
            var model = opt.Model;
            for (var i = 0; i < buttonVars.Length; i++)
            {
                if (model.Evaluate(buttonVars[i]) is IntNum num)
                {
                    totalSum += num.Int;
                }
            }
        }

        return totalSum;
    }
    
    public record Machine(string Lights, List<int[]> Buttons, int[] Joltage)
    {
        // Compute light mask efficiently using bitwise operations
        public int LightMask { get; } = ComputeLightMask(Lights);
    
        // Pre-compute button masks for XOR operations in Part1
        public int[] ButtonMasks { get; } = ComputeButtonMasks(Buttons);
        
        // Pre-compute which buttons affect which joltage indices for faster lookups in Part2
        public HashSet<int>[] ButtonAffectsJoltage { get; } = Buttons
            .Select(button => new HashSet<int>(button))
            .ToArray();

        private static int ComputeLightMask(string lights)
        {
            int mask = 0;
            for (int i = 0; i < lights.Length; i++)
            {
                if (lights[i] == '#')
                {
                    mask |= 1 << i;
                }
            }
            return mask;
        }

        private static int[] ComputeButtonMasks(List<int[]> buttons)
        {
            var masks = new int[buttons.Count];
            for (int b = 0; b < buttons.Count; b++)
            {
                int mask = 0;
                foreach (var index in buttons[b])
                {
                    mask |= 1 << index;
                }
                masks[b] = mask;
            }
            return masks;
        }

        public static Machine Parse(string line)
        {
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            
            var lights = parts[0][1..^1];
            
            var buttons = new List<int[]>(parts.Length - 2);
            for (int i = 1; i < parts.Length - 1; i++)
            {
                var buttonStr = parts[i][1..^1];
                var indices = Array.ConvertAll(buttonStr.Split(','), int.Parse);
                buttons.Add(indices);
            }
            
            var joltageStr = parts[^1][1..^1];
            var joltage = Array.ConvertAll(joltageStr.Split(','), int.Parse);

            return new Machine(lights, buttons, joltage);
        }
    }
}