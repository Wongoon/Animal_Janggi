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
    private static readonly string[] PIECE = { "King", "Knight", "Rook", "Pawn", "Elephant", "Null" };
    private static readonly string[] TEAM = { "Red", "Green", "Null" };
    public GameObject[] GUIBoard;
    public Material normalChoice;
    public Material normalNone;
    public Material redChoice;
    public Material redNone;
    public Material greenChoice;
    public Material greenNone;
    public Material normalHighlight;
    public Material redHighlight;
    public Material greenHighlight;
    private string[][][] board;
    private bool selected;
    private string team = "Red";

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
                new string[] { PIECE[1], TEAM[0] },
                new string[] { PIECE[0], TEAM[0] },
                new string[] { PIECE[2], TEAM[0] }
            },
            new string[][] {
                new string[] { PIECE[5], TEAM[2] },
                new string[] { PIECE[3], TEAM[0] },
                new string[] { PIECE[5], TEAM[2] }
            },
            new string[][] {
                new string[] { PIECE[5], TEAM[2] },
                new string[] { PIECE[3], TEAM[1] },
                new string[] { PIECE[5], TEAM[2] }
            },
            new string[][] {
                new string[] { PIECE[2], TEAM[1] },
                new string[] { PIECE[0], TEAM[1] },
                new string[] { PIECE[1], TEAM[1] }
            }
        };
        selected = false;
        ResetChoice(GUIBoard);
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

    public void ResetChoice(GameObject[] boards)
    {
        Renderer renderer = new();
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

                renderer = cubeTransform.GetComponent<Renderer>();

                if (i == 0)
                {
                    renderer.material = greenNone;
                }
                else if (i == 3)
                {
                    renderer.material = redNone;
                }
                else
                {
                    renderer.material = normalNone;
                }
            }
        }
    }

    public void SelectTile(RaycastHit hit, int x, int y)
    {
        Renderer renderer = hit.collider.GetComponent<Renderer>();
        if (hit.transform.parent.CompareTag(team))
        {
            if (y == 0 && team == "Red")
            {
                renderer.material = redChoice;
            }
            else if (y == 3 && team == "Green")
            {
                renderer.material = greenChoice;
            }
            else
            {
                renderer.material = normalChoice;
            }

        }
    }
}