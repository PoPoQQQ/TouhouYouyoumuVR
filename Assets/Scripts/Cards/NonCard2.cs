using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonCard2 : MonoBehaviour
{
    public void StartCard()
    	=> StartCoroutine(CardCoroutine());

    IEnumerator CardCoroutine()
    {
        GetComponent<WhiteScreenManager>().Flash(Color.white, 0.5f, 0.5f);
        GetComponent<SaigyoujiYuyuko>().MoveTo(new Vector3(0f, 0f, 100f));
        GetComponent<Floating>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        GetComponent<SaigyoujiYuyuko>().Ring(false);
        GetComponent<BackgroundManager>().SetBackground(6);
        yield return new WaitForSeconds(0.2f);
        GetComponent<SaigyoujiYuyuko>().petals.Play();
        
        float interval = 0.7f;
        yield return new WaitUntil(() => GameObject.Find("Player").GetComponentInChildren<AudioManager>().BGMTime() > 89.2f - interval);
        //yield return new WaitForSeconds(4f);
        GetComponent<SaigyoujiYuyuko>().petals.Stop();
        yield return new WaitForSeconds(interval);
        
        GetComponent<SaigyoujiYuyuko>().Oogi(true);
        yield return new WaitForSeconds(0.1f);
        GetComponent<WhiteScreenManager>().Flash(Color.white, 0.06f, 0.2f);
        yield return new WaitForSeconds(0.4f);
        GetComponent<Floating>().enabled = true;
        yield return new WaitForSeconds(1f);

        Coroutine c1 = StartCoroutine(ShootHemispheres());
        Coroutine c2 = StartCoroutine(ShootCrosses());

        yield return new WaitForSeconds(30f);
        StopCoroutine(c1);
        StopCoroutine(c2);
        yield return new WaitForSeconds(4f);
        GetComponent<Card2>().StartCard();
        Destroy(this);
    }

    IEnumerator ShootHemispheres()
    {
        GameObject blueOodamaDanmaku = Resources.Load<GameObject>("Prefabs/Danmaku/BlueOodamaDanmaku");
        float interval = 3f;
        bool left = true;
        while(true)
        {
            interval = Mathf.Max(interval * 0.92f, 1.5f);
            yield return new WaitForSeconds(interval);
            ShootHemisphere(blueOodamaDanmaku, transform.position + Vector3.left * (left? 1: -1) * 35f);
            left = !left;
        }
    }

    void ShootHemisphere(GameObject danmaku, Vector3 position)
    {
        Vector3 z = (GameObject.Find("Player").transform.position - position).normalized;
        Vector3 x = Vector3.ProjectOnPlane(Vector3.right, z).normalized;
        Vector3 y = Vector3.Cross(x, z);
        /*
        for(float rotationX = -90f; rotationX <= 90f; rotationX += 20f)
        {
            Vector3 zx = Quaternion.AngleAxis(rotationX, x) * z;
            for(float rotationY = -90f; rotationY <= 90f; rotationY += 20f)
            {
                Vector3 zy = Quaternion.AngleAxis(rotationY, y) * z;
                ShootDanmaku(danmaku, position, zx + zy - z);
            }
        }
        */
        for(int rotationX = -60; rotationX <= 60; rotationX += 10)
        {
            Vector3 zx = Quaternion.AngleAxis(rotationX, x) * z;
            ShootDanmaku(danmaku, position, zx, Mathf.Abs(rotationX));
        }
        for(int rotationY = -60; rotationY <= 60; rotationY += 10)
            if(rotationY != 0f)
            {
                Vector3 zy = Quaternion.AngleAxis(rotationY, y) * z;
                ShootDanmaku(danmaku, position, zy, Mathf.Abs(rotationY));
            }
    }

    void ShootDanmaku(GameObject danmaku, Vector3 position, Vector3 direction, float deltaSpeed)
    {
        Instantiate(danmaku).AddComponent<LinearDanmaku>().InitializeWithRay(position, direction, 40f - deltaSpeed * 0.25f);
    }

    IEnumerator ShootCrosses()
    {
        GameObject redBallDanmaku = Resources.Load<GameObject>("Prefabs/Danmaku/RedBallDanmaku");
        GameObject purpleBallDanmaku = Resources.Load<GameObject>("Prefabs/Danmaku/PurpleBallDanmaku");
        while(true)
        { 
            StartCoroutine(ShootCross(redBallDanmaku, purpleBallDanmaku));
            yield return new WaitForSeconds(0.25f);
        }
    }

    IEnumerator ShootCross(GameObject danmaku1, GameObject danmaku2)
    {
        Vector3 position = transform.position + (Vector3)Random.insideUnitCircle * 30f;
        Vector3 z = (GameObject.Find("Player").transform.position - position).normalized;
        Vector3 x = Vector3.ProjectOnPlane(Vector3.right, z).normalized;
        Vector3 y = Vector3.Cross(x, z);
        
        /*
        Vector3 zx1 = Quaternion.AngleAxis(angle, x) * z;
        Vector3 zx2 = Quaternion.AngleAxis(-angle, x) * z;
        Vector3 zy1 = Quaternion.AngleAxis(angle, y) * z;
        Vector3 zy2 = Quaternion.AngleAxis(-angle, y) * z;
        */

        float angle = 30f;
        StartCoroutine(ShootSubcross(danmaku1, danmaku2, position, x, y, z, 0f, 0f));
        StartCoroutine(ShootSubcross(danmaku1, danmaku2, position, x, y, z, angle, 0f));
        StartCoroutine(ShootSubcross(danmaku1, danmaku2, position, x, y, z, -angle, 0f));
        //StartCoroutine(ShootSubcross(danmaku1, danmaku2, position, x, y, z, 0f, angle));
        //StartCoroutine(ShootSubcross(danmaku1, danmaku2, position, x, y, z, 0f, -angle));
        yield break;
    }

    IEnumerator ShootSubcross(GameObject danmaku1, GameObject danmaku2, Vector3 position, Vector3 x, Vector3 y, Vector3 z, float angleX, float angleY)
    {
        Vector3 zx = Quaternion.AngleAxis(angleX, x) * z;
        Vector3 zy = Quaternion.AngleAxis(angleY, y) * z;
        Vector3 direction = zx + zy - z;
        GameObject instance = ShootDanmaku2(danmaku1, position, direction);
        yield return new WaitForSeconds(1f);
        if(!instance)
            yield break;
        Vector3 _position = instance.transform.position;
        float angle = 30f;
        {
            Vector3 _zx = Quaternion.AngleAxis(angleX, x) * z;
            Vector3 _zy = Quaternion.AngleAxis(angleY, y) * z;
            Vector3 _direction = _zx + _zy - z;
            ShootDanmaku2(danmaku2, _position, _direction);
        }
        /*{
            Vector3 _zx = Quaternion.AngleAxis(angleX + angle, x) * z;
            Vector3 _zy = Quaternion.AngleAxis(angleY, y) * z;
            Vector3 _direction = _zx + _zy - z;
            ShootDanmaku2(danmaku1, _position, _direction);
        }
        {
            Vector3 _zx = Quaternion.AngleAxis(angleX - angle, x) * z;
            Vector3 _zy = Quaternion.AngleAxis(angleY, y) * z;
            Vector3 _direction = _zx + _zy - z;
            ShootDanmaku2(danmaku1, _position, _direction);
        }*/
        {
            Vector3 _zx = Quaternion.AngleAxis(angleX, x) * z;
            Vector3 _zy = Quaternion.AngleAxis(angleY + angle, y) * z;
            Vector3 _direction = _zx + _zy - z;
             ShootDanmaku2(danmaku1, _position, _direction);
        }
        {
            Vector3 _zx = Quaternion.AngleAxis(angleX, x) * z;
            Vector3 _zy = Quaternion.AngleAxis(angleY - angle, y) * z;
            Vector3 _direction = _zx + _zy - z;
            ShootDanmaku2(danmaku1, _position, _direction);
        }
        Destroy(instance);
    }

    GameObject ShootDanmaku2(GameObject danmaku, Vector3 position, Vector3 direction)
    {
        GameObject instance = Instantiate(danmaku);
        instance.AddComponent<LinearDanmaku>().InitializeWithRay(position, direction, 25f);
        return instance;
    }
    
}
