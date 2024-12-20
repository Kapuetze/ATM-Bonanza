using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VorlesungUI : MonoBehaviour
{
    public Button button;

    public void LoadSceneZero()
    {
        StartCoroutine(PrepareNextLevel(0));
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public IEnumerator PrepareNextLevel(int index)
    {
        AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(index);

        sceneLoad.allowSceneActivation = false;

        while (!sceneLoad.isDone)
        {
            if (sceneLoad.progress >= 0.9f)
            {
                sceneLoad.allowSceneActivation = true;
            }

            Debug.Log("<color=yellow>Scene Loaded: </color>" + sceneLoad.progress);

            yield return new WaitForEndOfFrame();
        }
    }
}

