using FluffyVoid.Utilities.Formatters;
using Newtonsoft.Json;

namespace FluffyVoid.Utilities.DataStructures;

/// <summary>
///     Wrapper for an object that allows for notifications when the value has been changed
/// </summary>
/// <typeparam name="TType">The type to use as the value</typeparam>
[Serializable]
[JsonObject(MemberSerialization.OptIn)]
public class DataItem<TType> : IDataItem
{
    /// <summary>
    ///     Delegate declaration for when the value has changed
    /// </summary>
    public delegate void ValueChangedDelegate(TType? previous, TType? current);

    /// <summary>
    ///     String formatter that allows for an optional way to format the value in a readable way
    /// </summary>
    public IStringFormatter<TType>? Formatter { get; set; }
    /// <summary>
    ///     The name of the data object
    /// </summary>
    [JsonProperty]
    public string Name { get; protected set; }

    /// <summary>
    ///     The current value stored
    /// </summary>
    public virtual TType? Value
    {
        get => CurrentValue;
        set
        {
            PreviousValue = CurrentValue;
            CurrentValue = value;
            if (PreviousValue == null && CurrentValue != null)
            {
                ValueChanged?.Invoke(default, CurrentValue);
            }
            else if (PreviousValue != null &&
                     !PreviousValue.Equals(CurrentValue))
            {
                ValueChanged?.Invoke(PreviousValue, CurrentValue);
            }
        }
    }

    /// <summary>
    ///     The current value stored
    /// </summary>
    [JsonProperty]
    protected TType? CurrentValue { get; set; }
    /// <summary>
    ///     The previous value to use as a comparison for when the value changes
    /// </summary>
    protected TType? PreviousValue { get; set; }
    /// <summary>
    ///     Event for being notified when the value has changed
    /// </summary>
    public event ValueChangedDelegate? ValueChanged;

    /// <summary>
    ///     Constructor for the data item that initializes the stored data
    /// </summary>
    public DataItem()
    {
        CurrentValue = default;
        Name = "Data";
    }
    /// <summary>
    ///     Constructor for the data item that initializes the stored data
    /// </summary>
    /// <param name="startingValue">The value to set as the starting value</param>
    public DataItem(TType startingValue)
    {
        CurrentValue = startingValue;
        Name = "Data";
    }
    /// <summary>
    ///     Constructor for the data item that initializes the stored data
    /// </summary>
    /// <param name="startingValue">The value to set as the starting value</param>
    /// <param name="name">The name to assign to the data object</param>
    public DataItem(TType startingValue, string name)
    {
        Name = name;
        CurrentValue = startingValue;
    }
    /// <summary>
    ///     Constructor for the data item that initializes the stored data
    /// </summary>
    /// <param name="startingValue">The value to set as the starting value</param>
    /// <param name="name">The name to assign to the data object</param>
    /// <param name="formatter">The string formatter to assign to the data object</param>
    public DataItem(TType startingValue, string name,
                    IStringFormatter<TType> formatter)
    {
        Name = name;
        CurrentValue = startingValue;
        Formatter = formatter;
    }
    /// <summary>
    ///     Implicit override that allows casting a data item to its wrapped type
    /// </summary>
    /// <param name="right">Reference to the data item to cast</param>
    /// <returns>The embedded value</returns>
    public static implicit operator TType(DataItem<TType> right)
    {
        return right.Value!;
    }
    /// <summary>
    ///     Allows setting of a value without triggering the ValueChanged event
    /// </summary>
    /// <param name="value">The value to set</param>
    public virtual void SetWithoutNotify(TType value)
    {
        PreviousValue = CurrentValue;
        CurrentValue = value;
    }
    /// <summary>
    ///     ToString override that attempts to use an assigned string formatter if it can, otherwise it will ToString the
    ///     currently stored value
    /// </summary>
    /// <returns>The string representation of this data object</returns>
    public override string ToString()
    {
        if (Formatter != null)
        {
            string? result = Formatter.ToString(CurrentValue);
            return result ?? string.Empty;
        }

        if (CurrentValue != null &&
            !string.IsNullOrEmpty(CurrentValue.ToString()))
        {
            return CurrentValue.ToString()!;
        }

        return "null";
    }

    /// <summary>
    ///     Function used to allow child objects to fire the ValueChanged event
    /// </summary>
    /// <param name="previous">The previous value</param>
    /// <param name="current">The current value</param>
    protected void OnValueChanged(TType? previous, TType? current)
    {
        ValueChanged?.Invoke(previous, current);
    }
}