using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Common
{
    /// <summary>
    /// Redis缓存接口
    /// </summary>
    public interface IRedisCacheManager
    {

        //获取 Reids 缓存值
        string GetValue(string key);

        //获取值，并序列化
        TEntity Get<TEntity>(string key);

        //保存
        void Set(string key, object value, TimeSpan cacheTime);

        //判断是否存在
        bool Get(string key);

        //移除某一个缓存值
        void Remove(string key);

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
        long SortedSetAdd(string key, IDictionary<string, int> members);


        /// <summary>
        /// Sortedset score increase api
        /// if member doesn't exist,this api will add to the sortedset.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns>member</returns>
        double SortedSetIncrement(string key, string member, double score);


        /// <summary>
        /// SortedSet score decrease api
        /// if value doesn't exist,this api will add to the SortedSet.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="score"></param>
        /// <returns>value</returns>
        double SortedSetDecrement(string key, string member, double score);

        /// <summary>
        /// Get sortedset's Length.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        long SortedSetLength(string key);

        /// <summary>
        /// remove the value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        long SortedSetRemove(string key, IList<String> members);


        /// <summary>
        ///  Removes the specified members from the sorted set stored at key. Non existing
        ///  members are ignored.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="ordering"></param>
        /// <returns></returns>
        double SortedSetRank(string key, string member, string ordering);

        /// <summary>
        /// Get a element's Score in the sortedset.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        double SortedSetScore(string key, string member);

        //全部清除
        void Clear();
    }
}
