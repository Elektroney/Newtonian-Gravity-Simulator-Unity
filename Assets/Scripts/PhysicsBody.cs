using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PhysicsBody : MonoBehaviour
{
    [SerializeField]
    private float mass;
    [SerializeField]
    private bool setMassToRigidbodyMass;
    private PhysicsManager physicsManager;
    private Rigidbody myRb;
    void Start()
    {
        myRb = GetComponent<Rigidbody>();
        physicsManager = PhysicsManager.instance;
        if (physicsManager == null)
        {
            Debug.LogError("Physics Manager Not Found! Are you sure the script is Active?");

            return;
        }
        
        if (setMassToRigidbodyMass)
            myRb.mass = mass;
        physicsManager.AddPhysicsBody(this);
        physicsManager.PhysicsEvent += Simulate;
    }
    void Simulate()
    {

        for (int i = 0; i < physicsManager.physicsBodys.Count; i++)
        {
            PhysicsBody body = physicsManager.physicsBodys[i];
            if (body == this || body == null)
                continue;

            Vector3 Force = CalculateScalar(transform.position, body.transform.position, mass, body.mass) * (body.transform.position - transform.position);
            myRb.AddForce(Force, ForceMode.Force);
        }


    }
    private void OnDestroy()
    {
        physicsManager.PhysicsEvent -= Simulate;
    }
    float CalculateScalar(Vector3 pos1, Vector3 pos2, float mass1, float mass2)
    {
        /*
         * This Calculation is not Optimal cause F1 == F2 in Newtonian Gravtitation 
         * so we could just calculate this value once and assign it to both Physics Bodies.
         * But this Approach rather Follows Programming Principles rather than being most Effiecient
         * You can implement this on your own or even create a fork of this project if effieciency is key!
         */
        return physicsManager.gravitationalConstant * ((mass1 * mass2)/ Vector3.Distance(pos1,pos2));
    }
}
