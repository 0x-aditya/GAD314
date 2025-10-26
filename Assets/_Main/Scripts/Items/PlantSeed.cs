using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Items
{
    [CreateAssetMenu(fileName = "New Seed", menuName = "ScriptableObjects/Seed", order = 0)]
    public class PlantSeed : BaseItem
    {
        // Plant Textures
        [Header("Plant Textures")] [Tooltip("Plant seedling sprite")]
        public Sprite plantSprite;

        [Tooltip("Plant seedling sprite")] public List<Sprite> growingSprites;

        [Tooltip("Plant seedling sprite")] public Sprite readyToHarvestSprite;

        // Growth Properties
        [Header("Growth Properties")] [Range(1, 10)] [Tooltip("Number of days for the plant to fully grow")]
        public int growthTime;

        [Range(1, 10)] [Tooltip("Number of items on harvest")]
        public int yieldAmount;

        [Tooltip("Faster yield on the best season")]
        public Seasons bestSeason;

        [Tooltip("growth will be multiplied by this modifier if planted in the best season")]
        public float seasonGrowthModifier = 1.5f;
    }
}

public enum Seasons
{
    Spring,
    Summer,
    Fall,
    Winter
}