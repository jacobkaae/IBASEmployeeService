namespace IBASEmployeeService.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using IBASEmployeeService.Models;
    using Microsoft.Azure.Cosmos;
    using IBASEmployeeService.Services;
    using System.ComponentModel;
    using Microsoft.Azure.Cosmos.Linq;
    using System.Threading.Tasks;
    using Microsoft.Azure.Cosmos;
    using Container = Microsoft.Azure.Cosmos.Container;

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

        [HttpGet("/henvendelser")]
        public async Task<List<Henvendelse>> GetTestAsync()
        {
            using (CosmosClient cosmosClient = new CosmosClient("https://ibas-db-account-21919.documents.azure.com:443/", "PcwW3yzebGOlhjkAjpxWfmL1jJ4x6WY9EH6wPx6RunhPR0WR9ugFocqMfEmio59XwSPaHBS5JUxSQ7MP6VYQsg=="))
            {
                Container container = cosmosClient.GetContainer("IBasSupportDB", "ibassupport");

                using FeedIterator<Henvendelse> feed = container.GetItemQueryIterator<Henvendelse>(
                 queryText: "SELECT * FROM C");

                List<Henvendelse> list = new List<Henvendelse>();

                while (feed.HasMoreResults)
                {
                    FeedResponse<Henvendelse> response = await feed.ReadNextAsync();

                    // Iterate query results
                    foreach (Henvendelse item in response)
                    {
                        list.Add(item);
                    }
                }

                return list;
            }
        }


        [HttpPost("/add")]
        public async void Post(HenvendelseDTO test)
        {
            using (CosmosClient cosmosClient = new CosmosClient("https://ibas-db-account-21919.documents.azure.com:443/", "PcwW3yzebGOlhjkAjpxWfmL1jJ4x6WY9EH6wPx6RunhPR0WR9ugFocqMfEmio59XwSPaHBS5JUxSQ7MP6VYQsg=="))
            {
                Container container = cosmosClient.GetContainer("IBasSupportDB", "ibassupport");

                Henvendelse nyHenvendelse = new Henvendelse { Id = Guid.NewGuid().ToString(), Beskrivelse = test.Beskrivelse, Bruger = test.Bruger, Dato = test.Dato, Kategori = test.Kategori };

                var tempPartitionKey = new PartitionKey(test.Kategori);

                ItemResponse<Henvendelse> henvendelseResponse = await container.CreateItemAsync<Henvendelse>(nyHenvendelse, tempPartitionKey);
            }
        }

        [HttpGet("/employees")]
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