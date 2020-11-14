using System.Collections.Generic;

namespace Dice.Dice
{
  public class DiceResult
  {
    public SortedDictionary<D, IList<int>> rolls = new SortedDictionary<D, IList<int>>();
    public int? total = null;

  }

  public class DiceSet
  {
    private IDictionary<D, int> dice = new Dictionary<D, int>();

    public void Add(D type)
    {
      if (!dice.ContainsKey(type))
        dice[type] = 0;
      dice[type] = dice[type] + 1;
    }

    public void Remove(D type)
    {
      if (dice.ContainsKey(type))
        dice[type] = dice[type] - 1;
    }

    public void Clear()
    {
      dice.Clear();
    }

    public DiceResult Roll()
    {
      var results = new DiceResult();
      int total = 0;
      foreach (var item in dice)
      {
        if (item.Value == 0) continue;
        results.rolls[item.Key] = new List<int>();
        var die = DiceType.get(item.Key);
        for (int i = 0; i < item.Value; i++)
        {
          var roll = die.Roll();
          results.rolls[item.Key].Add(roll);
          total += roll;
        }
      }
      results.total = total;

      return results;
    }

    public DiceResult Default()
    {
      var results = new DiceResult();

      foreach (var item in dice)
      {
        if (item.Value == 0) continue;
        results.rolls[item.Key] = new List<int>();
        var die = DiceType.get(item.Key);
        for (int i = 0; i < item.Value; i++)
          results.rolls[item.Key].Add(die.max);
      }

      return results;
    }
  }
}
