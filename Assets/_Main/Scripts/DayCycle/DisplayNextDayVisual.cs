using System;
using System.Collections;
using System.Collections.Generic;
using Scripts.Farming;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.DayCycle
{
    public class DisplayNextDayVisual : MonoBehaviour
    {
        [SerializeField] private GameObject nextDayVisualCanvas;
        [SerializeField] private GameObject panelToFade;
        [SerializeField] private float fadeInDuration;
        [SerializeField] private float fadeOutDuration;
        [SerializeField] private float displayDuration;
        
        private Image _panelImage;
        private TextMeshProUGUI _textMeshPro;
        private void Start()
        {
            DayNightCycle.Instance.OnDayPassed += ShowNextDayVisual;
        }
        
        private void ShowNextDayVisual()
        {
            nextDayVisualCanvas.SetActive(true);
            _panelImage = panelToFade.GetComponent<Image>();
            _textMeshPro = nextDayVisualCanvas.GetComponentInChildren<TextMeshProUGUI>();
            _textMeshPro.text = $"Day: {DayNightCycle.Instance.currentDay}";
            StartCoroutine(FadeInPanelAlpha());
        }

        private IEnumerator FadeOutPanelAlpha()
        {
            Color color = _panelImage.color;
            Color textColor = _textMeshPro.color;
            textColor.a = 1;
            color.a = 1;
            for (int i = 0; i < 60; i++)
            {
                yield return new WaitForSeconds(fadeInDuration/60);
                color.a -= 1f / 60f;
                textColor.a -= 1f / 60f;
                
                _panelImage.color = color;
                _textMeshPro.color = textColor;

            }
            nextDayVisualCanvas.SetActive(false);
        }
        private IEnumerator FadeInPanelAlpha()
        {
            Color color = _panelImage.color;
            Color textColor = _textMeshPro.color;
            color.a = 0;
            for (int i = 0; i < 60; i++)
            {
                yield return new WaitForSeconds(fadeInDuration/60);
                color.a += 1f / 60f;
                textColor.a -= 1f / 60f;
                _panelImage.color = color;
                _textMeshPro.color = textColor;
            }
            yield return DisplayPanel();
        }

        private IEnumerator DisplayPanel()
        {
            yield return new WaitForSeconds(displayDuration);
            yield return FadeOutPanelAlpha();
        }
    }
}