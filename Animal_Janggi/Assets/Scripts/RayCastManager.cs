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
    [SerializeField] LayerMask catchLayerMask;
    int layerNum, catchLayerNum;
    RaycastHit hit;

    int prevX, prevY;

    void Start()
    {
        layerNum = LayerMask.NameToLayer("Tile");
        catchLayerNum = LayerMask.NameToLayer("Catch");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !CanvasManager.canvasRender) {
            Vector3 direction = pos.forward;
            if (Physics.Raycast(pos.position, direction, out hit, Mathf.Infinity, layerMask)) {
                var (x, y) = NameToInt(hit);
                if (hit.transform.parent.CompareTag(AnimalJanggi._instance.GetTeam()) && !AnimalJanggi._instance.GetSelected()) {
                    PieceMoving._instance.CheckBoard(x, y);
                    AnimalJanggi._instance.SelectTile(hit, x, y);
                    AnimalJanggi._instance.SetSelected();
                    prevX = x;
                    prevY = y;
                }
                else if (AnimalJanggi._instance.GetSelected()) {
                    Sprite normalHighlight = AnimalJanggi._instance.selectableTile[0];
                    Sprite redHighlight = AnimalJanggi._instance.selectableTile[1];
                    Sprite greenHighlight = AnimalJanggi._instance.selectableTile[2];
                    string hitSprite = hit.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name;
                    if (hitSprite == normalHighlight.name || hitSprite == redHighlight.name || hitSprite == greenHighlight.name) {
                        PieceMoving._instance.PieceMove(prevX, prevY, x, y);
                        AnimalJanggi._instance.ResetChoice(AnimalJanggi._instance.GUIBoard);
                        AnimalJanggi._instance.ChangeTeam();
                        CameraManager._instance.CameraRotation();
                    }
                    else {
                        AnimalJanggi._instance.ResetChoice(AnimalJanggi._instance.GUIBoard);
                    }
                    AnimalJanggi._instance.SetSelected();
                }
            }
            else if (Physics.Raycast(pos.position, direction, out hit, Mathf.Infinity, catchLayerMask)) {
                // 클릭 시 선택할 수 있는 위치 출력 후 selected에서 그 위치로 갈 수 있도록 한 다음 이 오브젝트.SetActive(false) 하기
            }
            else {
                AnimalJanggi._instance.ResetChoice(AnimalJanggi._instance.GUIBoard);
                if (AnimalJanggi._instance.GetSelected()) {
                    AnimalJanggi._instance.SetSelected();
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
