using Common.Entities;

namespace Common.Interfaces
{
    public interface ISquareService
    {
        /// <summary>
        /// Method for calculating squares for a given pointlist
        /// </summary>
        /// <param name="points">A pointlist to calculate squares for</param>
        /// <returns>Pointlist with updated squares property</returns>
        PointList CalculateSquares(PointList points);
    }
}
