﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

    public static GameManager Instance = null;

    private float _currentlv;



    public float CurrentLv
    {
        get { return _currentlv; }
        set { _currentlv = value; }
    }


    void Start()
    {
        CurrentLv = 1;

    }

    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }

        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        // Dont destroy on reloading the scene
        DontDestroyOnLoad(gameObject);

 
    }

    public PlayerController Player;
    
}
