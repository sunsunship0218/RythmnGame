using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    [SerializeField] float NoteSpeed=0;
    //Note List
    void Awake()
    {
     
    }
    void Update()
    {
        NotesToPad();
    }
    //Notes Move forward to pad
    void NotesToPad()
    {
        transform.position -= transform.forward*Time.deltaTime*NoteSpeed;
    }
}
