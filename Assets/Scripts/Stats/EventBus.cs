using System;
using System.Collections.Generic;

public static class EventBus
{
    private static Dictionary<Type, Delegate> _subscribers = new();

    public static void Subscribe<T>(Action<T> callback)
    {
        var type = typeof(T);

        if (_subscribers.TryGetValue(type, out var existing))
            _subscribers[type] = Delegate.Combine(existing, callback);
        else
            _subscribers[type] = callback;
    }

    public static void Unsubscribe<T>(Action<T> callback)
    {
        var type = typeof(T);

        if (!_subscribers.TryGetValue(type, out var existing))
            return;

        var current = Delegate.Remove(existing, callback);

        if (current == null)
            _subscribers.Remove(type);
        else
            _subscribers[type] = current;
    }

    public static void Publish<T>(T eventData)
    {
        var type = typeof(T);

        if (_subscribers.TryGetValue(type, out var del))
        {
            ((Action<T>)del)?.Invoke(eventData);
        }
    }
}