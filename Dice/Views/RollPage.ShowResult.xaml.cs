using Dice.Dice;
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
    private void RemoveHandlers()
    {
      removeHandlers = new Dictionary<D, EventHandler> {
        { D.D4, (object sender, EventArgs e) => Remove(D.D4) },
        { D.D6, (object sender, EventArgs e) => Remove(D.D6) },
        { D.D8, (object sender, EventArgs e) => Remove(D.D8) },
        { D.D12, (object sender, EventArgs e) => Remove(D.D12) }
      };
    }

    private int RowSize(int count)
    {
      return (int)Math.Max(1, Math.Ceiling(Math.Sqrt(count)));
    }

    private double DieSize(int count)
    {
      double size = Width;
      double rowSize = RowSize(count); // align dice in a square
      size *= 0.75; // padding on the sides
      size /= rowSize; // fix multiple dice in a row
      size /= 1.1; // leave 10% for spacing;
      if (size < 8) size = 8; // too small to read
      if (size > 128) size = 128; // images are 128 px, don't scale them up
      return size;
    }


    private void RenderResult(DiceResult result)
    {
      int totalCount = result.rolls.Values.Sum(r => r.Count);
      int rowSize = RowSize(totalCount);

      var view = new Grid
      {
        HorizontalOptions = LayoutOptions.Center,
        VerticalOptions = LayoutOptions.Start,
        RowSpacing = 5,
        ColumnSpacing = 5
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
