namespace PocMediatR.Common.Utils
{
    public static class Translation
    {
        public static string GetTranslatedMessage(string resourceItem, params string[] values)
        {
            var resource = Translations.Resources.Messages.ResourceManager;

            return string.Format(resource.GetString(resourceItem) ?? resourceItem, values);
        }
    }
}
