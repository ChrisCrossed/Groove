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

    public enum BlockSize
    {
        Normal,
        Wide,
        Tall
    }
    
    // Board Array
    private BlockType[] BoardArray;
    [SerializeField] int BoardWidth = 7;
    [SerializeField] int BoardHeight = 9;
    [SerializeField] bool MultiSizedBlocks = false;

    // Current Block
    private BlockType[] CurrentBlock;
    private BlockSize CurrentBlockSize;

    // Use this for initialization
    void Start()
    {
        BoardArray = new BlockType[ BoardHeight * BoardWidth ];

        for (int i = 0; i < BoardWidth * BoardHeight; i++)
        {
            BoardArray[i] = BlockType.None;
        }

        Init_Board();

        print(UnityEngine.Random.state);

        UnityEngine.Random rand = new UnityEngine.Random();

        // Forcibly sets the seed
        // UnityEngine.Random.InitState(10);

        
    }

    void Init_Board()
    {
        SpawnNewBlock();

        PrintBoardToConsole();
    }

    void PrintBoardToConsole ()
    {
        for ( int i = BoardHeight - 1; i >= 0; --i )
        {
            string CurrBlock = "";

            for ( int j = 0; j < BoardWidth; ++j )  
            {
                BlockType currBlock = BoardArray[ ( BoardWidth * i ) + j ];

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

    void SpawnNewBlock()
    {
        // Random value between 0 and 2 to assign size of block.
        BlockSize blockSize = BlockSize.Normal;

        // Determine size of next block (2x2, 3x3, etc...)
        BlockType[] nextBlock;
        if (!MultiSizedBlocks) nextBlock = new BlockType[4];
        else
        {
            // Determine if next block is 2x2, 2x3 or 3x2
            blockSize = GetRandomBlockSize();
            
            // 1/3rd chance for 2x2, otherwise needs 6 spaces for the block
            if( blockSize == 0 )
                nextBlock = new BlockType[4];
            else
                nextBlock = new BlockType[6];
        }

        // Set 'CurrBlock' based on block size (2x2, 3x3, etc...)
        int startX = BoardWidth / 2;
        int startY = BoardHeight - 2;
        
        // If starting block is 3 high, move the startY down;
        if ( blockSize == BlockSize.Tall )
            startY = BoardHeight - 3;

        // Randomly determine the blocks for the array
        for( int i = 0; i < nextBlock.Length; ++i )
        {
            nextBlock[i] = GetRandomBlockColor();
        }

        // Always initialize the first four blocks (NEED TO CONSIDER as rotating 3x2 or 2x3 changes things up!!!)
        SetBlockCluster( nextBlock, blockSize );

        // Set array of blocks to populate
        // TEST
        SetBlockAtPosition( BlockType.Red, startX, startY );
    }

    BlockType[] SetBlockCluster( BlockType[] blockArray_, BlockSize blockSize_ )
    {


        return null;
    }

    void SetBlockAtPosition( BlockType blockType_, int x_, int y_ )
    {
        BoardArray[ ( BoardWidth * y_ ) + x_ ] = blockType_;
    }

    BlockType GetRandomBlockColor()
    {
        // Get a list of all BlockType values
        var enumList = Enum.GetValues( typeof ( BlockType ) );

        // Pick from the second or third choices (Red or Blue)
        return (BlockType) enumList.GetValue( (int) UnityEngine.Random.Range( 1, 3 ) );
    }

    BlockSize GetRandomBlockSize()
    {
        // Get a list of all BlockSize values
        var enumList = Enum.GetValues( typeof( BlockSize ) );

        // Pick from the choices
        return ( BlockSize ) enumList.GetValue( (int) UnityEngine.Random.Range( 0, 3 ) );
    }

    void MoveBlocksDown()
    {
        // Moving horizontally, check each column
        for( int i = 0; i < BoardWidth; ++i )
        {
            // Start from the left column and work upwards
            for (int j = 1; j < BoardHeight; ++j)
            {
                // If the current block is not empty, proceed
                if (BoardArray[(BoardWidth * j) + i] != BlockType.None)
                {
                    // If the spot below the current block does not exist, move on (Bottom row)
                    if (j == 0) continue;

                    // If the spot below the current block is also occupied, move on
                    if (BoardArray[(BoardWidth * (j - 1) + i)] != BlockType.None) continue;

                    // Store current block temporarily
                    BlockType tempBlock = BoardArray[(BoardWidth * j) + i];

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

        if(f_Timer > 1f)
        {
            MoveBlocksDown();

            PrintBoardToConsole();

            f_Timer = 0f;
        }
	}
}