using System.Threading.Tasks;
using Ubiq.InterfaceAPI;
using Ubiq.Attributes;
using Ubiq.Subcore;

[assembly: UbiqUID("279b74c7-799c-4ef4-81fb-ad02cc6e4a15")]

namespace ServiceDemo
{
    [AppDescription("ServiceDemo")]
    partial class ServiceDemo : UbiqService
    {
        private UbiqInterface _serviceOutPort0 = null;

        internal const string ServiceName = "ServiceDemo";


        protected override async Task Main()
        {
            _serviceOutPort0 = UbiqInterface.PublishInterface(this, ServiceName, InterfaceAttrs.Duplex);

            await UserSection();
        }
    }
}
