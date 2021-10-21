using UnityEngine;

public class Clouds : MonoBehaviour
{
   [SerializeField] private PollutionDisplay _pollutionDisplay;
   [SerializeField] private Gradient[] _gradients;
   [SerializeField] private ParticleSystem _particleSystem;

   private float _firstThreshold = 0.25f;
   private float _secondThreshold = 0.75f;
   
   private void Awake()
   {
      _pollutionDisplay.NotifyPollutionChanged += OnPollutionChanged;
   }

   private void OnPollutionChanged(float pollution)
   {
      int group = 0;
      var normalisedPollution = pollution / _pollutionDisplay.MaxPollution;
      if (normalisedPollution > _firstThreshold)
      {
         group++;
      }

      if (normalisedPollution > _secondThreshold)
      {
         group++;
      }

      var mainModule = _particleSystem.main;
      mainModule.startColor = _gradients[group];
   }
}
