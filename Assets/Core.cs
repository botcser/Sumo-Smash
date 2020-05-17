using Assets.Scripts.Data;
using Assets.Scripts.Interface;
using UnityEngine;

namespace Assets
{
    public class Core : MonoBehaviour
    {
        public void Awake()
        {
            Profile.Load();
        }

        public void Start()
        {
            MainMenu.Instance.Open();
        }
    }
}
