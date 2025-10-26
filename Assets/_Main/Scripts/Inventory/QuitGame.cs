using UnityEngine;

public class QuitGame : ScriptLibrary.Inputs.KeyPressInput
{
    protected override void OnKeyDown()
    {
        Application.Quit();
    }
}
