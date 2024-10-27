using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    public void LoadFirstLevel()
    {
        
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    public void LoadExit()
    {

        SceneManager.LoadScene("StartScene", LoadSceneMode.Single);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
