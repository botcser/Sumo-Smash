using Assets.Scripts.Interface;
using UnityEngine;

namespace Assets.Scripts
{
    public class MainPlay : MonoBehaviour
    {
        public BaseInterface StartReady;

        public void Awake()
        {

        }

        public void Start()
        {
            StartReady.Open();
        }
    }
}