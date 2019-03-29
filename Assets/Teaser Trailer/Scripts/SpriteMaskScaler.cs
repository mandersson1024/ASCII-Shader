using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMaskScaler : MonoBehaviour
{
    void Update()
    {
        transform.localScale += transform.localScale * 0.2f * Time.deltaTime;
    }
}
