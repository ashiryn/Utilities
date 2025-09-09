namespace FluffyVoid.Utilities;

/// <summary>
///     Organizational class for utilities that relate to Tasks
/// </summary>
public static class TaskUtility
{
    /// <summary>
    ///     Adds cancellation support to async functions that do not support natively taking in a CancellationToken
    /// </summary>
    /// <param name="task">The task to run with cancellation support</param>
    /// <param name="token">The cancellation token to check for task cancellation</param>
    /// <typeparam name="T">The type the task is set up for</typeparam>
    /// <returns>The Task that is currently running</returns>
    public static Task<T> WithCancellation<T>(this Task<T> task,
                                              CancellationToken token)
    {
        return task.IsCompleted
            ? task
            : task.ContinueWith(completedTask => completedTask.GetAwaiter().GetResult(),
                                token,
                                TaskContinuationOptions.ExecuteSynchronously,
                                TaskScheduler.Default);
    }
}