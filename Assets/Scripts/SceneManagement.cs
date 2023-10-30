using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public string humanSceneName;
    public string AISceneName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadHumanScene()
    {
        SceneManager.LoadScene(humanSceneName);
    }
    public void LoadAIScene()
    {
        SceneManager.LoadScene(AISceneName);
    }
}
