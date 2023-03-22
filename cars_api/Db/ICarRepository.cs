using cars_api.Models;

namespace cars_api.Db
{
    /// <summary>
    /// Car Data 
    /// </summary>
    public interface ICarRepository
    {
        /// <summary>
        /// CreateAsync car
        /// </summary>
        /// <param name="car">Car</param>
        Task CreateAsync(Car car);

        /// <summary>
        /// Removes  <see cref="Car"/> from db
        /// </summary>
        /// <param name="id">Car ID</param>
        /// <returns><see langword="true"/> if the <see cref="Car"/> is removed, otherwise <see langword="false"/>.</returns>
        Task<bool> DeleteAsync(int id);


        /// <summary>
        /// GetAsync  <see cref="Car"/> by Id
        /// </summary>
        /// <param name="id"> <see cref="Car"/> id</param>
        /// <returns>returns  <see cref="Car"/> if found, otherwise  <see langword="null"/>.</returns>
        Task<Car?> GetAsync(int id);

        /// <summary>
        /// GetAsync all <see cref="Car"/>'s.
        /// </summary>
        /// <returns>Returns <see cref="List{Car}"/>.</returns>
        Task<IEnumerable<Car>> GetCarsAsync(int page = 1, int count = 100);

        /// <summary>
        /// Getting a list of <see cref="Car"/>'s in range.
        /// </summary>
        /// <param name="start">Start index</param>
        /// <param name="end">End index</param>
        // <returns>Returns <see cref="List{Car}"/>.</returns>

        Task<IEnumerable<Car>> GetCarsRangeAsync( int start, int end);

        /// <summary>
        /// UpdateAsync <see cref="Car"/>
        /// </summary>
        /// <param name="car">New <see cref="Car"/>New <see cref="Car"/></param>
        /// <param name="id">New <see cref="Car"/>Id of <see cref="Car"/></param>
        Task UpdateAsync(int id, Car car);
    }
}
