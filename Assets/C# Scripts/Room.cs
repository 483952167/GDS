using UnityEngine;
using System.Collections;
using System;

public class Room : MonoBehaviour {
    public int sizeX, sizeY;
    public RoomCell cellPrefab;
    public RoomWall wallPrefab;
    public Door doorPrefab;
    public RoomObstacle obstaclePrefab;
    public float obstacleChance;

    private RoomCell[,] cells;
    private RoomWall[,] walls;
    private Door[,] doors;
    private RoomObstacle[,] obstacles;

    public void GenerateNextRoom() {
        foreach (RoomObstacle o in obstacles)
        {
            Destroy(o);
        }
        Generate();
    }

    public void Generate() {
        cells = new RoomCell[sizeX, sizeY];
        walls = new RoomWall[sizeX, sizeY];
        doors = new Door[sizeX, sizeY];
        obstacles = new RoomObstacle[sizeX, sizeY];
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                CreateCell(x, y);
                if(shouldSpawnDoor(x, y))
                {
                    CreateDoor(x, y);
                }
                else if(x == 0 || x == sizeX - 1 || y == 0 || y == sizeY - 1)
                {
                    CreateWall(x, y);
                }
                else if (shouldSpawnObstacle(x, y))
                {
                    SpawnObstacle(x, y);
                }
            }
        }
    }

    private void CreateCell(int x, int y) {
        RoomCell newCell = Instantiate(cellPrefab) as RoomCell;
        cells[x, y] = newCell;
        newCell.name = "RoomCell " + x + ", " + y;
        newCell.transform.parent = transform;
        newCell.transform.localPosition = new Vector3(x - sizeX * 0.5f, y - sizeY * 0.5f, 0);
    }

    private void CreateWall(int x, int y) {
        RoomWall newWall = Instantiate(wallPrefab) as RoomWall;
        walls[x, y] = newWall;
        newWall.name = "Room Wall " + x + ", " + y;
        newWall.transform.parent = transform;
        newWall.transform.localPosition = new Vector3(x - sizeX * 0.5f, y - sizeY * 0.5f, 0);
    }

    private void CreateDoor(int x, int y) {
        Door newDoor = Instantiate(doorPrefab) as Door;
        doors[x, y] = newDoor;
        newDoor.name = "New Door " + x + ", " + y;
        newDoor.transform.parent = transform;
        newDoor.transform.localPosition = new Vector3(x - sizeX * 0.5f, y - sizeY * 0.5f, 0);
    }


    private void SpawnObstacle(int x, int y) {
        RoomObstacle newObst = Instantiate(obstaclePrefab) as RoomObstacle;
        obstacles[x, y] = newObst;
        newObst.name = "Obstacle " + x + ", " + y;
        newObst.transform.parent = transform;
        newObst.transform.localPosition = new Vector3(x - sizeX * 0.5f, y - sizeY * 0.5f, 0);
    }

    private bool shouldSpawnObstacle(int x, int y) {
        bool xBounds = x < sizeX - 2 && x > 1;
        bool yBounds = y < sizeY - 2 && y > 1;
        bool chance = UnityEngine.Random.Range(0f, 100f) < obstacleChance;
        return xBounds && yBounds && chance;
    }

    private bool shouldSpawnDoor(int x, int y) {
        bool north = x == (sizeX / 2) && y == sizeY - 1;
        bool south = x == (sizeX / 2) && y == 0;
        bool east = x == sizeX - 1 && y == (sizeY / 2);
        bool west = x == 0 && y == (sizeY / 2);
        return north || south || east || west;
    }

    public void SpawnCharacters(int direction, Character c) {
        float startX, startY;
        if (direction == DoorPositions.NORTH)
        {
            startX = 0;
            startY = (sizeY / 2) - Math.Min((sizeX * .15f), 3);
            c.setCharacterPosition(new Vector3((float)startX, (float)startY, 0f));
        }
        else if (direction == DoorPositions.EAST)
        {
            startX = (sizeX / 2) - Math.Min((sizeX  * .15f), 3);
            startY = 0;
            Debug.Log("X is " + startX);
            c.setCharacterPosition(new Vector3((float)startX, (float)startY, 0f));
        }
        else if (direction == DoorPositions.SOUTH)
        {
            startX = 0;
            startY = -(sizeY / 2) + Math.Min((sizeX * .15f), 3);
            c.setCharacterPosition(new Vector3((float)startX, (float)startY, 0f));
        }
        else if (direction == DoorPositions.WEST)
        {
            startY = 0;
            startX = -(sizeX / 2) + Math.Min((sizeX * .15f), 3);
            c.setCharacterPosition(new Vector3((float)startX, (float)startY, 0f));
        }
        else
        {
            Debug.Log("Invalid spawn characters direction " + direction);
        }

        Debug.Log("Character Position: " + c.getCharacterPosition());
    }
}
