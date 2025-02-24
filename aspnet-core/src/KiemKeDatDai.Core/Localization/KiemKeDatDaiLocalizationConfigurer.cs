using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace KiemKeDatDai.Localization;

public static class KiemKeDatDaiLocalizationConfigurer
{
    public static void Configure(ILocalizationConfiguration localizationConfiguration)
    {
        localizationConfiguration.Sources.Add(
            new DictionaryBasedLocalizationSource(KiemKeDatDaiConsts.LocalizationSourceName,
                new XmlEmbeddedFileLocalizationDictionaryProvider(
                    typeof(KiemKeDatDaiLocalizationConfigurer).GetAssembly(),
                    "KiemKeDatDai.Localization.SourceFiles"
                )
            )
        );
    }
}
