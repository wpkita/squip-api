using System;
using FluentAssertions;
using Squip.Rest.Ideas.Domain;
using Xunit;

namespace Squip.Rest.Tests.Domain;

public class GameTests
{
    [Fact]
    public void PopulateWinnerAndLoser_LeftWins_RightLoses()
    {
        var left = new Idea
        {
            Content = "Left",
            Id = Guid.Parse("0172c9e9-b8e4-4939-ac78-caef38e93ee1")
        };
        var right = new Idea
        {
            Content = "Right",
            Id = Guid.Parse("e9507d94-26a1-4106-b7b3-3816ffedd6f3")
        };

        var game = new Game { Left = left, Right = right };
        game.SetWinner(left);
        game.Loser.Should().Be(right);
    }

    [Fact]
    public void PopulateWinnerAndLoser_RightWins_LeftLoses()
    {
        var left = new Idea
        {
            Content = "Left",
            Id = Guid.Parse("0172c9e9-b8e4-4939-ac78-caef38e93ee1")
        };
        var right = new Idea
        {
            Content = "Right",
            Id = Guid.Parse("e9507d94-26a1-4106-b7b3-3816ffedd6f3")
        };

        var game = new Game { Left = left, Right = right };
        game.SetWinner(right);
        game.Loser.Should().Be(left);
    }

    [Fact]
    public void PopulateWinner_WinnerNotParticipant_ThrowsArgumentException()
    {
        var left = new Idea
        {
            Content = "Left",
            Id = Guid.Parse("0172c9e9-b8e4-4939-ac78-caef38e93ee1")
        };
        var right = new Idea
        {
            Content = "Right",
            Id = Guid.Parse("e9507d94-26a1-4106-b7b3-3816ffedd6f3")
        };

        var orphanedIdea = new Idea
        {
            Content = "Hello world.",
            Id = Guid.Parse("bb993296-733f-4ea0-a8d1-524c13349b97")
        };

        var game = new Game { Left = left, Right = right };
        Assert.Throws<ArgumentException>(() => game.SetWinner(orphanedIdea));
    }
}
