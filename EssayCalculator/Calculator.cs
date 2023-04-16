namespace EssayCalculator
{
	public class Calculator
	{
		private enum State { Start, WordChar, WhiteSpace, SentenceEnd, SpecialChar }

		private static readonly char[] SpecialChars = new char[] { ',', '"', '\'', '\\', '/', '*', '-', '+', '(', ')', '_', '<', '>', '`', '~', '@', '#', '$', '%', '^', '&'};
		private static readonly char[] EndingChars = new char[] { '.', '!', '?'};

		public bool ExtraInfo { get; set; } = false;

		public EssayParameters CalculateEssayParameters(string essay, int paragraphs=0)
		{
			State state = State.Start;
			int length = essay.Length;

			EssayParameters parameters = new();
			parameters.CharsCount = length;

			for (int i = 0; i < length; ++i)
			{
				switch (state)
				{
					case State.Start:
						if (char.IsLetterOrDigit(essay[i]))
							state = State.WordChar;
						break;

					case State.WordChar:
						if (char.IsWhiteSpace(essay[i]))
						{
							state = State.WhiteSpace;
							++parameters.WordsCount;
						}
						else if (SpecialChars.Contains(essay[i]))
						{
							state = State.SpecialChar;
							++parameters.WordsCount;
						}
						else if (EndingChars.Contains(essay[i]) && (i + 1 == length || char.IsWhiteSpace(essay[i + 1])))
						{
							state = State.SentenceEnd;
							++parameters.WordsCount;
							++parameters.SentencesCount;
						}
						break;

					case State.WhiteSpace:
						if (char.IsLetterOrDigit(essay[i]))
						{
							state = State.WordChar;
						}
						else if (EndingChars.Contains(essay[i]))
						{
							state = State.SentenceEnd;
							++parameters.SentencesCount;
						}
						else if (SpecialChars.Contains(essay[i]))
						{
							state = State.SpecialChar;
						}
						break;

					case State.SentenceEnd:
						if (char.IsLetterOrDigit(essay[i]))
						{
							state = State.WordChar;
						}
						else if (SpecialChars.Contains(essay[i]))
						{
							state = State.SpecialChar;
						}
						else if (char.IsWhiteSpace(essay[i]))
						{
							state = State.WhiteSpace;
						}
						break;

					case State.SpecialChar:
						if (EndingChars.Contains(essay[i]))
						{
							state = State.SentenceEnd;
							++parameters.SentencesCount;
						}
						else if (char.IsLetterOrDigit(essay[i]))
						{
							state = State.WordChar;
						}
						break;
				}

				if (state != State.WhiteSpace)
				{
					++parameters.CharsCountNoSpace;
					if (state == State.WordChar)
					{
						++parameters.WordCharsCount;
					}
				}
			}
			if (ExtraInfo)
			{
				parameters.CalculateLength();
				parameters.CalculatePerParagraph(paragraphs);
			}
			return parameters;
		}
	}
}