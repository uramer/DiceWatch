using Dice.Services;
using System;
using Xamarin.Forms;
using Tizen.Wearable.CircularUI.Forms;

namespace Dice.Views
{
  public partial class NewPage : ContentPage
  {
    private DiceSetCollection provider;
    private RollPage rollPage;

    public NewPage(DiceSetCollection provider, RollPage rollPage)
    {
      this.provider = provider;
      this.rollPage = rollPage;
      InitializeComponent();

      RotaryEventManager.Rotated += OnRotated;
    }

    private void New(object sender, EventArgs e)
    {
      provider.Get();
      rollPage.NextPage();
      Navigation.PopAsync();
    }

    void OnRotated(object sender, RotaryEventArgs args)
    {
      if (!args.IsClockwise)
        Navigation.PopAsync();
    }
  } 
}
