using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Items
{
    [CreateAssetMenu(fileName = "New Food", menuName = "ScriptableObjects/Food", order = 2)]
    public class FoodItem : BaseItem
    {
        //food item sprite
        [Header("Food Sprite")]
        [Tooltip("Food Sprite")]
        public Sprite foodSprite;

        //quality
        [Range(1, 5)]
        public int foodQuality;

        //stamina replenish
        [Range(1, 10)]
        public int staminaValue;

    }

}