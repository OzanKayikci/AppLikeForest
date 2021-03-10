using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*zamanın geriye doğru akışı*/
public class countDowm : MonoBehaviour
{
    public Camera Camera;
    public Camera MainCamera;

    public GameObject backButton;
    public treeSpawn spawntree;
    public Text timerText;

    static public float time;
    private float startTime;
    public int temp;

    private bool start = false;
    private bool Continue = false;
    public static bool overTime = false;


    

    //private void Awake()
    //{
    //    DontDestroyOnLoad(this.gameObject);

    //}
    
    public void Start()
    {
        temp = 1;
        treeSpawn spawntree = GetComponent<treeSpawn>();
 
    }

    private void Update()
    {
        if (treeSpawn.gamePaused == true)
        {
            if (time > 1)
            {
               spawntree.Invoke("addTree", 0);/*uygulama durduğu anda ölü treeSpawn classındaki addtree fonksiyonu*/
            }
            time = 0;
            
        }
        overTime = false;
        if (start == true)/* set time to countdown */
        {
            time = startTime;
            start = false;
        }
        if (time > 0)/* when time bigger than 0*/
        {
            overTime = false;
            Continue = true;
        }
        if (Continue == true)/* update time by per frame*/
        {
            UpdateTimer();
           
        }
        if (time < 1.0f && time >0.98f)/* time to add tree*/
        {
            overTime = true;
            Continue = false;
            
        }
            if (overTime == true)
            {
                tree();
                overTime = false;
            } 

        //Debug.Log("overtime " + overTime);
        //Debug.Log("time" + time);
    }

    


    public void startTimeCount()//start button
    {
       
        startTime = setTime.currentAmount;
        start = true;
    }
    void StartCoundownTimer()
    {
        
        if (timerText != null)
        {
            int hours = (int)(time / 3600);
            int munit = (int)(time / 60);
            int second = (int)(time % 60);
            int fractions = (int)((time * 100) % 100);
            timerText.text = "Time Left: " + hours+ munit + second + fractions ;
            InvokeRepeating("UpdateTimer", 0.0f, 0.0079f);
        }
    }

    void UpdateTimer()/* update and show time*/
    {
    

        if (time < 0)
        {

            time = 0.0f;
        }

        if (timerText != null)
          {
                time -= Time.deltaTime;

                string hour = Mathf.Floor(time / 3600).ToString("00");
                string minutes = Mathf.Floor((time / 60) % 60).ToString("00");
                string seconds = (time % 60).ToString("00");
                string fraction = ((time * 100) % 100).ToString("000");
                timerText.text = "Time Left: " + hour + ":" + minutes + ":" + seconds + ":" + fraction;
               
            }
        if (time == 0)
        {
            Debug.Log("end");
        }


    }

    public void tree()/* call  function from treespawn class*/
    {

        spawntree.Invoke("addTree", 0);
        overTime = false;
    }
    public void EnterForest()/* go to forest change camera*/
    {
        Camera.enabled = false;
        MainCamera.enabled  =true;
        backButton.gameObject.SetActive(true);
    }

    public void BactToClock()/*return clock*/
    {
        MainCamera.enabled = false;
        Camera.enabled = true;
        backButton.gameObject.SetActive(false);

    }

    public void quitAPP()
    {
        Application.Quit();
    }
}


