using UnityEngine;

[RequireComponent(typeof(Animator))]
public sealed class CharacterAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void UpdateState(int parameterId, float value)
    {
        _animator.SetFloat(parameterId, value);
    }

    public void UpdateState(int parameterId, bool value)
    {
        _animator.SetBool(parameterId, value);
    }

    public void SetTrigger(int parameterId)
    {
        _animator.SetTrigger(parameterId);
    }

    public void ResetTrigger(int parameterId)
    {
        _animator.ResetTrigger(parameterId);
    }

    public float GetFloat(int parameterId)
    {
        return _animator.GetFloat(parameterId);
    }

    public bool GetBool(int parameterId)
    {
        return _animator.GetBool(parameterId);
    }
}