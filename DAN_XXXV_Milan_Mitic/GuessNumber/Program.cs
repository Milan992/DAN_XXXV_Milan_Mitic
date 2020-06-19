using System;
using System.Threading;
using System.Threading.Tasks;

namespace GuessNumber
{
    class Program
    {
        static int numberOfParticipants;
        static int number;
        static readonly object l = new object();

        static void Main(string[] args)
        {
            Thread begin = new Thread(() => InsertParticipantAmount());
            begin.Start();

            numberOfParticipants = InsertParticipantAmount();

            Task<Thread[]> Thread_Generator = new Task<Thread[]>(() => GenerateThreads(numberOfParticipants));
            Thread_Generator.Start();

            Thread[] threadArray = GenerateThreads(numberOfParticipants);

            for (int i = 0; i < threadArray.Length; i++)
            {
                threadArray[i].Start();
            }
            Console.ReadLine();
        }

        private static int InsertParticipantAmount()
        {
            string participants = "";
            int guess;

            while (!int.TryParse(participants, out guess))
            {
                Console.WriteLine("\nPlease enter the number of participants:");
                participants = Console.ReadLine();
            }

            string numberToGuess = "";
            while (!int.TryParse(numberToGuess, out number))
            {
                Console.WriteLine("\nPlease enter a number to guess. The number muust be 1 - 100.");
                numberToGuess = Console.ReadLine();
            }
            Console.WriteLine("\nYou have entered the number of participanst. Number of participanst is: {0}", numberOfParticipants);
            Console.WriteLine("\n The number to guess is: {0}", number);
            return guess;
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
            Thread.Sleep(100);
            Random r = new Random();
            int guess = r.Next(1, 101);

            lock (l)
            {
                Console.WriteLine("\n{0} tried to guess with number: {1}", Thread.CurrentThread.Name, guess);
                if (guess % 2 == numberToGuess % 2)
                {
                    Console.WriteLine("{0} guessed the number's parity", Thread.CurrentThread.Name);
                }
                if (guess == numberToGuess)
                {
                    Console.WriteLine("\t\t\tThread_{0} won, number to guess was {1}", Thread.CurrentThread.Name, numberToGuess);
                }
            }

        }
    }
}
