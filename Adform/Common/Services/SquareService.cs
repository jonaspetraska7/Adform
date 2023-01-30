using Common.Entities;
using Common.Interfaces;

namespace Common.Services
{
    public class SquareService : ISquareService
    {
        public PointList UpdatePointListSquares(PointList pointList)
        {
            var result = GetSquares(pointList.Points);

            var squares = new List<List<Point>>();
            var squareSet = new HashSet<string>();

            foreach (var square in result)
            {
                var s = square.ToString();
                if (!squareSet.Contains(s))
                {
                    squares.Add(square.Points);
                    squareSet.Add(s);
                }
            }

            pointList.Squares = squares;
            pointList.SquaresCached = true;

            return pointList;
        }

        public List<Square> GetSquares(List<Point> input)
        {
            List<Square> squares = new List<Square>();
            HashSet<string> hashSet = input.Select(p => p.ToString()).ToHashSet();

            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input.Count; j++)
                {
                    if (input[i].Equals(input[j])) continue;

                    //For each Point i, Point j, check if b&d exist in set.
                    var DiagVertex = GetRestPoints(input[i], input[j]);
                    if (DiagVertex != null && hashSet.Contains(DiagVertex[0].ToString()) && hashSet.Contains(DiagVertex[1].ToString()))
                    {
                        squares.Add(new Square(input[i], DiagVertex[0], input[j], DiagVertex[1]));
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
