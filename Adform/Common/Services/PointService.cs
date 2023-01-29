using Common.Data;
using Common.Entities;
using Common.Interfaces;
using LinqToDB;

namespace Common.Services
{
    public class PointService : IPointService
    {
        private readonly AdformDataConnection _connection;
        public PointService(AdformDataConnection connection)
        {
            _connection = connection;
        }
        public Task<int> InsertPointList(PointList point)
        {
            return _connection.InsertAsync(point);
        }

        public Task<PointList?> GetPointList(Guid? id)
        {
            return _connection.PointLists.SingleOrDefaultAsync(x => x.Id == id);
        }

        public Task<int> DeletePointList(Guid id)
        {
            return _connection.PointLists.Where(x => x.Id == id).DeleteAsync();
        }

        public Task<int> UpdatePointList(PointList pointList)
        {
            return _connection.UpdateAsync(pointList);
        }

        public async Task<int> InsertPoint(Point point, Guid pointListId)
        {
            var pointList = await GetPointList(pointListId);

            if (pointList != null)
            {
                if (!pointList.Points.Contains(point))
                {
                    pointList.Points.Add(point);
                    pointList.SquaresCached = false;

                    return await UpdatePointList(pointList);
                }
            }

            return -1;
        }

        public async Task<int> DeletePoint(Point point, Guid pointListId)
        {
            var pointList = await GetPointList(pointListId);

            if (pointList != null)
            {
                if (pointList.Points.Remove(point))
                {
                    pointList.SquaresCached = false;

                    return await UpdatePointList(pointList);
                }
            }

            return -1;
        }

        public async Task<List<List<Point>>> GetSquares(Guid pointListId)
        {
            var pointList = await GetPointList(pointListId);

            if (pointList != null)
            {
                if (pointList.SquaresCached)
                {
                    return pointList.Squares;
                }
                else
                {
                    // Logic to calculate squares

                    await UpdatePointList(pointList);

                    return pointList.Squares;
                }
            }

            return new List<List<Point>>();
        }




    }
}
