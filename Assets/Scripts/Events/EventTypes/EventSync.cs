using System;
using System.Collections.Generic;

public class EventSync
{
    private HashSet<Action> subscribers = new HashSet<Action>();
    public void Publish()
    {
        foreach (var subscriber in this.subscribers)
        {
            subscriber();
        }
    }
    public void Subscribe(Action at)
    {
        if (!this.subscribers.Contains(at))
        {
            this.subscribers.Add(at);
        }
    }
    public void Unsubscribe(Action at)
    {
        if (this.subscribers.Contains(at))
        {
            this.subscribers.Remove(at);
        }
    }
}
public class EventSync<T>
{
    public HashSet<Action<T>> subscribers = new HashSet<Action<T>>();
    public void Publish(T t)
    {
        foreach (var subscriber in this.subscribers)
        {
            subscriber(t);
        }
    }
    public void Subscribe(Action<T> at)
    {
        if (!this.subscribers.Contains(at))
        {
            this.subscribers.Add(at);
        }
    }
    public void Unsubscribe(Action<T> at)
    {
        if (this.subscribers.Contains(at))
        {
            this.subscribers.Remove(at);
        }
    }
}
public class EventSync<T, U>
{
    public HashSet<Action<T, U>> subscribers = new HashSet<Action<T, U>>();
    public void Publish(T t, U u)
    {
        foreach (var subscriber in subscribers)
        {
            subscriber(t, u);
        }
    }
    public void Subscribe(Action<T, U> at)
    {
        if (!this.subscribers.Contains(at))
        {
            this.subscribers.Add(at);
        }
    }
    public void Unsubscribe(Action<T, U> at)
    {
        if (this.subscribers.Contains(at))
        {
            this.subscribers.Remove(at);
        }
    }
}
public class EventSync<T, U, V>
{
    public HashSet<Action<T, U, V>> subscribers = new HashSet<Action<T, U, V>>();
    public void Publish(T t, U u, V v)
    {
        foreach (var subscriber in subscribers)
        {
            subscriber(t, u, v);
        }
    }
    public void Subscribe(Action<T, U, V> at)
    {
        if (!this.subscribers.Contains(at))
        {
            this.subscribers.Add(at);
        }
    }
    public void Unsubscribe(Action<T, U, V> at)
    {
        if (this.subscribers.Contains(at))
        {
            this.subscribers.Remove(at);
        }
    }
}
public class EventSync<T, U, V, W>
{
    public HashSet<Action<T, U, V, W>> subscribers = new HashSet<Action<T, U, V, W>>();
    public void Publish(T t, U u, V v, W w)
    {
        foreach (var subscriber in subscribers)
        {
            subscriber(t, u, v, w);
        }
    }
    public void Subscribe(Action<T, U, V, W> at)
    {
        if (!this.subscribers.Contains(at))
        {
            this.subscribers.Add(at);
        }
    }
    public void Unsubscribe(Action<T, U, V, W> at)
    {
        if (this.subscribers.Contains(at))
        {
            this.subscribers.Remove(at);
        }
    }
}