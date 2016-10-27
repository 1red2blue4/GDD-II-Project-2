using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; //Load scenes

public class Cutscene : MonoBehaviour {

    public string nextScene;

	public void LoadNextScene()
    {
        SceneManager.LoadScene(nextScene); //Load the scene
    }

    private void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
