using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroudController : MonoBehaviour
{
    // Start is called before the first frame update
    float lifeTime;
    void Start()
    {
        lifeTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime < 1f)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, lifeTime);
        }
        else if (lifeTime > 1.5f)
        {
            Destroy(transform.gameObject);
        }
        else if (lifeTime > 1.3f)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f - (lifeTime - 1.3f) * 5);
        }
    }
}
