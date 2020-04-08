using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour
{
    //[SerializeField]
    //private Text timerLabel;

    [SerializeField]
    private Text moveLabel;

    private float currentspeed;

    private GameObject _player;
    private Rigidbody Rb;

    private StrafeMovement sm;

    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        Rb = _player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        currentspeed = Rb.velocity.magnitude;
        // timerLabel.text = FormatTime(GameManager.Instance.TimeTaken);
        moveLabel.text = FormatSpeed(currentspeed);

    }

    //private string FormatTime(float timeInSeconds)
    //{
    //    return string.Format("{0:#0}:{1:00}.{2:000}", Mathf.FloorToInt(timeInSeconds / 60), Mathf.FloorToInt(timeInSeconds % 60), Mathf.FloorToInt((timeInSeconds * 1000) % 1000));
    //}

    private string FormatSpeed(float ups)
    {
        return string.Format("{0}", Mathf.Round(ups * 100) / 100 + "ups");
    }
}
