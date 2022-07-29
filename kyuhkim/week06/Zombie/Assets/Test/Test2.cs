using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Test2 : MonoBehaviour
{
    public Test test;
    public GameObject prefab;
    public TextMeshProUGUI text;
    
    // Start is called before the first frame update
    private void Start()
    {
        // test.life = 3;
        for (var i = 0; i < 100; ++i)
        {
            Instantiate(prefab, transform);
            text.text = transform.childCount.ToString();
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

        yield return new WaitForSecondsRealtime(1);
        
        text.text = transform.childCount.ToString();
    }
}
