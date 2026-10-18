using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Transform _target;
    public Vector3 offset;
    public float bodyHeight;

    public float moveSpeed;
    public float rotateSPeed;

    private Vector3 _targetPos;
    private Quaternion _targetRot;
    void Update()
    {
        if (_target is null)
            return;
        //向后偏移Z坐标
        _targetPos = _target.position + _target.forward * offset.z;
        //向上偏移Y坐标
        _targetPos += Vector3.up * offset.y;
        //向右偏移X坐标
        _targetPos += _target.right * offset.x;
        
        this.transform.position = Vector3.Lerp(this.transform.position, _targetPos, moveSpeed * Time.deltaTime);

        _targetRot = Quaternion.LookRotation(_target.position + Vector3.up * bodyHeight - this.transform.position);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, _targetRot, rotateSPeed * Time.deltaTime);
    }

    public void SetTarget(Transform playerTarget)
    {
        _target = playerTarget;
    }
}
