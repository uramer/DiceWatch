using System;
using Xamarin.Forms;
using System.Collections.Generic;

namespace Dice.Views
{
    public partial class ClearPage : ContentPage
    {
        private readonly RollPage RollPage;

        private class ClearItem
        {
            public ImageSource Image { get; set; }
            public string Text { get; set; }
            public EventHandler Tapped { get; set; }
        }

        public ClearPage(RollPage rollPage)
        {
            RollPage = rollPage;
            InitializeComponent();
            MenuList.ItemsSource = new List<ClearItem>
            {
                new ClearItem
                {
                    Image = ImageSource.FromFile("Clear.png"),
                    Text = "Clear",
                    Tapped = ClearTapped
                },
                new ClearItem
                {
                    Image = ImageSource.FromFile("Discard.png"),
                    Text = "Discard",
                    Tapped = DiscardTapped
                }
            };
        }

        private void ClearTapped(object sender, EventArgs e)
        {
            RollPage.Clear();
            Navigation.PopAsync();
        }

        private void DiscardTapped(object sender, EventArgs e)
        {
            RollPage.Discard();
            Navigation.PopAsync();
        }

        private void MenuListTapped(object sender, ItemTappedEventArgs e)
        {
            var item = (ClearItem)e.Item;
            item.Tapped(sender, e);
        }
    }
}
