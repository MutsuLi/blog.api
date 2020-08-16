using System;
using AspNetCoreRateLimit;
using Blog.Api.Common;
using log4net;
using Microsoft.AspNetCore.Builder;

namespace Blog.Core.Extensions
{
    /// <summary>
    /// ip 限流
    /// </summary>
    public static class IpLimitMildd
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(IpLimitMildd));
        public static void UseIpLimitMildd(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            try
            {
                if (Appsettings.app("Middleware", "IpRateLimit", "Enabled").ObjToBool())
                {
                    app.UseIpRateLimiting();
                }
            }
            catch (Exception e)
            {
                log.Error($"Error occured limiting ip rate.\n{e.Message}");
                throw;
            }
        }
    }
}
