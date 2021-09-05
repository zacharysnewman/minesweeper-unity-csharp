using System;
using System.Collections.Generic;

public static class EventAggregator
{
    private static Dictionary<Type, object> subscribers = new Dictionary<Type, object>();
    public static TEventType Get<TEventType>() where TEventType : new()
    {
        var eventType = typeof(TEventType);
        if (!EventAggregator.subscribers.ContainsKey(eventType))
        {
            EventAggregator.subscribers.Add(eventType, new TEventType());
        }
        return (TEventType)subscribers[eventType];
    }
}