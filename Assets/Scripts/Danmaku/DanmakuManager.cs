using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanmakuManager : MonoBehaviour
{
	static HashSet<GameObject> danmakus = new HashSet<GameObject>();

    static public void Register(GameObject danmaku)
        => danmakus.Add(danmaku);

    static public void Unregister(GameObject danmaku)
        => danmakus.Remove(danmaku);

    static public void ClearDanmaku()
    {
        foreach(GameObject danmaku in danmakus)
            if(danmaku)
                Destroy(danmaku);
        danmakus.Clear();
    }

    void Update()
    {
        Debug.Log(danmakus.Count);
    }
}
