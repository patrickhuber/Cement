using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cement.Threading.Tasks;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;
using System.Linq;

namespace Cement.Tests.Unit.Threading.Tasks
{
    [TestClass]
    public class LimitedConcurrencyLevelTaskSchedulerTests
    {
        [TestMethod]
        public void Test_LimitedConcurrencyLevelTaskScheduler_With_Concurrency_Of_One_Runs_On_Single_Thread()
        {
            var limitedConcurrencyLevelTaskScheduler = new LimitedConcurrencyLevelTaskScheduler(1);
            var blockingList = new ConcurrentDictionary<int, int>();
            var taskList = new List<Task>();
            for (int i = 0; i < 100; i++)
            {
                Task t = new Task(() =>
                {
                    int managedThreadId = Thread.CurrentThread.ManagedThreadId;
                    Console.WriteLine("DateTime:{0}, ManagedThreadId: {1}", DateTime.Now, managedThreadId);
                    blockingList.AddOrUpdate(managedThreadId, 1, (key, value) => value++);
                });

                t.Start(limitedConcurrencyLevelTaskScheduler);
                taskList.Add(t);
            }
            Task.WaitAll(taskList.ToArray());
            Assert.AreEqual(1, blockingList.Count());
        }
    }
}
