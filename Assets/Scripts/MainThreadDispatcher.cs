using System;
using System.Threading;
using UnityEngine;

public class MainThreadDispatcher : MonoBehaviour
{
    private static SynchronizationContext context;

    void Awake()
    {
        context = SynchronizationContext.Current;
    }

    public static void RunOnMainThread(Action action)
    {
        context.Post(_ => action(), null);
    }
}
