using Dice.Services;
using System;
using Xamarin.Forms;
using Tizen.Wearable.CircularUI.Forms;

namespace Dice.Views
{
  public partial class NewPage : ContentPage
  {
    private DiceSetProvider provider;
    private RollPage rollPage;

    public NewPage(DiceSetProvider provider, RollPage rollPage)
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
