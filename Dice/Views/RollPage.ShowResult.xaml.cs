using Dice.Dice;
using Dice.Render;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Dice.Views
{
  partial class RollPage: ContentPage
  {
    private void Remove(D type)
    {
      set.Remove(type);
      UpdateDefault();
    }

    private Dictionary<D, EventHandler> removeHandlers;
    private void PrepareRemoveHandlers()
    {
      removeHandlers = new Dictionary<D, EventHandler>();
      foreach(D type in DiceType.Types) {
        removeHandlers.Add(type, (object sender, EventArgs e) => Remove(type));
      };
    }

    private int RowSize(int count)
    {
      return (int)Math.Max(1, Math.Ceiling(Math.Sqrt(count)));
    }

    private void RenderResult(DiceResult result)
    {
      int totalCount = result.rolls.Values.Sum(r => r.Count);
      int rowSize = RowSize(totalCount);

      var view = new Grid
      {
        HorizontalOptions = LayoutOptions.CenterAndExpand,
        VerticalOptions = LayoutOptions.FillAndExpand,
        RowSpacing = 3,
        ColumnSpacing = 3
      };

      for(int i = 0; i < rowSize; i++)
      {
        view.RowDefinitions.Add(new RowDefinition());
        view.ColumnDefinitions.Add(new ColumnDefinition());
      }

      int row = 0;
      int column = 0;
      foreach (var typeRolls in result.rolls)
      {
        view.RowDefinitions.Add(new RowDefinition());

        var type = typeRolls.Key;
        var rolls = typeRolls.Value;
        int count = rolls.Count;

        for (int i = 0; i < count; i++)
        {
          var tapGesture = new TapGestureRecognizer();
          tapGesture.Tapped += removeHandlers[type];

          var dieView = DiceRenderer.Render(type, rolls[i]);
          dieView.GestureRecognizers.Add(tapGesture);

          view.Children.Add(dieView, column, row);

          column++;
          if (column >= rowSize)
          {
            column = 0;
            row++;
          }
        }
      }

      rollResult.Content = view;
      totalLabel.Text = result.total?.ToString() ?? "";
    }
  }
}
