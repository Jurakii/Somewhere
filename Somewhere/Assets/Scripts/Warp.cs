using UnityEngine;

public class Warp : MonoBehaviour
{
    [SerializeField] private GameObject destination;
    public bool locked;


    public Transform GetDestination()
    {
        return destination.GetComponent<Transform>();
    }
}