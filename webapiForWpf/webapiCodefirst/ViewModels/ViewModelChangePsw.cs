using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapiCodefirst.ViewModels
{
    public class ViewModelChangePsw
    {
        public String Account { get; set; }

        public String OldPassword { get; set; }
        public String NewPassword { get; set; }
        public String SurePassword { get; set; }
    }
}