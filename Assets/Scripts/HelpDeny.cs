using UnityEngine;

namespace Assets.Scripts
{
    public class HelpDeny : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            PlayerPrefs.SetInt("HelpDenied", 1);
        }
    }
}
