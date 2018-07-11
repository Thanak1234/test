using K2API = SourceCode.Workflow.Client;
using SourceCode.Workflow.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Workflow.K2Service.Cores {
    public class DataFieldCollection : Collection<DataField> {

        public static DataFieldCollection FromApi(K2API.DataFields dataFields) {
            DataFieldCollection dataFieldCollection = new DataFieldCollection();
            foreach (K2API.DataField dataField in dataFields) {
                if (dataField != null) {
                    dataFieldCollection.Add(new DataField {
                        Name = dataField.Name,
                        Value = ConvertToString(dataField.Value)
                    });
                }
            }
            return dataFieldCollection;
        }

        public void ToApi(K2API.DataFields dataFields) {
            Dictionary<string, K2API.DataField> dictionary = new Dictionary<string, K2API.DataField>(dataFields.Count);
            foreach (K2API.DataField dataField in dataFields) {
                if (dataField != null) {
                    dictionary.Add(dataField.Name, dataField);
                }
            }
            foreach (DataField current in this) {
                if (current != null) {
                    K2API.DataField dataField2;
                    if (!dictionary.TryGetValue(current.Name, out dataField2)) {
                        throw new System.InvalidOperationException(string.Format("Field Not Found", current.Name));
                    }
                    dataField2.Value = FromString(current.Value, dataField2.ValueType);
                }
            }
        }

        public static string ConvertToString(object value) {
            if (value == null) {
                return null;
            }
            switch (System.Type.GetTypeCode(value.GetType())) {
                case System.TypeCode.Boolean:
                    return System.Xml.XmlConvert.ToString((bool)value);
                case System.TypeCode.Int16:
                    return System.Xml.XmlConvert.ToString((short)value);
                case System.TypeCode.Int32:
                    return System.Xml.XmlConvert.ToString((int)value);
                case System.TypeCode.Int64:
                    return System.Xml.XmlConvert.ToString((long)value);
                case System.TypeCode.Single:
                    return System.Xml.XmlConvert.ToString((float)value);
                case System.TypeCode.Double:
                    return System.Xml.XmlConvert.ToString((double)value);
                case System.TypeCode.Decimal:
                    return System.Xml.XmlConvert.ToString((decimal)value);
                case System.TypeCode.DateTime:
                    return System.Xml.XmlConvert.ToString((System.DateTime)value, System.Xml.XmlDateTimeSerializationMode.RoundtripKind);
                case System.TypeCode.String:
                    return (string)value;
            }
            if (value is byte[]) {
                byte[] array = (byte[])value;
                if (array.Length == 0) {
                    return string.Empty;
                }
                return System.Convert.ToBase64String(array);
            } else {
                System.IFormattable formattable = value as System.IFormattable;
                if (formattable != null) {
                    return formattable.ToString(null, System.Globalization.CultureInfo.InvariantCulture);
                }
                return value.ToString();
            }
        }

        public static object FromString(string value, DataType dataType) {
            switch (dataType) {
                case DataType.TypeBoolean:
                    if (value == null) {
                        return false;
                    }
                    if (value == string.Empty) {
                        return false;
                    }
                    return System.Xml.XmlConvert.ToBoolean(value);
                case DataType.TypeDate:
                    if (value == null) {
                        return System.DateTime.MinValue;
                    }
                    if (value == string.Empty) {
                        return System.DateTime.MinValue;
                    }
                    return System.Xml.XmlConvert.ToDateTime(value, System.Xml.XmlDateTimeSerializationMode.RoundtripKind);
                case DataType.TypeDecimal:
                    if (value == null) {
                        return 0m;
                    }
                    if (value == string.Empty) {
                        return 0m;
                    }
                    return System.Xml.XmlConvert.ToDecimal(value);
                case DataType.TypeDouble:
                    if (value == null) {
                        return 0.0;
                    }
                    if (value == string.Empty) {
                        return 0.0;
                    }
                    return System.Xml.XmlConvert.ToDecimal(value);
                case DataType.TypeInteger:
                    if (value == null) {
                        return 0;
                    }
                    if (value == string.Empty) {
                        return 0;
                    }
                    return System.Xml.XmlConvert.ToInt32(value);
                case DataType.TypeLong:
                    if (value == null) {
                        return 0L;
                    }
                    if (value == string.Empty) {
                        return 0L;
                    }
                    return System.Xml.XmlConvert.ToInt64(value);
                case DataType.TypeString:
                    return value;
                case DataType.TypeBinary:
                    if (value == null) {
                        return null;
                    }
                    if (value == string.Empty) {
                        return new byte[0];
                    }
                    return System.Convert.FromBase64String(value);
                default:
                    throw new System.NotSupportedException("Not Supported");
            }
        }
    }
}
