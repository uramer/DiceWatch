using System;
using System.Collections.Generic;

namespace Dice.Dice
{
  public class DiceResult
  {
    public SortedDictionary<D, IList<int>> rolls = new SortedDictionary<D, IList<int>>();
    public int? total;

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

    public byte[] Serialize()
    {
      var bytes = new List<byte>();

      var count = dice.Keys.Count;
      bytes.AddRange(BitConverter.GetBytes(count));
      
      foreach(var key in dice.Keys)
      {
        bytes.AddRange(BitConverter.GetBytes((int)key));
        bytes.AddRange(BitConverter.GetBytes(dice[key]));
      }

      return bytes.ToArray();
    }

    public static DiceSet Deserialize(byte[] input, out int length, int offset = 0)
    {
      int initialOffset = offset;
      var count = BitConverter.ToInt32(input, offset);
      offset += 4;
      var set = new DiceSet();

      for(int i = 0; i < count; i++)
      {
        D key = (D)BitConverter.ToInt32(input, offset);
        offset += 4;
        int value = BitConverter.ToInt32(input, offset);
        offset += 4;
        set.dice.Add(key, value);
      }

      length = offset - initialOffset;

      return set;
    }
  }
}
