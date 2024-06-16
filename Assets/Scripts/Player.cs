using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D.IK;

[RequireComponent(typeof(PlayerMove))]
public class Player : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private IKManager2D _IKManager2D;
    [SerializeField] private GameObject _hook;

    private PlayerMove _playerMove;
    private List<BodyPart> _bodyParts = new();

    private void Awake()
    {
        _playerMove = GetComponent<PlayerMove>();
        _bodyParts = GetComponentsInChildren<BodyPart>().ToList();
    }

    private void Start()
    {
        _hook.SetActive(false);
    }

    public void BecomeDynamic()
    {
        _animator.enabled = false;
        _IKManager2D.enabled = false;
        _playerMove.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static; //
        /* без этого капсуль коллайдер отделиться и будет влиять на рекдол,
        например если касулу выключить без этой строки*/
        _playerMove.enabled = false;
        _hook.SetActive(true);

        for (int i = 0; i < _bodyParts.Count; i++)
        {
            _bodyParts[i].BecomeDynamic();
        }
    }
}