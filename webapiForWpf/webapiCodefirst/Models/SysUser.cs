using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapiCodefirst.Models
{
    public class SysUser
    {
        public int ID { get; set; }
        /// <summary>
        /// 用户账号
        /// </summary>
        public string UserAccount { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPassword { get; set; }
        /// <summary>
        /// 回答拾回问题答案
        /// </summary>
        public string UserAnswer { get; set; }
        /// <summary>
        /// 记住密码的选择，1表示记忆，0表示不记忆
        /// </summary>
        public string RememberPassword { get; set; }


    }
}