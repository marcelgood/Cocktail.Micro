﻿// ====================================================================================================================
//   Copyright (c) 2012 IdeaBlade
// ====================================================================================================================
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE 
//   WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS 
//   OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR 
//   OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 
// ====================================================================================================================
//   USE OF THIS SOFTWARE IS GOVERENED BY THE LICENSING TERMS WHICH CAN BE FOUND AT
//   http://cocktail.ideablade.com/licensing
// ====================================================================================================================

using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cocktail
{
    /// <summary>
    ///   A set of cross-platform static and extension methods that operate on <see cref="Task" /> and <see cref="Task{T}" />
    /// </summary>
    public static class TaskFns
    {
        /// <summary>
        ///   Creates a <see cref="Task{T}" /> that's completed successfully with the specified result.
        /// </summary>
        /// <param name="resultValue"> The result value to store in the completed task. </param>
        public static Task<T> FromResult<T>(T resultValue)
        {
#if SILVERLIGHT
             var tcs = new TaskCompletionSource<T>();
             tcs.SetResult(resultValue);
             return tcs.Task;
#else
            return Task.FromResult(resultValue);
#endif
        }

        /// <summary>
        ///   Creates a <see cref="Task{T}" /> from a callback action that completes when the callback is called.
        /// </summary>
        /// <param name="action"> The callback action. </param>
        /// <typeparam name="T"> The type of the callback result. </typeparam>
        public static Task<T> FromCallbackAction<T>(Action<Action<T>> action)
        {
            var tcs = new TaskCompletionSource<T>();
            try
            {
                action(tcs.SetResult);
            }
            catch (Exception e)
            {
                tcs.TrySetException(e);
            }

            return tcs.Task;
        }

        /// <summary>
        /// Creates a task that will complete when all of the supplied tasks have completed
        /// </summary>
        /// <param name="tasks">The tasks to wait on for completion.</param>
        public static Task WhenAll(params Task[] tasks)
        {
#if SILVERLIGHT
            return Task.Factory.ContinueWhenAll(
                tasks,
                completedTasks =>
                    {
                        if (completedTasks.Any(x => x.IsFaulted))
                            throw new AggregateException(completedTasks.Where(x => x.IsFaulted).Select(x => x.Exception));
                        if (completedTasks.Any(x => x.IsCanceled))
                            throw new TaskCanceledException();
                    });
#else
            return Task.WhenAll(tasks);
#endif
        }
    }
}