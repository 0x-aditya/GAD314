using UnityEngine;

public class CopySprite : MonoBehaviour
{
    [SerializeField] SpriteRenderer copy; // what to copy
    [SerializeField] float brightnessAdd = 0.1f; // use to adjust original sprite brightness
    private SpriteRenderer paste;

    private void Awake()
    {
        paste = GetComponent<SpriteRenderer>();
        AddBrightness();
    }
    private void AddBrightness()
    {
        Color baseColor = copy.color;

        float r = Mathf.Clamp01(baseColor.r + brightnessAdd);
        float g = Mathf.Clamp01(baseColor.g + brightnessAdd);
        float b = Mathf.Clamp01(baseColor.b + brightnessAdd);

        paste.color = new Color(r, g, b, baseColor.a);
    }
    private void Update()
    {
        paste.sprite = copy.sprite;
        paste.flipX = copy.flipX;
    }
}
