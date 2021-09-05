using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldTile : MonoBehaviour
{
    public Vector2 coords;
    public int tileType = 0; // what is this for?
    public List<OldTile> nearby; // should be method?
    public bool isBomb = false; // reduced to state
    public bool isRevealed = false; // reduced to state
    public int bombCount = 0; // should be method

    public List<Sprite> numberSprites; // should not be stored or used here
    public List<Sprite> wallSprites; // should not be stored or used here
    public Sprite bombSprite; // should not be stored or used here
    private SpriteRenderer spriteRenderer; // should not be stored or used here

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown() // separated into renderer component for input handling
    {
        Reveal();
        //Debug.Log(coords);
    }

    public void Reveal() // LOGIC should be reused, shouldn't be part of tile, displayed tiles should be live updated based on state
    {
        if (isRevealed)
            return;

        // Reveal Tile
        isRevealed = true;
        UpdateSprite();

        if (isBomb)
        {
            Debug.Log("Game Over");
        }

        foreach (OldTile t in nearby)
        {
            if (!t)
                continue;

            if (!isBomb && bombCount == 0)
            {
                t.Reveal();
            }

            t.UpdateSprite();
        }
    }

    public void AddNearby() // should be part of GetNearby method
    {
        nearby.Add(OldMapGenerator.GetTile(new Vector2(coords.x - 1, coords.y - 1)));
        nearby.Add(OldMapGenerator.GetTile(new Vector2(coords.x, coords.y - 1)));
        nearby.Add(OldMapGenerator.GetTile(new Vector2(coords.x + 1, coords.y - 1)));
        nearby.Add(OldMapGenerator.GetTile(new Vector2(coords.x + 1, coords.y)));
        nearby.Add(OldMapGenerator.GetTile(new Vector2(coords.x + 1, coords.y + 1)));
        nearby.Add(OldMapGenerator.GetTile(new Vector2(coords.x, coords.y + 1)));
        nearby.Add(OldMapGenerator.GetTile(new Vector2(coords.x - 1, coords.y + 1)));
        nearby.Add(OldMapGenerator.GetTile(new Vector2(coords.x - 1, coords.y)));

        GetBombCount();
    }

    private void GetBombCount()
    {
        int newCount = 0;

        foreach (OldTile t in nearby)
        {
            if (t && t.isBomb)
                newCount++;
        }

        bombCount = newCount;
    }


    public void UpdateSprite() // split into renderer
    {
        if (isRevealed)
        {
            if (!isBomb)
                spriteRenderer.sprite = numberSprites[bombCount];
            else
                spriteRenderer.sprite = bombSprite;
        }
        else
        {
            int newIndex = GetWallIndex();

            if (newIndex >= 0)
                spriteRenderer.sprite = wallSprites[newIndex];
        }
    }

    private int GetWallIndex() // Investigate this logic???
    {
        if (!Top() && !Right() && !Bottom() && !Left())
        {
            return 0;
        }
        else if (!Top() && !Right() && !Bottom() && Left())
        {
            return 1;
        }
        else if (Top() && !Right() && !Bottom() && Left())
        {
            return 2;
        }
        else if (!Top() && Right() && !Bottom() && Left())
        {
            return 3;
        }
        else if (Top() && Right() && !Bottom() && Left())
        {
            return 4;
        }
        else if (Top() && !Right() && !Bottom() && !Left())
        {
            return 5;
        }
        else if (Top() && Right() && !Bottom() && !Left())
        {
            return 6;
        }
        else if (Top() && !Right() && Bottom() && !Left())
        {
            return 7;
        }
        else if (Top() && Right() && Bottom() && !Left())
        {
            return 8;
        }
        else if (!Top() && Right() && !Bottom() && !Left())
        {
            return 9;
        }
        else if (!Top() && Right() && Bottom() && !Left())
        {
            return 10;
        }
        else if (!Top() && Right() && Bottom() && Left())
        {
            return 11;
        }
        else if (!Top() && !Right() && Bottom() && !Left())
        {
            return 12;
        }
        else if (!Top() && !Right() && Bottom() && Left())
        {
            return 13;
        }
        else if (Top() && !Right() && Bottom() && Left())
        {
            return 14;
        }
        else if (Top() && Right() && Bottom() && Left())
        {
            return 15;
        }

        return -1;
    }

    private bool Top() { return Check(1); }
    private bool Right() { return Check(3); }
    private bool Bottom() { return Check(5); }
    private bool Left() { return Check(7); }

    private bool Check(int pos)
    {
        if (nearby[pos])
        {
            return nearby[pos].isRevealed;
        }

        return false;
    }

}
