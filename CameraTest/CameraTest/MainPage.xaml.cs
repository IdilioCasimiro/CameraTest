using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using Xamarin.Forms;

namespace CameraTest
{
    public partial class MainPage : ContentPage
    {
        private IMedia media;

        public MainPage()
        {
            media = CrossMedia.Current;
            InitializeComponent();
        }

        private async void TakePicture(object sender, EventArgs e)
        {
            //Inicializar os componentes da camera 
            await media.Initialize();

            //Verificar se o dispositivo possui uma camera e permite tirar fotos
            if (!media.IsCameraAvailable || !media.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            //Tirar a foto
            var file = await media.TakePhotoAsync(new StoreCameraMediaOptions()
            {
                Directory = "KIS Mobile",
                Name = "Foto",
                AllowCropping = true, //funciona apenas no iOS e UWP
                CompressionQuality = 92,
                PhotoSize = PhotoSize.Medium,
                DefaultCamera = CameraDevice.Front
            });

            if (file == null)
            {
                return;
            }

            await DisplayAlert("File Location", file.Path, "OK");

            foto.Source = ImageSource.FromStream(() =>
            {
                return file.GetStream();
            });
        }
    }
}
