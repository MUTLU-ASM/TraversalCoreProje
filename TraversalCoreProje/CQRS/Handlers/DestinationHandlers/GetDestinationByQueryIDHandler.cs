using DataAccessLayer.Concrete;
using TraversalCoreProje.CQRS.Queries.DestinationQueries;
using TraversalCoreProje.CQRS.Results.DestinationResults;

namespace TraversalCoreProje.CQRS.Handlers.DestinationHandlers
{
    public class GetDestinationByQueryIDHandler
    {
        Context _context;

        public GetDestinationByQueryIDHandler(Context context)
        {
            _context = context;
        }
        public GetDestinationByIDQueryResult Handle(GetDestinationByIDQuery query)
        {
            var values = _context.Destinations.Find(query);
            return new GetDestinationByIDQueryResult
            {
                DestinationId = values.DestinationId,
                City = values.City,
                Daynight = values.DayNight
            };
        }
    }
}
