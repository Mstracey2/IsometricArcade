using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableMinigameManager : Minigame
{
    #region Variables
    [SerializeField] private List<GameObject> poolBalls = new List<GameObject>();    // list of all the balls
    [SerializeField] private GameObject[] PoolBallSpawnLocations;                    //list of all the possible locations for the balls to spawn at
    private MinigameManager miniManager;
    private bool gameStarted;
    public List<int> alreadyChosenLocations = new List<int>();                      //int to store what locations are taken
    [SerializeField] private GameObject WinningScreen;
    [SerializeField] private GameObject failedScreen;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        inheritedFunction = GameSetup;
        miniManager = GetComponentInParent<MinigameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameStarted == true)
        {
            if (miniManager.CheckTime() <= 0)           //if the player failed
            {
                alreadyChosenLocations.Clear();                     //clears the used spawn locations
                MinigameButtonScript.Instance.ReturnToMain();       //the player returns to main scene
                foreach (GameObject ball in poolBalls)              //the balls return to a inactive state for when they are call upon again
                {
                    ball.SetActive(false);
                }
                gameStarted = false;
                failedScreen.SetActive(true);
            }
            else if (gameStarted)                           // checking to see if the player has won yet
            {
                bool gameComplete = false;
                foreach (GameObject ball in poolBalls)      //checks if each ball is still active, if one is active, then the player has not completed the task
                {
                    if (ball.activeInHierarchy == true)
                    {
                        gameComplete = false;
                        break;
                    }
                    else
                    {
                        gameComplete = true;
                    }
                }

                if (gameComplete)                           //if the player completed the task
                {
                    alreadyChosenLocations.Clear();         //clear the chosen spawn locations for minigame reset
                    gameStarted = false;                    // game is no longer active
                    MinigameButtonScript.Instance.TimesIncome();        // rewarding the player with extra income to that machine
                    MinigameButtonScript.Instance.ReturnToMain();       //player is returned to main scene
                    WinningScreen.SetActive(true);
                }
            }
        }
       
    }

    public void GameSetup()
    {
       StartCoroutine(RandomizeLocation());
    }

    IEnumerator RandomizeLocation()
    {
        bool freeLocation = true;
        foreach (GameObject ball in poolBalls)                          // tasked with spawning every ball
        {
            bool locationFound = false;
            while (locationFound == false)                              //will loop until a vacant position is found
            {
                int location = Random.Range(0, PoolBallSpawnLocations.Length);  //randomly selects a position from the list of locations
                foreach (int thisInt in alreadyChosenLocations)                 //checks if the location is taken
                {
                    if (thisInt == location)                                    // checks if this random location is already taken, if so, the loop breaks to choose a new location 
                    {
                        freeLocation = false;
                        break;
                    }
                    else
                    {
                        freeLocation = true;
                    }
                    yield return null;
                }
                if (freeLocation)                                               //if the location is free, then the ball is placed
                {
                    alreadyChosenLocations.Add(location);                       // adds the location to the occupied locations list
                    ball.transform.position = PoolBallSpawnLocations[location].transform.position;      //matches the transform of the ball with the spawner
                    ball.transform.rotation = PoolBallSpawnLocations[location].transform.rotation;
                    locationFound = true;
                }
                yield return null;
            }
            ball.SetActive(true);                                               // ball is then active, the player needs to put the balls in the holes to deactivate then, if they are all deactivated, the player wins
            yield return null;
        }

        gameStarted = true;                                                     //when all balls are placed, the game starts
        yield break;  
    }
    
}
