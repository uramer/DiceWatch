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
        private IDictionary<D, int> Dice = new Dictionary<D, int>();

        public void Add(D type)
        {
            if (!Dice.ContainsKey(type))
                Dice[type] = 0;
            Dice[type] = Dice[type] + 1;
        }

        public void Remove(D type)
        {
            if (Dice.ContainsKey(type))
                Dice[type] = Dice[type] - 1;
        }

        public void Clear()
        {
            Dice.Clear();
        }

        public DiceResult Roll()
        {
            var results = new DiceResult();
            int total = 0;
            foreach (var item in Dice)
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
            foreach (var item in Dice)
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

            var count = Dice.Keys.Count;
            bytes.AddRange(BitConverter.GetBytes(count));

            foreach (var key in Dice.Keys)
            {
                bytes.AddRange(BitConverter.GetBytes((int)key));
                bytes.AddRange(BitConverter.GetBytes(Dice[key]));
            }

            return bytes.ToArray();
        }

        public static (DiceSet, int) Deserialize(byte[] input, int offset = 0)
        {
            int initialOffset = offset;
            var count = BitConverter.ToInt32(input, offset);
            offset += 4;
            var set = new DiceSet();
            for (int i = 0; i < count; i++)
            {
                D key = (D)BitConverter.ToInt32(input, offset);
                offset += 4;
                int value = BitConverter.ToInt32(input, offset);
                offset += 4;
                set.Dice.Add(key, value);
            }

            int length = offset - initialOffset;
            return (set, length);
        }
    }
}
