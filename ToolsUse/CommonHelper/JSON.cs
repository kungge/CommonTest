using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.IO;

namespace ToolsUse.CommonHelper
{
    #region public static class JSON 解析JSON数据帮助类，仿Javascript风格
    /// <summary> 
    /// 解析JSON数据帮助类，仿Javascript风格 
    /// </summary> 
    public static class JSON
    {
        #region 把JSON字符串解析为泛型集合,示范: List<GoodsItem> _items = JSON.Parse<List<GoodsItem>>(jsonString);
        /// <summary>
        /// 把JSON字符串解析为泛型集合或数组        
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static T Parse<T>(string jsonString)
        {
            // 用法:  
            // 数组转换    JSON.Parse<Person[]>(jsonString); 
            // 反序列化，泛型集合     JSON.parse<List<Person>>(jsonString);
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
               
                return (T)new DataContractJsonSerializer(typeof(T)).ReadObject(ms);
            }
        }
        #endregion

        #region 把对象解析为JSON字符串
        /// <summary>
        /// 把对象解析为JSON字符串
        /// </summary>
        /// <param name="jsonObject"></param>
        /// <returns></returns>
        public static string FormatString(object jsonObject)
        {
            using (var ms = new MemoryStream())
            {
                new DataContractJsonSerializer(jsonObject.GetType()).WriteObject(ms, jsonObject);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }
        #endregion
    }

    #endregion
}
