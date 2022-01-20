using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class CellController : MonoBehaviour
{
    public Button generateButton;
    public Slider scaleSlider;
    public Slider xOffsetSlider;
    public Slider yOffsetSlider;
    public Slider speedSlider;

    public Tile tile;
    public float scale = 30f;
    public Vector2 offset = new Vector2(30f, 30f);
    public const int WIDTH = 100;
    public const int HEIGHT = 100;
    public float timeBetweenUpdates = 1f;
    public float timeSinceLastProcess;

    private Tilemap _map;
    private BitArray _states;

    void Awake()
    {
        _map = gameObject.GetComponent<Tilemap>();
    }

    void Start()
    {
        _states = new BitArray(WIDTH * HEIGHT, false);
        timeSinceLastProcess = 0f;
        generateButton.onClick.AddListener(GenerateMap);

        scaleSlider.value = scale;
        xOffsetSlider.value = offset.x;
        yOffsetSlider.value = offset.y;
        speedSlider.value = 1f / timeBetweenUpdates;
    }

    // Update is called once per frame
    void Update()
    {
        scale = scaleSlider.value;
        offset.x = xOffsetSlider.value;
        offset.y = yOffsetSlider.value;
        timeBetweenUpdates = 1f / speedSlider.value;

        timeSinceLastProcess += Time.deltaTime;
        if (timeSinceLastProcess > timeBetweenUpdates)
        {
            timeSinceLastProcess = 0f;
            ProcessMap();
        }
    }

    void GenerateMap()
    {
        _map.ClearAllTiles();

        Vector3Int position = Vector3Int.zero;
        for (int i = 0; i < WIDTH * HEIGHT; i++)
        {
            position.x = i % WIDTH;
            position.y = i / WIDTH;
            float xCoord = (float) i % WIDTH / WIDTH * scale + offset.x;
            float yCoord = (float) i / WIDTH / HEIGHT * scale + offset.y;
            if (Mathf.PerlinNoise(xCoord, yCoord) < 0.5f)
            {
                _map.SetTile(position, tile);
            }
        }
    }

    void ProcessMap()
    {
        for (int i = 0; i < WIDTH * HEIGHT; i++)
        {
            _states[i] = false;
        }

        Vector3Int position = Vector3Int.zero;
        for (int i = 0; i < WIDTH * HEIGHT; i++)
        {
            position.x = i % WIDTH;
            position.y = i / WIDTH;
            Tile tile1 = (Tile)_map.GetTile(position);
            if (tile1 != null) _states[i] = tile1.name.Equals(tile.name);
        }

        _map.ClearAllTiles();

        for (int i = 0; i < WIDTH * HEIGHT; i++)
        {
            position.x = i % WIDTH;
            position.y = i / WIDTH;

            int numNeighbours = 0;

            if ((i % WIDTH < WIDTH - 1) && (_states[i + 1])) numNeighbours++;
            if ((i % WIDTH > 0) && (_states[i - 1])) numNeighbours++;
            if ((i / WIDTH < HEIGHT - 1) && (_states[i + WIDTH])) numNeighbours++;
            if ((i / WIDTH > 0) && (_states[i - WIDTH])) numNeighbours++;
            if ((i % WIDTH < WIDTH - 1) && (i / WIDTH < HEIGHT - 1) && (_states[i + 1 + WIDTH])) numNeighbours++;
            if ((i % WIDTH > 0) && (i / WIDTH < HEIGHT - 1) && (_states[i - 1 + WIDTH])) numNeighbours++;
            if ((i % WIDTH < WIDTH - 1) && (i / WIDTH > 0) && (_states[i + 1 - WIDTH])) numNeighbours++;
            if ((i % WIDTH > 0) && (i / WIDTH > 0) && (_states[i - 1 - WIDTH])) numNeighbours++;

            if (numNeighbours == 2 && _states[i] || numNeighbours == 3)
            {
                _map.SetTile(position, tile);
            }
        }
    }
}
