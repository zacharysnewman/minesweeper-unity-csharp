using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


public class OldMapGenerator : MonoBehaviour // separate rendering from map generation
{
    public GameObject tilePrefab;
    public Vector2 mapSize;
    public int numberOfBombs;
    public int numberOfEmpties;

    public Text loadingText;
    public Image loadingBar;

    private List<Vector2> coords = new List<Vector2>();
    private List<int> tileTypes = new List<int>();

    public static List<OldTile> tiles = new List<OldTile>();
    public static Dictionary<Vector2, int> coordsToIndex;
    public static Dictionary<int, Vector2> indexToCoords;


    // Start is called before the first frame update
    void Start()
    {
        OldEvents.mapGenerated.AddListener(EventGeneratedMap);
        //Generate();
    }

    // Update is called once per frame
    void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(Generate());
        }
    }

    void UpdateLoadingBar(float progress)
    {
        float goalProgress = mapSize.x * mapSize.y * 2;
        float normalizedProgress = progress / goalProgress;

        loadingText.text = "Loading... " + Mathf.RoundToInt(normalizedProgress * 100) + "%";
        loadingBar.fillAmount = normalizedProgress;
    }

    void EventGeneratedMap()
    {
        Debug.Log("Map Done Generating...");
    }

    public static OldTile GetTile(Vector2 coords)
    {

        if (coordsToIndex.ContainsKey(coords))
        {
            //Debug.Log(coords);

            int index = coordsToIndex[coords];

            //Debug.Log(index);

            return tiles[index];
        }

        return null;
    }

    IEnumerator Generate() // reuse and rewrite some of this logic
    {
        coordsToIndex = new Dictionary<Vector2, int>();
        indexToCoords = new Dictionary<int, Vector2>();

        int waitFrames = 0;

        // Loop using map size x and y
        int index = 0;
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                // Add coordinates to a V2 list
                coords.Add(new Vector2(x, y));

                //Debug.Log(string.Format("X: {0}, Y: {1}", x, y));
                coordsToIndex.Add(new Vector2(x, y), index);
                indexToCoords.Add(index, new Vector2(x, y));

                index++;
            }
        }

        // Catch (unnecessary if input is validated)
        if (numberOfBombs >= coords.Count)
            numberOfBombs--;

        // Loop through bombs
        for (int i = 0; i < numberOfBombs; i++)
        {
            // Add bombs to object list
            tileTypes.Add(1);
        }

        numberOfEmpties = coords.Count - numberOfBombs;

        // Loop through empty spaces
        for (int i = 0; i < numberOfEmpties; i++)
        {
            // Add empties to object list
            tileTypes.Add(0);
        }

        // Randomly sort object list
        Shuffler.Shuffle(tileTypes);

        // Get offset to center map on 0,0
        Vector2 centerOffset = new Vector2(mapSize.x / 2, mapSize.y / 2);

        // Loop through V2 list
        for (int i = 0; i < coords.Count; i++)
        {
            // Create tiles and position
            GameObject tileObject = Instantiate(tilePrefab, new Vector3(coords[i].x - centerOffset.x, -coords[i].y + centerOffset.y), Quaternion.identity, GameObject.Find("World").transform);
            OldTile t = tileObject.GetComponent<OldTile>();
            t.coords = coords[i];
            t.tileType = tileTypes[i];
            if (tileTypes[i] == 1)
                t.isBomb = true;

            tiles.Add(t);

            UpdateLoadingBar(++waitFrames);
            if (waitFrames % 100 == 0)
            {
                //Debug.Log("Objects: " + waitFrames);
                yield return null;
            }
        }


        foreach (OldTile t in tiles)
        {
            t.AddNearby();
            t.gameObject.SetActive(true);

            UpdateLoadingBar(++waitFrames);
            if (waitFrames % 100 == 0)
            {
                //Debug.Log("Tiles: " + waitFrames);
                yield return null;
            }
        }

        //Debug.Log("Total Loops: " + waitFrames);

        //UpdateLoadingBar(mapSize.x * mapSize.y * 2);

        loadingText.transform.parent.gameObject.SetActive(false);
        OldEvents.mapGenerated.Invoke();
    }
}