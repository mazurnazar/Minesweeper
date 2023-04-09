using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Tile : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public int Number;
    public Sprite numberImage, defaultImage, flagImage;
    Manager manager;
    UIManager uiManager;
    public int xPos, yPos;
    public bool open;
    public bool flag;
    public bool canLeftClick = true;
    public bool canRightClick = true;
    private void Start()
    {
        GameObject Manager = GameObject.Find("Manager");
        manager = Manager.GetComponent<Manager>();
        uiManager = Manager.GetComponent<UIManager>();

    }
    public void SetTile(int number , Sprite sprite, int x, int y)
    {
        Number = number;
        numberImage = sprite;
        xPos = x;
        yPos = y;
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerId == -1 )
        {
            if (canLeftClick&&!manager.gameOver)
            {
                ShowTiles(this);
                if (Number == 0)
                {
                    open = true;
                    Check(xPos, yPos);
                }
                if (Number == -1) { uiManager.GameOver(); manager.gameOver = true; }
            }
        }
        else if(canRightClick&&!manager.gameOver)
        {
            Flag(this);
        }
    }
    void Check(int xPos, int yPos)
    {
        manager.Neighbours(xPos, yPos);
        foreach (var item in manager.Neighbours(xPos, yPos))
        {
            int tileNumber = manager.tiles[item].Number;
            if (tileNumber == 0 && manager.tiles[item].open == false) 
            { Debug.Log((item / manager.XCount)+" "+(item % manager.XCount));   // CHECK ALL NEIBORS ----FIX
                //Check(item / manager.XCount, item % manager.XCount);
            }
           // else

                ShowTiles(manager.tiles[item]);

        }
    }
    void ShowTiles(params Tile[] tiles )
    {
        foreach (var item in tiles)
        {
            item.GetComponent<Image>().sprite = item.GetComponent<Tile>().numberImage;
            item.canRightClick = false;
            item.canLeftClick = false;
            if (item.Number == 0) item.GetComponent<Image>().color = Color.white;
            item.open = true;
        }
    }
    void Flag(Tile tile)
    {
        if (flag)
        {
            tile.GetComponent<Image>().sprite = defaultImage;flag = false; canLeftClick = true;
            tile.GetComponent<Image>().color = Color.green;
        }
        else
        {
            tile.GetComponent<Image>().sprite = flagImage; flag = true; canLeftClick = false;
            tile.GetComponent<Image>().color = Color.white;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
       
    }
}
