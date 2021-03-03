using Dice.Dice;
using Dice.Services;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;

namespace Dice.Views
{
    partial class RollPage : ContentPage
    {
        private DiceSetCollection SetCollection;
        private DiceSet Set;
        private int Page;

        private void PreparePaging()
        {
            SetCollection = App.DiceSets;
            Page = 0;
            Set = SetCollection.Get(Page);

            RotaryEventManager.Rotated += OnRotated;
        }
        public void NextPage()
        {
            SwitchPage(Page + 1);
        }

        public void PrevPage()
        {
            SwitchPage(Page - 1);
        }

        public void SwitchPage(int newPage)
        {
            if (newPage < 0)
                return;
            else if (newPage >= SetCollection.Count())
                ShowNewPage();
            else
                ShowPage(newPage);
        }

        private void ShowPage(int newPage)
        {
            Page = newPage;
            Set = SetCollection.Get(Page);
            UpdateDefault();
            RenderPageBar();
        }

        private void ShowNewPage()
        {
            Navigation.PushAsync(new NewPage(SetCollection, this));
        }

        void OnRotated(object sender, RotaryEventArgs args)
        {
            if (!Appeared)
                return;

            if (args.IsClockwise)
                NextPage();
            else
                PrevPage();
        }

        private void RenderPageBar()
        {
            PageBar.Content = Render.PageBar.Render(Page, SetCollection.Count());
        }
    }
}
