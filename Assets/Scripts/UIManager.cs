using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UIManager is null");
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    [SerializeField] private Text coinDisplayText;
    // Start is called before the first frame update
    private int _coin = 0;
    public void UpdateCoinDisplayUI()
    {
        coinDisplayText.text = "Coins: " + ++_coin; //Do not like this code. 
    }
}
