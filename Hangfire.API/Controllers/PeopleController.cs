using Hangfire.API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hangfire.API.Controllers;

[ApiController]
[Route("api/people")]
public class PeopleController : ControllerBase
{
    private readonly ApplicationDbContext context;
    private readonly IBackgroundJobClient _backgroundJobClient;

    public PeopleController(ApplicationDbContext context,IBackgroundJobClient backgroundJobClient)
    {
        this.context = context;
        _backgroundJobClient = backgroundJobClient;
    }

    [HttpPost("create")]
    public async Task<ActionResult> Create(string personName)
    {
        //_backgroundJobClient.Enqueue(() => Console.WriteLine(personName));

        _backgroundJobClient.Enqueue(() => CreatePerson(personName));

      
        return Ok();
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [NonAction]
    public async Task CreatePerson(string personName)
    {
        Console.WriteLine($"Adding person {personName}");
        var person = new Person { Name = personName };
        context.Add(person);
        await Task.Delay(5000);
        await context.SaveChangesAsync();
        Console.WriteLine($"Added the person {personName}");
    }
}
