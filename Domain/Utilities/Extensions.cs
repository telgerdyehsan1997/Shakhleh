namespace Domain
{
    using System;
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;
    using Olive;
    using Olive.Entities;
    using System.Threading.Tasks;
    using Olive.Web;
    using System.Security.Claims;
    using Domain.AEB.DTOs;
    using System.Reflection;
    using System.ComponentModel;
    using System.Xml.Serialization;
    using System.Text;
    using System.Xml;
    using System.IO;
    using RestSharp;
    using Newtonsoft.Json;
    using System.Text.RegularExpressions;
    using APIHandler;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using System.Net.Sockets;
    using System.Net;
    using System.Web;
    using Microsoft.AspNetCore.Http;

    public static class Extensions
    {
        public static IDatabase Database => Context.Current.Database();


        public static async Task Archive(this IArchivable archivableObject) =>
            await Context.Current.Database().Update(archivableObject, x => x.IsDeactivated = true);

        public static async Task UnArchive(this IArchivable archivableObject) =>
            await Context.Current.Database().Update(archivableObject, x => x.IsDeactivated = false);

        public static TUser ExtractUser<TUser>(this ClaimsPrincipal @this) where TUser : IEntity
        {
            return HttpContextCache.GetOrAdd("Olive.Principal.ExtractedUser", () =>
            {
                var id = @this.GetId();
                if (id.IsEmpty()) return default(TUser);
                return Task.Factory.RunSync(() => Context.Current.Database().GetOrDefault<TUser>(id));
            });
        }
        public static async Task<string> GetContactNumber(Guid id)
        {
            var companyUser = await Database.Of<CompanyUser>().Where(x => x.ID == id).FirstOrDefault();
            var contact = await Database.Of<Contact>().Where(x => x.ID == id).FirstOrDefault();
            var person = await Database.Of<Person>().Where(x => x.ID == id).FirstOrDefault();

            var contactNumber = "";

            if (person != null)
                contactNumber = person.MobileNumber;

            if (contact != null)
                contactNumber = contact.TelephoneNumber;

            if (companyUser != null)
                contactNumber = companyUser.TelephoneNumber;

            return contactNumber;
        }
        public static DateAndZoneDTO ToDateAndZoneDTO(this DateTime date)
        {
            return new DateAndZoneDTO
            {
                DateInTimezone = $"{date:yyyy-MM-dd HH:mm:ss}",
                Timezone = TimeZone.CurrentTimeZone.StandardName
            };
        }

        public static int GetNumberOfDecimalPlaces(this decimal number) => BitConverter.GetBytes(decimal.GetBits(number)[3])[2];

        public static string GetDescription<T>(this T enumerationValue)
        where T : struct
        {
            Type type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("EnumerationValue must be of Enum type", "enumerationValue");
            }

            MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            return enumerationValue.ToString();
        }

        public static string ToXmlString<T>(this T obj, bool cleanUp = true, XmlSerializerNamespaces xmlSerializerNamespaces = null) where T : class
        {
            var myWriter = new StringWriter();
            var serializer = new XmlSerializer(obj.GetType());
            if (xmlSerializerNamespaces == null)
                serializer.Serialize(myWriter, cleanUp ? obj.CleanUp() : obj);
            else
                serializer.Serialize(myWriter, cleanUp ? obj.CleanUp() : obj, xmlSerializerNamespaces);

            return myWriter.ToString();
        }

        public static T ToObjectFromXml<T> (this string xmlString) where T : class
        {
            var ser = new XmlSerializer(typeof(T));
            try
            {
                using (TextReader reader = new StringReader(xmlString))
                {
                    return (T)ser.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static decimal RoundUpWithDecimalPlaces(this decimal number, int decimalPlaces)
        {
            return (decimal)decimal.ToDouble(number).RoundUpWithDecimalPlaces(decimalPlaces);
        }

        public static double RoundUpWithDecimalPlaces(this double number, int decimalPlaces)
        {
            var temp = Math.Pow(10, decimalPlaces);
            return Math.Ceiling(number * temp) / temp;
        }

        public static string ToJsonString<T>(this T obj) where T : class
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static bool IsAnyOf(this LogType @this, params LogType[] values)
            => values.Contains(@this);

        public static string ToErrorString(this RestResponse @this)
        {
            return $"{@this.StatusCode} {@this.ErrorMessage} {@this.Content} ";
        }

        public static string GetFileExtension<T>(this T enumerationValue)
        where T : struct
        {
            var type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("EnumerationValue must be of Enum type", "enumerationValue");
            }

            MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(FileExtensionAttribute), inherit: false);

                if (attrs != null && attrs.Length > 0)
                {
                    return ((FileExtensionAttribute)attrs[0]).FileType;
                }
            }
            return string.Empty;
        }

        public static T CleanUp<T>(this T @this) where T : class
        {
            if (@this == null)
                return default;

            var props = new List<PropertyInfo>(@this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance));

            props.ProcessMembers(@this);

            return @this;
        }
        private static void ProcessMembers<T, TClass>(this IEnumerable<T> members, TClass @this) where T : PropertyInfo where TClass : class
        {
            foreach (var member in members)
            {
                if (member.PropertyType == typeof(string))
                {
                    var propValue = member.GetGetMethod().Invoke(@this, null);
                    member.SetValue(@this, propValue.RemoveSpecials(), null);
                }
                else if ((member.GetValue(@this) as IXmlContract) != null)
                    member.GetValue(@this).CleanUp();

                else if (member.GetValue(@this) is IEnumerable<IXmlContract>)
                {
                    var list = member.GetValue(@this);
                    foreach (var item in list as IEnumerable<IXmlContract>)
                    {
                        item.CleanUp();
                    }
                }
                else if (member.PropertyType.IsArray)
                {
                    var list = member.GetValue(@this);
                    if (list != null)
                    {
                        foreach (var item in list as object[])
                        {
                            item.CleanUp();
                        }
                    }
                }
            }
        }

        public static object RemoveSpecials(this object @this)
        {
            try
            {
                return @this == null ? @this : Regex.Replace(@this.ToString(), @"[^0-9a-zA-Z:.\-/, _%]+", "").ToUpper();
            }
            catch (Exception)
            {
                return @this;
            }
        }

        public static string DeepRemoveSpecials(this string @this)
        {
            if (@this.IsEmpty()) return @this;

            try
            {
                return Regex.Replace(@this, @"[^0-9a-zA-Z]+", "").ToUpper();
            }
            catch (Exception ex)
            {
                return @this;
            }
        }

        public static string GetSubString(this string @this, int lenght)
        {
            if (@this.IsEmpty()) return @this;
            return @this.Substring(0, Math.Min(@this.Length, lenght));
        }
        public static string ReplaceQueryStringParam(string currentPageUrl, string paramToReplace, string newValue)
        {
            string urlWithoutQuery = currentPageUrl.IndexOf('?') >= 0
                ? currentPageUrl.Substring(0, currentPageUrl.IndexOf('?'))
                : currentPageUrl;

            string queryString = currentPageUrl.IndexOf('?') >= 0
                ? currentPageUrl.Substring(currentPageUrl.IndexOf('?'))
                : null;

            var queryParamList = queryString.HasValue() ? HttpUtility.ParseQueryString(queryString) : HttpUtility.ParseQueryString(string.Empty);

            if (queryParamList[paramToReplace] != null)
            {
                queryParamList[paramToReplace] = newValue;
            }
            else
            {
                queryParamList.Add(paramToReplace, newValue);
            }
            return String.Format("{0}?{1}", urlWithoutQuery, queryParamList);
        }

        public static string RemoveWhitespace(this string input)
        {
            if (input.IsEmpty())
                return input;

            return new string(input.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }

        public static string GetDateTimeString(this DateTime @this)
        {
            return $"{LocalTime.Now.Year}{LocalTime.Now.Month.ToString().PadLeft(2, '0')}{LocalTime.Now.Day.ToString().PadLeft(2, '0')}{LocalTime.Now.Hour.ToString().PadLeft(2, '0')}{LocalTime.Now.Minute.ToString().PadLeft(2, '0')}{LocalTime.Now.Second.ToString().PadLeft(2, '0')}";
        }
    }
}