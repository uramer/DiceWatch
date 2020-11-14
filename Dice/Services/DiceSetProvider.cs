using Dice.Dice;
using System.Collections.Generic;

namespace Dice.Services
{
  class DiceSetProvider
  {
    private int _id = 0;

    private Dictionary<int, DiceSet> sets;

    public DiceSetProvider()
    {
      sets = new Dictionary<int, DiceSet>();
    }

    public int New()
    {
      return _id++;
    }

    public DiceSet Get(int id)
    {
      if(!sets.ContainsKey(id))
        sets[id] = new DiceSet();
      return sets[id];
    }

    public void Discard(int id)
    {
      sets.Remove(id);
    }

    public int Count()
    {
      return sets.Keys.Count;
    }
  }
}
