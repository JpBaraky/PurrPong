using UnityEngine;

public class RoombaController: MonoBehaviour {
    public float upDistance = 2f;
    public float downDistance = 2f;
    public float rotationSpeed = 180f;
    public float moveSpeed = 2f;
    public float pauseTime = 1f;

    private bool movingUp = true;

    private void Start() {
        StartCoroutine(MoveRoutine());
    }

    private System.Collections.IEnumerator MoveRoutine() {
        while(true) {
            if(movingUp) {
                yield return StartCoroutine(MoveObject(transform.position + Vector3.up * upDistance,moveSpeed));
                yield return StartCoroutine(RotateObject(180f,rotationSpeed));
                movingUp = false;
            } else {
                yield return StartCoroutine(MoveObject(transform.position + Vector3.down * downDistance,moveSpeed));
                yield return StartCoroutine(RotateObject(180f,rotationSpeed));
                movingUp = true;
            }

            yield return new WaitForSeconds(pauseTime);
        }
    }

    private System.Collections.IEnumerator MoveObject(Vector3 targetPosition,float speed) {
        Vector3 startPosition = transform.position;
        float distance = Vector3.Distance(startPosition,targetPosition);
        float duration = distance / speed;

        float elapsedTime = 0f;
        while(elapsedTime < duration) {
            transform.position = Vector3.Lerp(startPosition,targetPosition,elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }

    private System.Collections.IEnumerator RotateObject(float targetRotation,float speed) {
        Quaternion startRotation = transform.rotation;
        Quaternion targetQuaternion = Quaternion.Euler(0f,0f,startRotation.eulerAngles.z + targetRotation);
        float duration = Mathf.Abs(targetRotation) / speed;

        float elapsedTime = 0f;
        while(elapsedTime < duration) {
            transform.rotation = Quaternion.Lerp(startRotation,targetQuaternion,elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetQuaternion;
    }
}