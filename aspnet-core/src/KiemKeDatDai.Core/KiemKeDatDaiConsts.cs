using KiemKeDatDai.Debugging;

namespace KiemKeDatDai;

public class KiemKeDatDaiConsts
{
    public const string LocalizationSourceName = "KiemKeDatDai";

    public const string ConnectionStringName = "Default";

    public const bool MultiTenancyEnabled = true;


    /// <summary>
    /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
    /// </summary>
    public static readonly string DefaultPassPhrase =
        DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "dc53d91464154910a3232088ab3aa86c";
}
