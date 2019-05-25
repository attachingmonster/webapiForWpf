using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapiCodefirst.ViewModels
{
    /// <summary>
    /// webapi返回的信息 这些地方与wpf一致的注释
    /// </summary>
    public class VMregisterInfomation
    {
        /// <summary>
        /// webapi注册信息：成功、失败、无效等
        /// </summary>
        public String Message { get; set; }

    }
}