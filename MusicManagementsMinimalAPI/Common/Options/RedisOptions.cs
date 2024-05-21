namespace MusicManagementsMinimalAPI.Common.Options
{
    public class RedisOptions
    {
        public string? Connection { get; set; } //redis连接地址，端口号
        public string? InstanceName { get; set; } //实例名
        public int DefaultDB { get; set; }//Db8数据库
    }
}
