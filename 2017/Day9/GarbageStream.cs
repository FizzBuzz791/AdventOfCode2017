using System.IO;

namespace Day9
{
	public class GarbageStreamReader : StringReader
	{
		public int Level { get; private set; }
		public int GroupScore { get; private set; }
		public int GarbageCount { get; private set; }

		private int _previousCharacter;
		private bool _inGarbage;

		public GarbageStreamReader(string s) : base(s)
		{
			Level = 0;
			GroupScore = 0;
			GarbageCount = 0;

			_inGarbage = false;
		}

		public override int Read()
		{
			int result = base.Read();

			if (_previousCharacter != 33) // ! or !!
			{
				switch (result)
				{
					case 33: // !
						// Do Nothing
						break;
					case 60: // <
						if (_inGarbage)
							GarbageCount++;
						else
							_inGarbage = true;
						break;
					case 62: // >
						if (_inGarbage)
							_inGarbage = false;
						break;
					case 123: // {
						if (_inGarbage)
							GarbageCount++;
						else
							Level++;
						break;
					case 125: // }
						if (Level > 0 && !_inGarbage)
						{
							GroupScore += Level;
							Level--;
						}
						else if (_inGarbage)
							GarbageCount++;
						break;
					default:
						if (_inGarbage)
							GarbageCount++;
						break;
				}
			}

			if (_previousCharacter == 33 && result == 33 || _inGarbage && result != 33)
				_previousCharacter = 0;
			else
				_previousCharacter = result;
			
			return result;
		}
	}
}