
namespace QimiaSchool1.DataAccess.MessageBroker.Abstractions;

public interface IEventBus
{
    Task PublishAsync<T>(T message, CancellationToken cancellationToken = default)
        where T : class;

}
