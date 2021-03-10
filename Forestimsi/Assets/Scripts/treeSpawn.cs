using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class treeSpawn : MonoBehaviour
{
    public GameObject[] platforms;
    public GameObject ground;
    public GameObject tree;
    public GameObject[] grounds;
    public GameObject newground;
    public GameObject trees;
    public GameObject diedTree;

    public float[] treesPosition = new float[1000];
    public float[] diedTreePosition = new float[1000];
    public int savedDataCount = 0;
    public int savedDiedDataCount = 0;
    public int savingtreeCount = 0;
    public int groundCount = 0;
    public static int treeCount = 0;
    public int[] addingplatform;
    public int fullplace = 0;

    public bool fullGround = false;
    public bool timeToTree;
    public bool treeAdding = false;
    bool isPaused = false;
    public static bool gamePaused = false;


    private int index = 0 ,index2 = 0;
    private int platformCount = 0;
    public static Vector3 treePosition;

    private void Awake()
    {
        platforms = new GameObject[16 * grounds.Length]; /*add all platfrom to platforms array*/
        for (int i = 0; i < grounds.Length; i++)
        {
            foreach (Transform child in grounds[i].transform)
            {
                platforms[platformCount] = child.gameObject;
                platformCount++;
            }
        }

        //PlayerPrefs.DeleteAll();
        groundCount = PlayerPrefs.GetInt("groundCount");/*active ground count*/
        for (int i = 0; i <= PlayerPrefs.GetInt("groundCount"); i++)
        {
            grounds[i].SetActive(true);
        }

        savedDataCount = PlayerPrefs.GetInt("savedDataCount");/*trees positions */
        savedDiedDataCount = PlayerPrefs.GetInt("savedDiedDataCount");/* dead trees positions*/
        savingtreeCount = PlayerPrefs.GetInt("savingtreeCount");/*all trees count*/

        Debug.LogWarning(PlayerPrefs.GetInt("savingtreeCount"));
        for (int i = 1; i <= PlayerPrefs.GetInt("savingtreeCount"); i++)/* making tag full which has tree */
        {
            platforms[PlayerPrefs.GetInt("tree" + i.ToString())].tag = "full";
        }

        if (PlayerPrefs.HasKey("savedDataCount"))/*instantiate saving trees */
        {
          
            Debug.Log("tree count " + PlayerPrefs.GetInt("treesChildCount"));
            while (index < PlayerPrefs.GetInt("savedDataCount") )
            {
                Debug.Log(" idnex " + index);
                treesPosition[index] = PlayerPrefs.GetFloat(index.ToString());
                treesPosition[index + 1] = PlayerPrefs.GetFloat(index + 1.ToString());
                treesPosition[index + 2] = PlayerPrefs.GetFloat(index + 2.ToString());

              

                Debug.Log("after x y z " + PlayerPrefs.GetFloat(index.ToString()) + " " + PlayerPrefs.GetFloat(index + 1.ToString()) + " " + PlayerPrefs.GetFloat(index + 2.ToString()));
                Vector3 posVec = new Vector3(treesPosition[index], treesPosition[index + 1], treesPosition[index + 2]);



                //if (treesPosition[index] >= 0)
                //{
                    GameObject newTree = Instantiate(tree, posVec, Quaternion.identity);
                    newTree.transform.SetParent(trees.transform, true);
                //}
              
                
                
                //newTree.transform.parent = trees.transform;
               
               
                index += 3;
                //TODO : ağaçları kaydedilen pozisyonlarda spamlamıyor 
            }
           
        }
        else
        {
            PlayerPrefs.SetInt("savedDataCount", 0);
        }

        if (PlayerPrefs.HasKey("savedDiedDataCount"))/*instantiate saving dead trees */
        {
            while (index2 < PlayerPrefs.GetInt("savedDiedDataCount"))
            {
                diedTreePosition[index2] = PlayerPrefs.GetFloat("died" + index2.ToString());
                diedTreePosition[index2 + 1] = PlayerPrefs.GetFloat("died" + index2 + 1.ToString());
                diedTreePosition[index2 + 2] = PlayerPrefs.GetFloat("died" + index2 + 2.ToString());

                Vector3 posVec2 = new Vector3(diedTreePosition[index2], diedTreePosition[index2 + 1], diedTreePosition[index2 + 2]);


                //if (diedTreePosition[index] >= 0)
                //{
                GameObject newTree2 = Instantiate(diedTree, posVec2, Quaternion.identity);
                newTree2.transform.SetParent(trees.transform, true);
                //}
                index2 += 3;
            }
        }
        else
        {
        
            PlayerPrefs.SetInt("savedDiedDataCount", 0);
        }

    }


    private void OnGUI()/* game puaesed or not*/
    {
       
        if (isPaused)
        {
            gamePaused = true;

        }
        else
        {
            gamePaused = false;
        }
    }

    private void OnApplicationFocus(bool hasFocus)/* when player in app*/
    {
        isPaused = !hasFocus;
    }

    private void OnApplicationPause(bool pauseStatus)/*when app is pause*/
    {
        isPaused = pauseStatus;
    }




    public void addTree()/*adding tree */
    {
        Debug.Log("ground count " + groundCount);
        do
        {
            treeCount = Random.Range(0, ((groundCount + 1) * 16));
            
        } while (platforms[treeCount].tag == "full");/* add tree to random position*/
        //Debug.Log("tree c" + treeCount + " platformm " + platforms[treeCount].transform.parent.gameObject);
        platforms[treeCount].tag = "full";

        savingtreeCount++;
        PlayerPrefs.SetInt("tree" + savingtreeCount.ToString(), treeCount);/*store which platform has tree*/
        PlayerPrefs.SetInt("savingtreeCount", savingtreeCount);/* store all trees count*/
        
        //for (int i = 0; i <= groundCount; i++)
        //{
        //    fullplace = 0;
        //    if (index < ground.transform.childCount * (groundCount + 1))
        //    {
        //        Debug.Log(" child is " + grounds[groundCount].transform.childCount);
        //        Debug.Log("i is " + index);
        //        if (grounds[i].transform.GetChild(index).tag == "full")
        //        {
        //            fullplace++;

        //        }
        //        Debug.Log("ful place " + fullplace);
        //        if (fullplace == 16 * (groundCount + 1))
        //        {
        //            fullGround = true;
        //            addGround();
        //            break;
        //        }
        //        index++;
        //    }

        //}
        //Debug.Log("key");
        //Debug.Log(treeCount + "asd");
        
        Vector3 groundposition = platforms[treeCount].transform.position;
        GameObject newtree;
        int i = savedDataCount;
        int j = savedDiedDataCount;
        if (gamePaused == true)/* instantiate and store  dead tree*/
        {
            newtree = Instantiate(diedTree, groundposition, Quaternion.identity);
            diedTreePosition[j] = newtree.transform.position.x;
            diedTreePosition[j + 1] = newtree.transform.position.y;
            diedTreePosition[j + 2] =  newtree.transform.position.z;

            PlayerPrefs.SetFloat("died" + j.ToString(), diedTreePosition[j]);
            PlayerPrefs.SetFloat("died" + j + 1.ToString(), diedTreePosition[j + 1]);
            PlayerPrefs.SetFloat("died" + j + 2.ToString(), diedTreePosition[j + 2]);

            savedDiedDataCount += 3;
        }
        else/* instantiate and store normal tree*/
        {
            newtree = Instantiate(tree, groundposition, Quaternion.identity);

            treesPosition[i] = newtree.transform.position.x;
            treesPosition[i + 1] = newtree.transform.position.y;
            treesPosition[i + 2] = newtree.transform.position.z;


            PlayerPrefs.SetFloat(i.ToString(), treesPosition[i]);
            PlayerPrefs.SetFloat(i + 1.ToString(), treesPosition[i + 1]);
            PlayerPrefs.SetFloat(i + 2.ToString(), treesPosition[i + 2]);

            savedDataCount += 3;
        }

        newtree.transform.parent = trees.transform;
        Debug.Log(trees.transform.childCount + "childsss");

        //for (int i = savedDataCount; i < savingDataCount; i++)
        //{


       

        Debug.Log(" x y z " + PlayerPrefs.GetFloat(i.ToString()) + " " + PlayerPrefs.GetFloat(i + 1.ToString()) + " " + PlayerPrefs.GetFloat(i + 2.ToString()));
        //}
       
     
        PlayerPrefs.SetInt("savedDataCount", savedDataCount);/* store normal trees position*/
        PlayerPrefs.SetInt("savedDiedDataCount", savedDiedDataCount);/*store died trees position*/
        PlayerPrefs.SetInt("treesChildCount", trees.transform.childCount);/* store trees count*/

        if (trees.transform.childCount == 16 * (groundCount + 1) - 1)/* add ground if active ground has 15 trees*/
        {
            addGround();
            //Debug.Log("grondadded");
        }
    }

    public void addGround()/*adding ground*/
    {
        int c = groundCount;
        //foreach(Transform child in grounds[c].transform)
        //{
        //    platforms[platformCount] = child.gameObject;
        //    platformCount++;
        //}
        //if (fullGround == true)
        //{
        //Debug.Log( "grondadded");
        for (int i = c; i <= groundCount + 2; i++)
        {
            //TODO : YENİ GROUND EKLEMEDEN ÖNCE SONSUZ DÖNGÜYE GİRİYOR 
            grounds[i].SetActive(true);

        }
        groundCount += 2;
        PlayerPrefs.SetInt("groundCount", groundCount);
        //    fullGround = false;
        //}
        //fullGround = false;
        /*int spawnCount =0;
        int mult1 = 1;
        int mult2 = 1;
        Vector3 pos = grounds[0].transform.position;
       
            
            spawnCount = groundCount + 2;
            
           
            for (int i = 0; i < spawnCount; i++)
            {
                mult2++;
                if ((int)(spawnCount / 2) < i)
                {
                    GameObject newground = Instantiate(ground, new Vector3(pos.x + 39.52f*(mult2 - 1), pos.y, pos.z + (39.52f *( mult1))), Quaternion.identity);
                    grounds.Add(newground);
                }
                else if ((int)(spawnCount / 2) > i)
                {
                    GameObject newground = Instantiate(ground, new Vector3(pos.x + (39.52f * (mult1)), pos.y , pos.z + 39.52f*(mult2-1)), Quaternion.identity);
                    grounds.Add(newground);
                }
                else
                {
                    GameObject newground = Instantiate(ground, new Vector3(pos.x + 39.52f*(mult2-1), pos.y , pos.z + 39.52f*(mult2-1)), Quaternion.identity);
                    grounds.Add(newground);
                }
                groundCount = spawnCount-1;
            }
            mult1++;
            
        }*/
    }

}

