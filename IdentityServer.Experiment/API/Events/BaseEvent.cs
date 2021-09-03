using System;

namespace Application.Core.Events
{
	public abstract class BaseEvent
	{
		public DateTimeOffset CreatedAt { get; protected set; } = DateTimeOffset.UtcNow;
	}
}
