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

        // TODO: Remove
        TEST_RedBlocksBottomLine();

        PrintBoardToConsole();
    }

    void PrintBoardToConsole ()
    {
        for ( int i = BoardHeight - 1; i >= 0; --i )
        {
            string CurrBlock = "";

            for ( int j = 0; j < BoardWidth; ++j )  
            {
                BlockType currBlock = BlockArray[ ( BoardWidth * i ) + j ];

                if (currBlock == BlockType.None )
                {
                    CurrBlock += "[    ]";
                }
                else if( currBlock == BlockType.Red )
                {
                    CurrBlock += "[ X ]";
                }
                else
                {
                    CurrBlock += "[ O ]";
                }
                
                // The Key To Everything
                // print( ( BoardWidth * i ) + j );
            }

            print( CurrBlock );
        }
    }
	
    void SetBlockAtPosition( BlockType blockType_, int x_, int y_ )
    {
        BlockArray[ ( BoardWidth * y_ ) + x_ ] = blockType_;
    }

    void TEST_RedBlocksBottomLine()
    {
        for (int i = 0; i < BoardWidth; ++i)
        {
            SetBlockAtPosition( BlockType.Red, i, 0 );
        }
    }

	// Update is called once per frame
	void Update ()
    {
	
	}
}