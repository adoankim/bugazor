using System;
public static class Tags
{

    public static string Player { get { return "Player"; } }
    public static string Diskette { get { return "Diskette"; } }

    public static string[] Enemies = new string[] { Diskette };

    public static bool IsEnemyTag(string tag)
    {
        return Array.IndexOf(Enemies, tag) > -1;
    }

}
