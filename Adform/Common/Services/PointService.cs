using Common.Data;
using Common.Entities;
using Common.Interfaces;
using LinqToDB;

namespace Common.Services
{
    public class PointService : IPointService
    {
        private readonly AdformDataConnection _connection;
        private readonly ISquareService _squareService;

        public PointService(AdformDataConnection connection, ISquareService squareService)
        {
            _connection = connection;
            _squareService = squareService;
        }

        public async Task<Guid> InsertPointList(List<Point> points, CancellationToken cancellationToken = default)
        {
            var pointList = new PointList() { Points = points };
            var result = await _connection.InsertAsync((PointListDto) pointList, token: cancellationToken);

            if(result != -1)
            {
                return pointList.Id;
            }

            return Guid.Empty;
        }

        public async Task<PointList?> GetPointList(Guid? id, CancellationToken cancellationToken = default)
        {
            return (PointList?) await _connection.PointLists.SingleOrDefaultAsync(x => x.Id == id, token: cancellationToken);
        }

        public Task<int> UpdatePointList(PointList pointList, CancellationToken cancellationToken)
        {
            return _connection.UpdateAsync((PointListDto) pointList, token : cancellationToken);
        }

        public async Task<int> InsertPoint(Point point, Guid pointListId, CancellationToken cancellationToken = default)
        {
            var pointList = await GetPointList(pointListId, cancellationToken);

            if (pointList == null)
            {
                return -1;
            }

            if (!pointList.Points.Contains(point))
            {
                pointList.Points.Add(point);
                pointList.SquaresCached = false;

                return await UpdatePointList(pointList, cancellationToken);
            }

            return -1;
        }

        public async Task<int> DeletePoint(Point point, Guid pointListId, CancellationToken cancellationToken = default)
        {
            var pointList = await GetPointList(pointListId, cancellationToken);

            if (pointList == null)
            {
                return -1;
            }

            if (pointList.Points.Remove(point))
            {
                pointList.SquaresCached = false;

                return await UpdatePointList(pointList, cancellationToken);
            }

            return -1;
        }

        public async Task<List<List<Point>>> GetSquares(Guid pointListId, CancellationToken cancellationToken = default)
        {
            var pointList = await GetPointList(pointListId, cancellationToken);

            if (pointList == null)
            {
                return new List<List<Point>>();
            }

            if (pointList.SquaresCached)
            {
                return pointList.Squares;
            }

            _squareService.UpdatePointListSquares(pointList, cancellationToken);
            await UpdatePointList(pointList, cancellationToken);

            return pointList.Squares;
        }
    }
}
