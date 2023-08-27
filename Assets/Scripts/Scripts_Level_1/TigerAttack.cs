using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TigerAttack : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string name;
        public int count;
        public float delay = 2f;
    }

    private float searchCountdown = 1f;
    private int max_Tigers;
    private bool enableWaves = true;
    private bool allowAttack = false;
    private bool hasWon = false;
    private int current_Wave;
    private int temp;
    public Wave[] waves;
    public GameObject[] TigerSpawnPoints;
    public Animator tigerAnimator;
    public GameObject tiger_Prefab;                 //Crow prefab
    public float minSpeed;                    //min crow movement speed
    public float maxSpeed;                    //max crow movement speed
    public GameObject[] inactiveCanvas;
    public string levelCompleteCanvasName;
    private GameObject[] Sheeps;
    public GameObject tigerSound;
    float distanceToAttackTag;
    private GameObject[] tigers;      //array of total crows instantiated
    private int[] randomNoRepeat;    //used to store the target locations assigned to each crow when instantiated
    private RandomNoRepeat obj;      //obj contains the range of target positions

    bool PlayAttackAnimation = false;

    GameObject[] _canvas;

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

    private void Start()
    {
        Debug.Log("Tiger Attack called.");
        tigerSound.SetActive(false);
        _canvas = GameObject.FindGameObjectsWithTag("canvas");

        max_Tigers = FindMaxWaveCount();  //gets the max numer of crows that are going to exist in the scene
        DisableUnusedCanvas();
        //	Debug.Log("Finding Sheeps");

        Sheeps = GameObject.FindGameObjectsWithTag("sheep");

        if (Sheeps != null)
        {
            //	Debug.Log("Sheeps Found");
            randomNoRepeat = new int[max_Tigers];
            obj = new RandomNoRepeat(0, Sheeps.Length - 1);
            temp = current_Wave;
            //	Debug.Log("Curren Wave "+temp);
        }
        else
        {

            //Debug.Log("No Sheeps Found");
        }
    }

    private void Update()
    {

        if (current_Wave == waves.Length)
            enableWaves = false;

        if (!hasWon)
            CheckWinCondition();

        if (!EnemyIsAlive() && enableWaves)                   //as the attack is allowed, controls goes inside if statement
        {
            allowAttack = false;
            current_Wave = temp;
            InstantiateTigers();
            //Debug.Log("TIgers Instantiated");
            if (current_Wave < waves.Length)
                StartCoroutine(StartTigerAttackAfterDelay(waves[current_Wave].delay));
            else
            {
                allowAttack = false;
                enableWaves = false;
            }
            temp++;
        }


        if (allowAttack)
        {
            Debug.Log("Tigers Attacking");
            AttackOnSheeps();

        }

        distanceToAttackTag = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("AttackRadius").transform.position);
        if (distanceToAttackTag <= 20)
        {
            PlayAttackAnimation = true;
        }
        else
        {
            PlayAttackAnimation = false;
        }


    }

    private IEnumerator StartTigerAttackAfterDelay(float delay)
    {
        allowAttack = false;
        tigerSound.SetActive(false);
        Debug.Log("Delay called");
        ReverseTimerController.instance.setTime(waves[current_Wave].delay + 1);
        yield return new WaitForSeconds(delay);
        allowAttack = true;

        // Enable NavMeshAgent before allowing the tigers to move
        for (int i = 0; i < tigers.Length; i++)
        {
            tigers[i].GetComponent<NavMeshAgent>().enabled = true;
        }

        tigerSound.SetActive(true);
    }


    void AttackOnSheeps()
    {

        for (int i = 0; i < waves[current_Wave].count; i++)
        {
            if (tigers[i] != null)
            {
                NavMeshAgent tigerAgent = tigers[i].GetComponent<NavMeshAgent>();
                // Debug.Log("REMAINING DISTANCE " + tigerAgent.remainingDistance);
                if (tigerAgent != null)
                {

                    tigerAnimator = tigers[i].GetComponent<Animator>();

                    if (Sheeps[randomNoRepeat[i]] != null)
                    {
                        tigerAgent.SetDestination(Sheeps[randomNoRepeat[i]].transform.position);
                        if (tigerAnimator != null && !PlayAttackAnimation)
                        {
                            tigerAnimator.SetBool("run", true);
                            tigerAnimator.SetBool("hit", false);
                        }
                    }
                    // check if  the tiger has reached the destination(sheep)



                    if (PlayAttackAnimation)
                    {

                        tigerAnimator.SetBool("run", false);
                        tigerAnimator.SetBool("hit", true);
                    }






                    if (Sheeps[randomNoRepeat[i]] == null)
                    {

                        tigerAgent.SetDestination(GameObject.FindGameObjectWithTag("ReturnTag").transform.position);
                        if (tigerAnimator != null && !PlayAttackAnimation)
                        {
                            tigerAnimator.SetBool("run", true);
                            tigerAnimator.SetBool("hit", false);
                        }
                    }

                }

            }
        }
    }


    void CheckWinCondition()
    {
        if (current_Wave == waves.Length && !EnemyIsAlive())
        {
            //   Debug.Log("All waves done and dusted.");       

            for (int i = 0; i < _canvas.Length; i++)
            {
                if (_canvas[i].gameObject.name == levelCompleteCanvasName)
                {
                    //	Debug.Log("Game win Canvas is active now.");
                    Time.timeScale = 0f;
                    _canvas[i].SetActive(true);
                    hasWon = true;
                }
                else
                    _canvas[i].SetActive(false);
            }
        }
    }

    void InstantiateTigers()
    {
        if (current_Wave < waves.Length)
        {
            tigers = new GameObject[waves[current_Wave].count];
            int j = 0;
            for (int i = 0; i < waves[current_Wave].count; i++)
            {
                tigers[i] = Instantiate(tiger_Prefab);
                tigers[i].transform.position = TigerSpawnPoints[j].transform.position;

                tigers[i].GetComponent<NavMeshAgent>().enabled = false; // Disable NavMeshAgent initially

                j++;
                if (j >= TigerSpawnPoints.Length)
                    j = 0;

                Vector3 directionTowardsCenter = GameObject.FindGameObjectWithTag("canonbase").transform.position - tigers[i].transform.position;
                directionTowardsCenter.y = 0f;
                tigers[i].transform.rotation = Quaternion.LookRotation(directionTowardsCenter);

                randomNoRepeat[i] = obj.GetNextValue();
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

    void DisableUnusedCanvas()
    {
        for (int i = 0; i < inactiveCanvas.Length; i++)
            inactiveCanvas[i].SetActive(false);
    }
}
