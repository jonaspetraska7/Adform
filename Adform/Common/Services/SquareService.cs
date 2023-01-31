using Common.Entities;
using Common.Interfaces;

namespace Common.Services
{
    public class SquareService : ISquareService
    {
        private const int _processSize = 500;
        private HashSet<string> _pointsSet;
        private List<Point> _points;

        public PointList UpdatePointListSquares(PointList pointList, CancellationToken cancellationToken = default)
        {
            _points = pointList.Points;
            _pointsSet = _points.Select(p => p.ToString()).ToHashSet();

            //process points in parallel with a set of 500
            int processes = (_points.Count / _processSize) + 1;

            var resultSet = new List<IEnumerable<Square>>();
            Parallel.For(0, processes, process => resultSet.Add(GetSquares(process, _processSize)));

            var squares = new List<List<Point>>();
            var squareSet = new HashSet<string>();

            foreach (var result in resultSet)
            {
                foreach (var square in result)
                {
                    var s = square.ToString();
                    if (!squareSet.Contains(s))
                    {
                        squares.Add(square.Points);
                        squareSet.Add(s);
                    }
                }
            }

            pointList.Squares = squares;
            pointList.SquaresCached = true;

            return pointList;
        }

        public List<Square> GetSquares(int process, int processSize, CancellationToken cancellationToken = default)
        {
            var squares = new List<Square>();

            var startIndex = process * processSize;
            var endIndex = startIndex + processSize > _points.Count ? _points.Count : startIndex + processSize;
            
            for (int i = startIndex; i < endIndex; i++)
            {
                for (int j = 0; j < _points.Count; j++)
                {
                    if (cancellationToken.IsCancellationRequested) cancellationToken.ThrowIfCancellationRequested();
                    
                    if (_points[i].Equals(_points[j])) continue;

                    var diagVertex = GetRestPoints(_points[i], _points[j]);
                    if (diagVertex != null && _pointsSet.Contains(diagVertex[0].ToString())
                        && _pointsSet.Contains(diagVertex[1].ToString()))
                    {
                        squares.Add(new Square(_points[i], diagVertex[0], _points[j], diagVertex[1]));
                    }
                }
            }
            return squares;
        }

        private List<Point>? GetRestPoints(Point a, Point c)
        {
            // find mid point of point a and c
            var midX = (float)(a.X + c.X) / 2;
            var midY = (float)(a.Y + c.Y) / 2;

            // calculate point b
            var Ax = a.X - midX;
            var Ay = a.Y - midY;
            var bX = midX - Ay;
            var bY = midY + Ax;

            // calculate point d
            var cX = (c.X - midX);
            var cY = (c.Y - midY);
            var dX = midX - cY;
            var dY = midY + cX;

            return IsInteger(bX) && IsInteger(bY) && IsInteger(dX) && IsInteger(dY)
                ? new List<Point> { new Point { X = (int)bX, Y = (int)bY }, new Point { X = (int)dX, Y = (int)dY } }
                : null;
        }

        private bool IsInteger(float p) => p - (int)p == 0;

    }
}
