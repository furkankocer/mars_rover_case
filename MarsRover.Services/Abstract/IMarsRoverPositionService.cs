using System.Collections.Generic;
using MarsRover.Common.Model;

namespace MarsRover.Services.Abstract
{
    public interface IMarsRoverPositionService
    {
        Position StartToMove(string moves);

        void SetStartPosition(string[] startPositions);

        void SetLimitedPoints(List<int> points);
    }
}
