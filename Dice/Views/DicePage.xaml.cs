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
        private readonly DiceSet Set;
        private readonly Vibrator Vibrator;

        public DicePage(DiceSet set)
        {
            Set = set;
            Vibrator = Vibrator.Vibrators[0];

            InitializeComponent();
            PrepareAddHandlers();
            RenderDiceButtons();
        }

        private void Add(D type, View sender)
        {
            sender.FadeTo(0.5, 150);
            Vibrator.Vibrate(150, 50);
            Set.Add(type);
            sender.FadeTo(1.0, 150);
        }

        private Dictionary<D, EventHandler> AddHandlers;
        private void PrepareAddHandlers()
        {
            AddHandlers = new Dictionary<D, EventHandler>();
            foreach (D type in DiceType.Types)
                AddHandlers.Add(type, (object sender, EventArgs e) => Add(type, sender as View));
        }

        private const int START_ROW = 1;
        private const int START_COLUMN = 1;
        private const int ROW_SIZE = 3;
        private void RenderDiceButtons()
        {
            int row = START_ROW;
            int column = START_COLUMN;
            foreach (D type in DiceType.Types)
            {
                var die = DiceType.get(type);
                var dieItem = new ImageButton();
                dieItem.Source = DiceRenderer.Image(type, die.max);
                dieItem.Clicked += AddHandlers[type];

                DiceGrid.Children.Add(dieItem, column, row);
                column++;
                if (column == START_COLUMN + ROW_SIZE)
                {
                    column = START_COLUMN;
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
