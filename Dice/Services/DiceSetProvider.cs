using Dice.Dice;
using System.Collections.Generic;
using System.Linq;

namespace Dice.Services
{
  public class DiceSetProvider
  {
    private LinkedList<DiceSet> sets;

    public DiceSetProvider()
    {
      sets = new LinkedList<DiceSet>();
    }

    public int New()
    {
      return sets.Count;
    }

    private LinkedListNode<DiceSet> Find(int id)
    {
      var node = sets.First;
      for (int i = 0; i < id; i++) node = node.Next;
      return node;
    }

    public DiceSet Get()
    {
      return Get(New());
    }

    public DiceSet Get(int id)
    {
      if(sets.Count <= id)
        return sets.AddLast(new DiceSet()).Value;
      else
        return Find(id).Value;
    }

    public void Discard(int id)
    {
      sets.Remove(Find(id));
    }

    public int Count()
    {
      return sets.Count;
    }

    public void Load(IList<DiceSet> newSets)
    {
      sets.Clear();
      foreach(var set in newSets)
      {
        sets.AddLast(set);
      }
    }

    public IList<DiceSet> Save()
    {
      return sets.ToList();
    }
  }
}
