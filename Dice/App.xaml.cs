using Dice.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Dice
{
    public partial class App : Application
    {
        public readonly Storage Storage;
        public readonly DiceSetCollection DiceSets;

        public App()
        {
            Storage = new Storage();
            DiceSets = new DiceSetCollection();
            InitializeComponent();
        }

        public delegate void EmptyEvent();

        public event EventHandler Loaded;

        private async void LoadSets()
        {
            DiceSets.Load(await Storage.ReadSets());
            Loaded?.Invoke(this, new EventArgs());
        }

        private async void SaveSets()
        {
            await Storage.WriteSets(DiceSets.Save());
        }

        protected override void OnSleep()
        {
            SaveSets();
        }

        protected override void OnResume()
        {
            LoadSets();
        }
    }
}
