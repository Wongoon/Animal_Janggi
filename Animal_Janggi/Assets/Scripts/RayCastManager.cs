using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

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
        Debug.DrawRay(pos.position, pos.forward * 10, Color.red);
        if (Input.GetMouseButtonDown(0) && !CanvasManager.canvasRender) {
            Debug.Log(AnimalJanggi._instance.GetTeam());
            Vector3 direction = pos.forward;
            // 메인 보드 클릭했을 때
            if (Physics.Raycast(pos.position, direction, out hit, Mathf.Infinity, layerMask)) {
                var (x, y) = NameToInt(hit);
                // 잡힌 타일을 클릭한 후 메인 보드를 클릭했다면
                if (CatchTile._instance.GetSelected() && CatchTile._instance.GetSelectable(hit)) {
                    PieceMoving._instance.CatchTileSelect(CatchTile._instance.index, x, y);
                    AnimalJanggi._instance.ResetChoice(AnimalJanggi._instance.GUIBoard);
                    AnimalJanggi._instance.ChangeTeam();
                    CameraManager._instance.CameraRotation();
                    CatchTile._instance.SetSelected();
                    return;
                }
                // 메인 보드 본인 기물 처음 선택했을 때
                if (hit.transform.parent.CompareTag(AnimalJanggi._instance.GetTeam()) && !AnimalJanggi._instance.GetSelected()) {
                    if (CatchTile._instance.GetSelected()) {
                        CatchTile._instance.SetSelected();
                        AnimalJanggi._instance.ResetChoice(AnimalJanggi._instance.GUIBoard);
                    }
                    PieceMoving._instance.CheckBoard(x, y);
                    AnimalJanggi._instance.SelectTile(hit, x, y);
                    AnimalJanggi._instance.SetSelected();
                    prevX = x;
                    prevY = y;
                }
                // 선택한 기물이 이동할 수 있는 곳을 클릭했을 때
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
            // 잡힌 타일을 클릭했을 때
            else if (Physics.Raycast(pos.position, direction, out hit, Mathf.Infinity, catchLayerMask)) {
                if (AnimalJanggi._instance.GetSelected()) {
                    AnimalJanggi._instance.ResetChoice(AnimalJanggi._instance.GUIBoard);
                    AnimalJanggi._instance.SetSelected();
                }
                if (hit.collider.CompareTag(AnimalJanggi._instance.GetTeam())) {
                    PieceMoving._instance.CatchTileToBoard(hit);
                }
            }
            // 제대로 된 곳을 클릭하지 않았다면
            else {
                AnimalJanggi._instance.ResetChoice(AnimalJanggi._instance.GUIBoard);
                if (AnimalJanggi._instance.GetSelected()) {
                    AnimalJanggi._instance.SetSelected();
                }
                if (CatchTile._instance.GetSelected()) {
                    CatchTile._instance.SetSelected();
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
