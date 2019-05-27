using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapiCodefirst.ViewModels
{
    public class ViewModelRetrievePsw
    {
        public String Account { get; set; }
        public String QuestionOrAnswer { get; set; }
        public String NewPassword { get; set; }
        public String SurePassword { get; set; }
    }
}