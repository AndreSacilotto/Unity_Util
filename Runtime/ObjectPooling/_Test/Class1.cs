using System;
using UnityEngine;

namespace Spectra.ObjectPooling.Test
{
    public class GOPoolTest
    {

    }


    public class PoolTest
    {
        public PoolTest Request()
        {
            throw new NotImplementedException();
        }

        public void Return(PoolTest item)
        {
            throw new NotImplementedException();
        }
    }

    public class PoolItemTest
    {
        public void Initialize(IObjectPool<PoolTest> pool)
        {
            throw new NotImplementedException();
        }
    }
}
