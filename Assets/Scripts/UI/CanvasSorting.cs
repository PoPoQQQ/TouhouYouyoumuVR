using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CanvasSorting : MonoBehaviour
{
	public string sortingLayerName;
	public int sortingOrder;

    // Start is called before the first frame update
    void Start()
	{
        Canvas canvas = GetComponent<Canvas>();
        canvas.sortingLayerName = sortingLayerName;
        canvas.sortingOrder = sortingOrder;
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
