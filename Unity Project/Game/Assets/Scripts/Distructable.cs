using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distructable : MonoBehaviour
{
    public GameObject destroyedVersion;
    // Start is called before the first frame update
    private void OnMouseDown()
    {
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
