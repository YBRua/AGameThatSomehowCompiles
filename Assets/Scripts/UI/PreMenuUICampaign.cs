using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreMenuUICampaign : MonoBehaviour
{
    public void GoToCampaign() => GameManager.Instance.SwitchScene("Scenes/SampleScene");
}
