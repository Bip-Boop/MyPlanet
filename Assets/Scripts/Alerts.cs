using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Alerts : MonoBehaviour
{
    [SerializeField] private PlayerDataContainer SaveManager;
    [SerializeField] private GameObject _tapBlocker;
    [System.Serializable]
    public struct AlertSettings
    {
        public GameObject AlertPanel;
        public CanvasGroup AlertCanvasGroup;
        public Text AlertText;
        public CoroutineManager.AnimationSettings AnimSettings;
    }

    [SerializeField] private AllManager AM;

    [SerializeField] private AlertSettings _importantAlert;

    [SerializeField] private CoroutineManager.AnimationSettings _tutorialSettings;
    [SerializeField] private CanvasGroup _tutorialCanvasGroup;

    private CanvasGroup _lastSpecialCanvasGroup;
    private CoroutineManager.AnimationSettings _lastSpecialSettings;
    
    public void ShowSpecialAlert(CanvasGroup canvasGroup, CoroutineManager.AnimationSettings settings)
    {
        _lastSpecialCanvasGroup = canvasGroup;
        _lastSpecialSettings = settings;
        canvasGroup.gameObject.SetActive(true);
        StartCoroutine(SmoothCanvasGroupAlphaTransition(canvasGroup, settings, false));
        Time.timeScale = 0f;
    }

    public void CloseSpecialAlert(CanvasGroup canvasGroup, CoroutineManager.AnimationSettings settings)
    {
        StartCoroutine(SmoothCanvasGroupAlphaTransition(canvasGroup, settings, true));
    }

    public void CloseSpecialAlert()
    {
        StartCoroutine(SmoothCanvasGroupAlphaTransition(_lastSpecialCanvasGroup, _lastSpecialSettings, true));   
    }

    public void ShowImportantAlert(string text)
    {
        Time.timeScale = 0f;
        _importantAlert.AlertText.text = text;
        _importantAlert.AlertCanvasGroup.alpha = 0;
        _importantAlert.AlertPanel.SetActive(true);
        StartCoroutine(SmoothCanvasGroupAlphaTransition(_importantAlert.AlertCanvasGroup,
            _importantAlert.AnimSettings, false));
    }

    public void CloseImportantAlert()
    {
        StartCoroutine(SmoothCanvasGroupAlphaTransition(_importantAlert.AlertCanvasGroup,
            _importantAlert.AnimSettings, true));
    }

    private IEnumerator SmoothCanvasGroupAlphaTransition(CanvasGroup canvasGroup, CoroutineManager.AnimationSettings settings, bool reversed)
    {
        _tapBlocker.SetActive(true);
        if (reversed)
        {
            float t = 1f;
            float a = 1f;
            while (t >= 0f)
            {
                a = settings.curve.Evaluate(t);
                canvasGroup.alpha = a;
                t -= Time.fixedDeltaTime * settings.speed;
                yield return 0;
            }

            Time.timeScale = 1f;
            canvasGroup.gameObject.SetActive(false);
            canvasGroup.alpha = 0f;
        }
        else
        {    
            float t = 0f;
            float a = 0f;
            
            while (t <= 1)
            {
                a = settings.curve.Evaluate(t);
                canvasGroup.alpha = a;
                t += Time.fixedDeltaTime * settings.speed;
                yield return 0;
            }
        }
        
        canvasGroup.alpha = 1f;
        _tapBlocker.SetActive(false);
    }

    private void Start()
    {
        ShowSpecialAlert(_tutorialCanvasGroup, _tutorialSettings);
    }

}
