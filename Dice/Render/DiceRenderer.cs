using Dice.Dice;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Dice.Render
{
  static class DiceRenderer
  {
    private static Dictionary<D, string> names = new Dictionary<D, string>
    {
      { D.D4, "D4" },
      { D.D6, "D6" },
      { D.D8, "D8" },
      { D.D12, "D12" },
      { D.D20, "D20" },
      { D.D10, "D10" }
    };

    private static string MakePath(D type, int value)
    {
      var die = DiceType.get(type);
      return $"dice/{names[type]}-{value - die.min + 1}.png";
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
