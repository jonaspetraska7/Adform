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

        public ITable<PointList> PointLists => this.GetTable<PointList>();

    }
}
