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
                {"Rod",     new Contestant(0b11110011011111 , 16) },
                {"Marleen", new Contestant(0b11110001011111 , 15) },
                {"Lauren",  new Contestant(0b11001001011001 , 14) },
                {"Matt W",  new Contestant(0b11010011111001 , 15) },
                {"Matt H",  new Contestant(0b00101101111101 , 13) },
                {"Eddie",   new Contestant(0b11110001110111 , 15) },
                {"Andrea",  new Contestant(0b10100011011111 , 18) },
                {"Emmie",   new Contestant(0b11110100011111 , 20) },
                {"Vicky",   new Contestant(0b11100010010100 , 17) },
                {"Bruno",   new Contestant(0b11100011111010 , 24) },
                {"Cass",    new Contestant(0b11110011011010 , 19) },
                {"Gene",    new Contestant(0b00111111110111 , 9) },
                {"Mike",    new Contestant(0b00111111110111 , 12) },
                {"Ryder",   new Contestant(0b11111001110000 , 18) },
                {"Ryan",    new Contestant(0b11100011111101 , 18) },
                {"Ralph",   new Contestant(0b01111011110100 , 16) },
                {"Andy",    new Contestant(0b11110011110010 , 15) },
                {"John",    new Contestant(0b11110011111101 , 16) },
                {"Phil",    new Contestant(0b11100101010011 , 16) },
                {"BJD",     new Contestant(0b11110010010100 , 13) },
                {"Mark",    new Contestant(0b11010011111010 , 18) },
                {"Will",    new Contestant(0b01100101010000 , 19) },
                {"Adam",    new Contestant(0b10111001111101 , 14) },
                {"Serena",  new Contestant(0b11111100011001 , 16) },
                {"Sean",    new Contestant(0b10101011111111 , 16) },
                {"Kelly",   new Contestant(0b01100001110100 , 16) },
                {"Ankit",   new Contestant(0b11001000111010 , 20) },
                {"L & B",   new Contestant(0b01110011010010 , 13) },
                {"Kireet",  new Contestant(0b10101101010000 , 16) },
                {"Corrie",  new Contestant(0b01100111110111 , 15) },
                {"Larry",   new Contestant(0b11111010111101 , 19) },
                {"Jim",     new Contestant(0b11010101011111 , 15) },
                {"Lynn",    new Contestant(0b11101011111001 , 16) },
                {"Trev",    new Contestant(0b11000011110010 , 18) }
            };
            var numRemainingGames = 14;
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
