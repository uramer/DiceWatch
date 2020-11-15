using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;

namespace Dice.Render
{
  static class PageBar
  {
    private static readonly ImageSource pageImage = ImageSource.FromFile("Page.png");
    private static readonly ImageSource currentImage = ImageSource.FromFile("PageOpen.png");
    private static readonly ImageSource newImage = ImageSource.FromFile("PageNew.png");

    public static View Render(int current, int total)
    {
      var view = new CircleStackLayout
      {
        HorizontalOptions = LayoutOptions.CenterAndExpand,
        VerticalOptions = LayoutOptions.CenterAndExpand,
        Orientation = StackOrientation.Horizontal
      };

      for (int i = 0; i <= total; i++)
      {
        var source = pageImage;
        if (i == current)
          source = currentImage;
        else if (i == total)
            source = newImage;

        view.Children.Add(new Image
        {
          VerticalOptions = LayoutOptions.CenterAndExpand,
          Aspect = Aspect.AspectFit,
          Source = source
        });
      }

      return view;
    }
  }
}
