using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour
{
    public void ReloadLevel()
    {
        Ninja.isJumping = false;
        Ninja.isRecovering = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
