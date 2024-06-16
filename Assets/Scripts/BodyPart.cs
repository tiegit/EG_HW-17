using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class BodyPart : MonoBehaviour
{
	private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void BecomeDynamic()
	{
        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        //_rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
	}
}
