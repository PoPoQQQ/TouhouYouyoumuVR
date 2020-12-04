using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISelectBehavior : MonoBehaviour
{
    public GameObject unselected;
    public GameObject selected;

    public bool isSelecting;

    // Start is called before the first frame update
    void Start()
    {
        unselected.SetActive(true);
        selected.SetActive(false);
        isSelecting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isSelecting)
        {
            unselected.SetActive(false);
            selected.SetActive(true);
        }
        else
        {
            unselected.SetActive(true);
            selected.SetActive(false);
        }
    }
}
