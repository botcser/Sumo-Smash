using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSettingsPanel : MonoBehaviour
{
    public GameObject SettingsPanel;

    public void OnClick()
    {
        SettingsPanel.SetActive(!SettingsPanel.activeSelf);
    }
}
