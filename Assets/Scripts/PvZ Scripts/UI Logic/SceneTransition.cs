using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    public void LoadScene(int sceneIndex)
    {
        FindObjectOfType<SceneLoader>().LoadScene(sceneIndex);
    }
}
