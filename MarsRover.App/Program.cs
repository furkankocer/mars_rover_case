using System;
using System.Linq;
using MarsRover.Services.Abstract;
using MarsRover.Services.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace MarsRover.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Dependency Injection
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IMarsRoverPositionService, MarsRoverPositionService>()
                .AddSingleton<IMoveCommandService, MoveCommandService>()
                .BuildServiceProvider();
            IMoveCommandService moveCommandService = serviceProvider.GetService<IMoveCommandService>();
            IMarsRoverPositionService marsRoverPositionService = serviceProvider.GetService<IMarsRoverPositionService>();

            try
            {
                // Read Inputs 
                Console.Write("Coordinates of the planet: ");
                var maxPoints = Console.ReadLine().Trim().Split(' ').Select(int.Parse).ToList();
                marsRoverPositionService.SetLimitedPoints(maxPoints);

                Console.Write("Coordinates of the Rover: ");
                var startPositions = Console.ReadLine()?.Trim().Split(' ');
                marsRoverPositionService.SetStartPosition(startPositions);

                Console.Write("Enter the Commands: ");
                var moves = Console.ReadLine()?.ToUpper();
                var currentPosition = marsRoverPositionService.StartToMove(moves);

                // Output
                Console.WriteLine($"Output: {currentPosition.X} {currentPosition.Y} {currentPosition.Direction}");
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
    }
}
