using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    Scene scene;
    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        //Debug.Log("Active Scene name is: " +  scene.buildIndex);
    }

    public void SceneUp()
    {
        SceneManager.LoadScene(scene.buildIndex + 1);
    }

    public void SceneDown()
    {
        SceneManager.LoadScene(scene.buildIndex - 1, LoadSceneMode.Single);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}
