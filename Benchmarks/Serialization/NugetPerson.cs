using MessagePack;

namespace Benchmarks.Serialization
{
    [MessagePackObject]
    public class NugetPerson
    {
        [Key(0)]
        public string? Name { get; set; }

        [Key(1)]
        public int Age { get; set; }
    }
}
