using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private ClippingRect _cameraClippingRect;
    private Tilemap _terrain;
    [SerializeField] private List<Tile> _tiles = new List<Tile>(4);

    [SerializeField] [Range(0.0f, 1.0f)] private float _waterUpperLimit = 0.1f;
    [SerializeField] [Range(0.0f, 1.0f)] private float _sandUpperLimit = 0.15f;
    [SerializeField] [Range(0.0f, 1.0f)] private float _grassUpperLimit = 0.55f;
    [SerializeField] [Range(0.0f, 1.0f)] private float _mountainUpperLimit = 1.0f;

    public AnimationCurve TerrainFunction;

    [SerializeField] [Range(0.0f, 100.0f)] private float _scale1X = 10f;
    [SerializeField] [Range(0.0f, 100.0f)] private float _scale1Y = 10f;
    [SerializeField] [Range(-1000.0f, 1000.0f)] private float _offset1X = 10f;
    [SerializeField] [Range(-1000.0f, 1000.0f)] private float _offset1Y = 10f;

    [SerializeField] [Range(0.0f, 100.0f)] private float _scale2X = 10f;
    [SerializeField] [Range(0.0f, 100.0f)] private float _scale2Y = 10f;
    [SerializeField] [Range(-1000.0f, 1000.0f)] private float _offset2X = 10f;
    [SerializeField] [Range(-1000.0f, 1000.0f)] private float _offset2Y = 10f;

    [SerializeField] [Range(0.0f, 100.0f)] private float _scale3X = 10f;
    [SerializeField] [Range(0.0f, 100.0f)] private float _scale3Y = 10f;
    [SerializeField] [Range(-1000.0f, 1000.0f)] private float _offset3X = 10f;
    [SerializeField] [Range(-1000.0f, 1000.0f)] private float _offset3Y = 10f;

    [SerializeField] [Range(0.0f, 100.0f)] private float _scale4X = 10f;
    [SerializeField] [Range(0.0f, 100.0f)] private float _scale4Y = 10f;
    [SerializeField] [Range(-1000.0f, 1000.0f)] private float _offset4X = 10f;
    [SerializeField] [Range(-1000.0f, 1000.0f)] private float _offset4Y = 10f;

    private struct ClippingRect
    {
        public Vector3 TopLeft;
        public Vector3 BottomRight;
    }

    void Awake()
    {
        _terrain = gameObject.GetComponent<Tilemap>();
    }

    void Start()
    {
        _cameraClippingRect = new ClippingRect();

        TerrainFunction = new AnimationCurve(
            new Keyframe(0.0f, 0.0f, 0.0f, 0.0f), 
            new Keyframe(0.69948394298553469f, 0.09790029376745224f, 0.492849737405777f, 0.492849737405777f), 
            new Keyframe(0.7496794581413269f, 0.19665521383285523f, 0.16247661411762238f, 0.16247661411762238f),
            new Keyframe(0.7893649339675903f, 0.4767066240310669f, 0.14278258383274079f, 0.14278258383274079f),
            new Keyframe(0.8784754276275635f, 0.5452040433883667f, 0.17801955342292787f, 0.17801955342292787f),
            new Keyframe(1.0f, 1.0f, 5.444188594818115f, 5.444188594818115f)
        );
        TerrainFunction.preWrapMode = WrapMode.Clamp;
        TerrainFunction.postWrapMode = WrapMode.Clamp;
        
    }

    void Update()
    {
        //_terrain.ClearAllTiles();
        SetTilesInView();
    }

    void SetTilesInView()
    {
        Vector3Int topRightCellCoord = _terrain.WorldToCell(_camera.ViewportToWorldPoint(new Vector3(1, 1, 0)));
        Vector3Int topLeftCellCoord = _terrain.WorldToCell(_camera.ViewportToWorldPoint(new Vector3(0, 1, 0)));
        Vector3Int bottomLeftCellCoord = _terrain.WorldToCell(_camera.ViewportToWorldPoint(new Vector3(0, 0, 0)));
        Vector3Int bottomRightCellCoord = _terrain.WorldToCell(_camera.ViewportToWorldPoint(new Vector3(1, 0, 0)));

        Vector3Int tilePosition = Vector3Int.zero;
        float xCoord1;
        float yCoord1;
        float xCoord2;
        float yCoord2;
        float xCoord3;
        float yCoord3;
        float xCoord4;
        float yCoord4;
        Tile tile = _tiles[3];
        float tileValue;

        for (int x = bottomLeftCellCoord.x - 1; x < topRightCellCoord.x + 1; x++)
        {
            for (int y = bottomRightCellCoord.y - 1; y < topLeftCellCoord.y + 1; y++)
            {
                tilePosition.x = x;
                tilePosition.y = y;

                if (_terrain.GetTile(tilePosition) != null) continue;

                xCoord1 = ((float) x / (1000)) * _scale1X + _offset1X;
                yCoord1 = ((float) y / (1000)) * _scale1Y + _offset1Y;

                xCoord2 = ((float) x / (1000)) * _scale2X + _offset2X;
                yCoord2 = ((float) y / (1000)) * _scale2Y + _offset2Y;

                xCoord3 = ((float) x / (1000)) * _scale3X + _offset3X;
                yCoord3 = ((float) y / (1000)) * _scale3Y + _offset3Y;

                xCoord4 = ((float) x / (1000)) * _scale4X + _offset4X;
                yCoord4 = ((float) y / (1000)) * _scale4Y + _offset4Y;

                tileValue = 
                    Mathf.PerlinNoise(xCoord1, yCoord1) + 
                    0.5f * (Mathf.PerlinNoise(xCoord2, yCoord2) - 0.5f) +
                    0.05f * (Mathf.PerlinNoise(xCoord3, yCoord3) - 0.5f) +
                    0.005f * (Mathf.PerlinNoise(xCoord4, yCoord4) - 0.5f);
                tileValue = Mathf.Clamp(tileValue, 0f, 1f);
                tileValue = TerrainFunction.Evaluate(tileValue);

                if (tileValue <= _waterUpperLimit) tile = _tiles[0];
                else if (tileValue <= _sandUpperLimit) tile = _tiles[1];
                else if (tileValue <= _grassUpperLimit) tile = _tiles[2];
                else if (tileValue <= _mountainUpperLimit) tile = _tiles[3];

                _terrain.SetTile(tilePosition, tile);
            }
        }
    }

    
}
