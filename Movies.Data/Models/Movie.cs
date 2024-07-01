using System;
using System.Collections.Generic;

namespace Movies.Data.Models;

public partial class Movie
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Genre { get; set; }

    public string? Description { get; set; }

    public string? ReleaseYear { get; set; }
}
