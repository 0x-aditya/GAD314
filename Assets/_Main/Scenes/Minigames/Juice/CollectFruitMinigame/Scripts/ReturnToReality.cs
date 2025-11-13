using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToReality : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.LoadScene("_Main/Scenes/Testing/Base Map");
    }
}