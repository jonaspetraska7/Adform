using Common.Entities;

namespace Common.Interfaces
{
    public interface ISquareService
    {
        /// <summary>
        /// Method for updating squares for a given pointlist
        /// </summary>
        /// <param name="pointList">A pointlist to calculate squares for</param>
        /// <returns>Pointlist with updated squares property</returns>
        PointList UpdatePointListSquares(PointList pointList, CancellationToken cancellationToken = default);

        /// <summary>
        /// Source : ChatGPT
        /// Input : C# algorithm to get all possible squares from a list of points with best time complexity
        /// Explanation :
        /// This method works by first finding all possible midpoints between every pair of points, 
        /// and grouping them based on the slope of the line connecting them. Then, for each group of midpoints 
        /// with the same slope, the method checks all pairs of midpoints to see if they form the midpoints of a square 
        /// by checking if the other two points that complete the square are also in the list of points. 
        /// If a square is found, it is added to the list of squares. 
        /// This solution has a time complexity of O(n^2) where n is the number of points.
        /// </summary>
        /// <param name="process"></param>
        /// <param name="processSize"></param>
        /// <returns>A list of squares</returns>
        List<Square> GetSquares(int process, int processSize, CancellationToken cancellationToken = default);
    }
}
