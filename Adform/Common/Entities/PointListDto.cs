using LinqToDB.Mapping;
using System.Text.Json;

namespace Common.Entities
{
    public class PointListDto
    {
        [PrimaryKey]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Points { get; set; } = "[{}]";
        public string Squares { get; set; } = "[{}]";
        public bool SquaresCached { get; set; } = false;

    }
}
