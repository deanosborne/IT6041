using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialEnd : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {

        GameManager.Instance.CurrentLv = SceneManager.GetActiveScene().buildIndex + 2;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
