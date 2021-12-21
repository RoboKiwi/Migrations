using Xunit;

namespace Migrations.Tests
{


    public class StringCasingHelperTests
    {
        [Fact]
        public void ToPascalCaseTest()
        {
            Assert.Equal("UrlValue", StringCasingHelper.ToPascalCase("URLValue"));
            Assert.Equal("Url", StringCasingHelper.ToPascalCase("URL"));
            Assert.Equal("Id", StringCasingHelper.ToPascalCase("ID"));
            Assert.Equal("I", StringCasingHelper.ToPascalCase("I"));
            Assert.Equal("", StringCasingHelper.ToPascalCase(""));
            Assert.Null(StringCasingHelper.ToPascalCase(null));
            Assert.Equal("Person", StringCasingHelper.ToPascalCase("Person"));
            Assert.Equal("IPhone", StringCasingHelper.ToPascalCase("iPhone"));
            Assert.Equal("IPhone", StringCasingHelper.ToPascalCase("IPhone"));
            Assert.Equal("IPhone", StringCasingHelper.ToPascalCase("I Phone"));
            Assert.Equal("IPhone", StringCasingHelper.ToPascalCase("I  Phone"));
            Assert.Equal("IPhone", StringCasingHelper.ToPascalCase(" IPhone"));
            Assert.Equal("IPhone", StringCasingHelper.ToPascalCase(" IPhone "));
            Assert.Equal("IsCia", StringCasingHelper.ToPascalCase("IsCIA"));
            Assert.Equal("VmQ", StringCasingHelper.ToPascalCase("VmQ"));
            Assert.Equal("Xml2Json", StringCasingHelper.ToPascalCase("Xml2Json"));
            Assert.Equal("SnAkEcAsE", StringCasingHelper.ToPascalCase("SnAkEcAsE"));
            Assert.Equal("SnAKEcAsE", StringCasingHelper.ToPascalCase("SnA__kEcAsE"));
            Assert.Equal("SnAKEcAsE", StringCasingHelper.ToPascalCase("SnA__ kEcAsE"));
            Assert.Equal("AlreadySnakeCase", StringCasingHelper.ToPascalCase("already_snake_case_ "));
            Assert.Equal("IsJsonProperty", StringCasingHelper.ToPascalCase("IsJSONProperty"));
            Assert.Equal("ShoutingCase", StringCasingHelper.ToPascalCase("SHOUTING_CASE"));
            Assert.Equal("99991231T2359599999999Z", StringCasingHelper.ToPascalCase("9999-12-31T23:59:59.9999999Z"));
            Assert.Equal("HiThisIsTextTimeToTest", StringCasingHelper.ToPascalCase("Hi!! This is text. Time to test."));
            Assert.Equal("Building", StringCasingHelper.ToPascalCase("BUILDING"));
            Assert.Equal("BuildingProperty", StringCasingHelper.ToPascalCase("BUILDING Property"));
            Assert.Equal("BuildingProperty", StringCasingHelper.ToPascalCase("Building Property"));
            Assert.Equal("BuildingProperty", StringCasingHelper.ToPascalCase("BUILDING PROPERTY"));
        }

        [Fact]
        public void ToCamelCaseTest()
        {
            Assert.Equal("urlValue", StringCasingHelper.ToCamelCase("URLValue"));
            Assert.Equal("url", StringCasingHelper.ToCamelCase("URL"));
            Assert.Equal("id", StringCasingHelper.ToCamelCase("ID"));
            Assert.Equal("i", StringCasingHelper.ToCamelCase("I"));
            Assert.Equal("", StringCasingHelper.ToCamelCase(""));
            Assert.Null(StringCasingHelper.ToCamelCase(null));
            Assert.Equal("person", StringCasingHelper.ToCamelCase("Person"));
            Assert.Equal("iPhone", StringCasingHelper.ToCamelCase("iPhone"));
            Assert.Equal("iPhone", StringCasingHelper.ToCamelCase("IPhone"));
            Assert.Equal("i Phone", StringCasingHelper.ToCamelCase("I Phone"));
            Assert.Equal("i  Phone", StringCasingHelper.ToCamelCase("I  Phone"));
            Assert.Equal(" IPhone", StringCasingHelper.ToCamelCase(" IPhone"));
            Assert.Equal(" IPhone ", StringCasingHelper.ToCamelCase(" IPhone "));
            Assert.Equal("isCIA", StringCasingHelper.ToCamelCase("IsCIA"));
            Assert.Equal("vmQ", StringCasingHelper.ToCamelCase("VmQ"));
            Assert.Equal("xml2Json", StringCasingHelper.ToCamelCase("Xml2Json"));
            Assert.Equal("snAkEcAsE", StringCasingHelper.ToCamelCase("SnAkEcAsE"));
            Assert.Equal("snA__kEcAsE", StringCasingHelper.ToCamelCase("SnA__kEcAsE"));
            Assert.Equal("snA__ kEcAsE", StringCasingHelper.ToCamelCase("SnA__ kEcAsE"));
            Assert.Equal("already_snake_case_ ", StringCasingHelper.ToCamelCase("already_snake_case_ "));
            Assert.Equal("isJSONProperty", StringCasingHelper.ToCamelCase("IsJSONProperty"));
            Assert.Equal("shoutinG_CASE", StringCasingHelper.ToCamelCase("SHOUTING_CASE"));
            Assert.Equal("9999-12-31T23:59:59.9999999Z", StringCasingHelper.ToCamelCase("9999-12-31T23:59:59.9999999Z"));
            Assert.Equal("hi!! This is text. Time to test.", StringCasingHelper.ToCamelCase("Hi!! This is text. Time to test."));
            Assert.Equal("building", StringCasingHelper.ToCamelCase("BUILDING"));
            Assert.Equal("building Property", StringCasingHelper.ToCamelCase("BUILDING Property"));
            Assert.Equal("building Property", StringCasingHelper.ToCamelCase("Building Property"));
            Assert.Equal("building PROPERTY", StringCasingHelper.ToCamelCase("BUILDING PROPERTY"));
        }

        [Fact]
        public void ToSnakeCaseTest()
        {
            Assert.Equal("url_value", StringCasingHelper.ToSnakeCase("URLValue"));
            Assert.Equal("url", StringCasingHelper.ToSnakeCase("URL"));
            Assert.Equal("id", StringCasingHelper.ToSnakeCase("ID"));
            Assert.Equal("i", StringCasingHelper.ToSnakeCase("I"));
            Assert.Equal("", StringCasingHelper.ToSnakeCase(""));
            Assert.Null(StringCasingHelper.ToSnakeCase(null));
            Assert.Equal("person", StringCasingHelper.ToSnakeCase("Person"));
            Assert.Equal("i_phone", StringCasingHelper.ToSnakeCase("iPhone"));
            Assert.Equal("i_phone", StringCasingHelper.ToSnakeCase("IPhone"));
            Assert.Equal("i_phone", StringCasingHelper.ToSnakeCase("I Phone"));
            Assert.Equal("i_phone", StringCasingHelper.ToSnakeCase("I  Phone"));
            Assert.Equal("i_phone", StringCasingHelper.ToSnakeCase(" IPhone"));
            Assert.Equal("i_phone", StringCasingHelper.ToSnakeCase(" IPhone "));
            Assert.Equal("is_cia", StringCasingHelper.ToSnakeCase("IsCIA"));
            Assert.Equal("vm_q", StringCasingHelper.ToSnakeCase("VmQ"));
            Assert.Equal("xml2_json", StringCasingHelper.ToSnakeCase("Xml2Json"));
            Assert.Equal("sn_ak_ec_as_e", StringCasingHelper.ToSnakeCase("SnAkEcAsE"));
            Assert.Equal("sn_a__k_ec_as_e", StringCasingHelper.ToSnakeCase("SnA__kEcAsE"));
            Assert.Equal("sn_a__k_ec_as_e", StringCasingHelper.ToSnakeCase("SnA__ kEcAsE"));
            Assert.Equal("already_snake_case_", StringCasingHelper.ToSnakeCase("already_snake_case_ "));
            Assert.Equal("is_json_property", StringCasingHelper.ToSnakeCase("IsJSONProperty"));
            Assert.Equal("shouting_case", StringCasingHelper.ToSnakeCase("SHOUTING_CASE"));
            Assert.Equal("9999-12-31_t23:59:59.9999999_z", StringCasingHelper.ToSnakeCase("9999-12-31T23:59:59.9999999Z"));
            Assert.Equal("hi!!_this_is_text._time_to_test.", StringCasingHelper.ToSnakeCase("Hi!! This is text. Time to test."));
        }

        [Fact]
        public void ToKebabCaseTest()
        {
            Assert.Equal("url-value", StringCasingHelper.ToKebabCase("URLValue"));
            Assert.Equal("url", StringCasingHelper.ToKebabCase("URL"));
            Assert.Equal("id", StringCasingHelper.ToKebabCase("ID"));
            Assert.Equal("i", StringCasingHelper.ToKebabCase("I"));
            Assert.Equal("", StringCasingHelper.ToKebabCase(""));
            Assert.Null(StringCasingHelper.ToKebabCase(null));
            Assert.Equal("person", StringCasingHelper.ToKebabCase("Person"));
            Assert.Equal("i-phone", StringCasingHelper.ToKebabCase("iPhone"));
            Assert.Equal("i-phone", StringCasingHelper.ToKebabCase("IPhone"));
            Assert.Equal("i-phone", StringCasingHelper.ToKebabCase("I Phone"));
            Assert.Equal("i-phone", StringCasingHelper.ToKebabCase("I  Phone"));
            Assert.Equal("i-phone", StringCasingHelper.ToKebabCase(" IPhone"));
            Assert.Equal("i-phone", StringCasingHelper.ToKebabCase(" IPhone "));
            Assert.Equal("is-cia", StringCasingHelper.ToKebabCase("IsCIA"));
            Assert.Equal("vm-q", StringCasingHelper.ToKebabCase("VmQ"));
            Assert.Equal("xml2-json", StringCasingHelper.ToKebabCase("Xml2Json"));
            Assert.Equal("ke-ba-bc-as-e", StringCasingHelper.ToKebabCase("KeBaBcAsE"));
            Assert.Equal("ke-b--a-bc-as-e", StringCasingHelper.ToKebabCase("KeB--aBcAsE"));
            Assert.Equal("ke-b--a-bc-as-e", StringCasingHelper.ToKebabCase("KeB-- aBcAsE"));
            Assert.Equal("already-kebab-case-", StringCasingHelper.ToKebabCase("already-kebab-case- "));
            Assert.Equal("is-json-property", StringCasingHelper.ToKebabCase("IsJSONProperty"));
            Assert.Equal("shouting-case", StringCasingHelper.ToKebabCase("SHOUTING-CASE"));
            Assert.Equal("9999-12-31-t23:59:59.9999999-z", StringCasingHelper.ToKebabCase("9999-12-31T23:59:59.9999999Z"));
            Assert.Equal("hi!!-this-is-text.-time-to-test.", StringCasingHelper.ToKebabCase("Hi!! This is text. Time to test."));
        }
    }
}
