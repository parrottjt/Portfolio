using System;
using System.Linq;
using FrikinCore.Enumeration;
using FrikinCore.Stats;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FrikinCore.Loot
{
    public static class UpdatedEnemyLootDrop
    {
        static LootItem[] loot;
        static float totalWeight;

        static void SetUpLootTable(float duck, float pHDuck, float superDuck, float tooth, float teeth, float ammoFull,
            float ammoQuarter)
        {
            float nothing = duck + pHDuck + tooth + teeth + ammoFull + ammoQuarter;
            loot = new LootItem[]
            {
                new LootItem(Resources.Load<GameObject>("LootItems/Duck_EnemyDrop"), duck),
                new LootItem(Resources.Load<GameObject>("LootItems/P.H.Duck_EnemyDrop"), pHDuck),
                new LootItem(Resources.Load<GameObject>("LootItems/SuperDuck_EnemyDrop"), superDuck),
                new LootItem(Resources.Load<GameObject>("LootItems/Tooth"), tooth),
                new LootItem(Resources.Load<GameObject>("LootItems/Teeth"), teeth),
                new LootItem(Resources.Load<GameObject>("LootItems/Ammo_Full_Single"), ammoFull),
                new LootItem(Resources.Load<GameObject>("LootItems/Ammo_Quarter_All"), ammoQuarter),
                new LootItem(null, nothing),
            };
            totalWeight = TotalWeighted(duck, pHDuck, superDuck, tooth, teeth, ammoFull, ammoQuarter, nothing);
            Array.Sort(loot);
        }

        public static GameObject FindItemToDrop(float duck, float pHDuck, float superDuck, float tooth, float teeth,
            float ammoFull, float ammoQuarter)
        {
            SetUpLootTable(
                UpdatedStatManager.GetStat(GameEnums.PermanentStats.DropRateAdjust).GetStatValue(duck) +
                GameManager.instance.loot.NormalDuckAdditionWeight,
                UpdatedStatManager.GetStat(GameEnums.PermanentStats.DropRateAdjust).GetStatValue(pHDuck) +
                GameManager.instance.loot.PHDuckAdditionWeight,
                UpdatedStatManager.GetStat(GameEnums.PermanentStats.DropRateAdjust).GetStatValue(superDuck) +
                GameManager.instance.loot.SuperDuckAdditionWeight,
                UpdatedStatManager.GetStat(GameEnums.PermanentStats.DropRateAdjust).GetStatValue(tooth) +
                GameManager.instance.loot.ToothAdditionWeight,
                UpdatedStatManager.GetStat(GameEnums.PermanentStats.DropRateAdjust).GetStatValue(teeth) +
                GameManager.instance.loot.TeethAdditionWeight,
                UpdatedStatManager.GetStat(GameEnums.PermanentStats.DropRateAdjust).GetStatValue(ammoFull),
                UpdatedStatManager.GetStat(GameEnums.PermanentStats.DropRateAdjust).GetStatValue(ammoQuarter));

            var dropNumber = Random.Range(0f, totalWeight);
            float weighted = 0;

            foreach (var item in loot)
            {
                weighted += item.rarity;
                if (dropNumber <= weighted)
                {
                    return item.item;
                }
                else if (weighted < 0)
                {
                    return null;
                }
            }

            return null;
        }

        static float TotalWeighted(params float[] weighted)
        {
            return weighted.Sum();
        }

    }

    [Serializable]
    public partial class LootItem
    {
        public GameObject item;
        public float rarity;

        public LootItem(GameObject newItem, float newRarity)
        {
            item = newItem;
            rarity = newRarity;
        }
    }

    public partial class LootItem : IComparable
    {
        public int CompareTo(object obj)
        {
            var loot = obj as LootItem;
            if (loot != null)
            {
                return rarity.CompareTo(loot.rarity); // compare rarity
            }

            throw new ArgumentException("Object is not a User");
        }
    }
}
