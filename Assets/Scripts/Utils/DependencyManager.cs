using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Dependencies
{
    static Dictionary<string, Func< object>> dependencies = new Dictionary<string, Func<object>>();


    public static void RegisterSingleton<T>( Func<object> generator)
    {
        dependencies.Add(typeof(T).FullName, generator);
    }

    public static T Get<T>()
    {
        if (dependencies.ContainsKey(typeof(T).FullName))
            return (T) dependencies[typeof(T).FullName]?.Invoke();
        else
            return default(T);
    }

}
