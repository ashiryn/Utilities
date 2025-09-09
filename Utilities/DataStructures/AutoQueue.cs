namespace FluffyVoid.Utilities.DataStructures;

/// <summary>
///     Custom queue that can process IQueueable objects automatically with dynamic queue times for each item in the queue
/// </summary>
/// <typeparam name="T">The type of item to queue</typeparam>
public class AutoQueue<T>
    where T : IQueueable
{
    /// <summary>
    ///     Collection of queued items
    /// </summary>
    private readonly Queue<T> _queuedItems;
    /// <summary>
    ///     The amount of time that has elapsed for the current queued item
    /// </summary>
    private float _elapsed;
    /// <summary>
    ///     The amount of time to wait until dispatching the next queued item
    /// </summary>
    private float _interval;
    /// <summary>
    ///     Whether the queue system is currently running or is idle
    /// </summary>
    private bool _isRunning;

    /// <summary>
    ///     Default constructor used to set up the queue
    /// </summary>
    public AutoQueue()
    {
        _queuedItems = new Queue<T>();
    }
    /// <summary>
    ///     Enqueues an object for processing into the queue
    /// </summary>
    /// <param name="item">The item to queue</param>
    public void Enqueue(T item)
    {
        if (!_isRunning)
        {
            item.Process();
            _interval = item.Time;
            _elapsed = 0.0f;
            _isRunning = true;
        }
        else
        {
            _queuedItems.Enqueue(item);
        }
    }
    /// <summary>
    ///     Flushes the queue and processes all items in the queue immediately
    /// </summary>
    public void Flush()
    {
        while (_queuedItems.Count > 0)
        {
            T item = _queuedItems.Dequeue();
            item.Process();
        }

        _isRunning = false;
        _elapsed = 0.0f;
    }
    /// <summary>
    ///     Processes the next item in the queue, skipping any current wait time in effect
    /// </summary>
    public void Next()
    {
        if (_queuedItems.Count > 0)
        {
            T item = _queuedItems.Dequeue();
            _interval = item.Time;
            _elapsed = 0.0f;
            item.Process();
        }
        else
        {
            _isRunning = false;
            _elapsed = 0.0f;
        }
    }
    /// <summary>
    ///     Resets the queue system back to its default state
    /// </summary>
    public void Reset()
    {
        _queuedItems.Clear();
        _isRunning = false;
        _elapsed = 0.0f;
    }
    /// <summary>
    ///     Updates the currently active queue time to dispatch items at variable intervals
    /// </summary>
    /// <param name="dt">The amount of time passed since the last call to Update</param>
    public void Update(float dt)
    {
        if (!_isRunning)
        {
            return;
        }

        _elapsed += dt;
        if (_elapsed >= _interval)
        {
            Next();
        }
    }
}