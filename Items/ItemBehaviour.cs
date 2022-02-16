using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //if (collision is CharacterCollision characterCollision)
        //{

        //}

        if (collision.gameObject.name == "BaseCharacter")
        {
            Destroy(this.transform.parent.gameObject);
            Debug.Log("Item collected!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (collision is CharacterCollision characterCollision)
        //{

        //}

        if (other.gameObject.name == "BaseCharacter")
        {
            Destroy(this.transform.parent.gameObject);
            Debug.Log("Item collected!");
        }
    }
}
