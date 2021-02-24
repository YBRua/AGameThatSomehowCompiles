using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class PreMenuUIButtons : MonoBehaviour
{
    public UnityEvent OnReturnDown;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (GetComponentInParent<MainMenuUI>().menuSelectionState == transform.GetSiblingIndex())
            {
                OnReturnDown.Invoke();
            }
        }

    }
}
