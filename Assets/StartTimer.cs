﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StartTimer : MonoBehaviour
{
    public GameObject TimerPanel;

    void Start()
    {
        StartCoroutine("Timer");
    }

    IEnumerator Timer()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
            transform.GetChild(i).gameObject.SetActive(false);
        }

        TimerPanel.SetActive(false);
    }
}