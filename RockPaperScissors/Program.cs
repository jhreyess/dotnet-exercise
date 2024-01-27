using System;
using System.Collections.Generic;

namespace RockPaperScissors
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //List<(string, string)> results = new List<(string, string)>
            //{
            //    ("R", "S"),
            //    ("S", "R"),
            //    ("P", "S"),
            //};

            List<(string, string)> results = new List<(string, string)>
            {
                ("R", "R"), // Empate
                ("R", "P"), // Paper - Jugador 2
                ("R", "S"), // Rock - Jugador 1
                ("P", "R"), // Paper - Jugador 1
                ("P", "P"), // Empate
                ("P", "S"), // Scissors - Jugador 2
                ("S", "R"), // Rock - Jugador 2
                ("S", "P"), // Scissors - Jugador 1
                ("S", "S"), // Empate
            };

            string winner = checkWinner(results);
            Console.WriteLine($"El ganador es: {winner}");
        }

        static string checkWinner(List<(string, string)> results)
        {
            int player1 = 0, player2 = 0;
            foreach(var result in results)
            {
                if(result.Item1 == result.Item2) { continue; }

                int winner = chooseWinner(result.Item1, result.Item2);

                if(winner == 1) { player1++; }
                else if(winner == 2) { player2++; }
                else { continue; }
            }

            return player1 > player2 
                ? "Jugador 1" 
                : player2 > player1 
                    ? "Jugador 2" 
                    : "Empate";
        }

        static int chooseWinner(string play1, string play2)
        {
            if(play1 == play2) { return 0; }
            
            if(
                play1 == "R" && play2 == "S" ||
                play1 == "P" && play2 == "R" ||
                play1 == "S" && play2 == "P"
            )
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
    }
}
