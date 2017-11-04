using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotZone : MonoBehaviour {
    public Material m_off_material;
    public Material m_on_material;
    public GameManager m_manager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Ball")
        {
            GetComponent<Renderer>().sharedMaterial = m_on_material;
            m_manager.OnBallOn();
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.name == "Ball")
        {
            m_manager.OnBallOn();
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "Ball")
        {
            GetComponent<Renderer>().sharedMaterial = m_off_material;
            m_manager.OnBallOff();
        }
    }
}
