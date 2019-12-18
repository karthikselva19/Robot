using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotCleaner.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //collect a set of inputs from standard in
            CommandFactory commandFactory = new CommandFactory();

            while (!commandFactory.InputsAreComplete)
            {
                Console.WriteLine(">");
                commandFactory.AddInput(Console.ReadLine());
            }
            Console.WriteLine("Input complete. Press any key to continue..");
            Console.ReadLine();

            //robot should execute cleaning commands
            SimpleReporter reporter = new SimpleReporter();
            Robot robbie = new Robot(commandFactory.GetCommandSet(), reporter, new Location(0,0), new Location(7,7));
            robbie.ExecuteCommands();

            //give output on the number of places cleaned
            Console.WriteLine(robbie.PrintReport());
        }
    }
}
