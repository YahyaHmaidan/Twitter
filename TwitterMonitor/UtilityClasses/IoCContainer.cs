using System;
using System; 
using System.Collections.Generic; 
using System.Web.Http; 
using System.Web.Http.Dependencies; 
using Microsoft.Practices.Unity; 

namespace TwitterMonitor
{   
    class IoCContainer : ScopeContainer, IDependencyResolver 
    { 
        public IoCContainer(IUnityContainer container) 
            : base(container) 
        { 
        } 
 
        public IDependencyScope BeginScope() 
        { 
            var child = container.CreateChildContainer(); 
            return new ScopeContainer(child); 
        } 
    } 
}
