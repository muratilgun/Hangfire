using Hangfire.API.Entities;
using Hangfire.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hangfire.API.Controllers;

[ApiController]
[Route("api/people")]
public class PeopleController : ControllerBase
{
    private readonly ApplicationDbContext context;
    private readonly IBackgroundJobClient _backgroundJobClient;

    public PeopleController(ApplicationDbContext context, IBackgroundJobClient backgroundJobClient)
    {
        this.context = context;
        _backgroundJobClient = backgroundJobClient;
    }

    [HttpPost("create")]
    public ActionResult Create(string personName)
    {
        //_backgroundJobClient.Enqueue(() => Console.WriteLine(personName));

        _backgroundJobClient
            .Enqueue<IPeopleRepository>(repository => repository.CreatePerson(personName));
        return Ok();
    }
    [HttpPost("schedule")]
    public ActionResult Schedule(string personName)
    {
        var jobId = _backgroundJobClient.Schedule(() => Console.WriteLine("The name is " + personName),
             TimeSpan.FromSeconds(10));
        _backgroundJobClient.ContinueWith(jobId, () => Console.WriteLine($"The job {jobId} has finished"));
        return Ok();
    }
}
