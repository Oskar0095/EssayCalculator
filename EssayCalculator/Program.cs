using EssayCalculator;
using System.Text;
using System.Diagnostics;

public static class Program
{
	private enum Command { None, Cls, E, D}

	public static void Main()
	{
		Console.WriteLine($"'cls' - clear console; '-E' - enable extra info; '-D' - disable extra info;" + Environment.NewLine);
		Calculator calculator = new();

		while (true)
		{
			Console.WriteLine($"Enter your essay: {Environment.NewLine}");
			int paragraphs = 0;

			Command command = Command.None;
			StringBuilder sb = new();
			Stopwatch lastRead = new();
			while (true)
			{
				string? line = Console.ReadLine();

				// Check for commands
				command = GetCommand(line);
				if (command != Command.None)
				{
					break;
				}

				// Check for time elapsed from last line read
				if (lastRead.ElapsedMilliseconds / 1000f < 0.15f)
				{
					++paragraphs;
					sb.Append(line);
					break;
				}
				lastRead.Restart();

				if (string.IsNullOrWhiteSpace(line))
				{
					--paragraphs;
					break;
				}
				++paragraphs;
				sb.Append(line);
			}
			string essay = sb.ToString();

			string nl = Environment.NewLine;
			if (essay != null && essay != string.Empty && command == Command.None)
			{
				EssayParameters parameters = calculator.CalculateEssayParameters(essay, paragraphs);
				Console.WriteLine($"{nl}Characters: {parameters.CharsCount}" + nl +
					$"Characters no space: {parameters.CharsCountNoSpace}" + nl +
					$"Words: {parameters.WordsCount}" + nl +
					$"Sentences: {parameters.SentencesCount}" + nl +
					$"Paragraphs: {paragraphs}" + nl + nl);
				if (calculator.ExtraInfo)
				{
					Console.WriteLine($"Word length: {parameters.WordLength}" + nl +
						$"Sentence length: {parameters.SentenceLength}" + nl +
						$"Words per paragraph: {parameters.WordsPerParagraph}" + nl +
						$"Sentences per paragraph: {parameters.SentencesPerParagraph}" + nl + nl);
				}
			}
			else if (command != Command.None)
			{
				switch (command)
				{
					case Command.Cls:
						Console.Clear();
						break;
					case Command.E:
						calculator.ExtraInfo = true;
						Console.WriteLine("Enabled extra info." + Environment.NewLine);
						break;
					case Command.D:
						calculator.ExtraInfo = false;
						Console.WriteLine("Disabled extra info." + Environment.NewLine);
						break;
				}
			}
			else
			{
				Console.WriteLine("Cannot calculate because no text were given." + nl + nl);
			}
		}


		static Command GetCommand(string? input)
		{
			if (input != null)
			{
				string preprocessed = input.Trim().ToLower();
				if (preprocessed == "cls")
				{
					return Command.Cls;
				}
				else if (preprocessed == "-e")
				{
						return Command.E;
				}
				else if (preprocessed == "-d")
				{
						return Command.D;
				}
				else
				{
					return Command.None;
				}
			}
			else
			{
				return Command.None;
			}
		}
	}
}