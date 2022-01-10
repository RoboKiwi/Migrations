using Migrations.Extensions;
using Xunit;

namespace Migrations.Tests
{
    public class StringCaseExtensionsTests
    {
        [Fact]
        public void DelimitWords()
        {
            var result = "WordWord Word word   -wordWord WORDWord WORD".DelimitWords('\0');
            Assert.Equal("word\0word\0word\0word\0word\0word\0word\0word\0word", result);
        }

        [Theory]
        [InlineData("UrlValue", "URLValue")]
        [InlineData("Url", "URL")]
        [InlineData("Id", "ID")]
        [InlineData("I", "I")]
        [InlineData("", "")]
        [InlineData(null, null)]
        [InlineData("Person", "Person")]
        [InlineData("IPhone", "iPhone")]
        [InlineData("IPhone", "IPhone")]
        [InlineData("IPhone", "I Phone")]
        [InlineData("IPhone", "I  Phone")]
        [InlineData("IPhone", " IPhone")]
        [InlineData("IPhone", " IPhone ")]
        [InlineData("IsCia", "IsCIA")]
        [InlineData("VmQ", "VmQ")]
        [InlineData("Xml2Json", "Xml2Json")]
        [InlineData("SnAkEcAsE", "SnAkEcAsE")]
        [InlineData("SnAKEcAsE", "SnA__kEcAsE")]
        [InlineData("SnAKEcAsE", "SnA__ kEcAsE")]
        [InlineData("AlreadySnakeCase", "already_snake_case_ ")]
        [InlineData("IsJsonProperty", "IsJSONProperty")]
        [InlineData("ShoutingCase", "SHOUTING_CASE")]
        [InlineData("99991231T2359599999999Z", "9999-12-31T23:59:59.9999999Z")]
        [InlineData("HiThisIsTextTimeToTest", "Hi!! This is text. Time to test.")]
        [InlineData("Building", "BUILDING")]
        [InlineData("BuildingProperty", "BUILDING Property")]
        [InlineData("BuildingProperty", "Building Property")]
        [InlineData("BuildingProperty", "BUILDING PROPERTY")]
        public void ToPascalCaseTest(string expected, string value)
        {
            Assert.Equal(expected, value.ToPascalCase());
        }

        [Theory]
        [InlineData("urlValue", "URLValue")]
        [InlineData("url", "URL")]
        [InlineData("id", "ID")]
        [InlineData("i", "I")]
        [InlineData("", "")]
        [InlineData(null, null)]
        [InlineData("person", "Person")]
        [InlineData("iPhone", "iPhone")]
        [InlineData("iPhone", "IPhone")]
        [InlineData("iPhone", "I Phone")]
        [InlineData("iPhone", "I  Phone")]
        [InlineData("iPhone", " IPhone")]
        [InlineData("iPhone", " IPhone ")]
        [InlineData("isCia", "IsCIA")]
        [InlineData("vmQ", "VmQ")]
        [InlineData("xml2Json", "Xml2Json")]
        [InlineData("snAkEcAsE", "SnAkEcAsE")]
        [InlineData("snAKEcAsE", "SnA__kEcAsE")]
        [InlineData("snAKEcAsE", "SnA__ kEcAsE")]
        [InlineData("alreadySnakeCase", "already_snake_case_ ")]
        [InlineData("isJsonProperty", "IsJSONProperty")]
        [InlineData("shoutingCase", "SHOUTING_CASE")]
        [InlineData("99991231T2359599999999Z", "9999-12-31T23:59:59.9999999Z")]
        [InlineData("hiThisIsTextTimeToTest", "Hi!! This is text. Time to test.")]
        [InlineData("building", "BUILDING")]
        [InlineData("buildingProperty", "BUILDING Property")]
        [InlineData("buildingProperty", "Building Property")]
        [InlineData("buildingProperty", "BUILDING PROPERTY")]
        public void ToCamelCaseTest(string expected, string value)
        {
            Assert.Equal(expected, value.ToCamelCase());
        }

        [Theory]
        [InlineData("url_value", "URLValue")]
        [InlineData("url", "URL")]
        [InlineData("id", "ID")]
        [InlineData("i", "I")]
        [InlineData("", "")]
        [InlineData(null, null)]
        [InlineData("person", "Person")]
        [InlineData("i_phone", "iPhone")]
        [InlineData("i_phone", "IPhone")]
        [InlineData("i_phone", "I Phone")]
        [InlineData("i_phone", "I  Phone")]
        [InlineData("i_phone", " IPhone")]
        [InlineData("i_phone", " IPhone ")]
        [InlineData("is_cia", "IsCIA")]
        [InlineData("vm_q", "VmQ")]
        [InlineData("xml2_json", "Xml2Json")]
        [InlineData("sn_ak_ec_as_e", "SnAkEcAsE")]
        [InlineData("sn_a_k_ec_as_e", "SnA__kEcAsE")]
        [InlineData("sn_a_k_ec_as_e", "SnA__ kEcAsE")]
        [InlineData("already_snake_case", "already_snake_case_ ")]
        [InlineData("is_json_property", "IsJSONProperty")]
        [InlineData("shouting_case", "SHOUTING_CASE")]
        [InlineData("9999_12_31_t23_59_59_9999999_z", "9999-12-31T23:59:59.9999999Z")]
        [InlineData("hi_this_is_text_time_to_test", "Hi!! This is text. Time to test.")]
        public void ToSnakeCaseTest(string expected, string value)
        {
            Assert.Equal(expected, value.ToSnakeCase());
        }

        [Theory]
        [InlineData("url-value", "URLValue")]
        [InlineData("url", "URL")]
        [InlineData("id", "ID")]
        [InlineData("i", "I")]
        [InlineData("", "")]
        [InlineData(null, null)]
        [InlineData("person", "Person")]
        [InlineData("i-phone", "iPhone")]
        [InlineData("i-phone", "IPhone")]
        [InlineData("i-phone", "I Phone")]
        [InlineData("i-phone", "I  Phone")]
        [InlineData("i-phone", " IPhone")]
        [InlineData("i-phone", " IPhone ")]
        [InlineData("is-cia", "IsCIA")]
        [InlineData("vm-q", "VmQ")]
        [InlineData("xml2-json", "Xml2Json")]
        [InlineData("ke-ba-bc-as-e", "KeBaBcAsE")]
        [InlineData("ke-b-a-bc-as-e", "KeB--aBcAsE")]
        [InlineData("ke-b-a-bc-as-e", "KeB-- aBcAsE")]
        [InlineData("already-kebab-case", "already-kebab-case- ")]
        [InlineData("is-json-property", "IsJSONProperty")]
        [InlineData("shouting-case", "SHOUTING-CASE")]
        [InlineData("9999-12-31-t23-59-59-9999999-z", "9999-12-31T23:59:59.9999999Z")]
        [InlineData("hi-this-is-text-time-to-test", "Hi!! This is text. Time to test.")]
        public void ToKebabCaseTest(string expected, string value)
        {
            Assert.Equal(expected, value.ToKebabCase());
        }
    }
}
