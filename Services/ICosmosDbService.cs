using System;
namespace IBASEmployeeService.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using IBASEmployeeService.Models;

    public interface ICosmosDbService
    {
        Task<IEnumerable<Employee>> GetItemsAsync(string query);
        Task<Employee> GetItemAsync(string id);
        Task AddItemAsync(Employee employee);
        Task UpdateItemAsync(string id, Employee employee);
        Task DeleteItemAsync(string id);
    }
}
