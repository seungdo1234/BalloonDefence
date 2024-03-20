using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour
{
    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }
}
