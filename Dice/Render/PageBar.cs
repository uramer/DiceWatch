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
      var view = new Grid
      {
        HorizontalOptions = LayoutOptions.CenterAndExpand,
        VerticalOptions = LayoutOptions.CenterAndExpand
      };

      view.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });

      for (int i = 0; i <= total; i++)
      {
        view.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

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
        }, i, 0);
      }

      return view;
    }
  }
}
