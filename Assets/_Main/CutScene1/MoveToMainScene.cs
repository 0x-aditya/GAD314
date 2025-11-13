using UnityEngine;

public class MoveToMainScene : MonoBehaviour
{
    void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("_Main/Scenes/Testing/Base Map");
    }

}
