using System;

namespace TestOverMobile.Core
{
    public class OptimizeServices
    {
        public void Optimize()
        {
            GC.Collect(0, GCCollectionMode.Optimized);
        }
    }
}