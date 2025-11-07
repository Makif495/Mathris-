using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameBoard : MonoBehaviour
{


    public TetrominoData lastSpawnedBlockData;  // Son oluşturulan Tetromino'nun verisi
    public Vector3Int lastSpawnedBlockPosition; // Son oluşturulan Tetromino'nun pozisyonu


    [SerializeField] private TetrominoData[] tetrominos;
    [SerializeField] private Vector3Int spawnPosition;

    public Vector2Int boardSize = new Vector2Int(8, 16);


    public RectInt Bounds
    {
        get
        {
            Vector2Int position = new Vector2Int(-boardSize.x / 2, -boardSize.y / 2);
            return new RectInt(position, boardSize);
        }
    }


    public Block ActiveBlock { get; private set; }
    public Tilemap Tilemap { get; private set; }

    private void Awake()
    {
        ActiveBlock = GetComponentInChildren<Block>();
        Tilemap = GetComponentInChildren<Tilemap>();

        for (int i = 0; i < tetrominos.Length; i++)
        {
            tetrominos[i].Initialize();
        }
    }

    public void Start()
    {
       // SpawnPiece();
        // Set(ActiveBlock);
    }

    public void SpawnPiece()
    {
        int random = Random.Range(0, tetrominos.Length);
        TetrominoData data = tetrominos[random];

        if (ActiveBlock == null)
        {
            Debug.LogError("ActiveBlock referansı null! Sahnedeki Block nesnesini kontrol et.");
            return;
        }

        ActiveBlock.Initialize(this, spawnPosition, data);
        lastSpawnedBlockData = data;
        lastSpawnedBlockPosition = spawnPosition;

        // MathQuestionGenerator içindeki block değişkenine atama yap
        MathQuestionGenerator questionGenerator = FindObjectOfType<MathQuestionGenerator>();
        if (questionGenerator != null)
        {
            questionGenerator.block = ActiveBlock;
        }
        else
        {
            Debug.LogError("MathQuestionGenerator bulunamadı! Blok ataması yapılamadı.");
        }

        Debug.Log($"Yeni blok oluşturuldu! Pozisyon: {lastSpawnedBlockPosition}");
    }





    public void Set(Block block)
    {
        for (int i = 0; i < block.Cells.Length; i++)
        {
            Vector3Int tilePosition = block.Cells[i] + block.Position;
            Tilemap.SetTile(tilePosition, block.TData.tile);
        }

    }
    public void Clear(Block block)
    {
        for (int i = 0; i < block.Cells.Length; i++)
        {
            Vector3Int tilePosition = block.Cells[i] + block.Position;
            Tilemap.SetTile(tilePosition, null);
        }

    }

    public bool IsValidPosition(Block block, Vector3Int position)
    {
        RectInt bounds = Bounds;

        for (int i = 0; i < block.Cells.Length; i++)
        {
            Vector3Int tilePosition = block.Cells[i] + position;

            if (!bounds.Contains((Vector2Int)tilePosition))
            {
                return false;
            }
            if (Tilemap.HasTile(tilePosition))
            {
                return false;
            }
        }
        return true;
    }

   


    





}
