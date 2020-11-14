using Dice.Dice;
using System;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;

namespace Dice.Views
{
  public partial class RollPage : ContentPage
  {
    private DiceSet set = new DiceSet();

    public RollPage()
    {
      InitializeComponent();

      RemoveHandlers();

      Clear();
    }

    protected override void OnAppearing()
    {
      UpdateDefault();
      UpdateChildrenLayout();
      EnableVoice();
    }

    protected override void OnDisappearing()
    {
      base.OnDisappearing();
      DisableVoice();
    }

    private void RegisterSwipes()
    {
      /*mainScroll.GestureRecognizers.Add(new SwipeGestureRecognizer
      {
        Direction = SwipeDirection.Down,
        Command =
      });*/
    }

    private void UpdateRoll()
    {
      RenderResult(set.Roll());
    }

    private void UpdateDefault()
    {
      RenderResult(set.Default());
    }

    private void Clear()
    {
      set.Clear();
      UpdateDefault();
    }

    private void Roll_Click(object sender, EventArgs e)
    {
      UpdateRoll();
    }

    private void Clear_Click(object sender, EventArgs e)
    {
      Clear();
    }

    private void Add_Click(object sender, EventArgs e)
    {
      Navigation.PushAsync(new DicePage(set));
    }

    public void OnRotated(object sender, RotaryEventArgs args)
    {
      
    }
  }
}
