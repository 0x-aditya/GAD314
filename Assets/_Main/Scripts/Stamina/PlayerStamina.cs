using Scripts.Items;
using System;
using System.Collections;
using ScriptLibrary.Singletons;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{

    [SerializeField] public static float maxStamina;
    [SerializeField] public static float playerStamina;

    [SerializeField] private Slider _slider;

    //[SerializeField] public Rigidbody2D _player;

    //player stamina manager
    //need to check stamina value in other scripts

    void Start()
    {
        playerStamina = maxStamina;
        //_slider = GetComponent<Slider>();
        //_slider.maxValue = maxStamina;
        maxStamina = _slider.maxValue;
        playerStamina = _slider.value;
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
        _slider.value = playerStamina;

        //color the slider thing
    }

    public bool OnStaminaUse()
    {
        if (playerStamina <= 1)
        {
            return false;
        }
        else 
        { return true; }
    }

    public void WakeupStamina(float percentage) //when player wakes up, setup as a percentage
    {
        playerStamina = maxStamina * (percentage / 100);
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
