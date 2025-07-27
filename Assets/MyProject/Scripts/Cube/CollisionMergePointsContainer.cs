using UnityEngine;

namespace ChainCube.Scripts.Cube
{
    [RequireComponent(typeof(PointsContainerCollisionDetector), typeof(PointsContainer))]
    public class CollisionMergePointsContainer : MonoBehaviour
    {
        private PointsContainer _pointsContainer;
        private PointsContainerCollisionDetector _detector;
        [SerializeField] private AudioClip damageSoundClip;

        //private AudioSource audioSource;

        private void Start()
        {
            _pointsContainer = GetComponent<PointsContainer>();
            _detector = GetComponent<PointsContainerCollisionDetector>();
            Subscribe();

            //audioSource = GetComponent<AudioSource>(); 
        }

        private void OnPointsContainerCollision(PointsContainer col)
        {
            if (col.points == _pointsContainer.points)
            {
                _pointsContainer.points *= 2;
                Destroy(col.gameObject);

                //audioSource.clip = damageSoundClip;
                //audioSource.Play();
                SoundFXManager.instance.PlaySoundeFXClip(damageSoundClip, transform);

                ScoreManager.instance.AddPoint();
            }
        }

        private void Subscribe()
        {
            _detector.onCollisionContinue += OnPointsContainerCollision;
        }

        private void Unsubscribe()
        {
            _detector.onCollisionContinue -= OnPointsContainerCollision;
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }
    }
}
