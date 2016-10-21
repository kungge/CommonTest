using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToolsUse.CommonHelper
{
    public class AjaxResult
    {
        /// <summary>
        /// 是否产生错误
        /// </summary>
        public bool IsError { get; set; }

        /// <summary>
        /// 错误信息，或者成功信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 成功可能时返回的数据
        /// </summary>
        public object Data { get; set; }

        #region Error
        public static AjaxResult Error()
        {
            return new AjaxResult
            {
                IsError = true
            };
        }
        public static AjaxResult Error(string message)
        {
            return new AjaxResult
            {
                IsError = true,
                Message = message
            };
        }
        public static AjaxResult Error(object data, string message)
        {
            return new AjaxResult
            {
                IsError = true,
                Message = message,
                Data = data
            };
        }
        #endregion

        #region Success
        public static AjaxResult Success()
        {
            return new AjaxResult
            {
                IsError = false
            };
        }
        public static AjaxResult Success(string message)
        {
            return new AjaxResult
            {
                IsError = false,
                Message = message
            };
        }
        public static AjaxResult Success(object data)
        {
            return new AjaxResult
            {
                IsError = false,
                Data = data
            };
        }
        public static AjaxResult Success(object data, string message)
        {
            return new AjaxResult
            {
                IsError = false,
                Data = data,
                Message = message
            };
        }
        #endregion

        /// <summary>
        /// 返回当前对象JSON字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JSON.FormatString(this);
        }
    }
}