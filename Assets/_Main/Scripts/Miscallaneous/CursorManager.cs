using System;
using UnityEngine;
using ScriptLibrary.Singletons;

public class CursorManager : Singleton<CursorManager>
{
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private Texture2D interactTexture;
    public bool leftCursorDown = false;
    public bool rightCursorDown = false;
    public void ChangeCursor(CursorType cursorType)
    {
        switch (cursorType)
        {
            case CursorType.Default:
                Cursor.SetCursor(cursorTexture,Vector2.zero, CursorMode.Auto);
                break;
            case CursorType.Interact:
                Cursor.SetCursor(interactTexture, new Vector2(interactTexture.width / 2f, interactTexture.height / 2f), CursorMode.Auto);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(cursorType), cursorType, null);
        }
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            leftCursorDown = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            leftCursorDown = false;
        }
        if (Input.GetMouseButtonDown(1))
        {
            rightCursorDown = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            rightCursorDown = false;
        }
    }
    public enum CursorType
    {
        Default,
        Interact,
    }
}