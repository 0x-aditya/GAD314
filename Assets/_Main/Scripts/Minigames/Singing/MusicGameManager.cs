using ScriptLibrary.Singletons;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Minigames.Singing
{
    public class MusicGameManager : Singleton<MusicGameManager>
    {
        [SerializeField] private GameObject[] strikes;
        private int maxStrikes => strikes.Length;

        private int currentStrikes
        {
            get
            {
                int c = 0;
                foreach (var strike in strikes)
                {
                    if (strike.activeSelf) c++;
                }

                return c;
            }
        }

        public void AddStrike()
        {
            if (currentStrikes < maxStrikes)
            {
                strikes[currentStrikes].SetActive(true);
            }
            else
            {
                EndGame();
            }
        }

        private void EndGame()
        {
            SceneManager.LoadScene(0);
        }
    }
}