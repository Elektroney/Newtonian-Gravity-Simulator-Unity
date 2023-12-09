using UnityEngine;

public class UIManager : MonoBehaviour
{

    private void OnGUI()
    {
        // Wrap everything in the designated GUI Area
        GUILayout.BeginArea(new Rect(50, 50, 400, 60));

        // Begin the singular Horizontal Group
        GUILayout.BeginHorizontal();

        // Place a Button normally

        // Arrange two more Controls vertically beside the Button
        GUILayout.BeginVertical();
        GUILayout.Box("Timescale : " + Mathf.Round(PhysicsManager.instance.timeScale) / 100);
        PhysicsManager.instance.timeScale = GUILayout.HorizontalSlider(PhysicsManager.instance.timeScale, 0f, 5000);

        // End the Groups and Area
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }

    // Prefab of the object to be created
    [SerializeField]
    private GameObject PhysicsBodyPrefab;

    private GameObject latestPhysicsBody;

    private float lastTimeScale;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))

            CreateObjectAtMousePosition();

        if(Input.GetKeyUp(KeyCode.E))
            PhysicsManager.instance.timeScale = lastTimeScale;

        else if (Input.GetKeyDown(KeyCode.W))

            DeleteObjectAtMousePosition();
        if (Input.GetKey(KeyCode.E) && latestPhysicsBody != null)
        {
            // Get the direction towards the mouse cursor
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - latestPhysicsBody.transform.position).normalized;

            // Calculate the distance between the object and the mouse cursor
            float distance = Vector3.Distance(mousePosition, latestPhysicsBody.transform.position);

            // Calculate the velocity based on the distance

            // Set the velocity of the latestPhysicsBody
            Rigidbody rb2d = latestPhysicsBody.GetComponent<Rigidbody>();
            rb2d.velocity = direction * distance * 0.1f;
        }
    }



    private void CreateObjectAtMousePosition()
    {
        lastTimeScale = PhysicsManager.instance.timeScale;
        PhysicsManager.instance.timeScale = 0;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        latestPhysicsBody = Instantiate(PhysicsBodyPrefab, mousePosition, Quaternion.identity);
    }

  private void DeleteObjectAtMousePosition()
    {

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition ) + Vector3.back * 2;

        RaycastHit hit;
        if (Physics.Raycast(mousePosition, Vector3.forward, out hit))
            if (hit.collider != null)
          
                Destroy(hit.collider.gameObject);
    }


}
