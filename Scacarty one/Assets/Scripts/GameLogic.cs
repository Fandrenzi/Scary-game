using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    public GameObject counter;
    public int pageCount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pageCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        counter.GetComponent<Text>().text = pageCount + "/3";
    }
}
