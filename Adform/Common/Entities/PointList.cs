using LinqToDB.Mapping;

namespace Common.Entities
{
    public class PointList
    {
        [PrimaryKey]
        public Guid Id { get; set; } = Guid.NewGuid();
        public List<Point> Points { get; set; }
        public List<List<Point>> Squares { get; set; }
        public bool SquaresCached { get; set; } = false;
    }
}
