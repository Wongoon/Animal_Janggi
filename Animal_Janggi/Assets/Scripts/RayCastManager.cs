using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.AssemblyQualifiedNameParser;
using UnityEngine;

public class RayCastManager : MonoBehaviour
{
    public Transform pos;

    [SerializeField] LayerMask layerMask;
    int layerNum;
    RaycastHit hit;
    int dist = 10;

    void Start()
    {
        layerNum = LayerMask.NameToLayer("Tile");
    }

    void Update()
    {
        Debug.DrawRay(pos.position, pos.forward * dist, Color.red);

        if (Input.GetMouseButtonDown(0)) {
            Vector3 direction = pos.forward;
            AnimalJanggi._instance.ResetChoice(AnimalJanggi._instance.GUIBoard);
            if (Physics.Raycast(pos.position, direction, out hit, Mathf.Infinity, layerMask)) {
                var (x, y) = NameToInt(hit);
                Debug.Log(x + ", " + y + " / " + AnimalJanggi._instance.CheckTile(x, y));
                if (hit.transform.parent.CompareTag(AnimalJanggi._instance.GetTeam())) {
                    AnimalJanggi._instance.SelectTile(hit, x, y);
                    PieceMoving._instance.CheckBoard(x, y);
                }
            }
            PieceMoving._instance.selectlist.Clear();
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
