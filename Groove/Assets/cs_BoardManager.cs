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

            // Test
            if( i < BoardWidth )
            {
                BlockArray[i] = BlockType.Red;
            }
        }

        print( BoardWidth );

        PrintBoardToConsole();
    }

    void PrintBoardToConsole ()
    {
        for (int i = BoardHeight - 1; i >= 0; --i)
        {
            string CurrLine = "";

            for (int j = 0; j < BoardWidth; ++j)
            {
                BlockType currBlock = BlockArray[(BoardHeight * i) + j];

                if (currBlock == BlockType.None )
                {
                    CurrLine += "[    ]";
                }
                else if( currBlock == BlockType.Red )
                {
                    CurrLine += "[ X ]";
                }
                else
                {
                    CurrLine += "[ O ]";
                }
            }

            print(CurrLine);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}