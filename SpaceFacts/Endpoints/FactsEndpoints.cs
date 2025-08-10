using SpaceFacts.Dtos;

namespace SpaceFacts.Endpoints
{
    public static class FactsEndpoints
    {
        public static RouteGroupBuilder MapFactsEndpoints(this WebApplication app, List<FactDto> facts)
        {
            var group = app.MapGroup("/facts");

            group.MapGet("/random", () =>
            {
                var random = new Random();
                int index = random.Next(facts.Count);
                return facts[index];
            });

            return group;

        }

    }
}
