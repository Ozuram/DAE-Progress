using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PlayerCharacter")
        {
            Destroy(gameObject);
        }
    }
}
