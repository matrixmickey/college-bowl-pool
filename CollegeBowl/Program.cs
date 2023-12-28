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
                {"Rod",     new Contestant(0b0011011110011011111 , 13) },
                {"Marleen", new Contestant(0b0101011110001011111 , 14) },
                {"Lauren",  new Contestant(0b1111111001001011001 , 12) },
                {"Matt W",  new Contestant(0b0110011010011111001 , 12) },
                {"Matt H",  new Contestant(0b0110100101101111101 , 11) },
                {"Eddie",   new Contestant(0b0101011110001110111 , 14) },
                {"Andrea",  new Contestant(0b1000010100011011111 , 14) },
                {"Emmie",   new Contestant(0b1000011110100011111 , 16) },
                {"Vicky",   new Contestant(0b1011011100010010100 , 13) },
                {"Bruno",   new Contestant(0b0110011100011111010 , 21) },
                {"Cass",    new Contestant(0b1101111110011011010 , 18) },
                {"Gene",    new Contestant(0b1101100111111110111 , 8) },
                {"Mike",    new Contestant(0b1111100111111110111 , 10) },
                {"Ryder",   new Contestant(0b0010011111001110000 , 14) },
                {"Ryan",    new Contestant(0b0111011100011111101 , 16) },
                {"Ralph",   new Contestant(0b1111001111011110100 , 13) },
                {"Andy",    new Contestant(0b0110011110011110010 , 12) },
                {"John",    new Contestant(0b0111011110011111101 , 14) },
                {"Phil",    new Contestant(0b0110011100101010011 , 13) },
                {"BJD",     new Contestant(0b0111111110010010100 , 12) },
                {"Mark",    new Contestant(0b0010011010011111010 , 14) },
                {"Will",    new Contestant(0b1110101100101010000 , 16) },
                {"Adam",    new Contestant(0b0110010111001111101 , 11) },
                {"Serena",  new Contestant(0b0101111111100011001 , 16) },
                {"Sean",    new Contestant(0b0111010101011111111 , 14) },
                {"Kelly",   new Contestant(0b0111001100001110100 , 14) },
                {"Ankit",   new Contestant(0b0101011001000111010 , 19) },
                {"L & B",   new Contestant(0b1101101110011010010 , 12) },
                {"Kireet",  new Contestant(0b1001010101101010000 , 13) },
                {"Corrie",  new Contestant(0b0111101100111110111 , 14) },
                {"Larry",   new Contestant(0b1101011111010111101 , 17) },
                {"Jim",     new Contestant(0b0101011010101011111 , 14) },
                {"Lynn",    new Contestant(0b0111111101011111001 , 15) },
                {"Trev",    new Contestant(0b0111111000011110010 , 17) }
            };
            var numRemainingGames = 19;
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
