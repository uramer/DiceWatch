using Dice.Services;
using System;
using Xamarin.Forms;
using Tizen.Wearable.CircularUI.Forms;

namespace Dice.Views
{
  public partial class ClearPage : ContentPage
  {
    private RollPage rollPage;

    public ClearPage(RollPage rollPage)
    {
      this.rollPage = rollPage;
      InitializeComponent();
    }

    private void Clear_Click(object sender, EventArgs e)
    {
      rollPage.Clear();
      Navigation.PopAsync();
    }

    private void Discard_Click(object sender, EventArgs e)
    {
      rollPage.Discard();
      Navigation.PopAsync();
    }
  } 
}
