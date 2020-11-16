using Dice.Dice;
using Dice.Render;
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
      addHandlers = new Dictionary<D, EventHandler>();
      foreach (D type in DiceType.Types) {
        addHandlers.Add(type, (object sender, EventArgs e) => Add(type, sender as View));
      }
    }

    private const int startRow = 1;
    private const int startColumn = 1;
    private const int rowSize = 3;
    private void RenderDiceButtons()
    {
      int row = startRow;
      int column = startColumn;
      foreach (D type in DiceType.Types)
      {
        var die = DiceType.get(type);
        var dieItem = new ImageButton();
        dieItem.Source = DiceRenderer.Image(type, die.max);
        dieItem.Clicked += addHandlers[type];

        diceGrid.Children.Add(dieItem, column, row);
        column++;
        if(column == startColumn + rowSize)
        {
          column = startColumn;
          row++;
        }
      }
    }

    private void Back(object sender, EventArgs e)
    {
      Navigation.PopAsync();
    }
  } 
}
