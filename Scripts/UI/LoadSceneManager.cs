using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour {

	// Use this for initialization
	public void loadByIndex(int sceneIndex)
	{
		SceneManager.LoadScene(sceneIndex);
	}
}
