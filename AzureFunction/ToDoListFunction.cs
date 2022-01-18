using AzureFunction.Autofac;
using AzureFunctions.Autofac;
using Domain;
using Domain.Interfaces;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace AzureFunction
{
    /// <summary>
    /// Autofac is used for parameter injection so that the endpoint methods can stay static
    /// and will remain stateless as designed.
    /// </summary>
    [DependencyInjectionConfig(typeof(DependencyInjectionInitializer))]
    public static class ToDoListFunction
    {
        [FunctionName(nameof(Get))]
        public static async Task<IActionResult> Get(
            [HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = null)] HttpRequest req,
            [Inject] IToDoService toDoService,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var toDoList = await toDoService.GetAll();

            return new OkObjectResult(toDoList);
        }

        [FunctionName(nameof(GetById))]
        public static async Task<IActionResult> GetById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "GetById/{id}")] HttpRequest req,
            string id,
            [Inject] IToDoService toDoService,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            ToDo result = await toDoService.GetById(id);

            return new OkObjectResult(result);
        }

        [FunctionName(nameof(Create))]
        public static async Task<IActionResult> Create(
            [HttpTrigger(AuthorizationLevel.Anonymous, "POST", Route = null)] HttpRequest req,
            [Inject] IToDoService toDoService,
            ILogger log)
        {
            ToDoDto dto = new ToDoDto();
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            try
            {
                dto = JsonConvert.DeserializeObject<ToDoDto>(requestBody);
            }
            catch
            {
                return new BadRequestResult();
            }

            var response = await toDoService.Create(dto);

            return new CreatedResult($"GetById{response.Id}", response);
        }

        [FunctionName(nameof(Update))]
        public static async Task<IActionResult> Update(
            [HttpTrigger(AuthorizationLevel.Anonymous, "PUT", Route = null)] HttpRequest req,
            [Inject] IToDoService toDoService,
            ILogger log)
        {
            ToDoDto dto = new ToDoDto();
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var isDeserialized = requestBody.Deserialize(out dto);

            if (!isDeserialized)
            {
                return new BadRequestResult();
            }

            var isUpdated = await toDoService.Update(dto);

            if (!isUpdated)
            {
                return new NotFoundResult();
            }

            var response = await toDoService.GetById(dto.Id);

            return new AcceptedResult($"GetById{dto.Id}", response);
        }

        [FunctionName(nameof(Delete))]
        public static async Task<IActionResult> Delete(
            [HttpTrigger(AuthorizationLevel.Anonymous, "DELETE", Route = "Delete/{id}")] HttpRequest req,
            string id,
            [Inject] IToDoService toDoService,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var isDeleted = await toDoService.Delete(id);

            if (!isDeleted)
            {
                return new NotFoundResult();
            }

            return new OkResult();
        }
    }
}
