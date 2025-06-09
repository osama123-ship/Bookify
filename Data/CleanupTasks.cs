namespace Bookify.Data;

public class CleanupTasks
{
    private readonly Context _context;

    public CleanupTasks(Context context)
    {
        _context = context;
    }

    public void RemoveExpiredTemporaryBookings()
    {
        var limit = DateTime.UtcNow.AddMinutes(-10);
        var expired = _context.Temporaries.Where(b => b.ReservedAt < limit).ToList();

        if (expired.Any())
        {
            _context.Temporaries.RemoveRange(expired);
            _context.SaveChangesAsync();
        }
    }
}

