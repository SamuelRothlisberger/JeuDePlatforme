using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carlosMovement : MonoBehaviour
{
    public GameObject BulletPrefad;
    public GameObject john;
    private float LastShoot;
    private int Health = 2;

    void Start()
    {
    }

    private void Update()
    {
        if (john == null) return;

        Vector3 direction = john.transform.position - transform.position;
        if (direction.x >= 0.0f) transform.localScale = new Vector3(0.05794363f, 0.05857131f, 1.0f);
        else transform.localScale = new Vector3(-0.05794363f, 0.05857131f, 1.0f);

        float distance = Mathf.Abs(john.transform.position.x - transform.position.x);

        if (distance < 1.0f && Time.time > LastShoot + 0.35f)
        {
            Shoot();
            LastShoot = Time.time;
        }


    }

    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 0.05794363f) direction = Vector3.right;
        else direction = Vector3.left;
        GameObject bullet = Instantiate(BulletPrefad, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    public void Hit()
    {
        Health = Health - 1;
        if (Health == 0)
        {
            Invoke("KillCarlos", 0.5f);
        }
    }

    public void KillCarlos()
    {
        Destroy(gameObject);
    }

}

