using System.Runtime.Serialization;

namespace SimpleMvcSitemap
{
    [DataContract]
    public enum ChangeFrequency
    {
        [EnumMember(Value = "always")]
        Always,

        [EnumMember(Value = "hourly")]
        Hourly,

        [EnumMember(Value = "daily")]
        Daily,

        [EnumMember(Value = "weekly")]
        Weekly,

        [EnumMember(Value = "monthly")]
        Monthly,

        [EnumMember(Value = "yearly")]
        Yearly,

        [EnumMember(Value = "never")]
        Never
    }
}