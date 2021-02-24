using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PreMenuUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hintUIText;
    private bool oneRoundComplete;
    private void Start()
    {
        oneRoundComplete = false;
    }
    private void Update()
    {
        if(!oneRoundComplete)
        {
            StartCoroutine(TextFader(hintUIText));
        }
        if(Input.GetKeyDown(KeyCode.Return))
        {
            UIManager.Instance.LoadMainMenu(gameObject);
        }
    }

    private IEnumerator TextFader(TextMeshProUGUI text)
    {
        oneRoundComplete = true;
        for (int i = 0; i < 20; ++i)
        {
            text.color = new Color(1.0f, 1.0f, 1.0f, 1 - (i/20.0f));
            yield return new WaitForSeconds(0.05f);
        }
        text.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        for (int i = 0; i < 20; ++i)
        {
            text.color = new Color(1.0f, 1.0f, 1.0f, (i/20.0f));
            yield return new WaitForSeconds(0.05f);
        }
        text.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        oneRoundComplete = false;
    }
}
