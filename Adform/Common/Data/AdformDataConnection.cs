using Common.Entities;
using LinqToDB;
using LinqToDB.Configuration;
using LinqToDB.Data;

namespace Common.Data
{
    public class AdformDataConnection : DataConnection
    {
        public AdformDataConnection(LinqToDBConnectionOptions<AdformDataConnection> options)
            : base(options) { }

        public ITable<PointListDto> PointLists => this.GetTable<PointListDto>();

    }
}
