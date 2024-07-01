using Movies.Data.Interfaces;
using Movies.Data.Models;

namespace Movies.Data.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MoviesContext _context;

        public MovieRepository(MoviesContext context)
        {
            _context = context;
        }

        public Movie DeleteMovie(int id)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.Id == id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                _context.SaveChanges();
                return movie;
            }

            return null;
        }

        public IEnumerable<Movie> GetAll()
        {
            return _context.Movies.ToList();
        }

        public Movie GetMovieByID(int id)
        {
            return _context.Movies.FirstOrDefault(m => m.Id == id);
        }

        public Movie InsertMovie(Movie movie)
        {
            var result = _context.Movies.Add(movie);
            _context.SaveChanges();

            return result.Entity;
        }

        public Movie UpdateMovie(Movie movie)
        {
            var movie_to_update = _context.Movies.FirstOrDefault(m => m.Id == movie.Id);
            if (movie_to_update == null) return null;

            movie_to_update.Title = movie.Title;
            movie_to_update.Genre = movie.Genre;
            movie_to_update.Description = movie.Description;
            movie_to_update.ReleaseYear = movie.ReleaseYear;
            _context.SaveChanges();

            return movie_to_update;
        }

        public IEnumerable<Movie> QueryStringFilter(string searchString, string orderby, int per_page, int page)
        {
            var filtered_movies = _context.Movies.ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                filtered_movies = filtered_movies.Where(m => m.Title.Contains(searchString, StringComparison.CurrentCultureIgnoreCase) ||
                m.Genre.Contains(searchString, StringComparison.CurrentCultureIgnoreCase) ||
                m.ReleaseYear.Contains(searchString, StringComparison.CurrentCultureIgnoreCase) ||
                m.Description.Contains(searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }

            // data example: orderby = "title asc"
            if (!string.IsNullOrEmpty (orderby))
            {
                orderby = orderby.ToLower();
                var order_by = orderby.Split(" ");
                string order_property = order_by[0];
                string order_direction = order_by[1];

                switch (order_property)
                {
                    case "title":
                        if (order_direction == "asc") filtered_movies = filtered_movies.OrderBy(m => m.Title).ToList();
                        if (order_direction == "desc") filtered_movies = filtered_movies.OrderByDescending(m => m.Title).ToList();
                        break;
                    case "genre":
                        if (order_direction == "asc") filtered_movies = filtered_movies.OrderBy(m => m.Genre).ToList();
                        if (order_direction == "desc") filtered_movies = filtered_movies.OrderByDescending(m => m.Genre).ToList();
                        break;
                    case "description":
                        if (order_direction == "asc") filtered_movies = filtered_movies.OrderBy(m => m.Description).ToList();
                        if (order_direction == "desc") filtered_movies = filtered_movies.OrderByDescending(m => m.Description).ToList();
                        break;
                    case "releaseyear":
                        if (order_direction == "asc") filtered_movies = filtered_movies.OrderBy(m => m.ReleaseYear).ToList();
                        if (order_direction == "desc") filtered_movies = filtered_movies.OrderByDescending(m => m.ReleaseYear).ToList();
                        break;
                }
            }

            if (per_page > 0 && page > 0)
            {
                filtered_movies = filtered_movies.Skip(per_page * (page - 1)).Take(per_page).ToList();
            }

            return filtered_movies;
        }
    }
}