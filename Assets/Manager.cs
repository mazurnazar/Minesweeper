using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Manager : MonoBehaviour
{
    public Image image;
    public Sprite bomb;
    public Sprite blank;
    public List<Sprite> numbers;
    public GameObject obj;
    public Canvas canvas;
    public int x, y;
    private int xCount=20, yCount =10;
    public int XCount { get => xCount; }
    public int YCount { get => yCount; }
    public int bombCount;
    public int[] field;
    public int[,] field2;
    public List<Tile> tiles;
    public bool gameOver = false;
    public void DefineSize()
    {
        var grid = image.GetComponent<GridLayoutGroup>();
        image.rectTransform.sizeDelta = canvas.pixelRect.size;
        float ratio = canvas.pixelRect.width / canvas.pixelRect.height;
        
        image.GetComponent<GridLayoutGroup>().cellSize = new Vector2((canvas.pixelRect.width - grid.spacing.x * (xCount -1) )/ xCount , 
                                                                     (canvas.pixelRect.height - grid.spacing.y * (yCount -1) )/ yCount );

    }
    public void CreateGrid(int x, int y)
    {
        for (int i = 0; i <x*y; i++)
        {
            GameObject gameObject =Instantiate(obj, image.transform);
            tiles.Add(gameObject.GetComponent<Tile>());
            tiles[i].GetComponent<Image>().color = Color.green;
        }
        
      
    }
    // Start is called before the first frame update
    void Start()
    {

        tiles = new List<Tile>();
        DefineSize();
        CreateGrid(xCount, yCount);
        bombCount = xCount * yCount / 5;
        field = new int[xCount* yCount];
        field2 = new int[yCount, xCount];
        SetBombs();
        CheckForBombs();
    }
    public void SetBombs()
    {
        while(bombCount>0)
        {
            int randomNumber = Random.Range(1, xCount * yCount);
            if (field[randomNumber] == -1) continue;
            else field[randomNumber] = -1;
            bombCount--;
            tiles[randomNumber].SetTile(-1, bomb, randomNumber/xCount, randomNumber%xCount);
        }
        for (int i = 0; i < yCount; i++)
        {
            for (int j = 0; j < xCount; j++)
            {
                field2[i, j] = field[i * xCount + j];
            }
        }
        string s = "";
        foreach (var item in field2)
        {
            s += item;
        }
        
    }
    public void CheckForBombs()
    {
        
        for (int i = 0; i < yCount; i++)
        {
            for (int j = 0; j < xCount; j++)
            {
                if (field2[i, j] != -1)
                {
                    Neighbours(i, j);
                    if (field2[i, j] > 0) { tiles[i * xCount + j].SetTile(field2[i, j], numbers[field2[i, j] - 1],j,i); }
                    else tiles[i * xCount + j].SetTile(field2[i, j], blank, i, j);
                }
            }
        }
      
    }
    public  int[] Neighbours(int i, int j)
    {
        int bombsNumber = 0;
        int[] neibours = new int[8];
        int neib = 0;
        for (int k = -1; k <= 1; k++)
        {
            for (int p = -1; p <=1; p++)
            {
                if (i + k >= 0 && i + k <yCount && j + p >= 0 && j + p <xCount)
                {
                    if (field2[i + k, j + p] == -1) bombsNumber++;
                    if (k == 0 && p == 0) continue;
                    else
                    { 
                       // neibours.Add(tiles[(i + k)*xCount + j + p]);
                        neibours[neib] = (i + k)*xCount + j + p;
                        neib++;
                       // Debug.Log(((i + k) * xCount + j + p)); 
                    }
                }
            }
        }
        field2[i, j] = bombsNumber;
        return neibours;
    }

}
