using Hangfire.ConsoleApp;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

var context = new ApplicationDbContext();

var result = context.Autores.Include(x => x.AutoresLibros)
                                .ThenInclude(x => x.Libro)
                                .ThenInclude(x => x.Editora)
                                .Include(x => x.AutoresLibros)
                                .ThenInclude(x => x.Libro)
                                .ThenInclude(x => x.Genero)
                                .ToList();