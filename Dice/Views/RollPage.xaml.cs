using System;
using Xamarin.Forms;

namespace Dice.Views
{
    public partial class RollPage : ContentPage
    {
        private App App;
        private bool Appeared;

        public RollPage()
        {
            Appeared = false;
            App = (App)Application.Current;

            InitializeComponent();

            PreparePaging();
            App.Loaded += Loaded;

            PrepareRemoveHandlers();
        }

        private void Loaded(object sender = null, EventArgs e = null)
        {
            SwitchPage(0);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Appeared = true;
            UpdateDefault();
        }

        protected override void OnDisappearing()
        {
            base.OnAppearing();
            Appeared = false;
        }

        private void UpdateRoll()
        {
            RenderResult(Set.Roll());
        }

        private void UpdateDefault()
        {
            RenderResult(Set.Default());
        }

        public void Clear()
        {
            Set.Clear();
            UpdateDefault();
        }

        public void Discard()
        {
            SetCollection.Discard(Page);
            if (SetCollection.Count() == 0 || Page == 0)
                ShowPage(0);
            else
                PrevPage();
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ClearPage(this));
        }

        private void Add_Click(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DicePage(Set));
        }

        private void Roll_Click(object sender, EventArgs e)
        {
            UpdateRoll();
        }
    }
}
