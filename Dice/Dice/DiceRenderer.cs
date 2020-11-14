using System.Collections.Generic;
using Xamarin.Forms;

namespace Dice.Dice
{
  static class DiceRenderer
  {
    private static Dictionary<D, string> names = new Dictionary<D, string>
    {
      { D.D4, "D4" },
      { D.D6, "D6" },
      { D.D8, "D8" },
      { D.D12, "D12" }
    };

    private static string MakePath(D type, int value)
    {
      return $"dice/{names[type]}-{value}.png";
    }

    public static string Text(D type)
    {
      return names[type];
    }

    public static ImageSource Image(D type, int value)
    {
      return ImageSource.FromFile(MakePath(type, value));
    }

    public static View Render(D type, int value)
    {
      return new Image
      {
        Source = Image(type, value),
        Aspect = Aspect.AspectFit
      };
    }
  }
}
