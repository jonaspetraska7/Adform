using LinqToDB.Mapping;

namespace Common.Entities
{
    public class PointListDto
    {
        [PrimaryKey]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column(DataType = LinqToDB.DataType.Text)]
        public string Points { get; set; } = "[{}]";

        [Column(DataType = LinqToDB.DataType.Text)]
        public string Squares { get; set; } = "[[{}]]";
        public bool SquaresCached { get; set; } = false;

    }
}
