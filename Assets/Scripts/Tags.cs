using System;
public static class Tags
{

    public static string Player { get { return "Player"; } }
    public static string Diskette { get { return "Diskette"; } }
    public static string Boss { get { return "Boss"; } }
    public static string EnemyAttack { get { return "EnemyAttack"; } }

    public static string[] Enemies = new string[] { Diskette, Boss };

    public static bool IsEnemyTag(string tag)
    {
        return Array.IndexOf(Enemies, tag) > -1;
    }

}
