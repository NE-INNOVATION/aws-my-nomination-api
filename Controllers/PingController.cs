
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using my_nomination_api.models;
using System.Net;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PingController : Controller
{

    private IMyNominationDatabaseSettings _settings = null;
    public PingController(IMyNominationDatabaseSettings settings)
    {
        _settings = settings;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var client = new MongoClient(System.Environment.GetEnvironmentVariable("ConnectionString") ?? _settings.ConnectionString);
            var database = client.GetDatabase(System.Environment.GetEnvironmentVariable("DatabaseName") ?? _settings.DatabaseName);

            if (database != null)
            {
                return Ok(System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
            }
            return BadRequest();

        }
        catch (System.Exception)
        {
            return BadRequest();           
        }
      
    }
}
