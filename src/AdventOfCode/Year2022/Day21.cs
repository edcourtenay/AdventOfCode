using System.Linq.Expressions;

namespace AdventOfCode.Year2022;

[Description("Monkey Math")]
public class Day21 : IPuzzle
{
    private const string HumanKey = "humn";

    public object Part1(string input)
    {
        var dict = ParseInput(input);

        var expr = GetExpression("root", dict);

        var lambda = Expression.Lambda(expr);
        Func<long> func = (Func<long>)lambda.Compile();
        var i = func();

        return i;
    }

    public object Part2(string input)
    {
        var dict = ParseInput(input);

        ParameterExpression parameterExpression = Expression.Parameter(typeof(long),HumanKey);
        BinaryExpression expr = GetExpression("root", dict, parameterExpression) as BinaryExpression
                                ?? throw new InvalidOperationException();

        var solved = (expr.Left, expr.Right) switch
        {
            (ConstantExpression ce, { } e) => SolveForHuman(e, ce),
            ({ } e, ConstantExpression ce) => SolveForHuman(e, ce),
            _ => throw new ArgumentOutOfRangeException()
        };

        return ((Func<long>)Expression.Lambda(solved).Compile())();
    }

    private static Dictionary<string, (string val, Expression? exp)> ParseInput(string input)
    {
        return input.ToLines()
            .ToDictionary<string, string, (string, Expression?)>(line => line[..4], line => (line[6..], null));
    }

    private Expression GetExpression(string key, Dictionary<string, (string val, Expression? exp)> dict, ParameterExpression? human = null)
    {
        var entry = dict[key];

        if (entry.exp != null)
        {
            return entry.exp;
        }

        var segments = entry.val.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        Expression expression = segments switch
        {
            [not null] when key == HumanKey && human != null => human,
            [{ } lVar] => Expression.Constant(long.Parse(lVar)),
            [{ } lVar, "+", { } rVar] => Expression.Add(GetExpression(lVar, dict, human), GetExpression(rVar, dict, human)),
            [{ } lVar, "-", { } rVar] => Expression.Subtract(GetExpression(lVar, dict, human), GetExpression(rVar, dict, human)),
            [{ } lVar, "*", { } rVar] => Expression.Multiply(GetExpression(lVar, dict, human), GetExpression(rVar, dict, human)),
            [{ } lVar, "/", { } rVar] => Expression.Divide(GetExpression(lVar, dict, human), GetExpression(rVar, dict, human)),
            _ => throw new ArgumentOutOfRangeException()
        };

        if (expression is BinaryExpression { Left: ConstantExpression, Right: ConstantExpression })
        {
            Func<long> lambda = (Func<long>)Expression.Lambda(expression).Compile();
            expression = Expression.Constant(lambda());
        }

        dict[key] = (entry.val, expression);
        return expression;
    }

    private static Expression SolveForHuman(Expression human, Expression targetExpression)
    {
        while (human.NodeType is not ExpressionType.Parameter)
        {
            (Expression newHuman, Expression newTarget) t = human switch
            {
                BinaryExpression { NodeType: ExpressionType.Add, Left: ConstantExpression ce } be
                    => (be.Right, Expression.Subtract(targetExpression, ce)),
                BinaryExpression { NodeType: ExpressionType.Add, Right: ConstantExpression ce } be
                    => (be.Left, Expression.Subtract(targetExpression, ce)),

                BinaryExpression { NodeType: ExpressionType.Subtract, Left: ConstantExpression ce } be
                    => (be.Right, Expression.Subtract(ce, targetExpression)),
                BinaryExpression { NodeType: ExpressionType.Subtract, Right: ConstantExpression ce } be
                    => (be.Left, Expression.Add(targetExpression, ce)),

                BinaryExpression { NodeType: ExpressionType.Multiply, Left: ConstantExpression ce } be
                    => (be.Right, Expression.Divide(targetExpression, ce)),
                BinaryExpression { NodeType: ExpressionType.Multiply, Right: ConstantExpression ce } be
                    => (be.Left, Expression.Divide(targetExpression, ce)),

                BinaryExpression { NodeType: ExpressionType.Divide, Left: ConstantExpression ce } be
                    => (be.Right, Expression.Multiply(targetExpression, ce)),
                BinaryExpression { NodeType: ExpressionType.Divide, Right: ConstantExpression ce } be
                    => (be.Left, Expression.Multiply(targetExpression, ce)),

                _ => throw new ArgumentOutOfRangeException(nameof(human), human, null)
            };

            targetExpression = t.newTarget;
            human = t.newHuman;
        }

        return targetExpression;
    }
}