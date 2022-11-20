namespace IBASEmployeeService.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using IBASEmployeeService.Models;
    using Microsoft.Azure.Cosmos;
    using IBASEmployeeService.Services;
    using System.ComponentModel;
    using Microsoft.Azure.Cosmos.Linq;
    using System.Threading.Tasks;

    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ICosmosDbService _cosmosDbService;

        //public EmployeeController(ICosmosDbService cosmosDbService)
        //{
        //    _cosmosDbService = cosmosDbService;
        //}

        //[HttpGet("hej")]
        //public async En Task<IActionResult> Index()
        //{
        //    return await _cosmosDbService.GetItemsAsync("SELECT * FROM c");
        //}

        [HttpGet("/test")]
        public async Task<IEnumerable<Employee>> GetTestAsync()
        {
            using CosmosClient client = new(
                accountEndpoint: Environment.GetEnvironmentVariable("https://ibas-db-account-21919.documents.azure.com:443/"),
                authKeyOrResourceToken: Environment.GetEnvironmentVariable("PcwW3yzebGOlhjkAjpxWfmL1jJ4x6WY9EH6wPx6RunhPR0WR9ugFocqMfEmio59XwSPaHBS5JUxSQ7MP6VYQsg=="));
            var db = client.GetDatabase("IBasSupportDB");
            var container = db.GetContainer("ibassupport");

            var q = container.GetItemLinqQueryable<Employee>();
            var iterator = q.ToFeedIterator();
            var results = await iterator.ReadNextAsync();
            return results;
        }



        [HttpGet("/")]
        public IEnumerable<Employee> Get()
        {
            var employees = new List<Employee>() {
            new Employee() {
                Id = "24",
                Name = "Mette Bangsbo",
                Email = "meba@ibas.dk",
                Department = new Department() {
                    Id = 1,
                    Name = "Salg"
                }
            },
            new Employee() {
                Id = "22",
                Name = "Hans Merkel",
                Email = "hame@ibas.dk",
                Department = new Department() {
                    Id = 2,
                    Name = "Support"
                }
            },
            new Employee() {
                Id = "23",
                Name = "Karsten Mikkelsen",
                Email = "kami@ibas.dk",
                Department = new Department() {
                    Id = 2,
                    Name = "Support"
                }
            },
            new Employee() {
                Id = "26",
                Name = "Anders Gammelmand",
                Email = "anders@gråguld.dk",
                Department = new Department() {
                    Id = 1,
                    Name = "Alderdom"
                }
            },
            new Employee() {
                Id = "24",
                Name = "Jacob Ungmand",
                Email = "jacob@hvidguld.dk",
                Department = new Department() {
                    Id = 3,
                    Name = "Ungdom"
                }
            }
        };
            return employees;
        }
    }


}