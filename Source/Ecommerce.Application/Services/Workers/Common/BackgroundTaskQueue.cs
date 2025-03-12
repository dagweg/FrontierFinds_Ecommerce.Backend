using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

public interface IBackgroundTaskQueue
{
  void QueueBackgroundWorkItem(Func<CancellationToken, ValueTask> workItem);
  ValueTask<Func<CancellationToken, ValueTask>> DequeueAsync(CancellationToken cancellationToken);
}

public class BackgroundTaskQueue : IBackgroundTaskQueue
{
  private ConcurrentQueue<Func<CancellationToken, ValueTask>> _workItems = [];

  private SemaphoreSlim _signal = new SemaphoreSlim(0);

  public void QueueBackgroundWorkItem(Func<CancellationToken, ValueTask> workItem)
  {
    _workItems.Enqueue(workItem);
    _signal.Release();
  }

  public async ValueTask<Func<CancellationToken, ValueTask>> DequeueAsync(
    CancellationToken cancellationToken
  )
  {
    await _signal.WaitAsync(cancellationToken);
    _workItems.TryDequeue(out var workItem);

    return workItem!;
  }
}
