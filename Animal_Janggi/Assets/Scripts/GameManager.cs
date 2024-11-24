using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] Tiles;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var t in Tiles)
        {
            if (t.CompareTag("Green"))
            {
                t.GetComponent<Transform>().GetChild(1).GetComponent<SpriteRenderer>().flipX = true;
                t.GetComponent<Transform>().GetChild(1).GetComponent<SpriteRenderer>().flipY = true;
            }
            else if (t.CompareTag("Red"))
            {
                t.GetComponent<Transform>().GetChild(1).GetComponent<SpriteRenderer>().flipX = false;
                t.GetComponent<Transform>().GetChild(1).GetComponent<SpriteRenderer>().flipY = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
