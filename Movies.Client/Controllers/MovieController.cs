using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Client.Models;

namespace Movies.Client.Controllers
{
    public class MovieController : Controller
    {
        private const string base_address = "https://localhost:7286/";

        // GET: MovieController
        public async Task<ActionResult> Index()
        {
            using HttpClient client = new HttpClient() { BaseAddress = new Uri(base_address) };
            var movies = await client.GetFromJsonAsync<List<Movie>>("api/Movies");

            return View(movies);
        }

        public async Task<ActionResult> Search(string? searchString, string? orderBy, int per_page, int page)
        {
            using HttpClient client = new HttpClient() { BaseAddress = new Uri(base_address) };
            var movies = await client.GetFromJsonAsync<List<Movie>>($"api/Movies/search?" +
                $"searchString={searchString}&orderBy={orderBy}&per_page={per_page}&page={page}");

            return View("Index", movies);
        }

        // GET: MovieController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            using HttpClient client = new HttpClient() { BaseAddress = new Uri(base_address) };
            var movie = await client.GetFromJsonAsync<Movie>($"api/Movies/{id}");

            return View(movie);
        }

        // GET: MovieController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MovieController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Movie movie)
        {
            try
            {
                using HttpClient client = new HttpClient() { BaseAddress = new Uri(base_address) };
                var response = await client.PostAsJsonAsync<Movie>($"api/Movies", movie);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(movie);
            }
        }

        // GET: MovieController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            using HttpClient client = new HttpClient() { BaseAddress = new Uri(base_address) };
            var movie = await client.GetFromJsonAsync<Movie>($"api/Movies/{id}");

            return View(movie);
        }

        // POST: MovieController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Movie movie)
        {
            try
            {
                using HttpClient client = new HttpClient() { BaseAddress = new Uri(base_address) };
                var response = await client.PutAsJsonAsync<Movie>($"api/Movies/{id}", movie);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(movie);
            }
        }

        // GET: MovieController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            using HttpClient client = new HttpClient() { BaseAddress = new Uri(base_address) };
            var movie = await client.GetFromJsonAsync<Movie>($"api/Movies/{id}");

            return View(movie);
        }

        // POST: MovieController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Movie movie)
        {
            try
            {
                using HttpClient client = new HttpClient() { BaseAddress = new Uri(base_address) };
                var response = await client.DeleteFromJsonAsync<Movie>($"api/Movies/{id}");

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(movie);
            }
        }
    }
}
