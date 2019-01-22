using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class cs_BoardManager : MonoBehaviour
{
    public enum BlockType
    {
        None,
        Red,
        Blue
    }
    
    private BlockType[] BlockArray;
    [SerializeField] int BoardWidth = 7;
    [SerializeField] int BoardHeight = 9;

    // Use this for initialization
    void Start()
    {
        BlockArray = new BlockType[ BoardHeight * BoardWidth ];

        for (int i = 0; i < BoardWidth * BoardHeight; i++)
        {
            BlockArray[i] = BlockType.None;
        }

        string CurrLine = "";
        for( int i = 0; i < BoardHeight; ++i )
        {
           // CurrLine += ""
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}