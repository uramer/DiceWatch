using Dice.Dice;
using Dice.Render;
using Dice.Services;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;

namespace Dice.Views
{
    partial class RollPage : ContentPage
    {
        private DiceSetCollection setCollection;
        private DiceSet set;
        private int page;

        private void PreparePaging()
        {
            setCollection = app.diceSets;
            page = 0;
            set = setCollection.Get(page);

            RotaryEventManager.Rotated += OnRotated;
        }
        public void NextPage()
        {
            SwitchPage(page + 1);
        }

        public void PrevPage()
        {
            SwitchPage(page - 1);
        }

        public void SwitchPage(int newPage)
        {
            if (newPage < 0)
                return;
            else if (newPage >= setCollection.Count())
                ShowNewPage();
            else
                ShowPage(newPage);
        }

        private void ShowPage(int newPage)
        {
            page = newPage;
            set = setCollection.Get(page);
            UpdateDefault();
            RenderPageBar();
        }

        private void ShowNewPage()
        {
            Navigation.PushAsync(new NewPage(setCollection, this));
        }

        void OnRotated(object sender, RotaryEventArgs args)
        {
            if (!appeared)
                return;
            if (args.IsClockwise)
                NextPage();
            else
                PrevPage();
        }

        private void RenderPageBar()
        {
            pageBar.Content = PageBar.Render(page, setCollection.Count());
            Logger.Info("Render page bar");
        }
    }
}
