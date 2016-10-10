using UnityEngine;
using UnityEditor.SceneManagement; //Load scenes
using System.Collections;

public class TitleScreenButtonManager : MonoBehaviour //KI
{
	void Start() //Use this for initialization
    {
	
	}

	void Update() //Update is called once per frame
    {
        
	}

    public void StartButton(string loadLevel) //When the start button is clicked
    {
        EditorSceneManager.LoadScene(loadLevel); //Load the scene
    }

    public void ExitButton() //When the exit button is clicked
    {
        Application.Quit(); //Quit the game
    }
}