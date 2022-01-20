using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CursorScript : MonoBehaviour
{
    private Vector3 mousePosition;
    public float moveSpeed = 0.1f;

    [SerializeField] private Sprite cursorSprite;
    [SerializeField] private Sprite crosshairSprite;
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            image.sprite = cursorSprite;
            mousePosition = Input.mousePosition;
            transform.position = mousePosition;
        } 
        else
        {
            image.sprite = crosshairSprite;
            mousePosition = Input.mousePosition;
            transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
        }
    }
}
