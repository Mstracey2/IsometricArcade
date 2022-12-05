using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableMinigameManager : Minigame
{
   [SerializeField] private List<GameObject> poolBalls = new List<GameObject>();
   [SerializeField] private Material[] ballMats;
   [SerializeField] private GameObject[] PoolBallSpawnLocations;
    private MinigameManager miniManager;

    public List<int> alreadyChosenLocations = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        inheritedFunction = GameSetup;
        miniManager = GetComponentInParent<MinigameManager>();
    }

    // Update is called once per frame
    void Update()
    {
       if(poolBalls.Count == 0)
        {
            miniManager.IncreaseIncomeOnMachine();
        }
    }

    public void GameSetup()
    {
        Mouse.Instance.mouseMode = Mouse.Instance.MouseMiniGameOne;
        StartCoroutine(RandomizeLocation());
    }

    public void RemoveFromList(GameObject ball)
    {
        poolBalls.Remove(ball);
    }

    IEnumerator RandomizeLocation()
    {
        bool freeLocation = true;
        foreach (GameObject ball in poolBalls)
        {
            bool locationFound = false;
            while (locationFound == false)
            {
                int location = Random.Range(0, PoolBallSpawnLocations.Length);
                foreach (int thisInt in alreadyChosenLocations)
                {
                    if (thisInt == location)
                    {
                        freeLocation = false;
                        break;
                    }
                    else
                    {
                        freeLocation = true;
                    }
                    
                }
                if (freeLocation)
                {
                    alreadyChosenLocations.Add(location);
                    Instantiate(ball, PoolBallSpawnLocations[location].transform.position, PoolBallSpawnLocations[location].transform.rotation);
                    locationFound = true;
                }
            }
        }
        yield break;  
    }
    
}
