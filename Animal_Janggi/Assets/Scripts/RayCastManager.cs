using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.AssemblyQualifiedNameParser;
using UnityEngine;
using static AnimalJanggi;

public class RayCastManager : MonoBehaviour
{
    private AnimalJanggi animalJanggi;
    public Transform pos;

    [SerializeField] LayerMask layerMask;
    int layerNum;
    RaycastHit hit;
    int dist = 10;
    // Start is called before the first frame update1
    void Start()
    {
        animalJanggi = FindObjectOfType<AnimalJanggi>();
        layerNum = LayerMask.NameToLayer("Tile");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(pos.position, pos.forward * dist, Color.red);

        if (Input.GetMouseButtonDown(0)) {
            Vector3 direction = pos.forward;
            animalJanggi.ResetChoice(animalJanggi.GUIBoard);
            if (Physics.Raycast(pos.position, direction, out hit, Mathf.Infinity, layerMask)) {
                var (x, y) = NameToInt(hit);
                Debug.Log(x + ", " + y);
                animalJanggi.SelectTile(hit, x, y);
            }
        }
    }

    public (int, int) NameToInt(RaycastHit hit)
    {
        string xName = hit.transform.parent.name;
        string yName = hit.transform.parent.parent.name;

        int x = int.Parse(xName.Split(" ")[1]) - 1;
        int y = int.Parse(yName.Split(" ")[1]) - 1;

        return (x, y);
    }
}
