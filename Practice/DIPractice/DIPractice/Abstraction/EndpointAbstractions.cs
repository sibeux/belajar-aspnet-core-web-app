using DIPractice.Abstraction;

public static class EndpointExtensions
{
    public static void MapAllEndpoints(this WebApplication app)
    {
        var endpointTypes = typeof(IEndpoint)
            .Assembly
            .GetTypes()
            .Where(t => typeof(IEndpoint).IsAssignableFrom(t) && t.IsClass);

        foreach (var type in endpointTypes)
        {
            var endpoint = (IEndpoint)Activator.CreateInstance(type)!;
            endpoint.Map(app);
        }
    }
}
