//-----------------------------User-section----------------------------------------------------
//  <User-defined part of application>
//
//      This is partial class that can be invoked from main entry point
//      This class is purposed for user-defined bussines logic of the application
//      The user should add proprietary code.
//      All modifications will be preserved during all automatic re-generations of the project
//  </User-defined part of application>
//----------------------------------------------------------------------------------------------


using System;
using Ubiq.Graphics;
using System.Threading.Tasks;

namespace DemoUI
{
    partial class DemoUI
    {
        private Button _btInc;
        TextBlock _value;
	    //User section for bussines logic
	    //Your code should be inserted here
        protected async Task UserSection()
        {
            _application0InPort0.DataUpdated += _application0InPort0_DataUpdated;
            _btInc = _ubiqDesign.GetChildByName("Button1") as Button;
            _value = _ubiqDesign.GetChildByName("TextLabel1") as TextBlock;

            _btInc.Pressed += _btInc_Pressed;
			Screen.Content = _ubiqDesign;
            _application0InPort0.InitAPI();
			for (; ; )
            {
                await Wait();
            }
        }

        void _btInc_Pressed(object sender, EventArgs e)
        {
            _application0InPort0.TestAPI();
        }
	
        void _application0InPort0_DataUpdated(int i)
        {
            _value.Text = i.ToString();
        }
    }
}


