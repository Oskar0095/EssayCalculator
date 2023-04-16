namespace EssayCalculator
{
	public struct EssayParameters
	{
		public int WordCharsCount;
		public int CharsCount;
		public int CharsCountNoSpace;
		public int WordsCount;
		public int SentencesCount;

		// extra parameters
		public float WordLength;
		public float SentenceLength;
		public float WordsPerParagraph;
		public float SentencesPerParagraph;


		public void CalculateLength()
		{
			WordLength = WordCharsCount / (float)WordsCount;
			SentenceLength = WordsCount / (float)SentencesCount;
		}


		public void CalculatePerParagraph(int paragraphsCount)
		{
			if (paragraphsCount > 0)
			{
				WordsPerParagraph = WordsCount / (float)paragraphsCount;
				SentencesPerParagraph = SentencesCount / (float)paragraphsCount;
			}
		}
	}
}