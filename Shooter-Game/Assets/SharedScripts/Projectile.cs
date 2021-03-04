using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float timeToLive;
    [SerializeField] float damage;

    void Start()
    {
        Destroy(gameObject, timeToLive);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime); 
    }

    void OnTriggerEnter(Collider other)
    {
        var destructable = other.transform.GetComponent<Destructable>(); //calls destructable to know how much damage will be taken from projectile
        if (destructable == null)
            return;

        destructable.TakeDamage(damage);

        PlayerHealthBar.health -= damage; //might need to be changed or moved to make sure health bar works

    }
}
