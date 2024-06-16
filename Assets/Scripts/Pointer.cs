using TMPro;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    [SerializeField] private Transform _hook;
    [SerializeField] private TMP_Text _selectedBodyPartText;
    [SerializeField] private Explosion _explosionPrefab;

    private BodyPart _selectedBodyPart;
    private SpringJoint2D _springJoint2D;


    private void Start()
    {
        _selectedBodyPartText.text = "none";
    }

    private void Update()
    {
        Vector3 cursorWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorWorldPosition.z = 0;

        _hook.position = cursorWorldPosition;

        RaycastHit2D hit = Physics2D.Raycast(cursorWorldPosition, Vector2.zero);

        if (hit)
        {
            _selectedBodyPart = hit.collider.GetComponent<BodyPart>();

            if (_selectedBodyPart)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (_springJoint2D)
                    {
                        return;
                        //Destroy(_springJoint2D);
                    }

                    _springJoint2D = _hook.gameObject.AddComponent<SpringJoint2D>();
                    _springJoint2D.connectedBody = _selectedBodyPart.GetComponent<Rigidbody2D>();
                    _springJoint2D.autoConfigureDistance = false;
                    _springJoint2D.distance = 0f;
                    _springJoint2D.frequency = 10f;
                    _springJoint2D.dampingRatio = 1f;

                    _selectedBodyPartText.text = _selectedBodyPart.name;
                }
            }                        
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_springJoint2D)
            {
                Destroy(_springJoint2D);
                _selectedBodyPartText.text = "none";
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Instantiate(_explosionPrefab, cursorWorldPosition, Quaternion.identity);
        }
    }
}
