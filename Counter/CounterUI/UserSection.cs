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

namespace CounterUI
{
    partial class CounterUI
    {
        private int _counter;

        TextBlock counterText;
        SelectableArea plusButton;
        SelectableArea minusButton;

        //User section for bussines logic
        //Your code should be inserted here
        protected async Task UserSection()
        {
            _counter = 0;

            Screen.Content = _ubiqDesign;
            counterText = _ubiqDesign.GetChildByName("tbNumber") as TextBlock;
            minusButton = _ubiqDesign.GetChildByName("btMinus") as SelectableArea;
            plusButton = _ubiqDesign.GetChildByName("btPlus") as SelectableArea;

            minusButton.Clicked += minusButton_Clicked;
            plusButton.Clicked += plusButton_Clicked;

            for (; ; )
            {
                await Wait();
            }
        }

        void plusButton_Clicked(SelectableArea sender, EventArgs e)
        {
            _counter++;
            counterText.Text = _counter.ToString();
        }

        void minusButton_Clicked(SelectableArea sender, EventArgs e)
        {
            _counter--;
            counterText.Text = _counter.ToString();
        }
    }
}


