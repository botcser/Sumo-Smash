using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNewScene : MonoBehaviour
{
    [Header("ID загружаемой сцены")]
    public int sceneID;

    public void OnClick()
    {
        SceneManager.LoadSceneAsync(sceneID);
    }
}