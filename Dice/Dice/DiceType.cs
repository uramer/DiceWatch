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
      return _random.Next(min, max + 1);
    }

    public static readonly DiceType D4 = new DiceType(1, 4);
    public static readonly DiceType D6 = new DiceType(1, 6);
    public static readonly DiceType D8 = new DiceType(1, 8);
    public static readonly DiceType D12 = new DiceType(1, 12);
    public static readonly DiceType D20 = new DiceType(1, 20);
    public static readonly DiceType D10 = new DiceType(0, 9);

    private static Dictionary<D, DiceType> diceTypes = new Dictionary<D, DiceType>
    {
      { D.D4, D4 },
      { D.D6, D6 },
      { D.D8, D8 },
      { D.D12, D12 },
      { D.D20, D20 },
      { D.D10, D10 },
    };

    public static DiceType get(D type)
    {
      return diceTypes[type];
    }

    public static readonly D[] Types = new D[] { D.D4, D.D6, D.D8, D.D12, D.D20, D.D10 };
  }

  public enum D
  {
    D4, D6, D8, D12, D20, D10
  }
}
