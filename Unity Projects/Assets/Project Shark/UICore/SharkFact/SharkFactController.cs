using System.Collections.Generic;
using System.Linq;
using FrikinCore.Player.Managers;
using UnityEngine;

namespace UICore.SharkFact
{
    public static class SharkFactController
    {
        static readonly List<SharkFact> factList = new List<SharkFact>();

        public static void BuildSharkFactList()
        {
            factList.Clear();

            var facts = Resources.LoadAll<SharkFact>("SharkFacts");
            foreach (var fact in facts)
            {
                if (fact.copiesOfFacts == 0 || (int)fact.worldUnlock - 1 >
                    ((int)PlayerDataManager.instance.normLevelNames - 2 >= 0
                        ? (int)PlayerDataManager.instance.normLevelNames - 2
                        : (int)PlayerDataManager.instance.normLevelNames) % 4) continue;
                for (var j = 0; j < fact.copiesOfFacts; j++)
                {
                    factList.Add(fact);
                }
            }
        }

        public static string GetRandomSharkFact() => factList[Random.Range(0, factList.Count)].fact;

        public static string GetSharkFactOfType(SharkFact.SharkFactType sharkFactType)
        {
            List<SharkFact> filteredList = factList.Where(list => list.typeOfFact == sharkFactType).ToList();
            return filteredList[Random.Range(0, filteredList.Count)].fact;
        }
    }
}