﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField] GameObject effect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!(collision.gameObject.name == "Player"))
        {
           Destroy(gameObject);
            if (collision.gameObject.CompareTag("Enemy"))
            {
                GameObject.Find("BonusRozeti").GetComponent<ScoreController>().AddScore(100);
                Instantiate(effect, collision.gameObject.transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
            }
        }

    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
