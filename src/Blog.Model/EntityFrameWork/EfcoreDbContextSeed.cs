using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Api.Models;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;

namespace Blog.Api.Model.EntityFrameworkCore
{
    public class EfcoreDbContextSeed
    {
        public static async Task SeedAsync(EfcoreDbContext myContext,
                          ILoggerFactory loggerFactory, int retry = 0)
        {
            int retryForAvailability = retry;
            try
            {
                var test = myContext.Posts.FirstOrDefault();
                if (!myContext.Posts.Any())
                {
                    myContext.Posts.AddRange(
                        new List<Posts>{
                            new Posts{
                                btitle = "Laozhang",
                                bcontent = "老张的哲学",
                                bsubmitter = "lz",
                                bsubmitterId = 0,
                                bRemark="phi"
                            }

                        }
                    );
                    await myContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var logger = loggerFactory.CreateLogger<EfcoreDbContextSeed>();
                    logger.LogError(ex.Message);
                    await SeedAsync(myContext, loggerFactory, retryForAvailability);
                }
            }
        }
    }
}
