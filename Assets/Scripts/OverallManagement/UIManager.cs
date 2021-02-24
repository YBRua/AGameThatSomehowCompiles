using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    protected void Start()
    {
        transform.Find("PreMenuUI").gameObject.SetActive(true);
    }

    public void LoadMainMenu(GameObject currentCanvas)
    {
        StartCoroutine(SwitchUI(currentCanvas, transform.Find("MainMenuUI").gameObject));
    }

    protected IEnumerator SwitchUI(GameObject current, GameObject next)
    {
        yield return StartCoroutine(FadeOutCanvas(current));
        yield return StartCoroutine(FadeInCanvas(next));
    }

    protected IEnumerator FadeOutCanvas(GameObject canvas)
    {
        if(canvas == null)
            Debug.LogError("[UIManager]: Failed to fade out. GameObject is null.");
        CanvasGroup group = canvas.GetComponent<CanvasGroup>();
        if(group == null)
            Debug.LogError("[UIManager]: Failed to fade out. CanvasGroup not found in " + canvas.name);

        // Fading
        group.alpha = 1.0f;
        for(int i = 0; i < 20; ++i)
        {
            group.alpha = 1 - (i/20.0f);
            yield return new WaitForSeconds(0.05f);
        }
        group.alpha = 0.0f;

        canvas.SetActive(false);
    }

    protected IEnumerator FadeInCanvas(GameObject canvas)
    {
        if(canvas == null)
            Debug.LogError("[UIManager]: Failed to fade in. GameObject is null.");
        
        canvas.SetActive(true);
        CanvasGroup group = canvas.GetComponent<CanvasGroup>();
        if(group == null)
            Debug.LogError("[UIManager]: Failed to fade in. CanvasGroup not found in " + canvas.name);
        
        // Fading
        group.alpha = 0.0f;
        for(int i = 0; i < 20; ++i)
        {
            group.alpha = (i/20.0f);
            yield return new WaitForSeconds(0.05f);
        }
        group.alpha = 1.0f;
    }
}
