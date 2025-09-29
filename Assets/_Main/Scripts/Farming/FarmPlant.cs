using System;
using UnityEngine;

    [CreateAssetMenu(fileName = "New Plant", menuName = "ScriptableObjects/Plant", order = 0)]
    public class FarmPlant : ScriptableObject
    {
        /*
        [Header("Plant Properties")]
        [Tooltip("Name of the plant")]
        */
        //name of the scriptable object is the same as the plant name
        public string plantName => name;
        
        [Tooltip("Sprite of the plant")]
        public Sprite plantSprite;
        
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