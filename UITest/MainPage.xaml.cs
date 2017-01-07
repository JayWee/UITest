using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Gpio;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UITest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            InitGpio();
            LEDSlider.ValueChanged += LEDSlider_ValueChanged;

        }

        private void LEDSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            int OldB = Convert.ToInt32(e.OldValue);
            led[OldB - 1].Write(GpioPinValue.Low);

            string Text = Convert.ToString(e.NewValue);
            LEDNumber.Text = Text;

            int NewB = Convert.ToInt32(e.NewValue);
            
            if (NewB > 12 || NewB < 1)
                LEDNumber.Text = "Something went wrong!";

            led[NewB - 1].Write(GpioPinValue.High);


        }

        private void InitGpio()
        {
            var gpio = GpioController.GetDefault();

            for (int i = 0; i <= 11; i++)
            {
                led[i] = gpio.OpenPin(LED[i]);
                led[i].Write(GpioPinValue.Low);
                led[i].SetDriveMode(GpioPinDriveMode.Output);
            }
            
        }
        private int D = 1;
        int[] LED = {25, 24, 23, 22, 27, 18, 17, 11, 10, 9, 8, 7 };
        GpioPin[] led = new GpioPin[12]; 
        

    }
}
