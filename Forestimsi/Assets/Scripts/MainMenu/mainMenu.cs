﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public ScriptableObject script;
 
    public void changeScence()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
