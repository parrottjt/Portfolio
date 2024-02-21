using System;
using UnityEngine;

namespace FrikinCore.Loot
{
    [Serializable]
    public class LootSystem
    {
        [HideInInspector] public string name;

        public float duckRarity = 50,
            pHDuckRarity = 35,
            superDuckRarity = .1f,
            toothRarity = 30,
            teethRarity = 20,
            ammoFullSingleRarity = 15,
            ammoQuarterAllRarity = 10;

        public LootSystem(string name, float duck, float pHDuck, float superDuck, float tooth, float teeth,
            float ammoFull,
            float ammoQuarter)
        {
            this.name = name;
            duckRarity = duck;
            pHDuckRarity = pHDuck;
            superDuckRarity = superDuck;
            toothRarity = tooth;
            teethRarity = teeth;
            ammoFullSingleRarity = ammoFull;
            ammoQuarterAllRarity = ammoQuarter;
        }
    }
}