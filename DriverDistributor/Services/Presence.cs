using System.Collections.Concurrent;

namespace DriverDistributor.Services;

public class Presence
{
    private readonly ConcurrentDictionary<string, DateTime> _lastSeen = new();

    public async Task UpdateLastSeen(string username)
    {
        _lastSeen[username] = DateTime.UtcNow;
    }

    public async Task<DateTime?> GetLastSeen(string username)
    {
        return _lastSeen.TryGetValue(username, out var lastSeen) ? lastSeen : null;
    }

    public Dictionary<string, DateTime> GetAllLastSeen() => _lastSeen.ToDictionary(x => x.Key, x => x.Value);
}
