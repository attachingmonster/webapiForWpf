using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapiCodefirst.ViewModels
{
    /// <summary>
    /// 这些地方与wpf一致的注释
    /// </summary>
    public class ViewModelRegister
    {
        public String Account { get; set; }
        public String Password { get; set; }
        public String QuestionOrAnswer { get; set; }
        public String RememberPasswerd { get; set; }
        public String RoleName { get; set; }
    }
}
