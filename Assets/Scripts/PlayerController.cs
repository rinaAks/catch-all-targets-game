using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float xforce;
    float zforce;
    Vector3 playerRot;
    Vector3 cameraRot;
    [SerializeField]float moveSpeed=2;
    [SerializeField]float lookSpeed=2;
    [SerializeField]GameObject cam;
    Rigidbody rb;
    [SerializeField]Vector3 boxSize;
    [SerializeField]float maxDistance;
    [SerializeField]LayerMask layerMask;

    public Vector3 jump;
    public float jumpForce = 2.0f;

    public bool isGrounded;
    public int health;

    public HealthBar healthBar;

    private void OnCollisionEnter(Collision collision)
    {
        // Проверяем, является ли объект пулей
        if (collision.gameObject.CompareTag("BulletTagByEnemy"))
        {
            TakeDamage(10);
        }
    }

    void Start()
    {
        rb=GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
        healthBar.SetMaxHealth(health);
    }

    void OnCollisionStay(){
        isGrounded = true;
    }

    void Update()
    {
        PlayerMovement();
        LookAround();
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
    		rb.AddForce(jump * jumpForce, ForceMode.Impulse);
    		isGrounded = false;
    	}
              
    }

    void LookAround()
    {
        cameraRot=cam.transform.rotation.eulerAngles;   
        cameraRot.x+=-Input.GetAxis("Mouse Y")*lookSpeed;      
        cameraRot.x=Mathf.Clamp((cameraRot.x <= 180) ? cameraRot.x : -(360 - cameraRot.x),-80f,80f); 
        cam.transform.rotation=Quaternion.Euler(cameraRot);  
        playerRot.y=Input.GetAxis("Mouse X")*lookSpeed;       
        transform.Rotate(playerRot);
        
    }

    void PlayerMovement()
    {
        xforce=Input.GetAxis("Horizontal")*moveSpeed;
        zforce=Input.GetAxis("Vertical")*moveSpeed;
        rb.linearVelocity=transform.forward*zforce+transform.right*xforce+transform.up*rb.linearVelocity.y;
    }
     void OnDrawGizmos()
    {
        Gizmos.color=Color.red;
        Gizmos.DrawCube(transform.position-transform.up*maxDistance,boxSize);
    }
    bool GroundCheck()
    {
        // Проверяем, есть ли коллайдер под игроком
        return Physics.BoxCast(transform.position, boxSize, -transform.up, transform.rotation, maxDistance, layerMask);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
        //if (health <= 0) winText.text = "Вы проиграли";
    }
}