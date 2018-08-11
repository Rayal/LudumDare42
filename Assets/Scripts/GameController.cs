using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController instance = null;

    public enum TurnState
    {
        Other,
        PlayerOne,
        OneToTwo,
        PlayerTwo,
        TwoToOne
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        initGame();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void initGame()
    {

    }
}
