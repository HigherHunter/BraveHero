﻿using UnityEngine;
using UnityEngine.SceneManagement;

//manages player instance
public class PlayerManager : MonoBehaviour {

    public static PlayerManager instance;

    private void Awake()
    {
        instance = this;
    }

    public GameObject player;

    public void KillPlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
