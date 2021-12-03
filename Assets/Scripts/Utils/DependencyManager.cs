using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Dependencies
{
    static Dictionary<string, object> dependencies = new Dictionary<string, object>();


    public static void RegisterSingleton<T>(string key, T value)
    {
        dependencies.Add(typeof(T).FullName, value);
    }

    public static T Get<T>()
    {
        if (dependencies.ContainsKey(typeof(T).FullName))
            return (T)dependencies[typeof(T).FullName];
        else
            return default(T);
    }

}
