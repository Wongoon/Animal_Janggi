using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CatchTile : MonoBehaviour
{
    public static CatchTile _instance;
    public GameObject[] redCatchObjects;
    public GameObject[] greenCatchObjects;

    public List<Sprite> redCatchTiles = new() { };
    public List<Sprite> greenCatchTiles = new() { };

    public bool catchSelected;

    public int index;

    void Awake() {
        if (_instance == null) {
            _instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    void Start() {
        InitSprite(redCatchObjects);
        InitSprite(greenCatchObjects);
        catchSelected = false;
    }

    private void InitSprite(GameObject[] gameObjects) {
        foreach (var tile in gameObjects) {
            SpriteRenderer spriteRenderer = tile.transform.GetChild(0).GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = AnimalJanggi._instance.sprites[6];
        }
    }

    public bool GetSelected() {
        return catchSelected;
    }

    public void SetSelected() {
        catchSelected = !catchSelected;
        Debug.Log(catchSelected);
    }

    public void CatchPiece(Sprite sprite, string team) {
        if (team.Equals("Red")) {
            redCatchTiles.Add(sprite);
        }
        else if (team.Equals("Green")) {
            greenCatchTiles.Add(sprite);
        }
        ResetObjectList(AnimalJanggi._instance.GetTeam());
    }

    public void ResetObjectList(string team) {
        int i;
        SpriteRenderer spriteRenderer;
        if (team.Equals("Red")) {
            for (i = 0; i < redCatchTiles.Count; i++) {
                spriteRenderer = redCatchObjects[i].transform.GetChild(0).GetComponent<SpriteRenderer>();

                spriteRenderer.sprite = redCatchTiles[i];
            }
            for (; i < redCatchObjects.Length; i++) {
                spriteRenderer = redCatchObjects[i].transform.GetChild(0).GetComponent<SpriteRenderer>();

                spriteRenderer.sprite = AnimalJanggi._instance.sprites[6];
            }
        }
        else if (team.Equals("Green")) {
            for (i = 0; i < greenCatchTiles.Count; i++) {
                spriteRenderer = greenCatchObjects[i].transform.GetChild(0).GetComponent<SpriteRenderer>();

                spriteRenderer.sprite = greenCatchTiles[i];
            }
            for (; i < greenCatchObjects.Length; i++) {
                spriteRenderer = greenCatchObjects[i].transform.GetChild(0).GetComponent<SpriteRenderer>();

                spriteRenderer.sprite = AnimalJanggi._instance.sprites[6];
            }
        }
    }

    public bool GetSelectable(RaycastHit hit) {
        if (hit.transform.parent.GetChild(1).GetComponent<SpriteRenderer>().sprite == null) {
            return true;
        }
        return false;
    }
}
