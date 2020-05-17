using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interface;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class UITimer : MonoBehaviour
    {
        public BaseInterface ThisPanel;
        public GameObject TutorialPlayPanel;


        void Start()
        {
            StartCoroutine(Timer());
        }

        IEnumerator Timer()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                yield return new WaitForSeconds(3f);
                transform.GetChild(i).gameObject.SetActive(false);
            }

            ThisPanel.Close();
            TutorialPlayPanel.SetActive(true);
        }
    }
}
