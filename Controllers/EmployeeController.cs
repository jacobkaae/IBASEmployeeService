namespace IBASEmployeeService.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using IBASEmployeeService.Models;
    using Microsoft.Azure.Cosmos;
    using IBASEmployeeService.Services;

    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        public EmployeeController(ILogger<EmployeeController> logger)
        {
            _logger = logger;
        }

        private readonly ICosmosDbService _cosmosDbService;
        public EmployeeController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        [HttpGet(Name = "API")]
        public async IAsyncEnumerable<Employee> Get()
        {
            return await _cosmosDbService.GetItemsAsync("SELECT * FROM c"));

        }

        [HttpGet(Name = "GetEmployees")]
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