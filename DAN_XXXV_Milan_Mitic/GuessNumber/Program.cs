using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GuessNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread begin = new Thread(() => InsertParticipantAmount());
        }

        private static void InsertParticipantAmount()
        {
            string participants = "";
            uint numberOfParticipants;

            while (!uint.TryParse(participants, out numberOfParticipants))
            {
                Console.WriteLine("Please enter the number of participants:");
                participants = Console.ReadLine();
            }

            string numberToGuess = "";
            int number;

            while (!int.TryParse(numberToGuess, out number) && number > 100 && number < 1)
            {
                Console.WriteLine("Please enter a number to guess. The number muust be 1 - 100.");
                numberToGuess = Console.ReadLine();
            }

        }
    }
}
