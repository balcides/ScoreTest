using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

/*
 * 		ScoreTest.cs
 *
 * 		Script designed to test user score
 * 		This is meant to execute calculations and updates.
 * 		It's the script to be added to buttons meant in one scene.
 * 
 * 		
 * 
 */

public class ScoreTest : MonoBehaviour {

	//Transform ScoreManager;
	private ScoreManage _scoreManage;
	private Player _player;
	private string _name;
	//private string[] _names = new string[10];


	void Awake(){
		_scoreManage = Camera.main.GetComponent<ScoreManage>();
		_player = Camera.main.GetComponent<Player>();
	}

	void OnMouseDown(){


		if(transform.name == "pointsButton"){
			// if the transform name is pointsbutton, make this happen.
			//add points on press
			AddPoints ();

		}

		//Calc final score and load leaderboard
		if(transform.name == "finalScore"){
			//append final score to save data and load next scene
			ScoreFinalAndLoad ();
		}

		//return to ScoreTest
		if (transform.name == "returnBtn") {
			SceneManager.LoadScene("ScoreTest");
		}
	}

	// meant to run with add points button
	void AddPoints(){
		
		print ("points button");
		_scoreManage.ScoreAdd += 100;

	}

	// does finale score stuff
	void ScoreFinalAndLoad(){

		//UPDATE LIST
		//grab player pref
		string grabPlayerPref = PlayerPrefs.GetString("leaderboard");

		//add new entry
		List<Player> prefsToList = new List<Player> ();
		prefsToList = _scoreManage.StringToList (grabPlayerPref);

		//sort
		prefsToList.Add(new Player(_scoreManage.MainInputField.text, _scoreManage.ScoreAdd));
		prefsToList.Sort ();
		prefsToList.Reverse ();

		string newUpdate = _scoreManage.ListToString (prefsToList);
		print("newUpdate" + newUpdate);

		//add to playerpref
		PlayerPrefs.SetString("leaderboard", newUpdate);

		//load next scene
		SceneManager.LoadScene("Leaderboard");
	}

}


//TODO: MASSIVE Cleanup
//TODO: Cleanup - remove all commented
//TODO; Cleanup - remove the most unneeded to make score board work
//TODO; Create Set limit number of lines in score
//TODO: Cleanup - simplify Player List system to just a list 
//TODO: Extra - Score class - Score.Sort, Score.Add, Score.Limit, Score.Clear, Score.Print();