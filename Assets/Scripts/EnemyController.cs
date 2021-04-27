using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private bool onGround;
    [SerializeField] LayerMask engel;
    private float width;
    private Rigidbody2D enemyBody;
    private static int EnemyCounter;
    // Start is called before the first frame update
    void Start()
    {
        EnemyCounter++;
       // Debug.Log(gameObject.name + ": " + EnemyCounter);
        width = GetComponent<SpriteRenderer>().bounds.extents.x;
        //Debug.Log("witdh: "+width+" Enemy: "+gameObject.name);
        enemyBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position+transform.right*width/2, Vector2.down, 2f,engel);

        if (hit.collider != null)
        {
            onGround = true;
        }
        else
        {
            onGround = false;
            transform.eulerAngles += new Vector3(0, 180, 0);
        }
        enemyBody.velocity = new Vector2(transform.right.x * 2f,0);

    }

  /*  private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 EnemyRealPosition = transform.position + transform.right * width / 2;
        Gizmos.DrawLine(EnemyRealPosition, EnemyRealPosition + new Vector3(0,-2f,0));

    }*/
}
