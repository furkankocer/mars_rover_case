using MarsRover.Common.Enums;

namespace MarsRover.Common.Model
{
    public class Position : Coordinate
    {
        public DirectionType Direction { get; set; } = DirectionType.N;

        public Position SetDirection(DirectionType direction)
        {
            Direction = direction;
            return this;
        }

        public Position ChangeXPosition(int value)
        {
            X += value;
            return this;
        }

        public Position ChangeYPosition(int value)
        {
            Y += value;
            return this;
        }
    }

}
