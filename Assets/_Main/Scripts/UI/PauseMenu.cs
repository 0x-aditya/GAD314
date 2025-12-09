using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseCanvas;
   

    private void Start()
    {
        
        pauseCanvas.SetActive(false);
      
    }

    private void Update()
    {
        Display();

    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Display()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseCanvas.activeSelf)
            {
                pauseCanvas.SetActive(false);
            }
            else 
            {
                pauseCanvas.SetActive(true);
            }

        }
    }
}
