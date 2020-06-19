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
        static uint numberOfParticipants;
        static int number;

        static void Main(string[] args)
        {
            Thread begin = new Thread(() => InsertParticipantAmount());
            
            Thread[] threadArray = GenerateThreads(Convert.ToInt32(numberOfParticipants));

            for (int i = 0; i < threadArray.Length; i++)
            {
                threadArray[i].Start();
            }
        }

        private static int InsertParticipantAmount()
        {
            string participants = "";

            while (!uint.TryParse(participants, out numberOfParticipants))
            {
                Console.WriteLine("\nPlease enter the number of participants:");
                participants = Console.ReadLine();
            }

            string numberToGuess = "";

            while (!int.TryParse(numberToGuess, out number) && number > 100 && number < 1)
            {
                Console.WriteLine("\nPlease enter a number to guess. The number muust be 1 - 100.");
                numberToGuess = Console.ReadLine();
            }

            Task<Thread[]> Thread_Generator = new Task<Thread[]>(() => GenerateThreads(Convert.ToInt32(numberOfParticipants)));
            Thread_Generator.Start();

            Console.WriteLine("\nYou have entered the number of participanst. Number of participanst is: {0}", numberOfParticipants);
            Console.WriteLine("\n The number to guess is: {0}", number);
            return Convert.ToInt32(numberOfParticipants);
        }

        private static Thread[] GenerateThreads(int numberOfThreads)
        {
            Thread[] threadArray = new Thread[numberOfThreads];

            for (int i = 0; i < numberOfThreads; i++)
            {
                Thread t = new Thread(() => GuessNumber(number));
                t.Name = "Participant_" + Convert.ToString(i);
                threadArray[i] = t;
            }
            return threadArray;
        }

        private static void GuessNumber(int numberToGuess)
        {
            throw new NotImplementedException();
        }
    }
}
