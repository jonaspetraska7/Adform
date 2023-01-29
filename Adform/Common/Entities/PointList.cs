using System.Text.Json;

namespace Common.Entities
{
    public class PointList
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public List<Point> Points { get; set; } = new List<Point>();
        public List<Square> Squares { get; set; } = new List<Square>();
        public bool SquaresCached { get; set; } = false;

        public PointList() { }

        public PointList(PointListDto dto)
        {
            Id = dto.Id;
            Points = JsonSerializer.Deserialize<List<Point>>(dto.Points) ?? new List<Point>();
            Squares = JsonSerializer.Deserialize<List<Square>>(dto.Squares) ?? new List<Square>();
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

        public static explicit operator PointList(PointListDto dto) => new PointList(dto);
        public static explicit operator PointListDto(PointList pointList) => pointList.ToDto();
    }
}
