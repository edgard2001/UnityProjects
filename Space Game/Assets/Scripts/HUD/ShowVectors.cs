using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShowVectors : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Text rightText, upText, frontText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rightText.text = "Right " + playerTransform.right.x.ToString() + " " + playerTransform.right.y.ToString() + " " + playerTransform.right.z.ToString();
        upText.text = "Up " + playerTransform.up.x.ToString() + " " + playerTransform.up.y.ToString() + " " + playerTransform.up.z.ToString();
        frontText.text = "Front " + playerTransform.forward.x.ToString() + " " + playerTransform.forward.y.ToString() + " " + playerTransform.forward.z.ToString();
    }
}
