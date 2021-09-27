using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Links")]
    [SerializeField] private new Camera camera;
    [SerializeField] private GameObject gameController;
    [SerializeField] private Animator Animator;


    [Header("TechnicalSpecifications")]
    private bool ispush;
    private float boundaries = 14;
    public int sphere;
    private Vector3 mousePosition;
    private Vector3 startPosition = new Vector3(0, 1.85f, 0);
    private Vector3 normalScale = new Vector3(2, 2, 2);
    private float moveSpeed_x = 80;
    private float minimumPushingSpeed = 15;
    private float accelerationPush = 30f;

    [Header("MoveSetting")]
    public Rigidbody rigid;
    public float maxSpeed;
    public float acceleration;
    private float internalAcceleration;
    public float speed;
    private float decelerationRate = 150;
    private float move_x;
    private const float move_y = -1f;
    private const float move_z = 1;
    public float position_x;
    public float position_y;



    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        internalAcceleration = acceleration;
    }


    void Update()
    {
        if (tag == "Player")
        {
            Сontrol();
            Push();
        }
        Animations();


    }

    void Сontrol()
    {

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -boundaries, boundaries), transform.position.y, transform.position.z);
        if (ispush == false)
        {
            speed += internalAcceleration * Time.deltaTime;
            speed = Mathf.Clamp(speed, 10, maxSpeed);
        }


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            position_x = mousePosition.x;
            position_y = mousePosition.y;
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector3 actual_mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            move_x = -(position_x - actual_mousePosition.x) / Screen.width;
            if (ispush == false)
            {
                speed -= (position_y - actual_mousePosition.y) / Screen.height * decelerationRate * Time.deltaTime;
            }
        }
        else
        {
            move_x = 0;
        }
        Vector3 moveVector = new Vector3(move_x * moveSpeed_x, move_y, move_z * speed);
        rigid.velocity = moveVector;


    }
    public void Death()
    {
        tag = "Death";
        Animator.SetBool("Death", true);
        Delay(0.1f);
        speed = 0;
        rigid.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
    }

    public void Win()
    {
        Animator.SetBool("Win", true);
        tag = "Death";
        speed = 0;
        rigid.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
    }

    public void Alive()
    {
        tag = "Player";
        rigid.constraints = RigidbodyConstraints.FreezeRotation;
    }

    IEnumerator Delay(float time)
    {
        yield return new WaitForSeconds(time);
    }
    




    void OnTriggerEnter(Collider other)
    {

        switch (other.tag)
        {
            case "Sphere":
                IncreaseInSize();
                break;

            case "ObstacleZone":
                int decrease = other.gameObject.GetComponent<Obstacle>().decrease;
                speed = -Mathf.Clamp(speed, minimumPushingSpeed, Mathf.Infinity);
                internalAcceleration = accelerationPush;
                ispush = true;              
                CollisionControl(decrease);
                break;

            case "Wall":
                gameController.GetComponent<GameController>().CollisionWall();
                sphere = 0;
                break;

            case "WinZone":
                gameController.GetComponent<GameController>().LevelComplete();
                sphere = 0;
                break;
        }

    }

    public void FalseWinAnivation()
    {
        Animator.SetBool("Win", false);
    }

    public void FalseDeathAnivation()
    {
        Animator.SetBool("Death", false);
    }
   
    void Animations()
    {
        Animator.SetFloat("Speed", speed);
        if (speed > 2)
        {
            Animator.SetBool("Collision", false);
        }
        else if(speed < -2)
        {
            Animator.SetBool("Collision", true);         
        }
    }

    public void ReloadPlayer()
    {
        transform.position = startPosition;
        transform.localScale = normalScale;
    }


    void Push()
    {
        if (ispush == true)
        {
            speed += internalAcceleration * Time.deltaTime;
            if (speed >= 0)
            {
                internalAcceleration = acceleration;
                ispush = false;
            }
        }
    }

    void IncreaseInSize()
    {
        transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
        camera.GetComponent<CameraMove>().Distancing();
        sphere += 1;
    }

    void CollisionControl(int decrease)
    {
        if (decrease > 0)
        {
            for (int i = 0; i < decrease; i++)
            {
                if (transform.localScale.x > 2 && transform.localScale.y > 2 && transform.localScale.z > 2)
                {
                    transform.localScale += new Vector3(-0.1f, -0.1f, -0.1f);
                    camera.GetComponent<CameraMove>().CameraZoom();
                }
            }
            if (sphere > 0)
            {
                sphere -= decrease;
            }
        }
    }


}



