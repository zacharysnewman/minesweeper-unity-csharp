using System.Collections;
using System.Collections.Generic;

public class EventCoroutine
{
    public HashSet<IEnumerator> subscribers = new HashSet<IEnumerator>();
    public void Publish()
    {
        foreach (var subscriber in this.subscribers)
        {
            CoroutineBehaviour.Self.StartCoroutine(subscriber);
        }
    }
    public void Subscribe(IEnumerator coroutine)
    {
        if (!this.subscribers.Contains(coroutine))
        {
            this.subscribers.Add(coroutine);
        }
    }
    public void Unsubscribe(IEnumerator coroutine)
    {
        if (this.subscribers.Contains(coroutine))
        {
            this.subscribers.Remove(coroutine);
        }
    }
}