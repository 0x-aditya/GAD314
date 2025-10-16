using UnityEngine;
using UnityEngine.SceneManagement;

public class classtestscript : MonoBehaviour
{
    public void TakeMeToInv()
    {
        SceneManager.LoadScene(0);
    }
    public void TakeMeToFarm()
    {
        SceneManager.LoadScene(1);
    }
}
