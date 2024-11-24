using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastManager : MonoBehaviour
{
    public Transform pos;

    [SerializeField] LayerMask layerMask;
    int layerNum;
    RaycastHit hit;
    int dist = 50;
    // Start is called before the first frame update1
    void Start()
    {
        layerNum = LayerMask.NameToLayer("Tile");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(pos.position, pos.forward * 10, Color.red);

        if (Input.GetMouseButtonDown(0)) {
            Vector3 direction = pos.forward;

            if (Physics.Raycast(pos.position, direction, out hit, Mathf.Infinity, layerMask)) {
                Debug.Log(hit.collider.name + ", " + hit.collider.tag);
            }
        }
    }
}
