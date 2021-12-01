
using System;
using System.Collections;
using System.Collections.Generic;

public static class ReflectionHelpers
{
    public static Type[] GetAllDerivedTypes(this AppDomain aAppDomain, Type aType)
    {
        var result = new List<Type>();
        var assemblies = aAppDomain.GetAssemblies();
        foreach (var assembly in assemblies)
        {
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (type.IsSubclassOf(aType))
                    result.Add(type);

            }
        }
        return result.ToArray();
    }

    public static Type[] GetAllDerivedTypes<T>(this AppDomain aAppDomain)
    {
        return GetAllDerivedTypes(aAppDomain, typeof(T));
    }

    public static Type[] GetTypesWithInterface(this AppDomain aAppDomain, Type aInterfaceType)
    {
        var result = new List<Type>();
        var assemblies = aAppDomain.GetAssemblies();
        foreach (var assembly in assemblies)
        {
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (aInterfaceType.IsAssignableFrom(type))
                    result.Add(type);
            }
        }
        return result.ToArray();
    }

    public static Type[] GetTypesWithInterface<T>(this AppDomain aAppDomain)
    {
        return GetTypesWithInterface(aAppDomain, typeof(T));
    }
}