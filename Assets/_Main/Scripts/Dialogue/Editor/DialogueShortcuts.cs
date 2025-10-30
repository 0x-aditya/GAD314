using CustomType;
using UnityEditor;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public static class DialogueShortcuts
{
    [Shortcut("Create/CreateDialogueText", KeyCode.L, ShortcutModifiers.Control)]
    private static void CreateDialogueText()
    {
        DialogueInfo asset = ScriptableObject.CreateInstance<DialogueInfo>();
         string path = AssetDatabase.GenerateUniqueAssetPath("Assets/_Main/Dialogues/DialogueTexts/dialogue_text.asset");
         EditorUtility.FocusProjectWindow();
         ProjectWindowUtil.CreateAsset(asset, path);
         Selection.activeObject = asset;
    }
    
    [Shortcut("Create/CreateDialogueChoices", KeyCode.L, ShortcutModifiers.Shift)]
    private static void CreateDialogueChoice()
    {
        ChoiceInfo asset = ScriptableObject.CreateInstance<ChoiceInfo>();
        string path = AssetDatabase.GenerateUniqueAssetPath("Assets/_Main/Dialogues/DialogueChoices/dialogue_choice.asset");
        EditorUtility.FocusProjectWindow();
        ProjectWindowUtil.CreateAsset(asset, path);
        Selection.activeObject = asset;
    }
    
}