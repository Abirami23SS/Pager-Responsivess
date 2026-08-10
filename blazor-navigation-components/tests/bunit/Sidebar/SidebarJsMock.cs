using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunit;
using Syncfusion.Blazor.Tests.Base;
using Syncfusion.Blazor.Navigations;
using System.Collections.Generic;
using Bunit.JSInterop;
using System.Reflection;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Syncfusion.Blazor.Tests.Sidebar
{
    public class SidebarJsMock : BaseTestContext
    {
        protected bool DisableScriptManager = true;

        public SidebarJsMock()
        {
            this.BeforeEachRun();
            this.UpdateRequiredMockJSRuntime();
        }

        public virtual void BeforeEachRun()
        {
            JSInterop.Mode = JSRuntimeMode.Loose;
            Services.AddSyncfusionBlazor().Replace(ServiceDescriptor.Transient<IComponentActivator, SfComponentActivator>()); 
            Services.AddOptions();
        }

        public virtual void UpdateRequiredMockJSRuntime()
        {
            JSInterop.Setup<bool>("sfBlazor.isRendered", _ = true).SetResult(true);
            var createMeasureElements = JSInterop.Setup<object>("createMeasureElements");
            createMeasureElements.SetResult("");
            var isDevice = JSInterop.Setup<bool>("sfBlazor.isDevice", false);
            isDevice.SetResult(false);
            var import = JSInterop.Setup<string>("sfBlazor.import", _ => true);
            import.SetResult("");
        }

        public void Dispose()
        {
            base.Dispose();
            this.AfterEachRun();
        }

        public virtual void AfterEachRun() { }
    }
}
