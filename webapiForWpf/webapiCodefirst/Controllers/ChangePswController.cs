using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using webapiCodefirst.DAL;
using webapiCodefirst.Methods;
using webapiCodefirst.ViewModels;

namespace webapiCodefirst.Controllers
{
    public class ChangePswController : ApiController
    {
        private AccountContext db = new AccountContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        [HttpPost]
        [ActionName("PostChangePsw")]
        public VMregisterInfomation ChangePsw(ViewModelChangePsw viewModelChangePsw)
        {
            VMregisterInfomation vMregisterInfomation = null;
            try
            {
                vMregisterInfomation = new VMregisterInfomation();
                string OldPassword = CreateMD5.EncryptWithMD5(viewModelChangePsw.OldPassword);     //原密码
                string NewPassword = CreateMD5.EncryptWithMD5(viewModelChangePsw.NewPassword);     //新密码

                var u = unitOfWork.SysUserRepository.Get().Where(s => s.UserAccount.Equals(viewModelChangePsw.Account.Trim()) && s.UserPassword.ToLower().Equals(OldPassword)).FirstOrDefault();//账号是否存在与密码是否相等
                if (u != null)
                {
                    if (viewModelChangePsw.NewPassword != "")
                    {
                        if (!viewModelChangePsw.OldPassword.Equals(viewModelChangePsw.NewPassword))
                        {
                            int number = 0, character = 0;
                            foreach (char c in viewModelChangePsw.NewPassword)   //规范密码必须由ASCII码33~126之间的字符构成
                            {
                                if (!(33 <= c && c <= 126))
                                {
                                    throw new Exception("符号错误，请重新输入！");
                                }
                                if ('0' <= c && c <= '9') //number记录数字个数
                                {
                                    number++;
                                }
                                else                      //character记录字符个数
                                {
                                    character++;
                                }
                            }
                            if (number < 5 || character < 2)  //密码的安全系数
                            {
                                throw new Exception("新密码安全系数太低！");
                            }
                            if (viewModelChangePsw.SurePassword.Equals(viewModelChangePsw.NewPassword)) //新密码与确认密码是否相等
                            {
                                u.UserPassword = viewModelChangePsw.NewPassword;
                                unitOfWork.SysUserRepository.Update(u);//更改密码
                                unitOfWork.Save();
                                throw new Exception("修改成功！");
                            }
                            else
                            {
                                throw new Exception("两次输入的密码不一致！");
                            }
                        }
                        else
                        {
                            throw new Exception("新密码与原密码不能相同！");
                        }
                    }
                    else
                    {
                        throw new Exception("新密码不能为空！");
                    }
                }
                else
                {
                    throw new Exception("用户名不存在或原密码输入错误！");
                }

            }
            catch (Exception ex)
            {
                vMregisterInfomation.Message = ex.Message;
                return vMregisterInfomation;
            }



        }
    }
}