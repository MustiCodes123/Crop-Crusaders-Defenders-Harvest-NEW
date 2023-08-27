using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmDestroy : MonoBehaviour
{

    public GameObject fire;
    public GameObject sheep1;
    public GameObject sheep2;
    public ColdBarScript _coldbar;
    public HealthBarScript _healthbar;

    void Update()
    {
        if (_coldbar.slider.value == 0)
        {
            _healthbar.slider.value = 0;
            Destroy(sheep1);
            Destroy(sheep2);
            Destroy(fire);
        }
    }
}
