using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using QimiaSchool1.Business.Abstracts;

namespace QimiaSchool1.Business.Implementations;

public class CacheService : ICacheService
{
    private readonly IDistributedCache _cache;
    private readonly ILogger<CacheService> _logger;

    public CacheService(IDistributedCache cache, ILogger<CacheService> logger)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<T> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        var value = await _cache.GetStringAsync(key, cancellationToken);

        if (value != null)
        {
            _logger.LogInformation("Cache successful for the key '{Key}'", key);

            return JsonSerializer.Deserialize<T>(value);
        }

        _logger.LogInformation("Cache missing for the key '{Key}'", key);

        return default;
    }

    public async Task<bool> SetAsync<T>(string key, T value, TimeSpan? expirationDate = null, CancellationToken cancellationToken = default)
    {
        var options = new DistributedCacheEntryOptions();

        if (expirationDate.HasValue)
        {
            options.AbsoluteExpirationRelativeToNow = expirationDate.Value;
        }

        var serializedValue = JsonSerializer.Serialize(value);
        await _cache.SetStringAsync(key, serializedValue, options, cancellationToken);
        _logger.LogInformation("Cache is set for the key '{Key}'", key);

        return true;
    }

    public async Task<bool> RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        var exist = await _cache.GetAsync(key, cancellationToken);

        if (exist != null)
        {
            await _cache.RemoveAsync(key, cancellationToken);
            _logger.LogInformation("Cache's been removed for the key '{Key}'", key);

            return true;
        }

        _logger.LogInformation("Cache removal's failed for the key '{Key}'", key);

        return false;
    }

}
