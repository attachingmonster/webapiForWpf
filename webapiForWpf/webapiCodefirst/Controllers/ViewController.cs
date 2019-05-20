using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using webapiCodefirst.DAL;
using webapiCodefirst.Models;
using webapiCodefirst.ViewMoles;

namespace webapiCodefirst.Controllers
{
    public class ViewController : ApiController
    {
        HttpClient client = new HttpClient();
        private AccountContext db = new AccountContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        [HttpGet]
        [ActionName("GetViewModels")]
        public List<ViewModel> GetViewModels(List<ViewModel> viewModels)
        {
            client.BaseAddress = new Uri("http://localhost:60033/");
            // Add an Accept header for JSON format.
            // 为JSON格式添加一个Accept报头
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.GetAsync("api/GetEmployees").Result; //转换为同步方法 
            
            var viewModel = (from u in unitOfWork.SysUserRepository.Get()
                             join ur in unitOfWork.SysUserRoleRepository.Get() on u.ID equals ur.SysUserID
                             join r in unitOfWork.SysRoleRepository.Get() on ur.SysRoleID equals r.ID

                             select new ViewModel{ ViewUserAccount = u.UserAccount, ViewRoleName = r.RoleName, ViewRoleDec = r.RoleDec }).ToList();
                                  

            return viewModel;
        }
    }
}
