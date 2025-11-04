using UnityEngine;

public class changeScene : MonoBehaviour
{
    [SerializeField] private string sceneName;
    public void ChangeScene(string scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }

}
