using System.Reflection;

namespace FluffyVoid.Utilities;

/// <summary>
///     Utility class for all things reflection
/// </summary>
public static class ReflectionUtility
{
    /// <summary>
    ///     Searches the loaded assemblies for classes that have the type of attribute assigned to them
    /// </summary>
    /// <param name="inherit">
    ///     Whether the search should include classes that inherit from a class that has the attribute
    ///     attached
    /// </param>
    /// <param name="namespaceFilter">Optional namespace filter to help narrow down the search of attributes</param>
    /// <typeparam name="TAttributeType">The type of attribute to find within the loaded assemblies</typeparam>
    /// <returns>An enumerable list of found classes with the attribute assigned</returns>
    public static IEnumerable<Type> GetAttributedClasses<TAttributeType>(
        bool inherit, string namespaceFilter = "")
        where TAttributeType : Attribute
    {
        bool isFiltering = !string.IsNullOrEmpty(namespaceFilter);
        IEnumerable<Type> foundTypes =
            from a in AppDomain.CurrentDomain.GetAssemblies()
            from t in a.GetTypes().Where(x => namespaceFilter != null &&
                                                x.IsClass &&
                                                (!isFiltering ||
                                                 (!string
                                                      .IsNullOrEmpty(x
                                                          .Namespace) &&
                                                  x.Namespace
                                                   .StartsWith(namespaceFilter))))
            let attributes =
                t?.GetCustomAttributes(typeof(TAttributeType), inherit) as
                    TAttributeType[]
            where attributes != null && attributes.Length > 0
            select t;

        foreach (Type currentType in foundTypes)
        {
            if (currentType != null!)
            {
                yield return currentType;
            }
        }
    }

    /// <summary>
    ///     Searches the loaded assemblies for attributes that have been assigned to classes
    /// </summary>
    /// <param name="inherit">
    ///     Whether the search should include classes that inherit from a class that has the attribute
    ///     attached
    /// </param>
    /// <param name="namespaceFilter">Optional namespace filter to help narrow down the search of attributes</param>
    /// <typeparam name="TAttributeType">The type of attribute to find within the loaded assemblies</typeparam>
    /// <returns>An enumerable list of found attribute instances</returns>
    public static IEnumerable<TAttributeType> GetAttributes<TAttributeType>(
        bool inherit, string namespaceFilter = "")
        where TAttributeType : Attribute
    {
        bool isFiltering = !string.IsNullOrEmpty(namespaceFilter);
        IEnumerable<TAttributeType[]> foundTypes =
            from a in AppDomain.CurrentDomain.GetAssemblies()
            from t in a.GetTypes().Where(x => namespaceFilter != null &&
                                                x.IsClass &&
                                                (!isFiltering ||
                                                 (!string
                                                      .IsNullOrEmpty(x
                                                          .Namespace) &&
                                                  x.Namespace
                                                   .StartsWith(namespaceFilter))))
            let attributes =
                t?.GetCustomAttributes(typeof(TAttributeType), inherit) as
                    TAttributeType[]
            where attributes != null && attributes.Length > 0
            select attributes;

        foreach (TAttributeType[] currentType in foundTypes)
        {
            if (currentType != null!)
            {
                foreach (TAttributeType? currentAttribute in currentType)
                {
                    if (currentAttribute != null!)
                    {
                        yield return currentAttribute;
                    }
                }
            }
        }
    }
    /// <summary>
    ///     Finds all the fields of a class and stores them within a lookup table
    /// </summary>
    /// <param name="classObject">Reference to the object to get fields from</param>
    /// <param name="includeProperties">Whether to include properties in the table or not</param>
    /// <param name="includeNonPublic">Whether to include protected/private fields in the table or not</param>
    /// <returns>Lookup table containing all the fields of a class</returns>
    public static Dictionary<string, object> GetClassContents(
        object? classObject, bool includeProperties = false,
        bool includeNonPublic = false)
    {
        Dictionary<string, object> result = new Dictionary<string, object>();
        if (classObject == null)
        {
            return result;
        }

        Type type = classObject.GetType();
        if (type.IsPrimitive || type == typeof(string))
        {
            result.Add(type.Name, classObject);
            return result;
        }

        BindingFlags bindFlags = includeNonPublic
            ? BindingFlags.Instance | BindingFlags.Public |
              BindingFlags.NonPublic
            : BindingFlags.Instance | BindingFlags.Public;

        FieldInfo[] fields = type.GetFields(bindFlags);
        foreach (FieldInfo currentField in fields)
        {
            if (currentField == null!)
            {
                continue;
            }

            result.Add(currentField.Name, currentField.GetValue(classObject)!);
        }

        if (includeProperties)
        {
            PropertyInfo[] properties = type.GetProperties(bindFlags);
            foreach (PropertyInfo currentProperty in properties)
            {
                result.Add(currentProperty.Name,
                           currentProperty.GetValue(classObject, null)!);
            }
        }

        return result;
    }
    /// <summary>
    ///     Extension method used to check the type hierarchy for a specific generic parent
    /// </summary>
    /// <param name="type">The type to check the hierarchy of</param>
    /// <param name="genericDefinition">The generic type to check for inheritance</param>
    /// <returns>True if the type is a descendant of the passed in type, or is the passed in type, otherwise false</returns>
    public static bool IsSubclassOfGeneric(this Type type,
                                           Type genericDefinition)
    {
        Type currentType = type;
        while (currentType != typeof(object))
        {
            Type toCheck = currentType.IsGenericType
                ? currentType.GetGenericTypeDefinition()
                : currentType;

            if (genericDefinition == toCheck)
            {
                return true;
            }

            currentType = currentType.BaseType!;
        }

        return false;
    }
    /// <summary>
    ///     Sets all the fields of a class from the passed in lookup table
    /// </summary>
    /// <param name="classObject">The class reference to populate</param>
    /// <param name="classTable">The field table to use to populate the class</param>
    /// <param name="includeProperties">Whether to populate properties as well as fields</param>
    /// <param name="includeNonPublic">Whether to populate protected/private fields as well</param>
    public static void SetClassContents(ref object? classObject,
                                        Dictionary<string, object> classTable,
                                        bool includeProperties = false,
                                        bool includeNonPublic = false)
    {
        if (classObject == null)
        {
            return;
        }

        Type type = classObject.GetType();
        if (type.IsPrimitive || type == typeof(string))
        {
            classObject =
                classTable.ContainsKey(type.Name) &&
                classTable[type.Name].GetType() == type
                    ? classTable[type.Name]
                    : null;

            return;
        }

        BindingFlags bindFlags = includeNonPublic
            ? BindingFlags.Instance | BindingFlags.Public |
              BindingFlags.NonPublic
            : BindingFlags.Instance | BindingFlags.Public;

        FieldInfo[] fields = type.GetFields(bindFlags);
        foreach (FieldInfo currentField in fields)
        {
            if (classTable.ContainsKey(currentField.Name) &&
                classTable[currentField.Name].GetType() ==
                currentField.GetType())
            {
                currentField.SetValue(classObject,
                                      classTable[currentField.Name]);
            }
        }

        if (!includeProperties)
        {
            return;
        }

        PropertyInfo[] properties = type.GetProperties(bindFlags);
        foreach (PropertyInfo currentProperty in properties)
        {
            if (classTable.ContainsKey(currentProperty.Name) &&
                classTable[currentProperty.Name].GetType() ==
                currentProperty.GetType())
            {
                currentProperty.SetValue(classObject,
                                         classTable[currentProperty.Name],
                                         null);
            }
        }
    }
    /// <summary>
    ///     Attempts to find a type by name, filtered by any number of namespaces
    /// </summary>
    /// <param name="typeName">The name of the type to find</param>
    /// <param name="result">The type if it was found, otherwise null</param>
    /// <param name="namespaces">Namespaces to look through for the type</param>
    /// <returns>True if the type was found, otherwise false</returns>
    public static bool TryGetType(string typeName, out Type? result,
                                  params string[]? namespaces)
    {
        result = null;
        if (namespaces != null)
        {
            foreach (string currentNamespace in namespaces)
            {
                if (string.IsNullOrEmpty(currentNamespace))
                {
                    continue;
                }

                if (TryGetTypeFromName(currentNamespace, out result))
                {
                    break;
                }
            }
        }
        else
        {
            TryGetTypeFromName(typeName, out result);
        }

        return result != null;
    }
    /// <summary>
    ///     Attempts to find a type by name, with an optional namespace filter
    /// </summary>
    /// <param name="typeName">The name of the type to find</param>
    /// <param name="result">The type if it was found, otherwise null</param>
    /// <param name="namespaceFilter">Optional namespace to find the type within</param>
    /// <returns>True if the type was found, otherwise false</returns>
    public static bool TryGetTypeFromName(string typeName, out Type? result,
                                          string? namespaceFilter = null)
    {
        foreach (Type currentType in from assembly in AppDomain.CurrentDomain
                                         .GetAssemblies()
                                     from type in assembly.GetTypes()
                                     where type.Name == typeName &&
                                           (string
                                                .IsNullOrEmpty(namespaceFilter) ||
                                            string
                                                .IsNullOrEmpty(type
                                                    .Namespace) ||
                                            type.Namespace
                                                .StartsWith(namespaceFilter))
                                     select type)
        {
            result = currentType;
            return true;
        }

        result = null;
        return false;
    }
}