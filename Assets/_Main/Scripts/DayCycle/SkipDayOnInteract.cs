using System.Collections;
using UnityEngine;
using ScriptLibrary;
using Scripts.DayCycle;
using UnityEngine.UI;

public class SkipDayOnInteract : OnInteractTrigger2D
{
    private GameObject _panelGameObject;
    /// <summary>
    /// For Bed Interaction, skips to the next day.
    /// </summary>
    protected override void OnInteract()
    {
        PlayerStamina.playerStamina = PlayerStamina.maxStamina;
        // create new panel to cover screen during day skip
        _panelGameObject = new GameObject("Panel", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        _panelGameObject.transform.SetParent(GameObject.Find("UI Canvas").transform, false);
        RectTransform rectTransform = _panelGameObject.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(1, 1);
        rectTransform.sizeDelta = new Vector2(0, 0);
        rectTransform.anchoredPosition = new Vector2(0, 0);
        FadeInPanelAlpha();
    }
    
    protected override void OnDisable()
    {
        base.OnDisable();
        if (_panelGameObject != null)
        {
            Destroy(_panelGameObject);
        }
    }
    private void FadeInPanelAlpha()
    {
        Image image = _panelGameObject.GetComponent<Image>();
        image.color = new Color(0,0,0,0);
        // fade in
        StartCoroutine(FadeInPanelAlphaCoroutine());

    }
    private IEnumerator FadeInPanelAlphaCoroutine()
    {
        Image image = _panelGameObject.GetComponent<Image>();
        Color color = image.color;
        for (int i = 0; i < 60; i++)
        {
            yield return new WaitForSeconds(0.01f);
            color.a += 1f / 60f;
            image.color = color;
        }
        DayNightCycle.Instance.SkipToNextDay();

        Destroy(_panelGameObject);
    }
}
