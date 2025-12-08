using System;
using System.Collections;
using System.Collections.Generic;
using Scripts.Farming;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.DayCycle
{
    // Will be changed later
    public class DisplayNextDayVisual : MonoBehaviour
    {
        [SerializeField] private GameObject nextDayVisualCanvas;
        [SerializeField] private GameObject panelToFade;
        [SerializeField] private float fadeInDuration;
        [SerializeField] private float fadeOutDuration;
        [SerializeField] private float displayDuration;
        
        private Image _panelImage;
        private TextMeshProUGUI _textMeshPro;
        /// <summary>
        /// Adds listener to DayPassed event
        /// </summary>
        private void Start()
        {
            DayNightCycle.Instance.OnDayPassedContinuous += ShowNextDayVisual;
        }
        /// <summary>
        /// Show the visual for the next day
        /// </summary>
        private void ShowNextDayVisual()
        {
            if (nextDayVisualCanvas == null) return;
            nextDayVisualCanvas.SetActive(true);
            _panelImage = panelToFade.GetComponent<Image>();
            _textMeshPro = nextDayVisualCanvas.GetComponentInChildren<TextMeshProUGUI>();
            _textMeshPro.text = $"Day: {DayNightCycle.Instance.currentDay}";
            StartCoroutine(FadeInPanelAlpha());
        }

        /// <summary>
        /// Fades out the panel alpha over time
        /// </summary>
        /// <returns>Waits for certain time</returns>
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
                
                _textMeshPro.color = textColor;
                
            }
            nextDayVisualCanvas.SetActive(false);
        }
        /// <summary>
        /// Fades in the panel alpha over time
        /// </summary>
        /// <returns> Waits for certain time</returns>
        private IEnumerator FadeInPanelAlpha()
        {
            Color color = _panelImage.color;
            Color textColor = _textMeshPro.color;
            color.a = 0;
            for (int i = 0; i < 60; i++)
            {
                yield return new WaitForSeconds(fadeInDuration/60);
                color.a += 1f / 60f;
                textColor.a += 1f / 60f;

                _textMeshPro.color = textColor;
            }
            yield return DisplayPanel();
        }

        /// <summary>
        /// Waits for certain time before fading out
        /// </summary>
        /// <returns>Waits for certain time</returns>
        private IEnumerator DisplayPanel()
        {
            yield return new WaitForSeconds(displayDuration);
            yield return FadeOutPanelAlpha();
        }
    }
}