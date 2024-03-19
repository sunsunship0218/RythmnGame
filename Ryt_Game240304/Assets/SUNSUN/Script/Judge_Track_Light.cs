using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judge_Track_Light : MonoBehaviour
{
    [SerializeField] float speed=0;
    [SerializeField] TrackNumber tracknumber;
    [SerializeField] float alpha = 0;
    private Renderer render;
  

    void Awake()
    {
        render = GetComponent<Renderer>();
    }
    void Update()
    {
        ChangeTrans();
    }

    void ChangeTrans()
    {
        //透明的話,將Alpha值設為不透明
        if (!(render.material.color.a<=0))
        {
            alpha = 1;
            render.material.color = new Color(render.material.color.r, render.material.color.g, render.material.color.b, alpha);
        }
        switch (tracknumber)
        {
            case TrackNumber.First :
                if (Input.GetKey(KeyCode.D))
                {
                    ColorChange();
                }
                break;
            case TrackNumber.Second:
                if (Input.GetKey(KeyCode.F))
                {
                    ColorChange();
                }
                break;
            case TrackNumber.Third:
                if (Input.GetKey(KeyCode.J))
                {
                    ColorChange();
                }
                break;
            case TrackNumber.Forth:
                if (Input.GetKey(KeyCode.K))
                {
                    ColorChange();
                }
                break;
        }
        alpha -= speed * Time.deltaTime;
    }

    void ColorChange()
    {
        alpha = 0.3f;
        //alpha是透明度 0:完全透明 1:不透明
        render.material.color = new Color(render.material.color.r, render.material.color.g, render.material.color.b,alpha);
    }
}

enum TrackNumber
{
    //0
    First,
    //1
    Second,
    //2
    Third,
    //3
    Forth
}