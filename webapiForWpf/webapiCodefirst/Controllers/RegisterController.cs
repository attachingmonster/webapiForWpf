using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using webapiCodefirst.ViewModels;
using webapiCodefirst.ViewMoles;

namespace webapiCodefirst.Controllers
{
    public class RegisterController : ApiController
    {
        [HttpPost]
        [ActionName("PostRegister")]
        
        public VMregisterInfomation Register(ViewModelRegister viewmodelRegister)
        {
            VMregisterInfomation vMregisterInfomation = new VMregisterInfomation();
            vMregisterInfomation.message = "注册成功";
            return vMregisterInfomation;
        }
    }
}
