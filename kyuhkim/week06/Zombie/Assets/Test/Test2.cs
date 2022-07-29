using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    public Test test;

    public GameObject prefab;
    
    // Start is called before the first frame update
    private void Start()
    {
        for (var i = 0; i < 100; ++i)
        {
            Instantiate(prefab);
        }

        StartCoroutine(Kill());
    }

    private IEnumerator Kill()
    {
        while (!test.life.Equals(0))
        {
            yield return new WaitForSecondsRealtime(1);
            --test.life;
            Debug.LogWarning($"life = {test.life}");
        }
    }
}
