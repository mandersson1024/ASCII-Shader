using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StressTest : MonoBehaviour
{
    public StressTestTile tilePrefab;

    void Start()
    {
        var cameraPrefab = Resources.Load<Camera>("Prefabs/Orthographic Camera");
        var camera = GameObject.Instantiate(cameraPrefab);

        for (int y = 0; y < 36; ++y)
        {
            for (int x = 0; x < 64; ++x)
            {
                var tile = GameObject.Instantiate(tilePrefab);
                tile.transform.localPosition = new Vector3((x * 2f) - 63f, (y * 2f) - 35f, 0f);
            }
        }
    }

}
