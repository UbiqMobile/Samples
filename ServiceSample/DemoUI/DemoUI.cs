﻿//------------------------------------------------------------------------------
// <auto-generated class>
//     This code was generated by Ubiq Mobile plug-in (read only).
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
//     You MUST NOT modify this source
//     This class contains initialization section and descriptions of 
//     component's interfaces
// </auto-generated class>
//------------------------------------------------------------------------------

using System.Reflection;
using System.Threading.Tasks;
using Ubiq.Graphics;
using Ubiq.Attributes;
using ServiceDemo;


[assembly: UbiqUID("13ed8f67-a3bd-4bc0-bd19-e2e60dd3a77e")]

namespace DemoUI
{

    [AppDescription("DemoUI")]
    public partial class DemoUI : MExtendedThreadApp
    {
        VisualElement _ubiqDesign;
        private ServiceDemoApiName _application0InPort0;


        //Initialized UI form and ubiq components
        private void InitUI()
        {
            _ubiqDesign = Screen.CreateElement("UbiqDesign");
        }

        //Main entry point of application
        protected override async Task MainOverride()
        {
            _application0InPort0 = new ServiceDemoApiName(this);

            Screen.GrabResources(Assembly.GetExecutingAssembly());
            InitUI();
            await UserSection();
        }
    }
}

