using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TurretPlacer : MonoBehaviour
{
    [SerializeField] private List<GameObject> _turretPrefabs;
    [SerializeField] private List<GameObject> _turretPreviewPrefabs;
    private List<GameObject> _turretPreviewObjects;
    private List<Transform> _placedTurretPositions;


    [SerializeField] private Camera _camera;

    private int _selectedTurretIndex = -1;
    private GameObject _selectedTurretPreviewObject;
    private Material _selectedTurretPreviewMaterial;

    [SerializeField] private Color _allowPlacementColor = new Color(0f, 1f, 0f, 0.4f);
    [SerializeField] private Color _restrictPlacementColor = new Color(1f, 0f, 0f, 0.4f);

    private GraphicRaycaster _raycaster;
    private PointerEventData _pointerEventData;
    private EventSystem _eventSystem;

    private void Start()
    {
        if (_camera == null) _camera = Camera.main;
        _turretPreviewObjects = new List<GameObject>(_turretPreviewPrefabs.Count);
        foreach (GameObject _turretPreviewPrefab in _turretPreviewPrefabs)
        {
            _turretPreviewObjects.Add(GameObject.Instantiate(_turretPreviewPrefab, transform));
        }
        foreach (GameObject _turretPreviewObject in _turretPreviewObjects)
        {
            _turretPreviewObject.SetActive(false);
        }
        _placedTurretPositions = new List<Transform>(100);

        //Fetch the Raycaster from the GameObject (the Canvas)
        _raycaster = GameObject.Find("Canvas").GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        _eventSystem = GetComponent<EventSystem>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (_selectedTurretIndex >= 0)
        {
            //Set up the new Pointer Event
            _pointerEventData = new PointerEventData(_eventSystem);
            //Set the Pointer Event Position to that of the mouse position
            _pointerEventData.position = Input.mousePosition;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            _raycaster.Raycast(_pointerEventData, results);
            if (results.Count > 0) return;

            _selectedTurretPreviewMaterial.color = _allowPlacementColor;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 gridPosition = Vector3.zero;
                gridPosition.x = Mathf.RoundToInt(hit.point.x);
                gridPosition.y = hit.point.y;
                gridPosition.z = Mathf.RoundToInt(hit.point.z);

                Transform turretAtThatPosition = _placedTurretPositions.Find(x => x.position == gridPosition);
                _selectedTurretPreviewObject.transform.position = gridPosition;
                if (!hit.collider.tag.Equals("TurretPlacementArea"))
                {
                    _selectedTurretPreviewMaterial.color = _restrictPlacementColor;
                }
                else if (Input.GetMouseButton(0) && turretAtThatPosition == null)
                {
                    _placedTurretPositions.Add(GameObject.Instantiate(_turretPrefabs[_selectedTurretIndex], gridPosition, Quaternion.identity).transform);
                    //_selectedTurretIndex = -1;
                    //_selectedTurretPreviewObject.SetActive(false);
                }
                else if (turretAtThatPosition != null)
                {
                    _selectedTurretPreviewMaterial.color = _restrictPlacementColor;
                    if (Input.GetMouseButton(1))
                    {
                        _placedTurretPositions.Remove(turretAtThatPosition);
                        Destroy(turretAtThatPosition.gameObject);
                    }
                }
            }
        }
    }

    public void SelectTurret(int index)
    {
        if (index < 0) return;
        if (index >= _turretPreviewPrefabs.Count) return;
        if (index == _selectedTurretIndex)
        {
            _selectedTurretIndex = -1;
            _selectedTurretPreviewObject.SetActive(false);
            return;
        }
        else
        {
            _selectedTurretIndex = index;
            _selectedTurretPreviewObject = _turretPreviewObjects[_selectedTurretIndex];
            _selectedTurretPreviewMaterial = _selectedTurretPreviewObject.transform.GetChild(2).GetComponent<MeshRenderer>().material;
            _selectedTurretPreviewObject.SetActive(true);
        }
    }

}
