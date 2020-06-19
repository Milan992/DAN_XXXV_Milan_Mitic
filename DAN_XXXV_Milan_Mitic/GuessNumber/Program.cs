using System;
using System.Threading;
using System.Threading.Tasks;

namespace GuessNumber
{
    class Program
    {
        static int numberOfParticipants;
        static int number = 0;
        static readonly object l = new object();

        static void Main(string[] args)
        {
            Thread begin = new Thread(() => InsertParticipantAmount());
            begin.Start();
            begin.Join();

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

        /// <summary>
        /// asks to insert number of participants and a nubmer to guess.
        /// </summary>
        /// <returns>number of participants which is equals to number of threads</returns>
        private static int InsertParticipantAmount()
        {
            string participants = "";
            uint participantsInt;

            while (!uint.TryParse(participants, out participantsInt) && participantsInt < 1)
            {
                Console.WriteLine("\nPlease enter the number of participants:");
                participants = Console.ReadLine();
            }

            string numberToGuess = "";

            if (number < 1)
            {
                while (!int.TryParse(numberToGuess, out number) && number < 1 || number > 100)
                {
                    Console.WriteLine("\nPlease enter a number to guess. The number must be 1 - 100.");
                    numberToGuess = Console.ReadLine();
                }
            }

            Console.WriteLine("\nYou have entered the number of participanst. Number of participants is: {0}", participantsInt);
            Console.WriteLine("\n The number to guess is: {0}", number);

            return Convert.ToInt32(participantsInt);
        }

        /// <summary>
        /// generates an array of threads.
        /// </summary>
        /// <param name="numberOfThreads"></param>
        /// <returns></returns>
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

        /// <summary>
        /// generates a random number and compares it to the parameter.
        /// </summary>
        /// <param name="numberToGuess"></param>
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
                    Console.WriteLine("\n\t\t\tThread_{0} won, number to guess was {1}", Thread.CurrentThread.Name, numberToGuess);
                    Console.ReadLine();
                }
            }
        }
    }
}
