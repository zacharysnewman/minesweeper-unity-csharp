using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class EventAsync
{
    public HashSet<Action> subscribers = new HashSet<Action>();

    public async Task Publish()
    {
        foreach (var subscriber in this.subscribers)
        {
            await Task.Run(() => subscriber());
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
public class EventAsync<T>
{
    public HashSet<Action<T>> subscribers = new HashSet<Action<T>>();
    public async Task Publish(T t)
    {
        foreach (var subscriber in this.subscribers)
        {
            await Task.Run(() => subscriber(t));
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
public class EventAsync<T, U>
{
    public HashSet<Action<T, U>> subscribers = new HashSet<Action<T, U>>();
    public async Task Publish(T t, U u)
    {
        foreach (var subscriber in subscribers)
        {
            await Task.Run(() => subscriber(t, u));
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
public class EventAsync<T, U, V>
{
    public HashSet<Action<T, U, V>> subscribers = new HashSet<Action<T, U, V>>();
    public async Task Publish(T t, U u, V v)
    {
        foreach (var subscriber in subscribers)
        {
            await Task.Run(() => subscriber(t, u, v));
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
public class EventAsync<T, U, V, W>
{
    public HashSet<Action<T, U, V, W>> subscribers = new HashSet<Action<T, U, V, W>>();
    public async Task Publish(T t, U u, V v, W w)
    {
        foreach (var subscriber in subscribers)
        {
            await Task.Run(() => subscriber(t, u, v, w));
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