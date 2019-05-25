using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using webapiCodefirst.Models;

namespace webapiCodefirst.DAL
{
    class AccountInitializer :
        DropCreateDatabaseIfModelChanges<AccountContext>
    {
        protected override void Seed(AccountContext context)
        {
           
        }
    }
}