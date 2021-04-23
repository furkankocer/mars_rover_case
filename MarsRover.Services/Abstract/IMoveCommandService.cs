using MarsRover.Common.Model;

namespace MarsRover.Services.Abstract
{
    public interface IMoveCommandService
    {
        Position Move(Position currentPosition, string commandType);
    }
}
