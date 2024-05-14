using Meta.WitAi;
using Meta.WitAi.TTS.Utilities;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FantasticVoyage.MiniBots
{
    public class FetchVoiceText : MonoBehaviour
    {
        #region Properties

        [Header("References")]
        [SerializeField]
        [Tooltip("Reference to the VoiceService, sends events with transcripts of voice-to-text.")]
        VoiceService voiceService;

        [SerializeField]
        [Tooltip("Reference to the TTSSpeaker, which will read out loud text.")]
        TTSSpeaker speaker;

        [Header("Tweakables")]
        [SerializeField]
        [Tooltip("The \'Database\' of questions and answers for the minibot.")]
        List<QuestionAndAnswer> questionsAndAnswers;

        #endregion

        #region Unity callbacks

        void OnEnable()
        {
            //More efficient than !=, does not call operator
            if (voiceService is null)
                return;

            //Subscribe to events
            voiceService.VoiceEvents.OnPartialTranscription.AddListener(OnTranscriptionChange);
            voiceService.VoiceEvents.OnError.AddListener(OnError);
        }

        void OnDisable()
        {
            //More efficient than !=, does not call operator
            if (voiceService is null)
                return;

            //Unsubscribe
            voiceService.VoiceEvents.OnPartialTranscription.RemoveListener(OnTranscriptionChange);
            voiceService.VoiceEvents.OnError.RemoveListener(OnError);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Called whenever the voice service has a new transcript.
        /// Not necessarily when done.
        /// </summary>
        /// <param name="text">The voice-to-text as string value.</param>
        void OnTranscriptionChange(string text)
        {
            Debug.Log($"[FetchVoiceText] Transcription Changed {text}");
            MatchTextToQuestion(text);
        }

        void OnError(string status, string error)
        {
#if UNITY_EDITOR
            Debug.LogError($"[FetchVoiceText] Error {status} - {error}");
#endif
        }

        /// <summary>
        /// Matches the transcript to the database of questions and answers.
        /// 
        /// Developer insight:
        /// Could definitely be improved by using intents and defining context in wit website configration,
        /// just like in the shape color changer - but considering I was not provided access and project
        /// was already setuped with a token, I will assume that it is out of scope of the assignment.
        /// </summary>
        void MatchTextToQuestion(string text)
        {
            //TODO:
            //Potentially improve the matching and storage of questions and answers.
            var questionAndAnswer = questionsAndAnswers.FirstOrDefault(qa=> qa.Question.Equals(text));
            if (questionAndAnswer is null)
                return;

            speaker.Speak(questionAndAnswer.Answer);
        }

        #endregion
    }
}
