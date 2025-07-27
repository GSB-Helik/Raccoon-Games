using UnityEngine;

namespace ChainCube.Scripts.Cube
{
    [RequireComponent(typeof(PointsContainer))]
    public class RandomPointsGenerator : MonoBehaviour
    {
        [SerializeField] private byte _minDegree = 0;
        [SerializeField] private byte _maxDegree = 2;

        // Probability weights for each possible power of 2 (e.g., 4, 8, 16)
         private int[] _probabilityWeights = { 75, 25, 0 }; // 70% for 4, 20% for 8, 10% for 16

        private PointsContainer _pointsContainer;
        private const byte defaultMinDegree = 1;
        private const byte defaultMaxDegree = 2;

        private void Start()
        {
            NormalizeDegree();
            _pointsContainer = GetComponent<PointsContainer>();
            _pointsContainer.points = GetRandomPointsByProbability();
        }

        private int GetRandomPointsByProbability()
        {
            // Calculate total weight
            int totalWeight = 0;
            foreach (int weight in _probabilityWeights)
            {
                totalWeight += weight;
            }

            // Generate a random number within the total weight
            int randomValue = Random.Range(0, totalWeight);
            int currentWeightSum = 0;

            // Determine which power of 2 to pick based on probability
            for (int i = 0; i < _probabilityWeights.Length; i++)
            {
                currentWeightSum += _probabilityWeights[i];
                if (randomValue < currentWeightSum)
                {
                    return (int)Mathf.Pow(2, _minDegree + i);
                }
            }

            // Fallback (shouldn't reach here if weights are correct)
            return (int)Mathf.Pow(2, Random.Range(_minDegree, _maxDegree));
        }

        private void NormalizeDegree()
        {
            if (_maxDegree > _minDegree)
                return;

            Debug.LogError("Invalid degree range or probability weights!");
            _minDegree = defaultMinDegree;
            _maxDegree = defaultMaxDegree;
            _probabilityWeights = new[] { 75, 25, 0 }; // Default weights (4:70%, 8:20%, 16:10%)
        }
    }
}