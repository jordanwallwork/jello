namespace Jello
{
    public class JelloSettings
    {
        public IDateParser DateParser { get; set; }

        public JelloSettings()
        {
            DateParser = new StandardDateParser();
        }
    }
}