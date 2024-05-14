namespace FantasticVoyage.MiniBots
{
    /// <summary>
    /// Just a wrapper class to serialize beautifully in inspector.
    /// Nothing fancy.
    /// </summary>
    [System.Serializable]
    public class QuestionAndAnswer
    {
        public string Question;
        public string Answer;

        public override string ToString()
        {
            return $"{Question} : {Answer}";
        }
    }
}