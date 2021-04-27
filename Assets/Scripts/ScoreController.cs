using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    [SerializeField] Text scoreValue;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            AddScore(50);
            Destroy(gameObject);
        }
    }
    public void AddScore(int score)
    {
        int scoreValueInt = int.Parse(scoreValue.text);
        scoreValueInt += score;
        scoreValue.text = scoreValueInt.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f,2f,0f));
    }
}
