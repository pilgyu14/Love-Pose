using Main.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PlayerInfo : MonoBehaviour
{
    public Slider hpBar;
    public int maxHp; 
    public void Start()
    {
        EventManager.Instance.StartListening(EventsType.SetHPUI, (x) => SetUI((int)x));
    }
    public void SetUI(int hp)
    {
        hpBar.value = hp / maxHp; 
    }
}
