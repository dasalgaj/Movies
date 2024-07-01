using Movies.Data.Models;

namespace Movies.Data.Interfaces
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetAll();
        Movie GetMovieByID(int id);
        Movie InsertMovie(Movie movie);
        Movie UpdateMovie(Movie movie);
        Movie DeleteMovie(int id);

        IEnumerable<Movie> QueryStringFilter(string searchString, string orderby, int per_page, int page);
    }
}
