using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{

    public MyGameManager gameManager;
    public Text score;
    
    // Start is called before the first frame update
    void Start()
    {
        if (score == null)
        {
            score = GetComponentInChildren<Text>();
        }
        if (gameManager == null)
        {
            gameManager = FindAnyObjectByType<MyGameManager>();
        }
        else
        {
            // initialize here, not in the MyGameManager class
            gameManager.ui = this;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
