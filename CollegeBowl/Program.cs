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
                {"Rod",     new Contestant(0b1011110011011111 , 15) },
                {"Marleen", new Contestant(0b1011110001011111 , 14) },
                {"Lauren",  new Contestant(0b1111001001011001 , 14) },
                {"Matt W",  new Contestant(0b0011010011111001 , 13) },
                {"Matt H",  new Contestant(0b0100101101111101 , 12) },
                {"Eddie",   new Contestant(0b1011110001110111 , 14) },
                {"Andrea",  new Contestant(0b0010100011011111 , 16) },
                {"Emmie",   new Contestant(0b0011110100011111 , 18) },
                {"Vicky",   new Contestant(0b1011100010010100 , 16) },
                {"Bruno",   new Contestant(0b0011100011111010 , 22) },
                {"Cass",    new Contestant(0b1111110011011010 , 19) },
                {"Gene",    new Contestant(0b1100111111110111 , 9) },
                {"Mike",    new Contestant(0b1100111111110111 , 12) },
                {"Ryder",   new Contestant(0b0011111001110000 , 16) },
                {"Ryan",    new Contestant(0b1011100011111101 , 17) },
                {"Ralph",   new Contestant(0b1001111011110100 , 15) },
                {"Andy",    new Contestant(0b0011110011110010 , 13) },
                {"John",    new Contestant(0b1011110011111101 , 15) },
                {"Phil",    new Contestant(0b0011100101010011 , 14) },
                {"BJD",     new Contestant(0b1111110010010100 , 13) },
                {"Mark",    new Contestant(0b0011010011111010 , 16) },
                {"Will",    new Contestant(0b0101100101010000 , 18) },
                {"Adam",    new Contestant(0b0010111001111101 , 12) },
                {"Serena",  new Contestant(0b1111111100011001 , 16) },
                {"Sean",    new Contestant(0b1010101011111111 , 15) },
                {"Kelly",   new Contestant(0b1001100001110100 , 15) },
                {"Ankit",   new Contestant(0b1011001000111010 , 19) },
                {"L & B",   new Contestant(0b1101110011010010 , 13) },
                {"Kireet",  new Contestant(0b1010101101010000 , 15) },
                {"Corrie",  new Contestant(0b1101100111110111 , 15) },
                {"Larry",   new Contestant(0b1011111010111101 , 18) },
                {"Jim",     new Contestant(0b1011010101011111 , 14) },
                {"Lynn",    new Contestant(0b1111101011111001 , 16) },
                {"Trev",    new Contestant(0b1111000011110010 , 18) }
            };
            var numRemainingGames = 16;
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
