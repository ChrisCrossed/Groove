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

        Init_Board();
    }

    void Init_Board()
    {
        SetBlockAtPosition(BlockType.Red, 0, BoardHeight - 1);
        SetBlockAtPosition(BlockType.Blue, 0, BoardHeight - 5);

        SetBlockAtPosition(BlockType.Red, 2, BoardHeight - 1);
        SetBlockAtPosition(BlockType.Blue, 2, BoardHeight - 3);

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

        print( "-------------------------------" );
    }
	
    void SetBlockAtPosition( BlockType blockType_, int x_, int y_ )
    {
        BlockArray[ ( BoardWidth * y_ ) + x_ ] = blockType_;
    }

    void MoveBlocksDown()
    {
        // Moving horizontally, check each column
        for( int i = 0; i < BoardWidth; ++i )
        {
            // Start from the left column and work upwards
            for (int j = 0; j < BoardHeight; ++j)
            {
                // If the current block is not empty, proceed
                if (BlockArray[(BoardWidth * j) + i] != BlockType.None)
                {
                    // If the spot below the current block does not exist, move on (Bottom row)
                    if (j == 0) continue;

                    // If the spot below the current block is also occupied, move on
                    if (BlockArray[(BoardWidth * (j - 1) + i)] != BlockType.None) continue;

                    // Store current block temporarily
                    BlockType tempBlock = BlockArray[(BoardWidth * j) + i];

                    // Move block one position down vertically
                    SetBlockAtPosition(tempBlock, i, j - 1);

                    // Clear current position
                    SetBlockAtPosition(BlockType.None, i, j);
                }
            }
        }
    }

    // Update is called once per frame
    float f_Timer;
	void Update ()
    {
        f_Timer += Time.deltaTime;

        if(f_Timer > 1.0f)
        {
            MoveBlocksDown();

            PrintBoardToConsole();

            f_Timer = 0f;
        }
	}
}