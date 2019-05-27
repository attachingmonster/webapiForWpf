using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using webapiCodefirst.DAL;
using webapiCodefirst.Methods;
using webapiCodefirst.ViewModels;

namespace webapiCodefirst.Controllers
{
    public class RetrievePswController : ApiController
    {
        private AccountContext db = new AccountContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        [HttpPost]
        [ActionName("PostRetrievePsw")]
        public VMregisterInfomation RetrievePsw(ViewModelRetrievePsw viewModelRetrievePsw)
        {
            VMregisterInfomation vMregisterInfomation = null;
            try
            {
                vMregisterInfomation = new VMregisterInfomation();
                var u = unitOfWork.SysUserRepository.Get().Where(s => s.UserAccount.Equals(viewModelRetrievePsw.Account)).FirstOrDefault();  //查找是否存在账号
                if (u != null)
                {
                    if (viewModelRetrievePsw.QuestionOrAnswer != "你最喜欢的颜色是" || viewModelRetrievePsw.QuestionOrAnswer != "你的生日是" || viewModelRetrievePsw.QuestionOrAnswer != "你的父亲叫什么名字" || viewModelRetrievePsw.QuestionOrAnswer != "你最喜欢做什么" || viewModelRetrievePsw.QuestionOrAnswer != "你的梦想是")
                    {
                        if (u.UserAnswer.Equals(CreateMD5.EncryptWithMD5(viewModelRetrievePsw.QuestionOrAnswer)))
                        {
                            if (viewModelRetrievePsw.NewPassword != "")
                            {
                                int number = 0, character = 0;
                                foreach (char c in viewModelRetrievePsw.NewPassword)   //规范密码必须由ASCII码33~126之间的字符构成
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
                                if (!u.UserPassword.Equals(CreateMD5.EncryptWithMD5(viewModelRetrievePsw.NewPassword)))
                                {
                                    if (viewModelRetrievePsw.NewPassword.Equals(viewModelRetrievePsw.SurePassword))//判断密码与确认密码是否相等
                                    {

                                        u.UserPassword = CreateMD5.EncryptWithMD5(viewModelRetrievePsw.NewPassword);
                                        unitOfWork.SysUserRepository.Update(u);//重置密码
                                        unitOfWork.Save();
                                        throw new Exception("密码重置成功");
                                    }
                                    else
                                    {
                                        throw new Exception("两次输入的密码不一致！");
                                    }
                                }
                                else
                                {
                                    throw new Exception("新密码不能与原密码一致！");
                                }


                            }
                            else
                            {
                                throw new Exception("新密码不能为空！");
                            }
                        }
                        else
                        {
                            throw new Exception("密码拾回问题答案错误");
                        }
                    }
                    else
                    {
                        throw new Exception("请您输入密码拾回问题答案");
                    }
                }
                else
                {
                    throw new Exception("账号不存在！");
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
