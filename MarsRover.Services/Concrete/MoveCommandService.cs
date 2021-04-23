using System;
using System.Collections.Generic;
using MarsRover.Common.Enums;
using MarsRover.Common.Model;
using MarsRover.Services.Abstract;

namespace MarsRover.Services.Concrete
{
    public class MoveCommandService : IMoveCommandService
    {
        public readonly Dictionary<DirectionType, Func<Position, CommandType, Position>> MoveManager =
            new Dictionary<DirectionType, Func<Position, CommandType, Position>>
            {
                {DirectionType.N, MoveNorthPosition},
                {DirectionType.E, MoveEastPosition},
                {DirectionType.S, MoveSouthPosition },
                {DirectionType.W, MoveWestPosition }
            };
        public Position Move(Position currentPosition, string commandType) =>
            MoveManager[currentPosition.Direction](currentPosition, (CommandType)Enum.Parse(typeof(CommandType), commandType));

        private static Position MoveEastPosition(Position currentPosition, CommandType commandType) =>
            commandType switch
            {
                CommandType.R => currentPosition.SetDirection(DirectionType.S),
                CommandType.L => currentPosition.SetDirection(DirectionType.N),
                CommandType.M => currentPosition.ChangeXPosition(1)
            };

        private static Position MoveNorthPosition(Position currentPosition, CommandType commandType) =>
            commandType switch
            {
                CommandType.R => currentPosition.SetDirection(DirectionType.E),
                CommandType.L => currentPosition.SetDirection(DirectionType.W),
                CommandType.M => currentPosition.ChangeYPosition(1)
            };

        private static Position MoveSouthPosition(Position currentPosition, CommandType commandType) =>
            commandType switch
            {
                CommandType.R => currentPosition.SetDirection(DirectionType.W),
                CommandType.L => currentPosition.SetDirection(DirectionType.E),
                CommandType.M => currentPosition.ChangeYPosition(-1)
            };

        private static Position MoveWestPosition(Position currentPosition, CommandType commandType) =>
            commandType switch
            {
                CommandType.R => currentPosition.SetDirection(DirectionType.N),
                CommandType.L => currentPosition.SetDirection(DirectionType.S),
                CommandType.M => currentPosition.ChangeXPosition(-1)
            };
    }
}
