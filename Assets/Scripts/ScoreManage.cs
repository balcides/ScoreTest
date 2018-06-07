using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.
using System.Collections.Generic;
using System;

/*
 * 		ScoreManage.cs
 *
 * 		Script designed to test user score
 * 		Score Manager is meant to be the central nervous system
 * 		of managing the score. Like the game manager of score keeping.
 * 		Meant to be used across all scenes keeping score or adding.
 * 
 */


public class ScoreManage : MonoBehaviour {

	public int ScoreAdd;
	public TextMesh ScoreText;
	public bool EnableScoreAdd;
	public InputField MainInputField;
	//private string _scoreInfo;

	//static int _listLen = 8;
	//int[] _scores = new int[_listLen];
//	int[] _scores2 = new int[_listLen];
	//int[] Array = { 11, 33, 5, -3, 19, 8, 49 };
	int temp;
	//string[] _names = new string[_listLen];
	public int InterValueTest = 0;

	public List<Player> MyList = new List<Player>();
	List<Player> prefsToList = new List<Player> ();



	private string _leaderboardInput;

	private void Awake(){

		//setup default values for user score on start
		//TODO: make this work with IronPython, muahahah!
		MyList.Add(new Player("Doug", 32));	
		MyList.Add(new Player("Ted", 35));
		MyList.Add(new Player("Gabe", 33));	
		MyList.Add(new Player("Lou", 34));

		InterValueTest = 10;

		//load default into playerprefs dir
		//load initial values from player scores in random order to prefs
		//TODO: make this a public function
		foreach (Player player in MyList) {
			PlayerPrefs.SetInt(player.name, player.score);
		}

		_leaderboardInput = "";

	}

	// Use this for initialization
	private void Start () {

		//If there's no Input field used for user score input, do nothing
		if (!MainInputField) {
		} else {
			
			//otherwise, set default comment in InputField
			MainInputField.text = "blah blah blah...";
		}
	
	}
	
	// Update is called once per frame
	private void Update () {
		
		// score add makes central score text the current points. 
		if (EnableScoreAdd) {
			ScoreText.text = ScoreAdd.ToString ();
		} else {
			
			//else it displays the leaderboard

			//shuffle ranks
			_leaderboardInput = PlayerPrefs.GetString("leaderboard","None");


			prefsToList = StringToList (_leaderboardInput);
			string _leaderboardToString = "";

			int i = 0;
			int scoreDisplayLimit = 8;
			foreach (Player item in prefsToList) {
				if (i >= scoreDisplayLimit) {
				} else {
					_leaderboardToString = _leaderboardToString + "\n" + item.name + "..." + item.score;
				}
				i++;
			}

			//display leaderboard
			ScoreText.text = _leaderboardToString;

		}			
	}

	//meant to take the score array and sort in ascending order and provide it in one line
	//TODO: later try the DataTable.Sort() method. See if that works? 2 lines of code possibly (ahaha)
	//TODO: Start easy and simple before going over loop, one step at a time. Slow and simple.

	public void ScoreSort2(){

		string _testString = "";

		MyList.Sort();
		MyList.Reverse();

		_testString = ListToString(MyList);

		print("ListToString: _testString=" + _testString);


		PlayerPrefs.SetString ("leaderboard", _testString);
		List<Player> scoresx = new List<Player> ();
		scoresx = StringToList (_testString);
		scoresx.Add(new Player("Jurvetson", 37));	
		scoresx.Sort();
		scoresx.Reverse();
		PrintPlayerList (scoresx);

		//now we're going to try this with playerprefs and we'll be money!
		PlayerPrefs.SetString ("leaderboard", ListToString(scoresx));

		string getpref = PlayerPrefs.GetString ("leaderboard","None");

		prefsToList = StringToList (getpref);

	}

	//convert list to string
	public string ListToString(List<Player> theList){
		string _testString = "";

		foreach (Player player in theList) {
			print (player.name + " " + player.score);
			_testString = _testString + player.name + ',' + player.score + ':';
		}

		return _testString;
	}

	//convert string to list
	public List<Player> StringToList(string theString){

		string[] lines = theString.Split (':');
		print ("theString=" + theString);

		foreach (string thing in lines) {
			print ("thing=" + thing);
		}

		List<Player> scoreitems = new List<Player>();

		foreach (string item in lines) {

			if (item == "") {
			} else {
				string[] items = item.Split(',');


				scoreitems.Add (new Player (items [0], Convert.ToInt32 (items [1])));
			}
		}

		return scoreitems;
	}

	//print player list
	public void PrintPlayerList(List<Player> listing){
		
		foreach (Player item in listing) {
			print (item.name + " ~~ " + item.score);
		}
	}
}


//TODO: get user score to update on new value in rank order (the big one)


public class Player: IComparable<Player>
{
	public string name;
	public int score;

	public Player(string newName, int newScore)
	{
		name = newName;
		score = newScore;
	}

	//This method is required by the IComparable
	//interface. 
	public int CompareTo(Player other)
	{
		if(other == null){ return 1;
		}

		//Return the difference in power.
		return score - other.score;
	}
}