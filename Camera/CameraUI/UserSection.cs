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
using System.Collections.Generic;
using System.Net;
using Ubiq.Graphics;
using System.Threading.Tasks;
using Ubiq.Subcore;

namespace CameraUI
{
    partial class CameraUI
    {
        private VisualElement _screenView;
        private VisualElement _screenList;
        private VisualElement _screenInfo;

        enum AppState
        {
            List, // первый экран, список камер
            StartingView, // ожидание первой картинки с камеры
            View, // просмотр камеры
            Error, // ошибка
        }
        private AppState _state;

        #region screen list

        private ListBox screenList_List;
        private SelectableArea screenList_BtAdd;
        #endregion

        #region screen info

        private TextBlock _screenInfo_Caption;
        private TextBlock _screenInfo_MainText;
        private SelectableArea _screenInfo_BtBack;

        #endregion

        #region screen view

        private ImageBlock _screenView_Image;
        private TextBlock _screenView_CameraName;
        private SelectableArea _screenView_BtBack;
        #endregion

        private List<String> _cameras;
        private int _activeCamera;

        private Image _lastImage;

        BarcodeReader _barcodeReader;

        //User section for bussines logic
        //Your code should be inserted here
        protected async Task UserSection()
        {
            _screenView = Screen.CreateElement("Design.ScreenView");
            _screenList = Screen.CreateElement("Design.ScreenList");
            _screenInfo = Screen.CreateElement("Design.ScreenInfo");

            _state = AppState.List;

            #region init
            // load Screen List controls
            screenList_List = _screenList.GetChildByName("MainList") as ListBox;
            screenList_List.Clickable = true;
            screenList_List.Clicked += screenList_List_Clicked;
            screenList_BtAdd = _screenList.GetChildByName("BtAdd") as SelectableArea;
            screenList_BtAdd.Clicked += screenList_BtAdd_Clicked;

            // load screen info controls
            _screenInfo_Caption = _screenInfo.GetChildByName("tbCaption") as TextBlock;
            _screenInfo_MainText = _screenInfo.GetChildByName("tbMainText") as TextBlock;
            _screenInfo_BtBack = _screenInfo.GetChildByName("BtBack") as SelectableArea;
            _screenInfo_BtBack.Clicked += BackToSCreenList;

            // load screen view controls

            _screenView_Image = _screenView.GetChildByName("MainImage") as ImageBlock;
            _screenView_CameraName = _screenView.GetChildByName("TlName") as TextBlock;
            _screenView_BtBack = _screenView.GetChildByName("BtBack") as SelectableArea;
            _screenView_BtBack.Clicked += BackToSCreenList;

            // qrcode reader
            _barcodeReader = new BarcodeReader(this);

            _barcodeReader.BarcodeReady += (s, be) =>
            {
                if (_barcodeReader.RC == FunctionRC.OK)
                {
                    AddCamera(_barcodeReader.MainData);
                }
                else
                {
                    // todo error reporting
                    //barcodeState = TPositioningState.ErrorReceived;
                }
            };

            #endregion

            _cameras = new List<string>();
            _state = AppState.List;

            AddCamera("100-066e23cd012e8f79afc37722e07bf694");
            AddCamera("100-f0a176294a5c51a822ee6f4c0ae89a2c");
            AddCamera("100-2033519638a3a4abb9b41d3a04bdb666");
#if false
            // можно включить
            AddCamera("100-2fbb74c566c49f4cfb6e2ea2ce1c9b4d");
            AddCamera("100-40580f419398b68e0a18fa0fc319101b");
            AddCamera("100-1bb9cf88076de6341a294182a49c6cf5");
#endif

            for (; ; )
            {
                switch (_state)
                {
                case AppState.List:
                    Screen.Content = _screenList;
                    break;
                case AppState.StartingView:
                    // start timer
                    Schedule(1000, GetPicture);
                    _screenInfo_Caption.Text = "Подключение";
                    _screenInfo_MainText.Text = "Пожалуйста, подождите. Идет подключение...";
                    Screen.Content = _screenInfo;
                    break;
                case AppState.View:
                    _screenView_Image.Image = _lastImage;
                    _screenView_CameraName.Text = _cameras[_activeCamera];
                    Screen.Content = _screenView;
                    break;
                case AppState.Error:
                    Screen.Content = _screenInfo;
                    break;
                }
                await Wait();
            }
        }

        void BackToSCreenList(SelectableArea sender, EventArgs e)
        {
            _state = AppState.List;
        }

        void AddCamera(String camera)
        {
            // добавляем в список камер
            _cameras.Add(camera);
            //создаем новый контрол для списка
            var item = Screen.CreateElement("Design.ControlListItem");
            var tlName = item.GetChildByName("TlName") as TextBlock;
            // задаем текст созданного контрола
            tlName.Text = camera;
            // добавляем контрол в список на экране
            screenList_List.Children.Add(item);
        }

        public static double DateTimeToUnixTimestamp(DateTime dateTime)
        {
            return (dateTime - new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds;
        }

        private bool GetPicture()
        {
            if (_state != AppState.View && _state != AppState.StartingView)
            {
                return false;
            }

            try
            {
                WebRequest requestPic = WebRequest.Create(String.Format("https://streaming.ivideon.com/preview/live?q=2&sessionId=demo&camera=0&ts={0}&server={1}", DateTimeToUnixTimestamp(DateTime.UtcNow), _cameras[_activeCamera]));
                WebResponse responsePic = requestPic.GetResponse();
                using (var img = System.Drawing.Image.FromStream(responsePic.GetResponseStream()))
                {
                    _lastImage = new Image(new System.Drawing.Bitmap(img), EncodingFormat.EFormatJpeg, false);
#if false
                    // for debug 
                    img.Save(UbiqEnvironment.BaseDirectory+"\\_"+_cameras[_activeCamera]+".jpg");
#endif
                }
                if (_state == AppState.StartingView)
                {
                    _state = AppState.View;
                }
            }
            catch (Exception e)
            {
                _state = AppState.Error;
                _screenInfo_Caption.Text = "Ошибка";
                _screenInfo_MainText.Text = "Камера недоступна.";

            }

            return _state == AppState.View;
        }

        void screenList_List_Clicked(ListBox sender, EventArgs<int> e)
        {
            _activeCamera = sender.Focus - 1;
            _state = AppState.StartingView;
        }

        private void screenList_BtAdd_Clicked(SelectableArea sender, EventArgs e)
        {
            _barcodeReader.Read();

        }
    }
}



