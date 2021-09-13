using Orleans;
using System;
using System.Threading.Tasks;

namespace MsOrleansKubernetes
{
    public interface ITestGrain : IGrainWithGuidKey
    {
        Task Nic();
        Task Test();

    }
    public class TestGrain : Grain, ITestGrain
    {
        public Task Nic()
        {
            Console.WriteLine("Nic");
            return Task.CompletedTask;
        }

        public async Task Test()
        {
            Console.WriteLine("WAITING");
            await Task.Delay(TimeSpan.FromSeconds(30));
            Console.WriteLine("COMPLETED");
        }
    }
}