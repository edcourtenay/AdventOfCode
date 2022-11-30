using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2015;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[Description("Some Assembly Required")]
public class Day07 : IPuzzle
{
    private readonly Regex _lineRegex = new(@"(?<expression>.+)\s->\s(?<output>\w+)", RegexOptions.Compiled);
    private readonly Regex _constantRegex = new(@"^(?<constant>\d+)$", RegexOptions.Compiled);
    private readonly Regex _variableRegex = new(@"^(?<variable>\w+)$", RegexOptions.Compiled);
    private readonly Regex _notRegex = new(@"^NOT\s(?<left>\w+)$", RegexOptions.Compiled);
    private readonly Regex _binaryRegex = new(@"^(?<left>[a-z]+|\d+)\s(?<operation>[A-Z]+)\s(?<right>[a-z]+|\d+)$", RegexOptions.Compiled);

    public object Part1(string input)
    {
        var dictionary = Data(input);
        return EvaluateKey(dictionary, "a");
    }

    public object Part2(string input)
    {
        var dictionary = Data(input);
        dictionary["b"] = EvaluateKey(dictionary, "a").ToString();
        return EvaluateKey(dictionary, "a");
    }

    public IDictionary<string, string> Data(string input)
    {
        IEnumerable<KeyValuePair<string, string>> GetEntries()
        {
            using var r = new StringReader(input);

            while (r.ReadLine() is { } line)
            {
                if (_lineRegex.Match(line) is { Success: true } lineMatch)
                {
                    yield return new KeyValuePair<string, string>(lineMatch.Groups["output"].Value, lineMatch.Groups["expression"].Value);
                }
            }
        }

        return new Dictionary<string, string>(GetEntries());
    }

    private static int ExecuteExpression(Expression expression)
    {
        var le = Expression.Lambda<Func<Int32>>(expression);
        var compile = le.Compile();
        return compile();
    }

    public int EvaluateKey(IDictionary<string, string> dictionary, string key)
    {
        var cache = new Dictionary<string, Expression>();
        var expression = EvaluateKey(dictionary, cache, key);
        return ExecuteExpression(expression);
    }

    private Expression EvaluateKey(IDictionary<string, string> dictionary, IDictionary<string, Expression> cache, string key)
    {
        if (cache.TryGetValue(key, out var cachedExpression))
        {
            return cachedExpression;
        }
        
        Expression expression = GenerateExpression(dictionary, cache, key);

        if (expression is ConstantExpression)
        {
            return expression;
        }

        var constant = Expression.Constant(ExecuteExpression(expression));
        cache.Add(key, constant);
        
        return constant;
    }

    private Expression GenerateExpression(IDictionary<string, string> dictionary, IDictionary<string, Expression> cache,
        string key)
    {
        if (!dictionary.TryGetValue(key, out var fooData))
        {
            switch (_constantRegex.Match(key))
            {
                case { Success: true } constantMatch:
                    {
                        var value = Int32.Parse(constantMatch.Groups["constant"].Value);
                        return Expression.Constant(value);
                    }
                default:
                    throw new Exception($@"Cannot evaluate '{key}'");
            }
        }

        if (_variableRegex.Match(fooData) is { Success: true } variableMatch)
        {
            return EvaluateKey(dictionary, cache, variableMatch.Groups["variable"].Value);
        }

        if (_notRegex.Match(fooData) is { Success: true } notMatch)
        {
            Expression left = EvaluateKey(dictionary, cache, notMatch.Groups["left"].Value);
            return Expression.Not(left);
        }

        switch (_binaryRegex.Match(fooData))
        {
            case { Success: true } binaryMatch:
                {
                    Expression left = EvaluateKey(dictionary, cache, binaryMatch.Groups["left"].Value);
                    Expression right = EvaluateKey(dictionary, cache, binaryMatch.Groups["right"].Value);

                    return binaryMatch.Groups["operation"].Value switch
                    {
                        "AND" => Expression.And(left, right),
                        "OR" => Expression.Or(left, right),
                        "LSHIFT" => Expression.LeftShift(left, right),
                        "RSHIFT" => Expression.RightShift(left, right),
                        _ => throw new ArgumentOutOfRangeException()
                    };
                }
            default:
                throw new Exception("Failed");
        }
    }
}