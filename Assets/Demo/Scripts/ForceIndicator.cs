using UnityEngine;
[RequireComponent(typeof(PhysicsBody))]

public class ForceIndicator : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody myRb;
    [SerializeField]
    private GameObject indicator;
    private Material mat;
    
    void Awake()
    {
        myRb = GetComponent<Rigidbody>();
        mat = GetComponent<MeshRenderer>().material;
        mat.color = new Color(1, 1, 1, myRb.velocity.magnitude);
        StretchToForce(transform.position, myRb.velocity + transform.position);
    }

    // Update is called once per frame
    void Update()
    {

        mat.color = new Color(1,1,1,myRb.velocity.magnitude);
        StretchToForce(transform.position,myRb.velocity + transform.position);
    }
    void StretchToForce(Vector3 pos1 , Vector3 pos2)
    {
        float dist = Vector3.Distance(pos1,pos2 );
        float scaling = Mathf.Clamp(dist / 10, 0, 0.3f);
        indicator.transform.localScale = new Vector3((dist *-Mathf.Log10(dist*0.001f) )/ 33, scaling, scaling) ;
        Vector3 middlepoint = (pos1 + pos2) / 2;
        Vector3 rotationDir = (pos2 - pos1);
        indicator.transform.right = -rotationDir;
    }
}
