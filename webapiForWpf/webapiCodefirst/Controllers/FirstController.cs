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
    public class FirstController : ApiController
    {
        
        private AccountContext db = new AccountContext();
        private UnitOfWork unitOfWork = new UnitOfWork();

        HttpClient client = new HttpClient();
           
        [HttpGet]
        [ActionName("GetUsers")]
        public List<SysUser> GetUser()
        {
            client.BaseAddress = new Uri("http://localhost:9000/");
            // Add an Accept header for JSON format.
            // 为JSON格式添加一个Accept报头
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.GetAsync("api/GetEmployees").Result; //转换为同步方法 
            var sysUsers = (unitOfWork.SysUserRepository.Get()).ToList();
            return sysUsers;
        }
        [ActionName("GetRoles")]
        public List<SysRole> GetRoles()
        {
            var sysRoles = (unitOfWork.SysRoleRepository.Get()).ToList();
            return sysRoles;
        }
        [HttpPost]
        [ActionName("GetID")]
        public int GetId([FromBody]int Id)
        {
            //var sysUsers = unitOfWork.SysUserRepository.Get().Where(s => s.ID == Id).ToList();
            return Id ;
        }
    }
}
