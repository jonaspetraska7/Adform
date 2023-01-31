using System.Text.Json;

namespace Common.Entities
{
    public class PointList
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public List<Point> Points { get; set; } = new List<Point>();
        public List<List<Point>> Squares { get; set; } = new List<List<Point>>();
        public bool SquaresCached { get; set; } = false;

        public PointList() { }

        public PointList(PointListDto dto)
        {
            Id = dto.Id;
            Points = JsonSerializer.Deserialize<List<Point>>(dto.Points) ?? new List<Point>();
            Squares = JsonSerializer.Deserialize<List<List<Point>>>(dto.Squares) ?? new List<List<Point>>();
            SquaresCached = dto.SquaresCached;
        }

        public PointListDto ToDto()
        {
            var result = new PointListDto();

            result.Id = Id;
            result.Points = JsonSerializer.Serialize(Points);
            result.Squares = JsonSerializer.Serialize(Squares);
            result.SquaresCached = SquaresCached;

            return result;
        }

        public static explicit operator PointList?(PointListDto dto) => dto == null ? null : new PointList(dto);
        public static explicit operator PointListDto(PointList pointList) => pointList.ToDto();
    }
}
