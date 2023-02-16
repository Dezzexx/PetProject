using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestMB : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public Animator GetAnimator() {
        return _animator;
    }
}
