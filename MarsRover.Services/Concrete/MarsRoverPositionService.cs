using System;
using System.Collections.Generic;
using MarsRover.Common.Enums;
using MarsRover.Common.Model;
using MarsRover.Services.Abstract;

namespace MarsRover.Services.Concrete
{
    public class MarsRoverPositionService : IMarsRoverPositionService
    {
        private readonly IMoveCommandService _moveCommandService;

        public Position CurrentPosition { get; set; } = new();
        public Coordinate Coordinate { get; set; } = new();

        public MarsRoverPositionService(IMoveCommandService moveCommandService) =>
            (_moveCommandService) = (moveCommandService);

        public Position StartToMove(string moves)
        {
            // Move Rover according to Commands and update position 
            foreach (var move in moves)
            {
                CurrentPosition = _moveCommandService.Move(CurrentPosition, move.ToString());

                CheckPosition();
            }

            return CurrentPosition;
        }

        public void SetStartPosition(string[] startPositions)
        {
            if (startPositions.Length != 3)
            {
                throw new Exception("You entered a missing value");
            }

            // Set the Started Position of Rover
            CurrentPosition.X = Convert.ToInt32(startPositions[0]);
            CurrentPosition.Y = Convert.ToInt32(startPositions[1]);
            CurrentPosition.Direction = (DirectionType)Enum.Parse(typeof(DirectionType), startPositions[2]);

            CheckPosition();
        }

        public void SetLimitedPoints(List<int> points)
        {
            Coordinate.X = points[0];
            Coordinate.Y = points[1];
        }

        public void CheckPosition()
        {
            // Check the Started Position in the Area
            if (CurrentPosition.X < 0 || CurrentPosition.X > Coordinate.X || CurrentPosition.Y < 0 || CurrentPosition.Y > Coordinate.Y)
            {
                throw new Exception($"The starting point is not within the boundaries of (0 , 0) and ({Coordinate.X} , {Coordinate.Y})");
            }
        }
    }
}
