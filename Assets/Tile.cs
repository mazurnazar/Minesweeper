using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Tile : MonoBehaviour, IPointerUpHandler,IPointerDownHandler
{
    public int Number;
    public Sprite image;
    Manager manager;
    public int xPos, yPos;
    bool open;
    private void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<Manager>();
    }
    public void SetTile(int number , Sprite sprite, int x, int y)
    {
        Number = number;
        image = sprite;
        xPos = x;
        yPos = y;
        Debug.Log("tile " + xPos + " " + yPos);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log(Number);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //GetComponent<Image>().sprite = image;
        ShowTiles(this);
        //Debug.Log(this.Number);
        if (Number == 0)
        {
            open = true;
           // GetComponent<Image>().color = Color.red;
            Check(xPos, yPos);
            Debug.Log("tile" + xPos + " " + yPos);
        }
    }
    void Check(int xPos, int yPos)
    {
        manager.Neighbours(xPos, yPos);
        foreach (var item in manager.Neighbours(xPos, yPos))
        {
            int tileNumber = manager.tiles[item].Number;
            Debug.Log(tileNumber);
            if (tileNumber==0&&manager.tiles[item].open==false) Check(item/manager.XCount, item%manager.XCount);
            else 
                ShowTiles(manager.tiles[item]);
        }
    }
    void ShowTiles(params Tile[] tiles )
    {
        foreach (var item in tiles)
        {
            item.GetComponent<Image>().sprite = item.GetComponent<Tile>().image;
            item.open = true;
        }
    }
}
