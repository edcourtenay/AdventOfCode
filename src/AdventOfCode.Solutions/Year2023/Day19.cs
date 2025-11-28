using System.Linq.Expressions;

namespace AdventOfCode.Solutions.Year2023;

[Description("Aplenty")]
public sealed class Day19 : IPuzzle
{
    public object Part1(string input)
    {
        (List<Workflow> workflows, List<Part> parts) = ParseData(input);

        ParameterExpression[] parameterExpressions =
        [
            Expression.Parameter(typeof(int), "x"), Expression.Parameter(typeof(int), "m"),
            Expression.Parameter(typeof(int), "a"), Expression.Parameter(typeof(int), "s")
        ];

        Expression inExpression = BuildExpressionFrom("in", workflows, parameterExpressions);

        var expr = Expression.Lambda<Func<int, int, int, int, bool>>(inExpression, false,
            parameterExpressions);

        Func<int,int,int,int,bool> func = expr.Compile();

        return parts
            .Where(part => func(part.X, part.M, part.A, part.S))
            .Sum(data => data.X + data.M + data.A + data.S);
    }

    public object Part2(string input)
    {
        (List<Workflow> workflows, _) = ParseData(input);

        ParameterExpression[] parameterExpressions =
        [
            Expression.Parameter(typeof(int), "x"), Expression.Parameter(typeof(int), "m"),
            Expression.Parameter(typeof(int), "a"), Expression.Parameter(typeof(int), "s")
        ];

        Expression inExpression = BuildExpressionFrom("in", workflows, parameterExpressions);

        var r= WalkExpression(inExpression);

        return r;
    }

    private static Expression BuildExpressionFrom(string start, IEnumerable<Workflow> workflows,
        ParameterExpression[] parameterExpressions)
    {
        var dict = workflows.ToDictionary(workflow => workflow.Name, workflow => workflow.Rules);
        Dictionary<string, Expression> expressions = new()
        {
            { "A", Expression.Constant(true) },
            { "R", Expression.Constant(false) },
            { "x", parameterExpressions.First(p => p.Name == "x") },
            { "m", parameterExpressions.First(p => p.Name == "m") },
            { "a", parameterExpressions.First(p => p.Name == "a") },
            { "s", parameterExpressions.First(p => p.Name == "s") }
        };

        return BuildExpression(start, dict, expressions);
    }

    private static Expression BuildExpression(string key, IReadOnlyDictionary<string, List<Rule>> workflows,
        IDictionary<string, Expression> expressions)
    {
        if (expressions.TryGetValue(key, out var expression))
        {
            return expression;
        }

        Rule[] array = workflows[key].ToArray();
        IEnumerable<Rule> rules = ((IEnumerable<Rule>)array).Reverse();
        Expression chain = Expression.Constant(false);
        foreach (Rule rule in rules)
        {
            chain = rule switch
            {
                { Condition: null, Result: var r } => BuildExpression(r, workflows, expressions),
                { Condition: { } condition } => Expression.Condition(
                    Expression.MakeBinary(condition.Operator switch
                        {
                            '<' => ExpressionType.LessThan,
                            '>' => ExpressionType.GreaterThan,
                            _ => throw new ArgumentOutOfRangeException()
                        }, BuildExpression(condition.Parameter, workflows, expressions),
                        Expression.Constant(condition.Value)),
                    BuildExpression(rule.Result, workflows, expressions),
                    chain,
                    typeof(bool))
            };

            if (chain.CanReduce)
            {
                chain = chain.Reduce();
            }
        }

        expressions[key] = chain;
        return chain;
    }

    private static long WalkExpression(Expression expression, Stack<Expression>? stack = null)
    {
        stack ??= new Stack<Expression>();

        long result = 0L;
        switch (expression)
        {
            case ConstantExpression { Value: bool accepted }:
                return accepted ? CalculateCombinations(stack) : 0;
            case ConditionalExpression binaryExpression:
                stack.Push(binaryExpression.Test);
                result += WalkExpression(binaryExpression.IfTrue, stack);
                stack.Pop();

                stack.Push(InvertTest(binaryExpression.Test));
                result += WalkExpression(binaryExpression.IfFalse, stack);
                stack.Pop();
                break;
        }
        return result;
    }

    private static long CalculateCombinations(IEnumerable<Expression> expressions)
    {
        var dict = "xmas".ToDictionary(c => c.ToString(), _ => (from: 1, to: 4000));

        foreach (var expression in expressions)
        {
            switch (expression)
            {
                case BinaryExpression { Left: ParameterExpression { Name: {} name }, Right: ConstantExpression { Value: int value } } binary:
                    {
                        var range = dict[name];

                        range = binary.NodeType switch
                        {
                            ExpressionType.GreaterThan => (Math.Max(range.from, value + 1), range.to),
                            ExpressionType.LessThan => (range.from, Math.Min(range.to, value - 1)),
                            _ => range
                        };

                        dict[name] = range;
                        break;
                    }
            }
        }

        return dict.Values.Aggregate<(int from, int to), long>(1, (acc, cur) => acc * cur.RangeLength());
    }

    private static Expression InvertTest(Expression binaryExpressionTest)
    {
        return binaryExpressionTest switch
        {
            BinaryExpression
            {
                NodeType: var nodeType, Left: var left, Right: ConstantExpression { Value: int right }
            } => nodeType switch
            {
                ExpressionType.LessThan => Expression.GreaterThan(left, Expression.Constant(right - 1)),
                ExpressionType.GreaterThan => Expression.LessThan(left, Expression.Constant(right + 1)),
                _ => binaryExpressionTest
            },
            _ => binaryExpressionTest
        };
    }

    private static (List<Workflow> Workflows, List<Part> Input) ParseData(string input)
    {
        var workflows = new List<Workflow>();
        var inputs = new List<Part>();
        var blankHit = false;
        foreach (string line in input.ToLines())
        {
            if (string.IsNullOrEmpty(line))
            {
                blankHit = true;
                continue;
            }

            if (!blankHit)
            {
                workflows.Add(ParseWorkflow(line));
            }
            else
            {
                inputs.Add(ParseInput(line));
            }
        }

        return (workflows, inputs);
    }

    private static Part ParseInput(string line)
    {
        var dict = line[1..^1].Split(',')
            .ToDictionary(s => s[..1], s => int.Parse(s[2..]));

        return new Part(dict["x"], dict["m"], dict["a"], dict["s"]);
    }

    private static Workflow ParseWorkflow(string input)
    {
        var span = input.AsSpan();
        var openingBrace = span.IndexOf('{');

        var name = span[..openingBrace].ToString();
        var rules = new List<Rule>();

        var start = openingBrace + 1;
        for (int i = start; i < span.Length; i++)
        {
            if (span[i] is not ('}' or ','))
            {
                continue;
            }

            rules.Add(ParseRule(span[start..i]));
            start = i + 1;
        }

        if (rules.DistinctBy(rule => rule.Result).Count() == 1)
        {
            rules = [new Rule(null, rules[0].Result)];
        }

        return new Workflow(name, rules);
    }

    private static Rule ParseRule(ReadOnlySpan<char> input)
    {
        var colon = input.IndexOf(':');
        if (colon == -1)
        {
            return new Rule(null, input.ToString());
        }

        var condition = new Condition(input[0].ToString(), input[1], int.Parse(input[2..colon].ToString()));
        return new Rule(condition, input[(colon + 1)..].ToString());
    }

    private record Condition(string Parameter, char Operator, int Value);

    private record Rule(Condition? Condition, string Result);

    private record Workflow(string Name, List<Rule> Rules);

    private record struct Part(int X, int M, int A, int S);
}