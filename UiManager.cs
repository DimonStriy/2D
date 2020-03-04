using UnityEngine;
using UnityEngine.UI;
public class UiManager : MonoBehaviour {

    public Button[] buttons;
    public Text scoreText;
    int score = 0;
    
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void IncrementScore() {
        score++;
        scoreText.text = "Очки: " + score;
    }

    public void Play()
    {
        Application.LoadLevel("level1");
    }
}
