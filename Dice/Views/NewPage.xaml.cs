using Dice.Services;
using System;
using Xamarin.Forms;
using Tizen.Wearable.CircularUI.Forms;

namespace Dice.Views
{
    public partial class NewPage : ContentPage
    {
        private readonly DiceSetCollection SetCollection;
        private readonly RollPage RollPage;

        public NewPage(DiceSetCollection setCollection, RollPage rollPage)
        {
            SetCollection = setCollection;
            RollPage = rollPage;

            InitializeComponent();
            RotaryEventManager.Rotated += OnRotated;
        }

        private void New(object sender, EventArgs e)
        {
            SetCollection.Get();
            RollPage.NextPage();
            Navigation.PopAsync();
        }

        void OnRotated(object sender, RotaryEventArgs args)
        {
            if (!args.IsClockwise)
                Navigation.PopAsync();
        }
    }
}
