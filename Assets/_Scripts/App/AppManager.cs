using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppManager : Singleton<AppManager> 
{
	private int mCurrentScene;
	private int mScenesLength;
	private EditorBuildSettingsScene[] mScenes;

	#region Don't Destroy OnLoad Singleton

	public bool iAmFirst;

    void Awake()
    {
       DontDestroyOnLoad(Instance);

       AppManager[] appManagers = FindObjectsOfType(typeof(AppManager)) as AppManager[];

       if(appManagers.Length > 1)
       {
           for(int i = 0; i < appManagers.Length; i++)
           {
               if(!appManagers[i].iAmFirst)
               {
                   DestroyImmediate(appManagers[i].gameObject);
               }
           }
       }
       else
       {
           iAmFirst = true;
       }
    }

	#endregion
	
	// Use this for initialization
	void Start () 
	{
		// Get Scenes Length
		this.mScenes = EditorBuildSettings.scenes;
		this.mScenesLength = this.mScenes.Length;
		this.mCurrentScene = 0;
		this.ShowScenesInfo();
	}

	// Update is called once per frame
	void Update () 
	{
		// Input
		this.CheckInput();
	}

	public void NextScene () 
	{
		this.ChangeScene(this.mCurrentScene + 1);
	}

	public void PreviousScene () 
	{
		this.ChangeScene(this.mCurrentScene - 1);
	}

	public void ChangeScene (int scene)
	{
		this.mCurrentScene = scene;
		SceneManager.LoadScene(scene);
	}

	private void CheckInput()
	{
		// KEYBOARD

		// Get Next Scene
		if (Input.GetKeyUp(KeyCode.Plus)) 
		{
			if (this.mCurrentScene < this.mScenesLength - 1) 
			{
				this.ChangeScene(this.mCurrentScene + 1);
			} 
			else 
			{
				this.ChangeScene(0);
			}
			
		}
		// Get Previous Scene
		if (Input.GetKeyUp(KeyCode.Minus)) 
		{
			if (this.mCurrentScene > 0) 
			{
				this.ChangeScene(this.mCurrentScene - 1);
			} 
			else 
			{
				this.ChangeScene(this.mScenesLength - 1);
			}
			
		}	
	}

	#region Debug Functions
	
	private const string mScenesPathPrefix = "Assets/_Scenes/";
	private const string mScenesPathSuffix = ".unity";

	private void ShowScenesInfo()
	{
		Debug.Log("Number of scenes: " + this.mScenesLength);
		for(int i = 0; i < this.mScenesLength; i++)
		{
			string name = this.mScenes[i].path
				.Replace(mScenesPathPrefix, "")
				.Replace(mScenesPathSuffix, "");
			Debug.Log("Scene " + i + ": " + name);
		}
	}
	
	#endregion
}
