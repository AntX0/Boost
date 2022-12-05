using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Nothing happens");
                break;
            case "Finish":
                Debug.Log("Concgrats! You've reached the FINISH!!!");
                    break;
            default:
                Debug.Log("You took damage!");
                break;
        }
    }
}
