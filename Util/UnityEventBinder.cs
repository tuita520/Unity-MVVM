﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventBinder
{
    //TODO: Find UnityEvent Type based on event arguments

    //HACK: Currently no way to unsubscribe these bindings without destroying the whole object

    public delegate void ParamsAction(params object[] arguments);

    public static void BindEventWithArgs(object e, ParamsAction callback)
    {
        if (e as UnityEvent<string> != null)
            (e as UnityEvent<string>).AddListener(new UnityAction<string>((newval) => { callback?.Invoke(newval); }));
        else if (e as UnityEvent<int> != null)
            (e as UnityEvent<int>).AddListener(new UnityAction<int>((newVal) => { callback?.Invoke(newVal); }));
        else if (e as UnityEvent<bool> != null)
            (e as UnityEvent<bool>).AddListener(new UnityAction<bool>((newVal) => { callback?.Invoke(newVal); }));
        else if (e as UnityEvent != null)
            (e as UnityEvent).AddListener(new UnityAction(() => { callback?.Invoke(); }));
        else
        {
            Debug.LogError("Couldn't bind UnityEvent");
            return;
        }

        Debug.Log("Bound event");
    }

    public static void BindEvent(object e, Action callback)
    {
        if (e as UnityEvent<string> != null)
            (e as UnityEvent<string>).AddListener(new UnityAction<string>((newval) => { callback?.Invoke(); }));
        else if (e as UnityEvent<int> != null)
            (e as UnityEvent<int>).AddListener(new UnityAction<int>((newVal) => { callback?.Invoke(); }));
        else if (e as UnityEvent<bool> != null)
            (e as UnityEvent<bool>).AddListener(new UnityAction<bool>((newVal) => { callback?.Invoke(); }));
        else if (e as UnityEvent != null)
            (e as UnityEvent).AddListener(new UnityAction(() => { callback?.Invoke(); }));
        else
        {
            Debug.LogError("Couldn't bind UnityEvent");
            return;
        }

    }
}