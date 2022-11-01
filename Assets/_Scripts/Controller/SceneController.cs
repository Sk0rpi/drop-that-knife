using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public static class SceneController
{
    public enum Scene
    {
        scene_0, scene_1, scene_2, scene_3,
    }
    
    public static void Load(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
        
    }
}
