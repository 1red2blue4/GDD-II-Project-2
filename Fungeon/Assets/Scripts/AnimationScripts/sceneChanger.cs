using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class sceneChanger : MonoBehaviour {
	
	public List<GameObject> Scenes = new List<GameObject>();
	public int currentScene;
	public List<Animator> SceneAnim;

	void start()
	{
		currentScene = 0;
		SceneAnim = new List<Animator>();
		foreach(GameObject scn in Scenes)
		{
			SceneAnim.Add( scn.GetComponent<Animator>() );
		}
	}
	// Update is called once per frame
	void Update () {
		if(currentScene <= Scenes.Count -2)
		{
			if(animEnd()){
				Debug.Log("I Finished");
				Scenes[currentScene].SetActive(false);
				currentScene++;
				Scenes[currentScene].SetActive(true);
			}
		}
		else if(currentScene == Scenes.Count - 1)
		{
			if(animEnd()){
				Scenes[currentScene].GetComponent<Animator>().enabled = false;
				Application.LoadLevel("Stage1");
			}
		}
		else
		{
			Scenes[currentScene].GetComponent<Animator>().enabled = false;
			Application.LoadLevel("Stage1");
		}
	}

	bool animEnd()
	{
		if(Scenes[currentScene].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >=0.99)
		{
			return true;
		}
		else
		{
			return false;
		}
		return false;
	}
}

