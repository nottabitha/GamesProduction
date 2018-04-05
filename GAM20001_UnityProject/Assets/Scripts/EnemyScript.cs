 using UnityEngine;
using System.Collections;

public class EnemyScript: MonoBehaviour 
{
	private Rigidbody body;
	public bool randomForce = true;
	public float velocity = 10f;
    public GameObject collisionParticle;

    public GameObject controller;
	private GameplayController gameController;
	// Use this for initialization
	void Awake () 
	{
		gameController = controller.GetComponent<GameplayController>();
		body = gameObject.GetComponent<Rigidbody>();
		body.AddForce(new Vector3(25, 0, 25), ForceMode.Impulse);
		
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag == "Player")
			gameController.RespawnPlayer();
		Vector3 reflection = body.velocity.normalized + ( 2 * (Vector3.Dot(body.velocity.normalized, col.contacts[0].normal)) * col.contacts[0].normal);
		body.velocity = reflection * velocity;

        DoStuff(col.gameObject);
    }

    private void OnTriggerEnter(Collider col)
    {
        DoStuff(col.gameObject);
    }

    void DoStuff (GameObject col)
    {
        if (collisionParticle != null)
        {
            GameObject temp = (GameObject)Instantiate(collisionParticle, transform.position, Quaternion.identity);
            ParticleSystem ps = temp.GetComponent<ParticleSystem>();
            ps.Play();
            //Destroy(temp, ps.main.duration + 1f);
        }
    }
}
