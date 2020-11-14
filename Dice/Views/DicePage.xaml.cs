using Dice.Dice;
using System;
using System.Collections.Generic;
using Tizen.System;
using Xamarin.Forms;

namespace Dice.Views
{
  public partial class DicePage : ContentPage
  {
    private DiceSet set;
    private readonly Vibrator vibrator;

    public DicePage(DiceSet set)
    {
      this.set = set;
      vibrator = Vibrator.Vibrators[0];

      InitializeComponent();

      AddHandlers();

      RenderDiceButtons();
    }

    private void Add(D type, View sender)
    {
      sender.FadeTo(0.5, 150);
      vibrator.Vibrate(150, 50);
      set.Add(type);
      sender.FadeTo(1.0, 150);
    }

    private Dictionary<D, EventHandler> addHandlers;
    private void AddHandlers()
    {
      addHandlers = new Dictionary<D, EventHandler> {
        { D.D4, (object sender, EventArgs e) => Add(D.D4, sender as View) },
        { D.D6, (object sender, EventArgs e) => Add(D.D6, sender as View) },
        { D.D8, (object sender, EventArgs e) => Add(D.D8, sender as View) },
        { D.D12, (object sender, EventArgs e) => Add(D.D12, sender as View) }
      };
    }
    private void RenderDiceButtons()
    {
      var types = new List<D> {
        D.D4, D.D6, D.D8, D.D12
      };
      foreach (var type in types)
      {
        var die = DiceType.get(type);
        var dieItem = new ImageButton();
        dieItem.Source = DiceRenderer.Image(type, die.max);
        dieItem.Clicked += addHandlers[type];
        dieItem.Margin = new Thickness(5);

        diceList.Children.Add(dieItem);
      }
    }

    private void Back(object sender, EventArgs e)
    {
      Navigation.PopAsync();
    }
  } 
}
