using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatScoreText : MonoBehaviour
{
    [SerializeField] float upSpeed = 5f;

    public void SetScoreValue(int multiplier)
    {
        var text = GetComponent<TextMeshProUGUI>();

        if (multiplier < 3)
            text.color = Color.white;
        else if(multiplier < 7)
            text.color = Color.green;
        else if(multiplier < 11)
            text.color = Color.yellow;
        else if(multiplier < 17)
            text.color = new Color(255f, 155f, 0f);
        else if(multiplier < 30)
            text.color = Color.red;

        text.SetText("x" + multiplier);

        Destroy(gameObject, 5f);
    }

    void Update() 
    {
        transform.position += transform.up * Time.deltaTime * upSpeed;    
    }

}
