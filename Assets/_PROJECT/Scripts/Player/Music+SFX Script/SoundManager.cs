using UnityEngine;

public class SoundManager : MonoBehaviour
{
   public static SoundManager Instance;
   
   [SerializeField] private AudioSource mainMusicSource;
   [SerializeField] private AudioClip ambientMusic;
   
   [Header("Training Music Override")]
   [SerializeField] private AudioSource trainingMusicSource;
   private AudioClip currentTrainingClip;
   
   [Header("Sound Effects")]
   [SerializeField] private AudioSource sfxSource;
   [SerializeField] private AudioClip shootSFX;
   [SerializeField] private AudioClip hitSFX;

   private bool trainingMusicActive = false;

   private void Awake()
   {
      if (Instance != null && Instance != this)
      {
         Destroy(gameObject);
         return;
      }

      Instance = this;
      DontDestroyOnLoad(gameObject);
      
   }
   
   private void Start()
   {
      PlayMainMusic();
   }

   public void PlayMainMusic()
   {
      if (ambientMusic == null || mainMusicSource == null) return;
      
      if (trainingMusicSource != null)
         trainingMusicSource.Stop();

      mainMusicSource.Stop();
      mainMusicSource.clip = ambientMusic;
      mainMusicSource.loop = true;
      mainMusicSource.Play();

      trainingMusicActive = false;
      currentTrainingClip = null;
   }

   public void StopMainMusic()
   {
      if (mainMusicSource != null)
         mainMusicSource.Stop();
   }

   public void PlayTrainingMusic(AudioClip clip)
   {
      if (clip == null || trainingMusicSource == null) return;
      
      if (mainMusicSource != null)
         mainMusicSource.Stop();
      
      
      
      trainingMusicSource.Stop();
      trainingMusicSource.clip = clip;
      trainingMusicSource.loop = true;
      trainingMusicSource.Play();
      
      currentTrainingClip = clip;
      trainingMusicActive = true;
   }

   public void StopTrainingMusic()
   {
      if (trainingMusicSource != null)
         trainingMusicSource.Stop();
      
      trainingMusicActive = false;
      currentTrainingClip = null;

      PlayMainMusic();
   }

   public void PlayShootSFX()
   {
      if (shootSFX != null) 
         sfxSource.PlayOneShot(shootSFX);
   }

   public void PlayHitSFX()
   {
      if (hitSFX != null)
         sfxSource.PlayOneShot(hitSFX);
   }

   public bool IsTrainingMusicActive()
   {
      return trainingMusicActive;
   }
}
