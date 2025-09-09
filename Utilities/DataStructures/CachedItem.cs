using FluffyVoid.Utilities.Formatters;
using Newtonsoft.Json;

namespace FluffyVoid.Utilities.DataStructures;

/// <summary>
///     Wrapper for an object that allows for storing a working value that can be applied or reset
/// </summary>
/// <typeparam name="TType">The type to use as the value</typeparam>
[Serializable]
[JsonObject(MemberSerialization.OptIn)]
public class CachedItem<TType>
    : DataItem<TType>
{
    /// <summary>
    ///     The actual value of the object, that is only updated to the working value upon Apply
    /// </summary>
    [JsonProperty]
    public override TType? Value
    {
        get => CurrentValue;
        set => WorkingValue = value;
    }
    /// <summary>
    ///     The working value that is in flux until Apply or Reset is called
    /// </summary>
    public TType? WorkingValue { get; protected set; }

    /// <summary>
    ///     Constructor used to initialize the CachedItem
    /// </summary>
    public CachedItem()
    {
        WorkingValue = default;
        Name = "Data";
    }
    /// <summary>
    ///     Constructor used to initialize the CachedItem
    /// </summary>
    /// <param name="startingValue">The value to assign to the working and true values</param>
    public CachedItem(TType startingValue)
    {
        WorkingValue = startingValue;
        PreviousValue = default;
        CurrentValue = startingValue;
        Name = "Data";
    }
    /// <summary>
    ///     Constructor used to initialize the CachedItem
    /// </summary>
    /// <param name="startingValue">The value to assign to the working and true values</param>
    /// <param name="name">The name to assign to the data object</param>
    public CachedItem(TType startingValue, string name)
    {
        WorkingValue = startingValue;
        PreviousValue = default;
        CurrentValue = startingValue;
        Name = name;
    }
    /// <summary>
    ///     Constructor used to initialize the CachedItem
    /// </summary>
    /// <param name="startingValue">The value to assign to the working and true values</param>
    /// <param name="name">The name to assign to the data object</param>
    /// <param name="formatter">The string formatter to assign to the data object</param>
    public CachedItem(TType startingValue, string name,
                      IStringFormatter<TType> formatter)
    {
        WorkingValue = startingValue;
        PreviousValue = default;
        CurrentValue = startingValue;
        Name = name;
        Formatter = formatter;
    }
    /// <summary>
    ///     Constructor used to initialize the CachedItem
    /// </summary>
    /// <param name="startingValue">The value to assign to the working and true values</param>
    /// <param name="formatter">The string formatter to assign to the data object</param>
    public CachedItem(TType startingValue, IStringFormatter<TType> formatter)
    {
        WorkingValue = startingValue;
        PreviousValue = default;
        CurrentValue = startingValue;
        Name = "Data";
        Formatter = formatter;
    }

    /// <summary>
    ///     Applies the working value to the true value
    /// </summary>
    public void Apply()
    {
        PreviousValue = CurrentValue;
        CurrentValue = WorkingValue;
        if (PreviousValue == null && CurrentValue != null)
        {
            OnValueChanged(default, CurrentValue);
        }
        else if (PreviousValue != null && !PreviousValue.Equals(CurrentValue))
        {
            OnValueChanged(PreviousValue, CurrentValue);
        }
    }
    /// <summary>
    ///     Resets the working value back to the true value
    /// </summary>
    public void Reset()
    {
        WorkingValue = CurrentValue;
    }
    /// <summary>
    ///     Allows setting of a value without triggering the ValueChanged event
    /// </summary>
    /// <param name="value">The value to set</param>
    public override void SetWithoutNotify(TType value)
    {
        base.SetWithoutNotify(value);
        WorkingValue = value;
    }
    /// <summary>
    ///     ToString override that attempts to use an assigned string formatter if it can, otherwise it will ToString the
    ///     current working value
    /// </summary>
    /// <returns>The string representation of this data object</returns>
    public override string ToString()
    {
        if (Formatter != null)
        {
            string? result = Formatter.ToString(WorkingValue);
            return result ?? string.Empty;
        }

        if (WorkingValue != null &&
            !string.IsNullOrEmpty(WorkingValue.ToString()))
        {
            return WorkingValue.ToString()!;
        }

        return "null";
    }
}