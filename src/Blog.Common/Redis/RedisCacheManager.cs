using System;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using Blog.Api.Common;

namespace Blog.Common
{
    public class RedisCacheManager : IRedisCacheManager
    {
        private readonly string redisConnenctionString;
        public volatile ConnectionMultiplexer redisConnection;
        private readonly object redisConnectionLock = new object();
        public RedisCacheManager()
        {
            string redisConfiguration = Appsettings.app(new string[] { "AppSettings", "RedisCachingAOP", "ConnectionString" });//获取连接字符串

            if (string.IsNullOrWhiteSpace(redisConfiguration))
            {
                throw new ArgumentException("redis config is empty", nameof(redisConfiguration));
            }
            this.redisConnenctionString = redisConfiguration;
            this.redisConnection = GetRedisConnection();
        }

        /// <summary>
        /// 核心代码，获取连接实例
        /// 通过双if 夹lock的方式，实现单例模式
        /// </summary>
        /// <returns></returns>
        private ConnectionMultiplexer GetRedisConnection()
        {
            //如果已经连接实例，直接返回
            if (this.redisConnection != null && this.redisConnection.IsConnected)
            {
                return this.redisConnection;
            }
            //加锁，防止异步编程中，出现单例无效的问题
            lock (redisConnectionLock)
            {
                if (this.redisConnection != null)
                {
                    //释放redis连接
                    this.redisConnection.Dispose();
                }
                try
                {
                    this.redisConnection = ConnectionMultiplexer.Connect(redisConnenctionString);
                }
                catch (Exception)
                {

                    throw new Exception("Redis服务未启用，请开启该服务");
                }
            }
            return this.redisConnection;
        }

        public void Clear()
        {
            foreach (var endPoint in this.GetRedisConnection().GetEndPoints())
            {
                var server = this.GetRedisConnection().GetServer(endPoint);
                foreach (var key in server.Keys())
                {
                    redisConnection.GetDatabase().KeyDelete(key);
                }
            }
        }

        public bool Get(string key)
        {
            return redisConnection.GetDatabase().KeyExists(key);
        }

        public string GetValue(string key)
        {
            return redisConnection.GetDatabase().StringGet(key);
        }

        public TEntity Get<TEntity>(string key)
        {
            var value = redisConnection.GetDatabase().StringGet(key);
            if (value.HasValue)
            {
                //需要用的反序列化，将Redis存储的Byte[]，进行反序列化
                return SerializeHelper.Deserialize<TEntity>(value);
            }
            else
            {
                return default(TEntity);
            }
        }

        public void Remove(string key)
        {
            redisConnection.GetDatabase().KeyDelete(key);
        }
        public void Set(string key, object value, TimeSpan cacheTime)
        {
            if (value != null)
            {
                //序列化，将object值生成RedisValue
                redisConnection.GetDatabase().StringSet(key, SerializeHelper.Serialize(value), cacheTime);
            }
        }

        public bool SetValue(string key, byte[] value)
        {
            return redisConnection.GetDatabase().StringSet(key, value, TimeSpan.FromSeconds(120));
        }

        /// <summary>
        ///     Adds all the specified members with the specified scores to the sorted set stored
        ///     at key. If a specified member is already a member of the sorted set, the score
        ///     is updated and the element reinserted at the right position to ensure the correct
        ///     ordering.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public long SortedSetAdd(string key, IDictionary<string, int> members)
        {
            try
            {
                if ((object)key == null)
                    return 0;
                else
                    return redisConnection.GetDatabase().SortedSetAdd(key, members.Select((pair) => new SortedSetEntry(pair.Key, pair.Value)).ToArray(), CommandFlags.None);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Sortedset score increase api
        /// if member doesn't exist,this api will add to the sortedset.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns>member</returns>
        public double SortedSetIncrement(string key, string member, double score)
        {
            try
            {
                if ((object)key == null)
                    return 0;
                else
                    return redisConnection.GetDatabase().SortedSetIncrement(key, member, score);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// SortedSet score decrease api
        /// if value doesn't exist,this api will add to the SortedSet.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="score"></param>
        /// <returns>value</returns>
        public double SortedSetDecrement(string key, string member, double score)
        {
            try
            {
                if ((object)key == null)
                    return 0;
                else
                    return redisConnection.GetDatabase().SortedSetDecrement(key, member, score);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get sortedset's Length.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long SortedSetLength(string key)
        {
            return redisConnection.GetDatabase().SortedSetLength(key);
        }

        /// <summary>
        /// remove the value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long SortedSetRemove(string key, IList<String> members)
        {
            try
            {
                if ((object)key == null)
                    return 0;
                else
                    return redisConnection.GetDatabase().SortedSetRemove(key, members.Select(e => new RedisValue(e)).ToArray());
            }
            catch (Exception)
            {
                return 0;
            }
        }


        /// <summary>
        ///  Removes the specified members from the sorted set stored at key. Non existing
        ///  members are ignored.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="ordering"></param>
        /// <returns></returns>
        public Dictionary<string,double> SortedSetRangeByRank(string key, int start,int end, string ordering)
        {
            Order order = (ordering.IndexOf("desc") > -1) ? Order.Descending : Order.Ascending;
            Dictionary<string, double> ranks = new Dictionary<string, double>();
            try
            {
                if ((object)key == null)
                    return ranks;
                else
                {
                    return redisConnection.GetDatabase().SortedSetRangeByRankWithScores(key, start, end, order).ToDictionary(t => t.Element.ToString(), t => t.Score);
                }
            }
            catch (Exception)
            {
                return ranks;
            }
        }

        /// <summary>
        /// Get a element's Score in the sortedset.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public double SortedSetScore(string key, string member)
        {
            try
            {
                if ((object)key == null)
                    return 0;
                else
                    return redisConnection.GetDatabase().SortedSetScore(key, member) ?? 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
