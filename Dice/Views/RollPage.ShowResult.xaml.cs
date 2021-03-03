using Dice.Dice;
using Dice.Render;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Dice.Views
{
    partial class RollPage : ContentPage
    {
        private void Remove(D type)
        {
            Set.Remove(type);
            UpdateDefault();
        }

        private Dictionary<D, EventHandler> RemoveHandlers;
        private void PrepareRemoveHandlers()
        {
            RemoveHandlers = new Dictionary<D, EventHandler>();
            foreach (D type in DiceType.Types)
                RemoveHandlers.Add(type, (object sender, EventArgs e) => Remove(type));
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
                HorizontalOptions = LayoutOptions.Fill,
                RowSpacing = 3,
                ColumnSpacing = 3
            };
            view.BindingContext = RollResult;
            view.SetBinding(WidthProperty, "Width");

            for (int i = 0; i < rowSize; i++)
            {
                view.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                view.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
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
                    tapGesture.Tapped += RemoveHandlers[type];

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

            RollResult.Content = view;
            TotalLabel.Text = result.total?.ToString() ?? "";
        }
    }
}
