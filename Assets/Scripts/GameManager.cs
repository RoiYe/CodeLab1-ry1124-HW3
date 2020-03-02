using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //static variable means the value is the same for all the objects of this class type and the class itself
    public static GameManager instance; //this static var will hold the Singleton

    public Text infoText; //Text Component to tell you the time and the score

    private float timerFloat = 0.0f;
    private int timer = 0; 

    private int score = 0;

    //private const string PLAY_PREF_KEY_HS = "High Score";
    //private const string PLAY_PREF_KEY_P1_HS = "P1 HIGH SCORE";

    private const string FILE_HS = "/CodeLab1-S2020-highscore.txt";
    private const string FILE_ALL_SCORES = "/All_scores.txt";
    private const string FILE_TIME = "/Time.txt";
    private const string FILE_ALL_TIMES = "/All_times.txt";

    //Property
    public int Time
	{
		get
		{
            return timer;
		}
		set
		{
            timer = value;
            if(timer > longestTime)
			{
                LongestTime = timer;
			}
		}
	}

    private int longestTime = 0;

    private int LongestTime
	{
		get
		{
            return longestTime;
		}
		set
		{
            longestTime = value;
            File.WriteAllText(Application.dataPath + FILE_TIME, longestTime + "");

            allTimes.Add(longestTime);

            string allTimeString = "";
            for (int i = 0; i < allTimes.Count; i++)
			{
                allTimeString = allTimeString + allTimes[i] + ",";
			}
            File.WriteAllText(Application.dataPath + FILE_ALL_TIMES, allTimeString);
        }
	}

    private List<int> allTimes = new List<int>();

    public int Score{
        get{
            return score;
        }
        set{
            score = value;
            if(score > highScore){
                HighScore = score;
            }
        }
    }

    private int highScore = 0;

    private int HighScore{
        get{
            return highScore;
        }
        set{
            highScore = value;
            //Save it somewhere
            //PlayerPrefs.SetInt(PLAY_PREF_KEY_HS, highScore);
            File.WriteAllText(Application.dataPath + FILE_HS, highScore + "");
        

            allScores.Add(highScore);

            string allScoreString = "";
            for (int i = 0; i < allScores.Count; i++){
                allScoreString = allScoreString + allScores[i] + ",";
            }
            File.WriteAllText(Application.dataPath + FILE_ALL_SCORES, allScoreString);
        }
    }

    private List<int> allScores = new List<int>();

    private void Awake()
    {
        if(instance == null){ //instance hasn't been set yet
            instance = this; //set instance to this object
            DontDestroyOnLoad(gameObject); //Dont Destroy this object when you load a new scene
        } else { //if the instance is already set to an object
            Destroy(gameObject); //destroy this new object, so there is only ever one
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Application.dataPath);

        infoText = GetComponentInChildren<Text>(); //get the text component from the children of this gameObject
        if (File.Exists(Application.dataPath + FILE_HS))
        {
            string hsString = File.ReadAllText(Application.dataPath + FILE_HS);

            print(hsString);
            string[] splitString = hsString.Split(',');
            highScore = int.Parse(splitString[0]);

            for (int i = 0; i < splitString.Length; i++){
                print(splitString[i]);
            }
        }

        if (File.Exists(Application.dataPath + FILE_TIME))
        {
            string tmString = File.ReadAllText(Application.dataPath + FILE_TIME);

            print(tmString);
            string[] splitStringT = tmString.Split(',');
            longestTime = int.Parse(splitStringT[0]);

            for (int i = 0; i < splitStringT.Length; i++)
            {
                print(splitStringT[i]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        timerFloat += Time.deltaTime;//increase the timer by deltaTime every frame
        timer = (int)timerFloat; 
        infoText.text = "Score: " + GameManager.instance.score + " Time: " + timer + " High Score: " + highScore + "Longest Time: " + longestTime ; //update the text component with the score and time
    }
}