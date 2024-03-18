using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EmployeeManagement.API
{
    public class HealthDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Paths.Add("/health", new OpenApiPathItem
            {
                Operations = new Dictionary<OperationType, OpenApiOperation>
                {
                    [OperationType.Get] = new OpenApiOperation
                    {
                        Tags = new List<OpenApiTag> { new OpenApiTag {Name = "Health"}},
                        Summary = "Check health status",
                        Description = "Check the health status for the application",
                        Responses = new OpenApiResponses
                        {
                            ["200"] = new OpenApiResponse { Description = "OK" }
                        }
                    }
                }
            });
        }
    }
}