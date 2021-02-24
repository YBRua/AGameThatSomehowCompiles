using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject Highlighter;
    public int menuSelectionState;
    private int numOfSelections;
    private float init;

    private void Start()
    {
        menuSelectionState = 0;
        numOfSelections = 5;
        init = Highlighter.GetComponent<RectTransform>().localPosition.y;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
            IncreseSelectState();
        if(Input.GetKeyDown(KeyCode.UpArrow))
            DecreseSelectState();
    }

    private void IncreseSelectState()
    {
        menuSelectionState = (menuSelectionState + 1) % numOfSelections;
        UpdateHighlighter();    
    }
    private void DecreseSelectState()
    {
        menuSelectionState = (menuSelectionState + numOfSelections - 1) % numOfSelections;
        UpdateHighlighter();
    }
    private void UpdateHighlighter()
    {
        float newY = init - menuSelectionState * 100.0f;
        Vector3 pos = Highlighter.GetComponent<RectTransform>().localPosition;
        Highlighter.GetComponent<RectTransform>().localPosition = new Vector3(pos.x, newY, pos.z);
    }
}
