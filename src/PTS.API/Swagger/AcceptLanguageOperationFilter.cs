using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PTS.API.Swagger;

public sealed class AcceptLanguageOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= [];

        if (operation.Parameters.Any(parameter =>
                parameter.In == ParameterLocation.Header
                && string.Equals(parameter.Name, "Accept-Language", StringComparison.OrdinalIgnoreCase)))
        {
            return;
        }

        operation.Parameters.Add(
            new OpenApiParameter
            {
                Name = "Accept-Language",
                In = ParameterLocation.Header,
                Required = false,
                Description = "Response language. Use `ne` for Nepali or `en` for English.",
                Schema = new OpenApiSchema
                {
                    Type = JsonSchemaType.String,
                },
            }
        );
    }
}
