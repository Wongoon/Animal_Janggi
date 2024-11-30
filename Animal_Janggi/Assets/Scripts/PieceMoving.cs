using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Rendering;

public class PieceMoving : MonoBehaviour
{
    public static PieceMoving _instance;
    public bool[][] selectBoard = new bool[][] {
        new bool[] { false, false, false },
        new bool[] { false, false, false },
        new bool[] { false, false, false },
        new bool[] { false, false, false }
    };
    private int prevX, prevY, nowX, nowY;
    public List<(int x, int y)> selectlist = new() { };

    public GameObject redCatchPosition;
    public GameObject greenCatchPosition;

    void Awake() {
        if (_instance == null) {
            _instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    public void CheckBoard(int x, int y) {
        string piece = AnimalJanggi._instance.CheckTile(x, y);
        string team = AnimalJanggi._instance.GetTeam();

        switch (piece) {
            case "RedKing":
                CheckKing(team, x, y);
                break;
            case "GreenKing":
                CheckKing(team, x, y);
                break;
            case "Knight":
                CheckKnight(team, x, y);
                break;
            case "Rook":
                CheckRook(team, x, y);
                break;
            case "Pawn":
                CheckPawn(team, x, y);
                break;
            case "Elephant":
                CheckElephant(team, x, y);
                break;
            default:
                break;
        }
        foreach (var item in selectlist) {
            SetSelected(item.x, item.y);
        }
    }

    public bool IsInBounds(int x, int y) {
        return x >= 0 && x < 3 && y >= 0 && y < 4;
    }

    public void CheckKing(string team, int x, int y) {
        int[] dx = {-1, 0, 1, 1, 1, 0, -1, -1};
        int[] dy = {-1, -1, -1, 0, 1, 1, 1, 0};

        for (int i = 0; i < dx.Length; i++) {
            int nx = x + dx[i];
            int ny = y + dy[i];
            if (IsInBounds(nx, ny) && !AnimalJanggi._instance.CheckTeam(nx, ny).Equals(team)) {
                selectlist.Add((nx, ny));
            }
        }
    }

    public void CheckKnight(string team, int x, int y) {
        int[] dx = {-1, 1, 1, -1};
        int[] dy = {-1, -1, 1, 1};

        for (int i = 0; i < dx.Length; i++) {
            int nx = x + dx[i];
            int ny = y + dy[i];
            if (IsInBounds(nx, ny) && !AnimalJanggi._instance.CheckTeam(nx, ny).Equals(team)) {
                selectlist.Add((nx, ny));
            }
        }
    }

    public void CheckRook(string team, int x, int y) {
        int[] dx = { 0, 1, 0, -1 };
        int[] dy = { -1, 0, 1, 0};

        for (int i = 0; i < dx.Length; i++) {
            int nx = x + dx[i];
            int ny = y + dy[i];
            if (IsInBounds(nx, ny) && !AnimalJanggi._instance.CheckTeam(nx, ny).Equals(team)) {
                selectlist.Add((nx, ny));
            }
        }
    }

    public void CheckPawn(string team, int x, int y) {
        int ny;
        if (team.Equals("Red")) {
            ny = y + 1;
        }
        else {
            ny = y - 1;
        }

        if (IsInBounds(x, ny) && !AnimalJanggi._instance.CheckTeam(x, ny).Equals(team)) {
            selectlist.Add((x, ny));
        }
    }

    public void CheckElephant(string team, int x, int y) {
        int[] dx = { 0, 1, 1, 0, -1, -1 };
        int[] dy = { -1, 0, 1, 1, 1, 0, };

        if (team.Equals("Green")) {
            for (int i = 0; i < dx.Length; i++) {
                dx[i] = -dx[i];
                dy[i] = -dy[i];
            }
        }

        for (int i = 0; i < dx.Length; i++) {
            int nx = x + dx[i];
            int ny = y + dy[i];
            if (IsInBounds(x, ny) && !AnimalJanggi._instance.CheckTeam(nx, ny).Equals(team)) {
                selectlist.Add((nx, ny));
            }
        }
    }

    public void SetSelected(int x, int y) {
        SpriteRenderer renderer = AnimalJanggi._instance.GUIBoard[y * 3 + x].transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
        if (y == 0) {
            renderer.sprite = AnimalJanggi._instance.selectableTile[1];
        }
        else if (y == 3) {
            renderer.sprite = AnimalJanggi._instance.selectableTile[2];
        }
        else {
            renderer.sprite = AnimalJanggi._instance.selectableTile[0];
        }
    }

    public void PieceMove(int prevX, int prevY, int nowX, int nowY) {
        string team = AnimalJanggi._instance.GetTeam();

        string name = AnimalJanggi._instance.GUIBoard[prevY * 3 + prevX].transform.GetChild(1).GetComponent<SpriteRenderer>().sprite.name;
        
        if (AnimalJanggi._instance.board[nowY][nowX][0] != "Null") {
            Catch(nowX, nowY);
        }
        AnimalJanggi._instance.board[prevY][prevX][0] = AnimalJanggi.PIECE[6];
        AnimalJanggi._instance.board[prevY][prevX][1] = AnimalJanggi.TEAM[2];
        AnimalJanggi._instance.board[nowY][nowX][0] = name;
        AnimalJanggi._instance.board[nowY][nowX][1] = team;
        if (AnimalJanggi._instance.board[nowY][nowX][0] == "Pawn" && (nowY == 3 || nowY == 0)) {
            AnimalJanggi._instance.board[nowY][nowX][0] = AnimalJanggi.PIECE[5];
        }
        
        AnimalJanggi._instance.ResetTile();
        AnimalJanggi._instance.ResetTag();
    }

    public void Catch(int x, int y) {
        Sprite sprite = AnimalJanggi._instance.GUIBoard[y * 3 + x].transform.GetChild(1).GetComponent<SpriteRenderer>().sprite;
        Debug.Log("Catch " + sprite.name);
        CatchTile._instance.CatchPiece(sprite, AnimalJanggi._instance.GetTeam());
    }

    public void CatchTileToBoard(RaycastHit hit) {
        string name = hit.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name;
        Debug.Log(name);
        CheckSelectable();
    }

    public void CheckSelectable() {
        // 이미 잡힌 타일을 클릭하면 어디에 둘 수 있는지에 대한 내용
    }
}
