using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleGrid : MonoBehaviour {
    float sizeX;
    float sizeY;
    GridLayoutGroup grid;
    public bool ScaleX = false;
    public bool ScaleY = false;

	void Start () {
        grid = this.GetComponent<GridLayoutGroup>();
        sizeY =  grid.cellSize.y + grid.spacing.y;
        sizeX = grid.cellSize.x + grid.spacing.x;
        Debug.Log(sizeX + "X Y" + sizeY);
    }
	

	void Update () {
        RectTransform rt = this.GetComponent(typeof(RectTransform)) as RectTransform;
        Vector2 scale = Vector2.zero;
        if (ScaleX)
        {
            scale.x = grid.padding.right + grid.padding.left + sizeX * transform.childCount;
        }
        else
        {
            scale.x = rt.sizeDelta.x;
        }
        if(ScaleY)
        {
            scale.y = grid.padding.bottom + grid.padding.top + sizeY * transform.childCount;
        }
        else
        {
            scale.y = rt.sizeDelta.y;
        }
        
        rt.sizeDelta = scale;
    }
}
