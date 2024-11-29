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
            case "King":
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
        foreach (var item in selectlist)
        {
            Debug.Log(item.x + ", " + item.y);
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
        AnimalJanggi._instance.ResetRenderQueue();
        Renderer renderer = AnimalJanggi._instance.GUIBoard[y * 3 + x].transform.GetChild(0).GetComponent<Renderer>();
        if (y == 0) {
            AnimalJanggi._instance.SetRenderQueue(AnimalJanggi._instance.redHighlight, 2000);
            renderer.material = AnimalJanggi._instance.redHighlight;
        }
        else if (y == 3) {
            AnimalJanggi._instance.SetRenderQueue(AnimalJanggi._instance.greenHighlight, 2000);
            renderer.material = AnimalJanggi._instance.greenHighlight;
        }
        else {
            AnimalJanggi._instance.SetRenderQueue(AnimalJanggi._instance.normalHighlight, 2000);
            renderer.material = AnimalJanggi._instance.normalHighlight;
        }
        Debug.Log(renderer.material.name);
    }

    public void PieceMove() {
        Debug.Log("PIECE MOVE");
    }

    public void CatchPiece() {

    }
}
