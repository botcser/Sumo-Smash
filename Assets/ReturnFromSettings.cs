using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnFromSettings : MonoBehaviour
{
    public GameObject SettingsPanel;

    public void OnClick()
    {
        SettingsPanel.SetActive(false);
    }
}
