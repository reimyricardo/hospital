using MediatR;

namespace Clinic.Data.Contracts;

public interface IEventHandler< in TEvent> : INotificationHandler<TEvent>
    where TEvent : IEvent
{
}
