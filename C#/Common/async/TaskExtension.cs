using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskDemo
{
    /// <summary>
    /// 提供异步Task的一些组合器或功能辅助
    /// 
    /// </summary>
  public static   class TaskExtension
    {
        /// <summary>
        /// 让一个任务能有超时时间
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="task"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        async static Task<TResult> WithTimeout<TResult>(this Task<TResult> task, TimeSpan timeout)
        {
            Task winner = await (Task.WhenAny(task, Task.Delay(timeout)));
            if (winner != task) throw new TimeoutException();
            return await task;
        }

        /// <summary>
        /// 可以取消一个任务(其实只是将结果弃之不用)
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="task"></param>
        /// <param name="cancelToken"></param>
        /// <returns></returns>
        static Task<TResult> WithCancellation<TResult>(this Task<TResult> task,
            CancellationToken cancelToken)
        {
            var tcs = new TaskCompletionSource<TResult>();

            var reg = cancelToken.Register(() => tcs.TrySetCanceled());

            task.ContinueWith(ant =>
            {
                reg.Dispose();
                if (ant.IsCanceled)
                {
                    tcs.TrySetCanceled();
                }
                else if (ant.IsFaulted)
                {
                    tcs.TrySetException(ant.Exception.InnerException);
                }
                else
                {
                    tcs.TrySetResult(ant.Result);
                }

            });
            return tcs.Task;
        }

        /// <summary>
        /// 等待全部任务完成，如果有出错的就返回
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="tasks"></param>
        /// <returns></returns>
        static async Task<TResult[]> WhenAllOrError<TResult>(params Task<TResult>[] tasks)
        {

            var killJoy = new TaskCompletionSource<TResult[]>();
            foreach (var task in tasks)
            {
                task.ContinueWith(ant =>
                {
                    if (ant.IsCanceled)
                    {
                        killJoy.TrySetCanceled();
                    }
                    else if (ant.IsFaulted)
                        killJoy.TrySetException(ant.Exception.InnerException);
                });
            }
            return await await Task.WhenAny(killJoy.Task, Task.WhenAll(tasks));

        }

        /// <summary>
        /// 异步运行一个方法，如果失败，则重试count次
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="function"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        static async Task<TResult> RetryMultiTimes<TResult>(this Func<TResult> function, int count)
        {
            for (int i = 0; i <= count; i++)
            {
                try
                {
                    Task<TResult> originTask = Task.Run<TResult>(function);
                    return await originTask;
                }
                catch (System.Exception ex)
                {
                    if (i>=count)
                    {
                        throw ex;
                    }
                }
            }
            throw new Exception();

        }
    }
}
