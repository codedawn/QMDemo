using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private long _uid;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Point;

    void Start()
    {
        Name.text = "liu";
        Point.text = "100";
    }

    void Update()
    {
        
    }

    public void SetPosition(float x, float y)
    {
        transform.position = new Vector3(x, 0, y);
    }

    public void SetPoint(int point)
    {
        Point.text = point.ToString();
    }

    public void SetName(string name)
    {
        Name.text = name;
    }

}
