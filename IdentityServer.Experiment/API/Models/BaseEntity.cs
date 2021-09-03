using Application.Core.Events;
using System.Collections.Generic;

namespace Application.Core.Entities
{
	public abstract class BaseEntity<T>
	{
		public T Id { get; set; }

		public List<BaseEvent> Events = new List<BaseEvent>();
	}
}
