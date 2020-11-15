using Dice.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Dice
{
  public partial class App : Application
  {
    public  StorageService storage { get; }
    public DiceSetProvider diceProvider { get; }

    public App()
    {
      storage = new StorageService();
      diceProvider = new DiceSetProvider();
      InitializeComponent();
    }

    public delegate void EmptyEvent();

    public event EventHandler Loaded;

    private async void LoadSets()
    {
      diceProvider.Load(await storage.ReadSets());
      Loaded?.Invoke(this, new EventArgs());
    }

    private async void SaveSets()
    {
      await storage.WriteSets(diceProvider.Save());
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
