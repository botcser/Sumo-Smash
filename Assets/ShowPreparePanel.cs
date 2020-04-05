using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPreparePanel : MonoBehaviour
{
    public GameObject PreparePanel;

    public void OnClick()
    {
        PreparePanel.SetActive(!PreparePanel.activeSelf);
    }
}