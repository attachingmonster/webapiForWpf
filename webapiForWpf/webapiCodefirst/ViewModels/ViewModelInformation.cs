using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapiCodefirst.ViewModels
{
    /// <summary>
    /// webapi返回的信息
    /// </summary>
    public class ViewModelInformation
    {
        /// <summary>
        /// webapi注册信息：成功、失败、无效等
        /// </summary>
        public String Message { get; set; }
    }
}