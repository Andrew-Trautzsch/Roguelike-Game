using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Video;

public class RoomBehaviour : MonoBehaviour
{
    public enum RoomType
    {
        Default,
        Start,
        Item,
        Boss
    }

    public RoomType roomType = RoomType.Default; // Default room type
    public GameObject[] walls; // 0 - up, 1 - down, 2 - right, 3 - left
    public GameObject[] doors;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void UpdateRoom(bool[] status)
    {
        for (int i = 0; i < status.Length; i++)
        {
            doors[i].SetActive(status[i]);
            walls[i].SetActive(!status[i]);
        }
    }

}
