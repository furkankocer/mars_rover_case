using System;
using System.Collections.Generic;
using MarsRover.Common.Enums;
using MarsRover.Common.Model;
using MarsRover.Services.Abstract;
using MarsRover.Services.Concrete;
using Moq;
using Xunit;

namespace MarsRover.Test
{
    public class MarsRoverTest
    {
        private readonly Mock<IMarsRoverPositionService> _marsRoverPositionServiceMock;
        private readonly Mock<IMoveCommandService> _moveCommandServiceMock;

        public MarsRoverTest()
        {
            _marsRoverPositionServiceMock = new Mock<IMarsRoverPositionService>();
            _moveCommandServiceMock = new Mock<IMoveCommandService>();
        }

        public static IEnumerable<object[]> TestStartPositionData =>
            new List<object[]>
            {
                new object[] {new List<string> { "1", "2" }},
                new object[] {new List<string> { "1", "2","3","N" }}
            };

        [Theory]
        [MemberData(nameof(TestStartPositionData))]
        public void SetStartPosition_ShouldThrowException_WhenEnteredMissingPositionValues(string[] startPositions)
        {
            //Act
            var marsRoverPositionService = new MarsRoverPositionService(_moveCommandServiceMock.Object);
            Action act = () => marsRoverPositionService.SetStartPosition(startPositions);

            //Assert
            Exception exception = Assert.Throws<Exception>(act);
            Assert.Equal("You entered a missing value", exception.Message);
        }

        [Fact]
        public void CheckPosition_ShouldThrowException_WhenPositionOutOfBounds()
        {
            //Act
            var marsRoverPositionService = new MarsRoverPositionService(_moveCommandServiceMock.Object);
            marsRoverPositionService.Coordinate.X = 5;
            marsRoverPositionService.Coordinate.Y = 5;
            marsRoverPositionService.CurrentPosition.X = 10;
            marsRoverPositionService.CurrentPosition.Y = 10;

            Action act = () => marsRoverPositionService.CheckPosition();

            //Assert
            Exception exception = Assert.Throws<Exception>(act);
            Assert.Equal($"The starting point is not within the boundaries of (0 , 0) and ({marsRoverPositionService.Coordinate.X} , {marsRoverPositionService.Coordinate.Y})", exception.Message);
        }

        [Fact]
        public void SetLimitedPoints_WhenLimitPointsNotEntered_ShouldReturnDefaultValues()
        {
            //Arrange 
            List<int> points = new List<int>();

            //Act
            var marsRoverPositionService = new MarsRoverPositionService(_moveCommandServiceMock.Object);
            Action act = () => marsRoverPositionService.SetLimitedPoints(points);

            //Assert
            var expected = new Coordinate
            {
                X = 0,
                Y = 0
            };

            Assert.Equal(expected.ToString(), marsRoverPositionService.Coordinate.ToString());
        }

        [Fact]
        public void Move_WhenCommandTypeIsMovetAndDirectionTypeIsNorth_ShouldReturnDirectionIsNorth()
        {
            //Arrange 
            List<int> points = new List<int>{5,5};
            Position position = new Position
            {
                X = 1,Y = 2,Direction = DirectionType.N
            };
            string move = "M";

            //Act
            var moveCommandService = new MoveCommandService();
            var marsRoverPositionService = new MarsRoverPositionService(moveCommandService);
            Action act = () => moveCommandService.Move(position,move);

            //Assert

            Assert.Equal(DirectionType.N, marsRoverPositionService.CurrentPosition.Direction);
        }


    }
}
