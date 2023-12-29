using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeBowl
{
    class Program
    {
        class Contestant
        {
            public readonly int picks;
            public readonly int points;

            public int winScenarios = 0;

            public Contestant(int picks, int points)
            {
                this.picks = picks;
                this.points = points;
            }
        }

        static void Main(string[] args)
        {
            var contestants = new Dictionary<string, Contestant>
            {
                {"Rod",     new Contestant(0b011110011011111 , 15) },
                {"Marleen", new Contestant(0b011110001011111 , 14) },
                {"Lauren",  new Contestant(0b111001001011001 , 14) },
                {"Matt W",  new Contestant(0b011010011111001 , 14) },
                {"Matt H",  new Contestant(0b100101101111101 , 13) },
                {"Eddie",   new Contestant(0b011110001110111 , 14) },
                {"Andrea",  new Contestant(0b010100011011111 , 17) },
                {"Emmie",   new Contestant(0b011110100011111 , 19) },
                {"Vicky",   new Contestant(0b011100010010100 , 16) },
                {"Bruno",   new Contestant(0b011100011111010 , 23) },
                {"Cass",    new Contestant(0b111110011011010 , 19) },
                {"Gene",    new Contestant(0b100111111110111 , 9) },
                {"Mike",    new Contestant(0b100111111110111 , 12) },
                {"Ryder",   new Contestant(0b011111001110000 , 17) },
                {"Ryan",    new Contestant(0b011100011111101 , 17) },
                {"Ralph",   new Contestant(0b001111011110100 , 15) },
                {"Andy",    new Contestant(0b011110011110010 , 14) },
                {"John",    new Contestant(0b011110011111101 , 15) },
                {"Phil",    new Contestant(0b011100101010011 , 15) },
                {"BJD",     new Contestant(0b111110010010100 , 13) },
                {"Mark",    new Contestant(0b011010011111010 , 17) },
                {"Will",    new Contestant(0b101100101010000 , 19) },
                {"Adam",    new Contestant(0b010111001111101 , 13) },
                {"Serena",  new Contestant(0b111111100011001 , 16) },
                {"Sean",    new Contestant(0b010101011111111 , 15) },
                {"Kelly",   new Contestant(0b001100001110100 , 15) },
                {"Ankit",   new Contestant(0b011001000111010 , 19) },
                {"L & B",   new Contestant(0b101110011010010 , 13) },
                {"Kireet",  new Contestant(0b010101101010000 , 15) },
                {"Corrie",  new Contestant(0b101100111110111 , 15) },
                {"Larry",   new Contestant(0b011111010111101 , 18) },
                {"Jim",     new Contestant(0b011010101011111 , 14) },
                {"Lynn",    new Contestant(0b111101011111001 , 16) },
                {"Trev",    new Contestant(0b111000011110010 , 18) }
            };
            var numRemainingGames = 15;
            var numPossibleGameResults = 1 << numRemainingGames;
            for (int possibleGameResults = 0; possibleGameResults < numPossibleGameResults; possibleGameResults++)
            {
                var maxPoints = -1;
                var winners = new List<string>();
                foreach (var contestant in contestants)
                {
                    var points = contestant.Value.points;
                    for (int gameSlot = 4; gameSlot < numRemainingGames; gameSlot++)
                    {
                        if (((1 << gameSlot) & possibleGameResults) == ((1 << gameSlot) & contestant.Value.picks)) points++;
                    }
                    for (int gameSlot = 2; gameSlot < 4; gameSlot++)
                    {
                        if (((1 << gameSlot) & possibleGameResults) == ((1 << gameSlot) & contestant.Value.picks)) points += 2;
                    }
                    if (((1 & possibleGameResults) == (1 & contestant.Value.picks)) && ((2 & possibleGameResults) == (2 & contestant.Value.picks))) points += 2;
                    if (points > maxPoints)
                    {
                        maxPoints = points;
                        winners.Clear();
                    }
                    if (points == maxPoints)
                    {
                        winners.Add(contestant.Key);
                    }
                }
                foreach (var contestant in contestants)
                {
                    if (winners.Contains(contestant.Key)) contestant.Value.winScenarios++;
                }
            }
            var output = "";
            foreach (var contestant in contestants.OrderByDescending(contestant => contestant.Value.winScenarios))
            {
                output += $"{contestant.Key}: {contestant.Value.winScenarios} - {String.Format("{0:0.##}", (double)contestant.Value.winScenarios / (double)numPossibleGameResults * 100.0)}%\n";
            }
            File.WriteAllText("output.txt", output);
        }
    }
}
