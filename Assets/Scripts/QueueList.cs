using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueList<T> : List<T> {
    public QueueList() : base() { }
    public QueueList(int capacity) : base(capacity) { }

    public void Enqueue(T item) {
        Add(item);
    }

    public T Dequeue() {
        var ret = this[0];
        for (int i = 0; i < Count - 1; i++) {
            this[i] = this[i + 1];
        }
        RemoveAt(Count - 1);
        return ret;
    }
}