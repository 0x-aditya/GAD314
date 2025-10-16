using ScriptLibrary.Inputs;

public class RemoveOnRightClick : KeyPressInput
{
    protected override void OnKeyDown()
    {
        PointerInventory.Instance.DestroyOldPointerObject();
    }
}
