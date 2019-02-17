using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPText : MonoBehaviour
{
    protected Player myPlayer;
    protected Text Text;

    void Awake()
    {
        myPlayer = FindObjectOfType<Controller>().GetComponent<Player>();
        Text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Text.text = myPlayer.HP.ToString() + " / 100 HP";
    }
}
