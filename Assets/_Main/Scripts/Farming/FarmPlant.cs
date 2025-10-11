using System;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(fileName = "New Plant", menuName = "ScriptableObjects/Plant", order = 0)]
    public class FarmPlant : ScriptableObject
    {
        
        [SerializeField] private Sprite objectIcon;
        [SerializeField] public Sprite inventoryPlaceholderIcon;
        [SerializeField] public Sprite inventoryItemIcon;
        public Sprite icon => objectIcon;
        
        [Header("Plant Textures")]
        
        [Tooltip("Plant seedling sprite")] public Sprite plantSprite;
        [Tooltip("Plant seedling sprite")] public List<Sprite> growingSprites;
        [Tooltip("Plant seedling sprite")] public Sprite readyToHarvestSprite;
        public string plantName => name;
        
        
        [Header("Growth Properties")]
        [Range(1,10)]
        [Tooltip("Number of days for the plant to fully grow")]
        public int growthTime;
        
        [Range(1,10)]
        [Tooltip("Number of items on harvest")]
        public int yieldAmount;
        
        [Tooltip("Faster yield on the best season")]
        public Seasons bestSeason;
        
        [Tooltip("growth will be multiplied by this modifier if planted in the best season")]
        public float seasonGrowthModifier = 1.5f;
    }
    public enum Seasons
    {
        Spring,
        Summer,
        Fall,
        Winter
    }