using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    float Speedx;
    [SerializeField] float speed;
    private Rigidbody2D myBody; 
    private Vector3 DefaultlocalScale;
    public bool onGround;
    [SerializeField] GameObject arrow;
    float CurrentAttackTimer=0.5f;
    float DefultAttackTimer=0.5f;
    bool attack=false;
    private Animator playerAnimator;
    [SerializeField] int arrowNumber;
    [SerializeField] Text arrowValue;
    [SerializeField] AudioClip dieSound;
    [SerializeField] GameObject winPanel, losePanel;

   
    bool DoubleJump;
    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();  
        DefaultlocalScale = transform.localScale;
        //attack = false;
        playerAnimator = GetComponent<Animator>();
        arrowValue.text = arrowNumber.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(Input.GetAxis("Horizontal"));
        Speedx = Input.GetAxis("Horizontal");
        playerAnimator.SetFloat("speed",Mathf.Abs(Speedx));
        myBody.velocity = new Vector2(Speedx * speed, myBody.velocity.y);
        if (Speedx > 0)
        {
            transform.localScale = new Vector3(DefaultlocalScale.x, DefaultlocalScale.y, DefaultlocalScale.z);
        }else if(Speedx<0) {
            transform.localScale = new Vector3(-DefaultlocalScale.x, DefaultlocalScale.y, DefaultlocalScale.z);
        }
        #region jump-space
        if (Input.GetKeyDown(KeyCode.Space)){
        if (onGround == true)
        {
            
            myBody.velocity = new Vector2(myBody.velocity.x,8f);
            DoubleJump = true;
            //playerAnimator.SetTrigger("Jump");
        }else
            {
               if (DoubleJump == true)              
                    myBody.velocity = new Vector2(myBody.velocity.x, 8f);
                    DoubleJump = false;
            }
        }
        #endregion

        #region Arrow
        if (Input.GetMouseButtonDown(0)&& arrowNumber>0&&attack==false)
        {
          
                arrowNumber--;
                arrowValue.text = arrowNumber.ToString();
                Invoke("shoot", 0.2f);// shoot();
               
                playerAnimator.SetTrigger("attack");
            attack = true;
                          
        }
       if(attack==true)
        {
            CurrentAttackTimer -=Time.deltaTime;
        }
        if (CurrentAttackTimer <= 0)
        {
            attack = false;
            CurrentAttackTimer = DefultAttackTimer;
           
        }

        #endregion

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GetComponent<TimeControl>().enabled = false;
            die();
        }
        if(collision.gameObject.name== "BitirmeRozeti")
        {
            /* winPanel.SetActive(true);
             Time.timeScale = 0;*/
            Destroy(collision.gameObject);
            StartCoroutine(Wait(true));
        }
    }
    
    public void die()
    {
        playerAnimator.SetTrigger("die");
        myBody.constraints = RigidbodyConstraints2D.FreezeAll;
        playerAnimator.SetFloat("speed", 0f); 
        GameObject.Find("SoundController").GetComponent<AudioSource>().clip=null;
        GameObject.Find("SoundController").GetComponent<AudioSource>().PlayOneShot(dieSound);
       // enabled = false;// PlayerController scriptini inaktif hale getirir. 
        StartCoroutine(Wait(false));
        // losePanel.SetActive(true);
       // Time.timeScale = 0;
    }
    void shoot()
    {
            GameObject TheArrow = Instantiate(arrow,transform.position,Quaternion.identity);
            TheArrow.transform.parent = GameObject.Find("Arrows").transform;
            if (transform.localScale.x > 0)
            {
                TheArrow.GetComponent<Rigidbody2D>().velocity = new Vector2(12f, 0f);
            }
            else
            {
                TheArrow.GetComponent<Rigidbody2D>().velocity = new Vector2(-12f, 0f);
                // Vector3 ScaleOfArrow = TheArrow.transform.localScale;
                //TheArrow.transform.localScale =new Vector3(-ScaleOfArrow.x,ScaleOfArrow.y,ScaleOfArrow.z);
                TheArrow.transform.localScale =new Vector3(-1f,1,1);
            }
    }

    IEnumerator Wait(bool win)
    {
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 0;
        if (win == true)
        {
            winPanel.SetActive(true);
        }
        else
        {
            losePanel.SetActive(true);
        }
    }


}
