using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;

    [SerializeField]
    private float baseShootTimeout = .3f;

    [SerializeField]
    private float currentShootTimeout;
    private float shootTimeout;

    private void Start()
    {
        shootTimeout = baseShootTimeout;
        currentShootTimeout = baseShootTimeout;
    }

    void Update()
    {
        Shoot();
    }

    public void IncreaseShootSpeed()
    {
        currentShootTimeout = Mathf.Clamp(currentShootTimeout - 0.05f, 0.1f, .3f);
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.timeScale > 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<BulletBehaviour>().Speed *= transform.localScale.y;
        }

        if (Input.GetKey(KeyCode.Space) && Time.timeScale > 0)
        {
            shootTimeout -= Time.deltaTime;
            if (shootTimeout > 0)
            {
                return;
            }
            shootTimeout = currentShootTimeout;
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<BulletBehaviour>().Speed *= transform.localScale.y;
        }
    }
}
