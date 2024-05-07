using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeTo : MonoBehaviour
{
    public void FightScene()
    {
        SceneManager.LoadScene("Fight Scene");
    }

    public void ZCDScene()
    {
        SceneManager.LoadScene("ZCD");
    }

    public void Quit()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
