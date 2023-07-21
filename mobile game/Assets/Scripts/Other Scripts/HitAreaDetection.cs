using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAreaDetection : MonoBehaviour
{
    public Collider CollidedObject;

    void OnTriggerStay(Collider other)
    {
        if(other == null) { return; }
        other.GetComponent<Health>().HealthUI.SetActive(true);

        if (other.gameObject.tag == "Tree")
        {
            CollidedObject = other;
        }
        else if (other.gameObject.tag == "Rock")
        {
            CollidedObject = other;
        }
        else if (other.gameObject.tag == "Enemy")
        {
            CollidedObject = other;
        }
        else { return; }
    }
    private void OnTriggerExit(Collider other)
    {
        CollidedObject.GetComponent<Health>().HealthUI.SetActive(false);
        CollidedObject = null;
    }
}
