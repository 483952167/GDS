using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour {
    public int sizeX, sizeY;
    public RoomCell cellPrefab;
    private RoomCell[,] cells;

    public void Generate() {
        cells = new RoomCell[sizeX, sizeY];
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                CreateCell(x, y);
            }
        }
    }

    private void CreateCell(int x, int y) {
        RoomCell newCell = Instantiate(cellPrefab) as RoomCell;
        cells[x, y] = newCell;
        newCell.name = "RoomCell " + x + ", " + y;
        newCell.transform.parent = transform;
        newCell.transform.localPosition = new Vector3(x - sizeX * 0.5f, y - sizeY * 0.5f + 0.5f, 0);
    }    
}
