﻿using Common.Entities;

namespace Common.Interfaces
{
    public interface IPointService
    {
        /// <summary>
        /// Method for inserting a pointlist to the database
        /// </summary>
        /// <param name="point">A pointlist object to insert</param>
        /// <returns>A status</returns>
        Task<int> InsertPointList(PointList point);

        /// <summary>
        /// Method for getting a pointlist from the database
        /// </summary>
        /// <param name="id">ID of the pointlist to get</param>
        /// <returns>A Pointlist object or null</returns>
        Task<PointList?> GetPointList(Guid? id);

        /// <summary>
        /// Method for deleting a pointlist from the database
        /// </summary>
        /// <param name="id">ID of the pointlist to delete</param>
        /// <returns>A status</returns>
        Task<int> DeletePointList(Guid id);

        /// <summary>
        /// Method for updating a pointlist in the database
        /// </summary>
        /// <param name="pointList">Pointlist to update</param>
        /// <returns>A status</returns>
        public Task<int> UpdatePointList(PointList pointList);

        /// <summary>
        /// Method for inserting a point to a pointlist
        /// </summary>
        /// <param name="point">Point object to insert</param>
        /// <param name="pointListId">ID of the pointlist to add the point to</param>
        /// <returns>A status</returns>
        Task<int> InsertPoint(Point point, Guid pointListId);

        /// <summary>
        /// Method for deleting a point from a pointlist
        /// </summary>
        /// <param name="point">Point object to delete</param>
        /// <param name="pointListId">ID of the pointlist to delete the point from</param>
        /// <returns>A status</returns>
        Task<int> DeletePoint(Point point, Guid pointListId);

        /// <summary>
        /// Method for getting a list of squares (Each square is a list of points)
        /// </summary>
        /// <param name="pointListId">ID of the pointlist to get the squares from</param>
        /// <returns>A list of a list of points</returns>
        Task<List<List<Point>>> GetSquares(Guid pointListId);
    }
}