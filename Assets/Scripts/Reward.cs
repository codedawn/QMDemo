using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Reward : MonoBehaviour
{
    private int _id;

    public void SetId(int id)
    {
        _id = id;
    }

    public void SetPosition(float x, float y)
    {
        transform.position = new Vector3(x, 0, y);
    }
}