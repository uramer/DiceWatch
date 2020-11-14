using System;
using System.Collections.Generic;

namespace Dice.Dice
{
  public class DiceType
  {
    public readonly int min;
    public readonly int max;
    private readonly Random _random;

    public DiceType(int min, int max)
    {
      this.min = min;
      this.max = max;
      _random = new Random();
    }

    public int Roll()
    {
      return _random.Next(min, max);
    }

    public static DiceType D4 = new DiceType(1, 4);
    public static DiceType D6 = new DiceType(1, 6);
    public static DiceType D8 = new DiceType(1, 8);
    public static DiceType D12 = new DiceType(1, 12);

    private static Dictionary<D, DiceType> diceTypes = new Dictionary<D, DiceType>
    {
      { D.D4, D4 },
      { D.D6, D6 },
      { D.D8, D8 },
      { D.D12, D12 }
    };

    public static DiceType get(D type)
    {
      return diceTypes[type];
    }
  }

  public enum D
  {
    D4, D6, D8, D12
  }
}
