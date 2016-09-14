using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace CommonTest.CommonCode
{
    public class CommonCode
    {
        #region MD5处理
        public string MD5Proc(string pwd)
        {
            MD5 md5 = new MD5CryptoServiceProvider();//注
            byte[] pwdByte = Encoding.Default.GetBytes(pwd);
            byte[] pwdHashByte = md5.ComputeHash(pwdByte);
            md5.Clear();
            string str = "";
            for (int i = 0; i < pwdHashByte.Length; i++)
            {
                str += pwdHashByte[i].ToString("x").PadLeft(2, '0');

                /* 注:
                 
                 ToString("X2") 为C#中的字符串格式控制符

X为     十六进制 
2为     每次都是两位数

比如   0x0A ，若没有2,就只会输出0xA 
假设有两个数10和26，正常情况十六进制显示0xA、0x1A，这样看起来不整齐，为了好看，可以指定"X2"，这样显示出来就是：0x0A、0x1A。 
参考网址： http://topic.csdn.net/t/20050709/17/4133902.html

                 */
            }
            return str;
        }
        #endregion

        #region 判断对象格式是否正确
        public static bool IsNumeric(object expression)
        {
            if (expression != null)
                return IsNumeric(expression.ToString());
            return false;
        }
        public static bool IsNumeric(string expression)
        {
            if (expression != null)
            {
                string str = expression;
                if (str.Length > 0 && str.Length <= 11 && Regex.IsMatch(str, @"^[-]?[0-9]*[.]?[0-9]*$"))
                {
                    if ((str.Length < 10) || (str.Length == 10 && str[0] == '1') || (str.Length == 11 && str[0] == '-' && str[1] == '1'))
                        return true;
                }
            }
            return false;
        }

        public static bool IsDouble(object expression)
        {
            if (expression != null)
            {
                return Regex.IsMatch(expression.ToString(), @"^([0-9])[0-9]*(\.\w*)?$");
            }
            return false;
        }

        /// <summary>
           /// 检测是否符合email格式
           /// </summary>
           /// <param name="strEmail">要判断的email字符串</param>
           /// <returns>判断结果</returns>
           public static bool IsValidEmail(string strEmail)
           {
               return Regex.IsMatch(strEmail, @"^[\w\.]+([-]\w+)*@[A-Za-z0-9-_]+[\.][A-Za-z0-9-_]");
           }
           public static bool IsValidDoEmail(string strEmail)
           {
               return Regex.IsMatch(strEmail, @"^@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
           }
   
           /// <summary>
           /// 检测是否是正确的Url
           /// </summary>
           /// <param name="strUrl">要验证的Url</param>
           /// <returns>判断结果</returns>
           public static bool IsURL(string strUrl)
          {
              return Regex.IsMatch(strUrl, @"^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$");
          }

        #endregion

        #region 数据转换

        /// <summary>
          /// 将字符串转换为数组
          /// </summary>
          /// <param name="str">字符串</param>
          /// <returns>字符串数组</returns>
           public static string[] GetStrArray(string str)
           {
               return str.Split(new char[',']);
               //str.Split(',');//注：todo 和上面的区别
           }
        #endregion
    }
}