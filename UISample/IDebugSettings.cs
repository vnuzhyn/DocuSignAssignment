namespace UISample
{
	public interface IDebugSettings
	{
		public bool ContinueOnFailure { get; }
		public bool AttachToExistingProcess { get; }
	}

	internal class DefaultDebugSettings : IDebugSettings
	{
		public bool ContinueOnFailure => false;
		public bool AttachToExistingProcess => false;
	}
}