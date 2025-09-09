namespace FluffyVoid.Utilities.Cloneable;

/// <summary>
///     Defines a contract for any class wishing to enforce deep copy capabilities
/// </summary>
public interface IDeepCloneable
{
    /// <summary>
    ///     Returns a deep copy of the object to ensure that the returned copy is a unique entity, and not sharing references
    ///     with the current object
    /// </summary>
    /// <returns>A new deeply copied version of the object</returns>
    object DeepClone();
}
/// <summary>
///     Defines a contract for any class wishing to enforce deep copy capabilities
/// </summary>
/// <typeparam name="T">The type of object that the class will be deep copying</typeparam>
public interface IDeepCloneable<out T> : IDeepCloneable
{
    /// <summary>
    ///     Returns a deep copy of the object to ensure that the returned copy is a unique entity, and not sharing references
    ///     with the current object
    /// </summary>
    /// <returns>A new deeply copied version of the object</returns>
    new T DeepClone();
}