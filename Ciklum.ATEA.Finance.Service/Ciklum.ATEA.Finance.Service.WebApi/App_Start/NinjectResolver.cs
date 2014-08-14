using Ninject;
using Ninject.Activation;
using Ninject.Parameters;
using Ninject.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

namespace Ciklum.ATEA.Finance.Service.WebApi
{

    /// <summary>
    /// Updated from https://github.com/WebApiContrib/WebApiContrib.IoC.Ninject/blob/master/src/WebApiContrib.IoC.Ninject/NinjectResolver.cs
    /// Updated From http://www.strathweb.com/2012/11/asp-net-web-api-and-dependencies-in-request-scope/
    /// </summary>
    public class NinjectDependencyScope : IDependencyScope
    {
        private IResolutionRoot resolver;

        internal NinjectDependencyScope(IResolutionRoot resolver)
        {
            this.resolver = resolver;
        }

        public object GetService(Type serviceType)
        {
            return resolver.TryGet(serviceType,new IParameter[0]);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return resolver.GetAll(serviceType, new IParameter[0]);
        }

        public void Dispose()
        {
            IDisposable disposable = (IDisposable)resolver;
            if (disposable != null) disposable.Dispose();
            resolver = null;
        }
    }

    public class NinjectResolver : NinjectDependencyScope, IDependencyResolver
    {
        private IKernel kernel;

        public NinjectResolver(IKernel kernel) 
            : base(kernel)
        {
            this.kernel = kernel;
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectDependencyScope(kernel.BeginBlock());
        }
    }
}