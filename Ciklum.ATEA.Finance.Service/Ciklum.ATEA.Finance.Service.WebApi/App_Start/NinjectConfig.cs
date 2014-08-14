using Ninject;
using Ninject.Extensions.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using Ciklum.ATEA.Finance.Service.Domain.Model;
//using Ciklum.ATEA.Finance.Service.Db.Impl;
using System.Data.Entity;

namespace Ciklum.ATEA.Finance.Service.WebApi
{
    public static class NinjectConfig
    {
        public static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                //Uses the ninject conventions for simplified setup.
                kernel.Bind(x => x.FromAssembliesMatching("Ciklum.ATEA.*")
                    .SelectAllClasses()
                    .BindDefaultInterface());

               // kernel.Bind<ICompanyRepository>().To<CompanyRepository>();
                return kernel;
            }
            catch (Exception)
            {
                throw;
            }

             {
       
    }



        }
    }
}