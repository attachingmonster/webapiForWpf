using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapiCodefirst.Models
{
    /// <summary>
    /// 用户角色
    /// </summary>
    public class SysRole
    {
        public int ID { get; set; }
        /// <summary>
        /// 角色名字
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 角色功能
        /// </summary>
        public string RoleDec { get; set; }
    }
}