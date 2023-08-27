using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CrowMove : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string name;
        public int count;
        public float delay = 2f;
    }

    private float searchCountdown = 1f;
    private int max_Crows;
    private bool enableWaves = true;
    private bool allowAttack = false;
    private bool hasWon = false;
    private int current_Wave;
    private int temp;
    public Wave[] waves;
    public GameObject[] CrowSpawnPoints;       //transform positions of spawn points in the scene
    public Transform[] targetPos;             //transform positions of target locations for crows
    public string cornfieldObjectName;        
    public Animator animationController;
    public GameObject Prefab;                 //Crow prefab
    public float minSpeed;                    //min crow movement speed
    public float maxSpeed;                    //max crow movement speed
    public GameObject[] inactiveCanvas;
	public string levelCompleteCanvasName;
  
    public GameObject crowSound;
    private Transform cornfieldTransform;
    private GameObject[] crows;      //array of total crows instantiated
    private int[] randomNoRepeat;    //used to store the target locations assigned to each crow when instantiated
    private RandomNoRepeat obj;      //obj contains the range of target positions
	
	GameObject[] _canvas; 

    private void Start()
    {
		crowSound.SetActive(false);
		_canvas = GameObject.FindGameObjectsWithTag("canvas");
       
        max_Crows =  FindMaxWaveCount();  //gets the max numer of crows that are going to exist in the scene
        DisableUnusedCanvas();
        cornfieldTransform = GameObject.Find(cornfieldObjectName)?.transform;   //find cornfield in the scene
        if (cornfieldTransform == null)
        {
            Debug.LogError("Cornfield GameObject not found. Make sure the name is correct.");
            return;
        }
        randomNoRepeat = new int[max_Crows];
        obj = new RandomNoRepeat(0, targetPos.Length - 1);
        temp = current_Wave;
    }
	
	void DisableUnusedCanvas()
	{
		for(int i=0;i<inactiveCanvas.Length;i++)
			 inactiveCanvas[i].SetActive(false);
	}
	
    private void Update()
    {
        if (current_Wave == waves.Length)
            enableWaves = false;

        if (!hasWon)
            CheckWinCondition();
		
        if (!EnemyIsAlive() && enableWaves)                   //as the attack is allowed, controls goes inside if statement
        {
            current_Wave = temp;
            InstantiateCrows();
            if (current_Wave < waves.Length)
                StartCoroutine(StartCrowAttackAfterDelay(waves[current_Wave].delay));
            else
            {
                allowAttack = false;
                enableWaves = false;
            }
            temp++;
        }
		

        if(allowAttack)
        {
            animationController.SetBool("fly", true);
            AttackonCornField();
        }
		
    }
    
    void CheckWinCondition()
    {
        if (current_Wave == waves.Length && !EnemyIsAlive())
        {
                  Debug.Log("All waves done and dusted.");       

            for (int i = 0; i < _canvas.Length; i++)
            {
                if (_canvas[i].gameObject.name == levelCompleteCanvasName)
                {
					Debug.Log("Game win Canvas is active now.");
					Time.timeScale=0f;
                    _canvas[i].SetActive(true);
                    hasWon = true;
                }
                else
                   _canvas[i].SetActive(false);
            }
        }
    }
    private bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;

        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }

        return true;
    }
    private IEnumerator StartCrowAttackAfterDelay(float delay)
    {
		Debug.Log("crow delay called");
		allowAttack = false;     
		crowSound.SetActive(false);
		ReverseTimerController.instance.setTime(waves[current_Wave].delay+1);
        yield return new WaitForSeconds(delay);
		Debug.Log("Now Attacking");
     
        allowAttack = true;       
        crowSound.SetActive(true);        
    }

   

    void AttackonCornField()
    {
        for (int i = 0; i < waves[current_Wave].count; i++)
        {
            float speed = Random.Range(minSpeed, maxSpeed) * Time.deltaTime;
            if (crows[i] != null)
            {
                
                crows[i].transform.position = Vector3.MoveTowards(crows[i].transform.position, targetPos[randomNoRepeat[i]].position, speed);
			}
        }
    }

    void InstantiateCrows()
    {
        if (current_Wave < waves.Length)
        {
            crows = new GameObject[waves[current_Wave].count];
            int j = 0;
            for (int i = 0; i < waves[current_Wave].count; i++)
            {
                crows[i] = Instantiate(Prefab);
                crows[i].transform.position = CrowSpawnPoints[j].transform.position;
			
                j++;
                if (j >= CrowSpawnPoints.Length)
                    j = 0;

                Vector3 directionToCornfield = cornfieldTransform.position - crows[i].transform.position;
                directionToCornfield.y = 0f;
                crows[i].transform.rotation = Quaternion.LookRotation(directionToCornfield); //Turns the faces of the crows towards the target position

                randomNoRepeat[i] = obj.GetNextValue(); //getting a unique random number every time for targetPos
            }
        }
        else
        {
            allowAttack = false;
            enableWaves = false;
        }
    }

    int FindMaxWaveCount()
    {
        int max = 0;
        for (int i = 0; i < waves.Length; i++)
        {
            if (waves[i].count > max)
                max = waves[i].count;
        }
        return max;
    }
}
