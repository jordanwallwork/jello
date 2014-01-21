namespace Jello
{
    public class Settings
    {
        public IDateParser DateParser { get; set; }

        public Settings()
        {
            DateParser = new StandardDateParser();
        }
    }
}