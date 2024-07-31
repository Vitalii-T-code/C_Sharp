using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private Camera[] _cameras;
    private int _currentCameraIndex = 0;
    private float _timer = 0f;
    private bool _firstPhase = true;

    void Start()
    {
        for (int i = 1; i < _cameras.Length; i++)
        {
            _cameras[i].gameObject.SetActive(false);
        }
        _cameras[0].gameObject.SetActive(true);
    }

    void Update()
    {
        _timer += Time.deltaTime;

        if (_firstPhase && _timer >= 1f)
        {
            SwitchCamera();
            _timer = 0f;

            if (_currentCameraIndex >= 2)
            {
                _firstPhase = false;
            }
        }
        else if (!_firstPhase && _timer >= 4f)
        {
            SwitchCamera();
            _timer = 0f;
        }
    }

    private void SwitchCamera()
    {
        _cameras[_currentCameraIndex].gameObject.SetActive(false);
        _currentCameraIndex = (_currentCameraIndex + 1) % _cameras.Length;
        _cameras[_currentCameraIndex].gameObject.SetActive(true);
    }
}
