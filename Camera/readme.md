[Examples list](../readme.md)

## Camera

This project demonstrates integration with external data source (unofficial grab ivideon thumbnails)

You can add a new camera using uniq id like a 100-066e23cd012e8f79afc37722e07bf694. There are three already hardcoded cameras. New cameras can be added via QR-code.

UI description: 
1. main screen shows list of the web-cameras and has add button that causes QR-code reading mode;
2. info screen is used for display errors or on waiting image loading;
3. image view screen.

**NB**: Real update timeout is about 5 second because this is timeout of external data source.

[CameraUI/Design/](./CameraUI/Design/)

UI markup and graphic resources.

[CameraUI/CameraUI.cs](./CameraUI/CameraUI.cs)

This auto-generated file contains system UbiqMobile platform code.

[CameraUI/UserSection.cs](./CameraUI/UserSection.cs)

Main business logic of the application 
