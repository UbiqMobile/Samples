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



using System;
using System.Reflection;
using System.Xml.Linq;
using System.Threading.Tasks;
using Ubiq.Graphics;
using Ubiq.Attributes;
using Ubiq.InterfaceAPI;


[assembly: UbiqUID("fc4a45fd-a726-4c31-8f44-9e1815c24add")]

namespace CameraUI
{
	
	[AppDescription("CameraUI")]
    public partial class CameraUI : MExtendedThreadApp
    {

		VisualElement _ubiqDesign;


	    //Initialized UI form and ubiq components
		private void InitUI()
        {
			_ubiqDesign = Screen.CreateElement("UbiqDesign");
        }

		//Main entry point of application
        protected override async Task MainOverride()
        {
			
			Screen.GrabResources(Assembly.GetExecutingAssembly());
            InitUI();
            await UserSection();
        }
    }
}

