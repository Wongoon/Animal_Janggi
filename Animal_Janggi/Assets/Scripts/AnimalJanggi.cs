using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class AnimalJanggi : MonoBehaviour
{
    public static AnimalJanggi _instance;
    public static readonly string[] PIECE = { "RedKing", "GreenKing", "Knight", "Rook", "Pawn", "Elephant", "Null" };
    public static readonly string[] TEAM = { "Red", "Green", "Null" };
    public Sprite[] sprites;
    public GameObject[] GUIBoard;
    public string[][][] board;
    private bool selected;
    private string team = "Red";
    public Sprite[] noneTile;
    public Sprite[] choiceTile;
    public Sprite[] selectableTile;

    void Awake() {
        if (_instance == null) {
            _instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        board = new string[][][] {
            new string[][] {
                new string[] { PIECE[2], TEAM[0] },
                new string[] { PIECE[0], TEAM[0] },
                new string[] { PIECE[3], TEAM[0] }
            },
            new string[][] {
                new string[] { PIECE[6], TEAM[2] },
                new string[] { PIECE[4], TEAM[0] },
                new string[] { PIECE[6], TEAM[2] }
            },
            new string[][] {
                new string[] { PIECE[6], TEAM[2] },
                new string[] { PIECE[4], TEAM[1] },
                new string[] { PIECE[6], TEAM[2] }
            },
            new string[][] {
                new string[] { PIECE[3], TEAM[1] },
                new string[] { PIECE[1], TEAM[1] },
                new string[] { PIECE[2], TEAM[1] }
            }
        };
        selected = false;
        ResetChoice(GUIBoard);
    }

    public void ResetFlip() {
        foreach (var t in GUIBoard)
        {
            if (t.CompareTag("Green"))
            {
                t.transform.GetChild(1).GetComponent<SpriteRenderer>().flipX = true;
                t.transform.GetChild(1).GetComponent<SpriteRenderer>().flipY = true;
            }
            else if (t.CompareTag("Red"))
            {
                t.transform.GetChild(1).GetComponent<SpriteRenderer>().flipX = false;
                t.transform.GetChild(1).GetComponent<SpriteRenderer>().flipY = false;
            }
        }
    }

    public string CheckTile(int x, int y)
    {
        return board[y][x][0];
    }

    public string CheckTeam(int x, int y) {
        return board[y][x][1];
    }

    public string GetTeam()
    {
        return team;
    }

    public void ChangeTeam()
    {
        switch (team)
        {
            case "Red":
                team = "Green";
                break;
            case "Green":
                team = "Red";
                break;
        }
    }

    public bool GetSelected() {
        return selected;
    }

    public void SetSelected() {
        selected = !selected;
    }

    public void ResetChoice(GameObject[] boards)
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                int index = i * 3 + j;
                if (boards[index] == null)
                {
                    Debug.Log("null");
                    continue;
                }
                Transform cubeTransform = boards[index].transform.GetChild(0);

                SpriteRenderer renderer = cubeTransform.GetChild(0).GetComponent<SpriteRenderer>();
                if (i == 3)
                {
                    renderer.sprite = noneTile[2];
                }
                else if (i == 0)
                {
                    renderer.sprite = noneTile[1];
                }
                else
                {
                    renderer.sprite = noneTile[0];
                }
            }
        }
        ResetTile();
    }

    public void SelectTile(RaycastHit hit, int x, int y)
    {
        SpriteRenderer renderer = hit.transform.GetChild(0).GetComponent<SpriteRenderer>();
        if (hit.transform.parent.CompareTag(team))
        {
            if (y == 0 && team == "Red")
            {
                renderer.sprite = choiceTile[1];
            }
            else if (y == 3 && team == "Green")
            {
                renderer.sprite = choiceTile[2];
            }
            else
            {
                renderer.sprite = choiceTile[0];
            }
        }
    }

    public void ResetTile() {
        for (int i = 0; i < GUIBoard.Length; i++) {
            int x = i % 3;
            int y = i / 3;
            SpriteRenderer spriteRenderer = GUIBoard[i].transform.GetChild(1).GetComponent<SpriteRenderer>();

            if (CheckTile(x, y).Equals(PIECE[0])) {
                spriteRenderer.sprite = sprites[0];
            }
            else if (CheckTile(x, y).Equals(PIECE[1])) {
                spriteRenderer.sprite = sprites[1];
            }
            else if (CheckTile(x, y).Equals(PIECE[2])) {
                spriteRenderer.sprite = sprites[2];
            }
            else if (CheckTile(x, y).Equals(PIECE[3])) {
                spriteRenderer.sprite = sprites[3];
            }
            else if (CheckTile(x, y).Equals(PIECE[4])) {
                spriteRenderer.sprite = sprites[4];
            }
            else if (CheckTile(x, y).Equals(PIECE[5])) {
                spriteRenderer.sprite = sprites[5];
            }
            else if (CheckTile(x, y).Equals(PIECE[6])) {
                spriteRenderer.sprite = sprites[6];
            }
        }
        ResetFlip();
    }

    public void ResetTag() {
        for (int i = 0; i < GUIBoard.Length; i++) {
            int x = i % 3;
            int y = i / 3;

            string team = CheckTeam(x, y);

            board[y][x][1] = team;
            if (team == "Null") {
                GUIBoard[i].tag = "Untagged";
                continue;
            }
            GUIBoard[i].tag = team;
        }
    }
}