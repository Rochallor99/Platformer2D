using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeControl : MonoBehaviour
{
   [SerializeField] Text TimeText;
   [SerializeField] float time;
    bool gameActive;
    // Start is called before the first frame update
    void Start()
    {
        TimeText.text = time.ToString();
        gameActive = true;
        //time = int.Parse(TimeText.text);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameActive==true)
        {
        time -= Time.deltaTime;
        TimeText.text =((int)time).ToString();
        }
        
        if (time < 0)
        {
            gameActive = false;
            time = 60;
            GetComponent<PlayerController>().die();
            
        }
    }

}
