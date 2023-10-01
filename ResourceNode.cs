using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ResourceNode : MonoBehaviour
{
    public Sprite sprite;
    public SpriteRenderer spriteRenderer;
    float speed = 1f;
    float time = 1f;
    float startTime=0f;
    public GameObject floater;

    private Vector3 pos;
    public bool isNegative;
    public int prefabType;

    void Start()
    {
        pos = floater.transform.position;
        floater.SetActive(false);
    }


    void Update()
    {
        if (floater.activeSelf)
        {

            if (Time.time < startTime + time) 
            {

                floater.transform.position += new Vector3(0, speed * Time.deltaTime, 0);
            }
            else if (Time.time > startTime + time) 
            {
                floater.transform.position = pos;
                floater.SetActive(false);            
            }
        }
    }

    public void OnTick()
    {
        floater.transform.position = pos;
        startTime = Time.time;
        floater.SetActive(true);
        spriteRenderer.sprite = sprite;
        if(isNegative)
        {
            spriteRenderer.color = Color.red;
        }
    }
}
