using UnityEngine;

public class GunScript : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private GameObject bulletPoint;
    [SerializeField] private float bulletSpeed = 600;
    private bool _shoot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _shoot = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _shoot = true;
        }

        if (_shoot)
        {
            Shoot();
            _shoot = false;
        }    
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletPoint.transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
        Destroy(bullet, 2f);
    }
}
