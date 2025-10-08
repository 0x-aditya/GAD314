using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObjects/Item", order = 1) ]
public class ItemType : ScriptableObject
{

    [SerializeField] private string itemName;
    [SerializeField] private float price;
    [SerializeField] private int quantity;
    [SerializeField] private int level;
    [SerializeField] public Type typeOfItem;

    
}
public enum Type
{
    Food,
    Tool,
    Material
}


