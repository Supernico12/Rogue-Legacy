using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] float interactRadious = 1f;
    [HideInInspector]
    public GameObject player;


    void Start()
    {
        player = PlayerManager.instance.player;
    }


    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance < interactRadious)
        {
            if (Input.GetButtonDown("Interact"))
                Interact();
        }
    }

    public virtual void Interact()
    {
        Debug.Log("Interacted with:" + gameObject.name);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRadious);
    }

    public static Vector2 ThrowForce(float force = 15f)
    {
        Vector2 throwForce = Vector2.zero;
        throwForce.x = Random.Range(-1f, 1f);
        throwForce.y = .5f;
        throwForce *= force;
        return throwForce;
    }
}
