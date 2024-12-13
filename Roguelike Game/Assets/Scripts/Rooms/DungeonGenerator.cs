using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public class Cell
    {
        public bool visited = false;
        public bool[] status = new bool[4];
    }

    [System.Serializable]
    public class Rule
    {
        public GameObject room;
        public Vector2Int minPosition;
        public Vector2Int maxPosition;
        public bool obligatory;
        public string roomType;

        public int ProbabilityOfSpawning(int x, int y)
        {
            // 0 - cannot spawn, 1 - can spawn, 2 - HAS to spawn
            if (x >= minPosition.x && x <= maxPosition.x && y >= minPosition.y && y <= maxPosition.y)
            {
                return obligatory ? 2 : 1;
            }
            return 0;
        }
    }

    public Vector2Int size;
    public int startPos = 0;
    public Rule[] rooms;
    public Vector2 offset;
    public GameObject roomPrefab;

    List<Cell> board;

    // Remove the generation from Start()
    // LevelManager will call InitDungeon() instead
    void Start()
    {
        // Do nothing here
    }

    public void InitDungeon()
    {
        MazeGenerator();
    }

    GameObject FindRoomByType(string type)
    {
        foreach (var rule in rooms)
        {
            if (rule.roomType == type)
                return rule.room;
        }
        return roomPrefab; // fallback
    }

    void GenerateDungeon()
    {
        bool startRoomPlaced = false;
        bool itemRoomPlaced = false;
        bool bossRoomPlaced = false;

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                Cell currentCell = board[(i + j * size.x)];
                if (currentCell.visited)
                {
                    GameObject selectedRoomPrefab = roomPrefab;
                    string roomNameSuffix = "Default";
                    RoomBehaviour.RoomType assignType = RoomBehaviour.RoomType.Default;

                    if (!startRoomPlaced && i == 0 && j == 0)
                    {
                        selectedRoomPrefab = FindRoomByType("Start");
                        roomNameSuffix = "Start";
                        assignType = RoomBehaviour.RoomType.Start;
                        startRoomPlaced = true;
                    }
                    else if (!bossRoomPlaced && i == size.x - 1 && j == size.y - 1)
                    {
                        selectedRoomPrefab = FindRoomByType("Boss");
                        roomNameSuffix = "Boss";
                        assignType = RoomBehaviour.RoomType.Boss;
                        bossRoomPlaced = true;
                    }
                    else if (!itemRoomPlaced && Random.value > 0.8f)
                    {
                        selectedRoomPrefab = FindRoomByType("Item");
                        roomNameSuffix = "Item";
                        assignType = RoomBehaviour.RoomType.Item;
                        itemRoomPlaced = true;
                    }

                    var newRoom = Instantiate(
                        selectedRoomPrefab,
                        new Vector3(i * offset.x, 0, -j * offset.y),
                        Quaternion.identity,
                        transform
                    ).GetComponent<RoomBehaviour>();

                    newRoom.UpdateRoom(currentCell.status);
                    newRoom.name += $" {roomNameSuffix} {i}-{j}";
                    newRoom.roomType = assignType;
                }
            }
        }
    }

    void MazeGenerator()
    {
        board = new List<Cell>();
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                board.Add(new Cell());
            }
        }

        int currentCell = startPos;
        Stack<int> path = new Stack<int>();
        int k = 0;

        while (k < 1000)
        {
            k++;
            board[currentCell].visited = true;

            if (currentCell == board.Count - 1)
            {
                break;
            }

            List<int> neighbors = CheckNeighbors(currentCell);

            if (neighbors.Count == 0)
            {
                if (path.Count == 0) break;
                else currentCell = path.Pop();
            }
            else
            {
                path.Push(currentCell);
                int newCell = neighbors[Random.Range(0, neighbors.Count)];

                // Create passages
                if (newCell > currentCell)
                {
                    if (newCell - 1 == currentCell)
                    {
                        board[currentCell].status[2] = true;
                        currentCell = newCell;
                        board[currentCell].status[3] = true;
                    }
                    else
                    {
                        board[currentCell].status[1] = true;
                        currentCell = newCell;
                        board[currentCell].status[0] = true;
                    }
                }
                else
                {
                    if (newCell + 1 == currentCell)
                    {
                        board[currentCell].status[3] = true;
                        currentCell = newCell;
                        board[currentCell].status[2] = true;
                    }
                    else
                    {
                        board[currentCell].status[0] = true;
                        currentCell = newCell;
                        board[currentCell].status[1] = true;
                    }
                }
            }
        }

        GenerateDungeon();
    }

    List<int> CheckNeighbors(int cell)
    {
        List<int> neighbors = new List<int>();

        // Up
        if (cell - size.x >= 0 && !board[(cell - size.x)].visited)
            neighbors.Add(cell - size.x);
        // Down
        if (cell + size.x < board.Count && !board[(cell + size.x)].visited)
            neighbors.Add(cell + size.x);
        // Right
        if ((cell + 1) % size.x != 0 && !board[(cell + 1)].visited)
            neighbors.Add(cell + 1);
        // Left
        if (cell % size.x != 0 && !board[(cell - 1)].visited)
            neighbors.Add(cell - 1);

        return neighbors;
    }
}
