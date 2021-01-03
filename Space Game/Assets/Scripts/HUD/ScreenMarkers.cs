using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenMarkers : MonoBehaviour
{

    [SerializeField] private List<GameObject> enemies;
    //[SerializeField] private float detectionRadius;
    [SerializeField] private Camera cam;
    [SerializeField] private Texture marker;

    private void OnGUI()
    {
        foreach (GameObject enemy in enemies)
        {
            Vector3 markerPosition = cam.WorldToScreenPoint(enemy.transform.position);

            markerPosition.y = Screen.height - markerPosition.y;

            if (markerPosition.z < 0)
            {
                if (markerPosition.x < Screen.width/2)
                {
                    markerPosition.x = Screen.width;
                }
                else
                {
                    markerPosition.x = 0;
                }
            }

            markerPosition.x = Mathf.Clamp(markerPosition.x, 15, Screen.width - 10);
            markerPosition.y = Mathf.Clamp(markerPosition.y, 15, Screen.height - 15);

            GUI.Label( new Rect(markerPosition.x - 15, markerPosition.y - 15, markerPosition.x + 15, 30), marker);
        }
    }



}
