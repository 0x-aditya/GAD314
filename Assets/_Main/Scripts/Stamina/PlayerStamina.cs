using Scripts.Items;
using System;
using System.Collections;
using ScriptLibrary.Singletons;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : Singleton<PlayerStamina>
{

    [SerializeField] public float maxStamina;
    [SerializeField] public static float playerStamina;

    [SerializeField] private Slider _slider;

    //[SerializeField] public Rigidbody2D _player;

    //player stamina manager
    //need to check stamina value in other scripts

    void Start()
    {
        playerStamina = maxStamina;
        //_slider = GetComponent<Slider>();
        _slider.maxValue = maxStamina;
        _slider.value = playerStamina;
    }

    void Update()
    {
        if (playerStamina < 0)
        {
            playerStamina = 0;
        }
        else if (playerStamina > maxStamina)
        {
            playerStamina = maxStamina;
        }
        else
        {
            //color the stamina bar in UI
        }
    }
    
    public void ReduceStamina(int amount) //this reduces stamina based on any given action
    {
        playerStamina -= amount;
        _slider.value = playerStamina;
    }

    public void IncreaseStamina(int amount) //this increases stamina if an item in consumed
    {
        playerStamina += amount;
        _slider.value = playerStamina;
    }

    public void IncreaseMaxStamina(int amount) //this is used if max stamina is going to get upgraded
    {
        maxStamina += amount;
        _slider.maxValue = maxStamina;
        playerStamina = maxStamina;
    }

}
