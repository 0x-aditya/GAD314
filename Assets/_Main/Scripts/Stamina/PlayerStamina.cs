using Scripts.Items;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerStamina : MonoBehaviour
{

    [SerializeField] public float maxStamina;
    [SerializeField] public static float playerStamina;

    [SerializeField] public Rigidbody2D _player;

    void Start()
    {
        playerStamina = maxStamina;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        playerStamina--;
    }

    void Update()
    {
        //have something reflects the speed
        //have when player uses item
        //have when
    }
}
