using ScriptLibrary.Inputs;

public class RemoveOnRightClick : KeyPressInput
{
    protected override void OnKeyPress()
    {
        PointerInventory.Instance.DestroyOldPointerObject();
    }
}
