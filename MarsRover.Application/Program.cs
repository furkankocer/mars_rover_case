using System;
using System.Linq;
using MarsRover.Common.Enums;
using MarsRover.Common.Model;
using MarsRover.Services.Abstract;

namespace MarsRover.Application
{
    public class Program
    {
        private static IMarsRoverPositionService _marsRoverPositionService;

        public Program(IMarsRoverPositionService marsRoverPositionService) =>
            (_marsRoverPositionService) = (marsRoverPositionService);

        public static void Main(string[] args)
        {
            var maxPoints = Console.ReadLine().Trim().Split(' ').Select(int.Parse).ToList();
            var startPositions = Console.ReadLine()?.Trim().Split(' ');
            Position position = new();

            position.X = Convert.ToInt32(startPositions[0]);
            position.Y = Convert.ToInt32(startPositions[1]);
            position.Direction = (Directions)Enum.Parse(typeof(Directions), startPositions[2]);


            var moves = Console.ReadLine()?.ToUpper();

            try
            {
                //position.StartMoving(maxPoints, moves);
                var currePosition = _marsRoverPositionService.StartToMove(position, maxPoints, moves);

                Console.WriteLine($"{currePosition.X} {currePosition.Y} {currePosition.Direction.ToString()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
    }
}
