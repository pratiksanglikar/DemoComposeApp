using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IDistributedCache _distributedCache;
        public string redisMessage;
        private string sqlMessage;

        public IndexModel(ILogger<IndexModel> logger, IDistributedCache distributedCache)
        {
            _logger = logger;
            _distributedCache = distributedCache;
        }

        public void OnGet()
        {
            try
            {
                var cacheKey = "number";
                var existingTime = _distributedCache.GetString(cacheKey);
                var num = 0;
                if (!string.IsNullOrEmpty(existingTime))
                {
                    num = int.Parse(existingTime);
                }
                _distributedCache.SetString(cacheKey, (num + 1).ToString());

                redisMessage = "number: " + num;
            }
            catch (Exception)
            {
                redisMessage = "Redis isn't available at the moment.";
            }
        }
    }
}
