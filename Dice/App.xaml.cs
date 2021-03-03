using Dice.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Dice
{
    public partial class App : Application
    {
        public Storage storage { get; }
        public DiceSetCollection diceSets { get; }

        public App()
        {
            storage = new Storage();
            diceSets = new DiceSetCollection();
            InitializeComponent();
        }

        public delegate void EmptyEvent();

        public event EventHandler Loaded;

        private async void LoadSets()
        {
            diceSets.Load(await storage.ReadSets());
            Loaded?.Invoke(this, new EventArgs());
        }

        private async void SaveSets()
        {
            await storage.WriteSets(diceSets.Save());
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
