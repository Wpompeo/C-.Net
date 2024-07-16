using Journey.Exception.ExceptionsBase;
using Journey.Exception;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Activities.Delete;

public class DeleteActivityForTripUseCase
{

    public void Execute(Guid triId, Guid activityId)
    {
        var dbContext = new JourneyDbContext();

        var activity = dbContext
            .Activities
            .FirstOrDefault(activity => activity.Id == activityId && activity.TripId == triId);

        if (activity is null)
        {
            throw new DirectoryNotFoundException(ResourceErrorMessage.ACTIVITY_NOT_FOUND);
        }

        dbContext .Activities.Remove(activity);
        dbContext .SaveChanges();
    }
}
