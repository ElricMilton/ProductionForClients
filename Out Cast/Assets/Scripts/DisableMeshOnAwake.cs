using UnityEngine;

public class DisableMeshOnAwake : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }
}
