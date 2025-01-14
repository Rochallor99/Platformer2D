﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform playerTransform;
    [SerializeField] float minX=16, maxX=48;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Player").transform; 
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(playerTransform.position.x,minX,maxX), transform.position.y, transform.position.z);
    }
}
