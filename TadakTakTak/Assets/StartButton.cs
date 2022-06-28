using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
public class StartButton : MonoBehaviour
{
    public Button startBtn; 

    void Start()
    {
        startBtn.onClick.AddListener(() => SceneManager.LoadScene("Game"));
    }


}
