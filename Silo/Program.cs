using System;
using Orleans.Runtime.Configuration;
using Orleans.Runtime.Host;

namespace Silo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var clusterConfiguration = new ClusterConfiguration
            {
                Defaults = { }
            };
            
            var siloHost = new SiloHost("TestingSilo", ClusterConfiguration.LocalhostPrimarySilo());
            siloHost.LoadOrleansConfig();
            siloHost.InitializeOrleansSilo();
            
            siloHost.StartOrleansSilo();
            
            siloHost.WaitForOrleansSiloShutdown();
        }
    }
}