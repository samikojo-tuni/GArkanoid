namespace GA.GArkanoid.Persistance
{
	/// <summary>
	/// Interface for objects that can be saved and loaded.
	/// </summary>
	public interface ISaveable
	{
		/// <summary>
		/// A unique ID which identifies the object between play sessions.
		/// </summary>
		string ID { get; }

		/// <summary>
		/// Save the object to a file.
		/// </summary>
		void Save(BinarySaver write);

		/// <summary>
		/// Load the object from a file.
		/// </summary>
		void Load(BinarySaver reader);
	}
}
