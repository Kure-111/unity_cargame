﻿using System.Collections.Generic;

public class LimitedQueue<T> : Queue<T>
{
    private int limit = -1;

    public int Limit
    {
        get { return limit; }
        set { limit = value; }
    }

    public LimitedQueue(int limit)
        : base(limit)
    {
        this.Limit = limit;
    }

    public new void Enqueue(T item)
    {
        if (this.Count >= this.Limit)
        {
            this.Dequeue();
        }
        base.Enqueue(item);
    }
}