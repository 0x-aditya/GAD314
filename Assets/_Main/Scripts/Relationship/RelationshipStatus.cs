using System;
using Scripts.DayCycle;
using UnityEngine;

public class RelationshipStatus : MonoBehaviour
{
    [SerializeField] private int friendshipLevel = 0;
    [SerializeField] private int maxFriendshipLevel = 10;
    [SerializeField] private int firstFriendshipCutscene = 2;
    [SerializeField] private int secondFriendshipCutscene = 4;
    [SerializeField] private int ascensionFriendshipLevel = 6;
    [SerializeField] private GameObject[] cutscenes;
    [SerializeField] private GameObject characterAscentCutscene;
    
    public void IncreaseAffection(int amount)
    {
        friendshipLevel += amount;
        if (friendshipLevel > maxFriendshipLevel) // clamp value
        {
            friendshipLevel = maxFriendshipLevel;
        }
        
        if (friendshipLevel == firstFriendshipCutscene) // unlock cutscenes on certain friendship levels
        {
            UnlockCutscene(firstFriendshipCutscene);
        }
        else if (friendshipLevel == secondFriendshipCutscene)
        {
            UnlockCutscene(secondFriendshipCutscene);
        }
        else if (friendshipLevel == ascensionFriendshipLevel)
        {
            AscendCharacter();
        }
    }
    
    
    public void DecreaseAffection(int amount)
    {
        friendshipLevel -= amount;
        if (friendshipLevel < 0) // clamp value
        {
            friendshipLevel = 0;
        }
    }

    private void UnlockCutscene(int timelineID)
    {
        if (cutscenes.Length == 0 || timelineID-1 >= cutscenes.Length) // check if cutscene exists
            throw new Exception($"Cutscene ID out of range cutscene. array lenght:{cutscenes.Length}, requested ID:{timelineID-1}");

        DayNightCycle.Instance.OnDayPassed += () => { cutscenes[timelineID - 1].SetActive(true); };
    }

    private void AscendCharacter()
    {
        characterAscentCutscene.SetActive(true);
    }
}
