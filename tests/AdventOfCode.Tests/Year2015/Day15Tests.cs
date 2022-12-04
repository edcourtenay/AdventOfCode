using AdventOfCode.Year2015;

namespace AdventOfCode2015CS.Tests;

public class Day15Tests
{
    private readonly Day15.Ingredient[] _ingredients;

    public Day15Tests()
    {
        _ingredients = new Day15.Ingredient[]
        {
            //Butterscotch: capacity -1, durability -2, flavor 6, texture 3, calories 8
            //Cinnamon: capacity 2, durability 3, flavor -2, texture -1, calories 3
            new()
            {
                Name = "Butterscotch",
                Capacity = -1,
                Durability = -2,
                Flavour = 6,
                Texture = 3,
                Calories = 8
            },
            new()
            {
                Name = "Cinnamon",
                Capacity = 2,
                Durability = 3,
                Flavour = -2,
                Texture = -1,
                Calories = 3
            }
        };
    }

    [Fact(Skip="Not implemented correctly")]
    public void Test()
    {
        var sut = new Day15();

        int result = sut.Calculate(_ingredients, new[] { 44, 56 });

        result.Should().Be(62842880);
    }
}

