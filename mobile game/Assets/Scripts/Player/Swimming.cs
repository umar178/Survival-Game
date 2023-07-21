using StarterAssets;
using UnityEngine;
using UnityEngine.Rendering;

public class Swimming : MonoBehaviour
{
    public float swimSpeed = 5f;
    public float SwimSprint = 8f;
    public float jumpSpeed = 5f;
    public float gravity = 10f;
    public float waterLevel;

    public Color FogColor;

    public bool isSwimming = false;

    StarterAssetsInputs input;
    public Rigidbody rb;

    public GameObject Camera;

    [SerializeField] GameObject swimPanel;
    [SerializeField] GameObject swimPanel2;
    [SerializeField] GameObject swimPanel3;

    [SerializeField] GameObject SwimUpButton;
    [SerializeField] GameObject SwimDownButton;

    public Transform PlayerHead;

    public bool Grounded = true;
    public float GroundedOffset = -0.14f;
    public float GroundedRadius = 0.5f;
    public LayerMask GroundLayers;
    public float WaterGravity;

    void Start()
    {
        input = GetComponent<StarterAssetsInputs>();
    }

    private void Update()
    {
        if (PlayerHead.position.y < waterLevel && PlayerHead.position.y > 0)
        {
            swim();
            SwimUp();
            SwimDown();
            GravityHandler();
        }
        
        if (PlayerHead.position.y < (26.3) && PlayerHead.position.y > 0)
        {
            swimPanel.SetActive(true);
            RenderSettings.fog = true;
            RenderSettings.fogColor = FogColor;
            RenderSettings.fogMode = FogMode.Linear;
            RenderSettings.fogStartDistance = -84;
            swimPanel2.SetActive(true);
            swimPanel3.SetActive(true);
            SwimUpButton.SetActive(true);
            SwimDownButton.SetActive(true);
        }
        else
        {
            swimPanel.SetActive(false);
            RenderSettings.fog = false;
            swimPanel2.SetActive(false);
            swimPanel3.SetActive(false);
            SwimUpButton.SetActive(false);
            SwimDownButton.SetActive(false);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "River")
        {
            EnterWater();
            ExitWater();
        }
    }

    void EnterWater()
    {
        if (isSwimming) { return; }
        if (PlayerHead.position.y < waterLevel && PlayerHead.position.y > 0)
        {
            isSwimming = true;
            GetComponent<CharacterController>().enabled = false;
            GetComponent<FirstPersonController>().enabled = false;
            Rigidbody rb1 = gameObject.AddComponent<Rigidbody>();
            rb1.useGravity = false;
            rb = GetComponent<Rigidbody>();
        }
    }
    
    void ExitWater()
    {
        if (isSwimming == false) { return; }
        if (PlayerHead.position.y > waterLevel)
        {
            isSwimming = false;
            GetComponent<CharacterController>().enabled = true;
            GetComponent<FirstPersonController>().enabled = true;
            Rigidbody rb1 = gameObject.GetComponent<Rigidbody>();
            Destroy(rb1);
        }
    }

    void swim()
    {

        if (input.sprint == true)
        {
            swimSpeed = SwimSprint;
        }
        else { swimSpeed = 5; }

        if(input.move.y > 0) 
        {
            transform.position += Camera.transform.forward * swimSpeed * Time.deltaTime * input.move.y;
        }
        if (input.move.y < 0) 
        {
            transform.position += Camera.transform.forward * swimSpeed * Time.deltaTime * input.move.y; 
        }
        
        if (input.move.x > 0) 
        {
            transform.position += Camera.transform.right * swimSpeed * Time.deltaTime * input.move.x; 
        }
        if (input.move.x < 0) 
        {
            transform.position += Camera.transform.right * swimSpeed * Time.deltaTime * input.move.x; 
        }


    }

    void GravityHandler()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
        Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);

        if (Grounded) { return; }
        else
        {
            rb.velocity = -Vector3.up * WaterGravity;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isSwimming)
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (isSwimming)
        {
            rb.isKinematic = false;
        }
    }

    public void SwimUp()
    {
        if (input.jump)
        {
            transform.position += Vector3.up * swimSpeed * Time.deltaTime;
        }
    }
    
    public void SwimDown()
    {
        if (input.sprint)
        {
            transform.position += -Vector3.up * swimSpeed * Time.deltaTime;
        }
    }

}

