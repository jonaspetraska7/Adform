namespace Common.Entities
{
    public class Square
    {
        public List<Point> Points { get; set; }

        public Square(Point p1, Point p2, Point p3, Point p4)
        {
            Points = new List<Point> { p1, p2, p3, p4 };
        }

        public override string ToString() => string.Join(",", Points.OrderBy(p => p.X).ThenBy(p => p.Y));
    }
}
