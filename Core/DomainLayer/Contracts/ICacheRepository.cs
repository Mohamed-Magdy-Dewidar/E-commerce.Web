﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface ICacheRepository
    {
        Task<string?> GetAsync(string key);


        Task SetAsync(string key , string CacheValue , TimeSpan TimeToLive);

  


    }
}
