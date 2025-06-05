using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Events : MonoBehaviour
{
   public void Replay()
    {
        SceneManager.LoadScene(0);
    }
}
