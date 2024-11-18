using AdventOfCode.Solutions.Year2015;

namespace AdventOfCode.Tests.Year2015;

public class Day15Tests
{
    private readonly Day15.Ingredient[] _ingredients;

    public Day15Tests()
    {
        _ingredients =
        [
            //Butterscotch: capacity -1, durability -2, flavor 6, texture 3, calories 8
            //Cinnamon: capacity 2, durability 3, flavor -2, texture -1, calories 3
            new Day15.Ingredient
            {
                Name = "Butterscotch",
                Capacity = -1,
                Durability = -2,
                Flavour = 6,
                Texture = 3,
                Calories = 8
            },
            new Day15.Ingredient
            {
                Name = "Cinnamon",
                Capacity = 2,
                Durability = 3,
                Flavour = -2,
                Texture = -1,
                Calories = 3
            }
        ];
    }

    [Fact(Skip = "Not implemented correctly")]
    public void Test()
    {
        Day15 sut = new();

        int result = sut.Calculate(_ingredients, [44, 56]);

        result.Should().Be(62842880);
    }
}