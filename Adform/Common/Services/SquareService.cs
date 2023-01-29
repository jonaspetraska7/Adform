using Common.Entities;
using Common.Interfaces;

namespace Common.Services
{
    public class SquareService : ISquareService
    {
        public PointList UpdatePointListSquares(PointList pointList)
        {
            List<Square> squares = GetSquares(pointList.Points);

            pointList.Squares = squares;
            pointList.SquaresCached = true;

            return pointList;
        }

        public List<Square> GetSquares(List<Point> points)
        {
            List<Square> squares = new List<Square>();

            foreach (Point point1 in points)
            {
                foreach (Point point2 in points)
                {
                    if (point1.X == point2.X && point1.Y == point2.Y)
                    {
                        continue;
                    }

                    int deltaX = point2.X - point1.X;
                    int deltaY = point2.Y - point1.Y;
                    int distance = deltaX * deltaX + deltaY * deltaY;

                    Point point3 = new Point(point2.X + deltaY, point2.Y - deltaX);
                    Point point4 = new Point(point3.X + deltaX, point3.Y + deltaY);

                    if (points.Contains(point3) && points.Contains(point4))
                    {
                        Point point5 = new Point(point1.X + deltaY, point1.Y - deltaX);
                        if (points.Contains(point5))
                        {
                            squares.Add(new Square(point1, point2, point3, point4));
                        }
                    }
                }
            }

            return squares;
        }

    }
}
