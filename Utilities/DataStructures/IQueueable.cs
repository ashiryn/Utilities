namespace FluffyVoid.Utilities.DataStructures;

/// <summary>
///     Defines a contract for any class that is capable of being queued by the AutoQueue
/// </summary>
public interface IQueueable
{
    /// <summary>
    ///     The amount of time the object should reserve before the next item can be processed
    /// </summary>
    float Time { get; }
    /// <summary>
    ///     Processes the object when it has been dequeued from the AutoQueue
    /// </summary>
    void Process();
}