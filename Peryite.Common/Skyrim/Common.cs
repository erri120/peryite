using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Peryite.Common.Skyrim
{
    /// <summary>
    /// A string prefixed with a uint16 length. NOT zero terminated.
    /// Uses the Windows-1252 encoding.
    /// </summary>
    public struct WString
    {
        /// <summary>
        /// Length of <see cref="String"/>
        /// </summary>
        public ushort Prefix;

        /// <summary>
        /// The actual String
        /// </summary>
        public string String;

        #region Operators

        public static implicit operator WString(string s)
        {
            return new WString { String = s } ;
        }

        public static bool operator == (WString s1, WString s2)
        {
            return s1.String == s2.String;
        }

        public static bool operator !=(WString s1, WString s2)
        {
            return s1.String != s2.String;
        }

        #endregion

        #region Overrides

        public override bool Equals(object obj)
        {
            if (obj is string s)
            {
                return String == s;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return String;
        }

        #endregion
    }

    /// <summary>
    ///     FormIDs in save files are stored as 3 bytes, rather than the usual 4 byte uint32 formID. These will be referred to
    ///     as RefID.
    /// </summary>
    public struct RefID
    {
        public byte PluginID;
        public uint ID;
    }

    /// <summary>
    /// VSVAL (Variable-sized value)
    /// </summary>
    public struct VSVAL
    {
        public byte Type;
        public uint Value;

        #region Operators

        public static implicit operator VSVAL(int i)
        {
            return new VSVAL { Value = (uint)i, Type = 2 } ;
        }

        public static bool operator ==(VSVAL v1, VSVAL v2)
        {
            return v1.Value == v2.Value;
        }

        public static bool operator !=(VSVAL v1, VSVAL v2)
        {
            return v1.Value != v2.Value;
        }

        public static bool operator <(VSVAL v1, VSVAL v2)
        {
            return v1.Value < v2.Value;
        }

        public static bool operator >(VSVAL v1, VSVAL v2)
        {
            return v1.Value > v2.Value;
        }

        #endregion

        #region Overrides

        public override bool Equals(object obj)
        {
            if (obj is int i)
            {
                return Value == i;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        #endregion
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ReadAttribute : Attribute
    {
        public int Order;
        public Type? EnumType;
        public bool IsCustomType;

        public ReadAttribute(int order)
        {
            Order = order;
        }
    }

    public class ConditionalParsing : Attribute
    {
        public Type? Type;
        public bool And;
        public bool Not;
        public object[]? Chaining;
        public Type? ChainingType;
        public string? Name;
    }

    public static partial class BinaryReaderExtensions
    {
        /// <summary>
        /// This very useful extension uses Reflection to read all needed data from the save file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="br"></param>
        /// <returns></returns>
        public static T ReadThis<T>(this T t, BinaryReader br)
        {
            if (t == null)
                return default!;

            // we start by getting all public properties and fields from the class
            // they need the ReadAttribute and an order because GetMembers() is not sorted
            // adding the MemberInfo and the ReadAttribute to a dictionary will simplify some
            // later operations

            var dic = new Dictionary<MemberInfo, ReadAttribute>();

            IOrderedEnumerable<MemberInfo> members = t.GetType().GetMembers()
                .Where(m => m.MemberType == MemberTypes.Property || m.MemberType == MemberTypes.Field)
                .OrderBy(m =>
                {
                    var attr = (ReadAttribute[]) m.GetCustomAttributes(typeof(ReadAttribute));

                    if (attr.Length != 1)
                        return 0;

                    var current = attr[0];
                    dic.Add(m, current);
                    return current.Order;
                });

            members.Do(m =>
            {
                if (!dic.TryGetValue(m, out var attribute))
                    return;

                /*
                 *  we start by checking if we should even read and fill the member
                 *  if the Member has the ConditionalParsing Attribute, we need to determine
                 *  whether we should parse it
                 */

                var conditionalAttributes = (ConditionalParsing[]) m.GetCustomAttributes(typeof(ConditionalParsing));

                var statement = true;

                if (conditionalAttributes.Length == 1)
                {
                    var cur = conditionalAttributes[0];

                    if (cur.Chaining == null || cur.Chaining.Length == 0)
                        return;
                    
                    /*
                     * Example condition if the type is an Enum:
                     * if (CrimeType == CrimeType.Theft || CrimeType == CrimeType.Pickpocketing)
                     *
                     * translating that means getting the compare value first since enums
                     * can be represented using integer based values, we can simply convert
                     * it to the appropriate type using Convert.ChangeType
                     */

                    var isEnum = cur.Type != null && cur.Type.IsEnum;
                    var isName = !string.IsNullOrWhiteSpace(cur.Name);

                    var (key, readAttribute) = dic.First(x =>
                    {
                        var (memberInfo, _) = x;

                        if (memberInfo is FieldInfo fieldInfo)
                        {
                            if(isName)
                                return fieldInfo.Name == cur.Name;
                            if(isEnum)
                                return fieldInfo.FieldType == cur.Type;
                        }

                        if (!(memberInfo is PropertyInfo propertyInfo)) return false;

                        if(isName)
                            return propertyInfo.Name == cur.Name;
                        if(isEnum)
                            return propertyInfo.PropertyType == cur.Type;


                        return false;
                    });

                    object? compareValueObject = null;

                    if (key is FieldInfo compareField)
                    {
                        compareValueObject = compareField.GetValue(t);
                    } else if (key is PropertyInfo compareProperty)
                    {
                        compareValueObject = compareProperty.GetValue(t);
                    }

                    if (compareValueObject == null)
                        return;

                    object? compareValue = null;

                    if (isEnum)
                    {
                        // here we convert the enum to an integer based value, eg: CrimeType.Theft becomes 0
                        compareValue = Convert.ChangeType(compareValueObject, readAttribute.EnumType);
                    } else if (isName)
                    {
                        compareValue = Convert.ChangeType(compareValueObject, cur.ChainingType);
                    }

                    if (compareValue == null)
                        return;

                    // simple compare function using .Equals instead of ==
                    bool CompareFunc(object x)
                    {
                        var value = Convert.ChangeType(x, cur.ChainingType);
                        if(cur.Not)
                            return value.GetType() != compareValue.GetType() && value.Equals(compareValue);
                        return value.GetType() == compareValue.GetType() && value.Equals(compareValue);
                    }

                    // And = true means ALL conditions need to be true so we use the .All extension
                    // for And = true it's the opposite: .Any
                    statement = cur.And ? cur.Chaining.All(CompareFunc) : cur.Chaining.Any(CompareFunc);
                }

                // if the condition, parsed above, is not true we exit
                if (!statement)
                    return;

                // properties are only used for counts which have a special setter
                // that initializes an array

                if (m is PropertyInfo p)
                {
                    if(p.PropertyType == typeof(uint))
                        p.SetValue(t, br.ReadUInt32());
                    else if(p.PropertyType == typeof(VSVAL))
                        p.SetValue(t, br.ReadVSVAL());

                    return;
                }

                if (!(m is FieldInfo f)) return;

                if (!f.IsPublic)
                    return;

                /*
                 * arrays are very funny:
                 * arrays are being initialized by the property setter of a count which is
                 * read before. We then iterate of the array and call the appropriate Read
                 * Extension
                 */

                if (f.FieldType.IsArray)
                {
                    var arr = (Array) f.GetValue(t);
                    if (arr == null || arr.Length == 0)
                        return;
                    var type = arr.GetType().HasElementType ? arr.GetType().GetElementType() :
                        arr.GetValue(0)?.GetType();
                    if (type == null)
                        return;
                    for (var i = 0; i < arr.Length; i++)
                    {
                        object instance;

                        if (type == typeof(RefID))
                            instance = br.ReadRefID();
                        else if (type == typeof(WString))
                            instance = br.ReadWString();
                        else if (type == typeof(VSVAL))
                            instance = br.ReadVSVAL();
                        else
                            instance = Activator.CreateInstance(type).ReadThis(br);

                        arr.SetValue(instance, i);
                    }
                    f.SetValue(t, arr);
                } else if (f.FieldType.IsEnum)
                {
                    if (attribute.EnumType == null)
                        return;

                    FillFieldInfo(ref f, br, attribute.EnumType, t);
                } else if (attribute.IsCustomType)
                {
                    var value = Activator.CreateInstance(f.FieldType).ReadThis(br);
                    f.SetValue(t, value);
                }
                else
                {
                    FillFieldInfo(ref f, br, f.FieldType, t);
                }
            });

            return t;
        }

        public static void FillFieldInfo(ref FieldInfo f, BinaryReader br, Type type, object o)
        {
            if(type == typeof(byte))
                f.SetValue(o, br.ReadByte());
            else if(type == typeof(uint))
                f.SetValue(o, br.ReadUInt32());
            else if(type == typeof(ushort))
                f.SetValue(o, br.ReadUInt16());
            else if(type == typeof(int))
                f.SetValue(o, br.ReadInt32());
            else if(type == typeof(float))
                f.SetValue(o, br.ReadSingle());
            else if(type == typeof(WString))
                f.SetValue(o, br.ReadWString());
            else if(type == typeof(RefID))
                f.SetValue(o, br.ReadRefID());
            else if(type == typeof(VSVAL))
                f.SetValue(o, br.ReadVSVAL());
            else
                throw new ArgumentException($"The provided type {type} is not valid!");
        }

        public static WString ReadWString(this BinaryReader br)
        {
            var result = new WString
            {
                Prefix = br.ReadUInt16()
            };

            result.String = new string(br.ReadChars(result.Prefix));

            return result;
        }

        public static DateTime ReadFileTime(this BinaryReader br)
        {
            byte[] b = br.ReadBytes(8);
            var fileTime = BitConverter.ToInt64(b, 0);
            return DateTime.FromFileTimeUtc(fileTime);
        }

        public static RefID ReadRefID(this BinaryReader br)
        {
            var byte1 = br.ReadByte();
            var byte2 = br.ReadByte();
            var byte3 = br.ReadByte();

            //the global variable "DragonsAbsorbed" (0x0001C0F2) becomes the bytes: 41 C0 F2
            
            var type = byte1 >> 2;
            var raw = (byte1 + byte2 + byte3) << 2;

            switch (type)
            {
                case 0: break;
                case 1: break;
                case 2: break;
                case 3: break;
                //Impossible
                default: break;
            }

            return default;
        }

        public static VSVAL ReadVSVAL(this BinaryReader br)
        {
            var res = new VSVAL();

            var byte1 = br.ReadByte();

            res.Type = (byte) (byte1 & 0x3);

            if (res.Type == 0)
            {
                res.Value = (uint)(byte1 >> 2);
            } else if (res.Type >= 1 && res.Type <= 2)
            {
                var byte2 = br.ReadByte();
                if(res.Type == 1)
                {
                    res.Value = (uint) (byte1 | byte2 << 8) >> 2;
                }
                else
                {
                    var byte3 = br.ReadByte();
                    res.Value = (uint) (byte1 | (byte2 << 8) | (byte3 << 16) >> 2);
                }
            }
            else
            {
                throw new CorruptedSaveFileException($"VSVAL Type is {res.Type} but can only be 0, 1 or 2!", br);
            }

            return res;
        }
    }
}
