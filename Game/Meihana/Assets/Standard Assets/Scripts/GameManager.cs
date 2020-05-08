using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

    public static GameManager Instance = null;

    private float _currentlv;
    private float _completed;
    private float _lv1completed;


    public float CurrentLv
    {
        get { return _currentlv; }
        set { _currentlv = value; }
    }

    public float Completed
    {
        get { return _completed; }
        set { _completed = value; }
    }

    public float Lv1
    {
        get { return _lv1completed; }
        set { _lv1completed = value; }
    }

    void Start()
    {
        CurrentLv = 1;

        Lv1 = 0;


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
