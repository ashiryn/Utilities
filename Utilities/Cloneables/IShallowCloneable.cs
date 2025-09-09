namespace FluffyVoid.Utilities.Cloneable;

/// <summary>
///     Defines a contract for any class wishing to enforce shallow copy capabilities
/// </summary>
public interface IShallowCloneable
{
    /// <summary>
    ///     Returns a shallow copy of the object
    /// </summary>
    /// <returns>A new shallowly copied version of the object</returns>
    object ShallowClone();
}
/// <summary>
///     Defines a contract for any class wishing to enforce shallow copy capabilities
/// </summary>
/// <typeparam name="T">The type of object that the class will be shallow copying</typeparam>
public interface IShallowCloneable<out T> : IShallowCloneable
{
    /// <summary>
    ///     Returns a shallow copy of the object
    /// </summary>
    /// <returns>A new shallowly copied version of the object</returns>
    new T ShallowClone();
}