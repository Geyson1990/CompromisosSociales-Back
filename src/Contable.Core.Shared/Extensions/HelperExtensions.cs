using Abp;
using Abp.Extensions;
using Abp.Localization;
using Abp.Localization.Sources;
using Abp.Runtime.Session;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Contable.Application.Extensions
{
    public static class HelperExtensions
    {

        #region Numeros a texto

        private const int UNI = 0, DIECI = 1, DECENA = 2, CENTENA = 3;
        private static string[,] _matriz = new string[CENTENA + 1, 10]
            {
            {null," uno", " dos", " tres", " cuatro", " cinco", " seis", " siete", " ocho", " nueve"},
            {" diez"," once"," doce"," trece"," catorce"," quince"," dieciséis"," diecisiete"," dieciocho"," diecinueve"},
            {null,null,null," treinta"," cuarenta"," cincuenta"," sesenta"," setenta"," ochenta"," noventa"},
            {null,null,null,null,null," quinientos",null," setecientos",null," novecientos"}
            };

        private const Char sub = (Char)26;
        public const String SeparadorDecimalSalidaDefault = "con";
        public const String MascaraSalidaDecimalDefault = "00'/100 '";
        public const Int32 DecimalesDefault = 2;
        public const Boolean LetraCapitalDefault = false;
        public const Boolean ConvertirDecimalesDefault = false;
        public const Boolean ApocoparUnoParteEnteraDefault = false;
        public const Boolean ApocoparUnoParteDecimalDefault = false;

        public static string ToCardinal(this decimal Numero, string currencyName = "SOLES")
        {
            return Convertir(Numero, DecimalesDefault, SeparadorDecimalSalidaDefault, MascaraSalidaDecimalDefault + currencyName, true, LetraCapitalDefault, ConvertirDecimalesDefault, ApocoparUnoParteEnteraDefault, ApocoparUnoParteDecimalDefault);
        }
        public static string Convertir(Decimal Numero, Int32 Decimales, String SeparadorDecimalSalida, String MascaraSalidaDecimal, Boolean EsMascaraNumerica, Boolean LetraCapital, Boolean ConvertirDecimales, Boolean ApocoparUnoParteEntera, Boolean ApocoparUnoParteDecimal)
        {
            Int64 Num;
            Int32 terna, centenaTerna, decenaTerna, unidadTerna, iTerna;
            String cadTerna;
            StringBuilder Resultado = new StringBuilder();

            Num = (Int64)Math.Abs(Numero);

            if (Num >= 1000000000000 || Num < 0) throw new ArgumentException("El número '" + Numero.ToString() + "' excedió los límites del conversor: [0;1.000.000.000.000)");
            if (Num == 0)
                Resultado.Append(" cero");
            else
            {
                iTerna = 0;
                while (Num > 0)
                {
                    iTerna++;
                    cadTerna = String.Empty;
                    terna = (Int32)(Num % 1000);

                    centenaTerna = (Int32)(terna / 100);
                    decenaTerna = terna % 100;
                    unidadTerna = terna % 10;

                    if ((decenaTerna > 0) && (decenaTerna < 10))
                        cadTerna = _matriz[UNI, unidadTerna] + cadTerna;
                    else if ((decenaTerna >= 10) && (decenaTerna < 20))
                        cadTerna = cadTerna + _matriz[DIECI, unidadTerna];
                    else if (decenaTerna == 20)
                        cadTerna = cadTerna + " veinte";
                    else if ((decenaTerna > 20) && (decenaTerna < 30))
                        cadTerna = " veinti" + _matriz[UNI, unidadTerna].Substring(1);
                    else if ((decenaTerna >= 30) && (decenaTerna < 100))
                        if (unidadTerna != 0)
                            cadTerna = _matriz[DECENA, (Int32)(decenaTerna / 10)] + " y" + _matriz[UNI, unidadTerna] + cadTerna;
                        else
                            cadTerna += _matriz[DECENA, (Int32)(decenaTerna / 10)];

                    switch (centenaTerna)
                    {
                        case 1:
                            if (decenaTerna > 0) cadTerna = " ciento" + cadTerna;
                            else cadTerna = " cien" + cadTerna;
                            break;
                        case 5:
                        case 7:
                        case 9:
                            cadTerna = _matriz[CENTENA, (Int32)(terna / 100)] + cadTerna;
                            break;
                        default:
                            if ((Int32)(terna / 100) > 1) cadTerna = _matriz[UNI, (Int32)(terna / 100)] + "cientos" + cadTerna;
                            break;
                    }

                    if ((iTerna > 1 | ApocoparUnoParteEntera) && decenaTerna == 21)
                        cadTerna = cadTerna.Replace("veintiuno", "veintiún");
                    else if ((iTerna > 1 | ApocoparUnoParteEntera) && unidadTerna == 1 && decenaTerna != 11)
                        cadTerna = cadTerna.Substring(0, cadTerna.Length - 1);

                    else if (decenaTerna == 22) cadTerna = cadTerna.Replace("veintidos", "veintidós");
                    else if (decenaTerna == 23) cadTerna = cadTerna.Replace("veintitres", "veintitrés");
                    else if (decenaTerna == 26) cadTerna = cadTerna.Replace("veintiseis", "veintiséis");

                    switch (iTerna)
                    {
                        case 3:
                            if (Numero < 2000000) cadTerna += " millón";
                            else cadTerna += " millones";
                            break;
                        case 2:
                        case 4:
                            if (terna > 0) cadTerna += " mil";
                            break;
                    }
                    Resultado.Insert(0, cadTerna);
                    Num = (Int32)(Num / 1000);
                }
            }


            //if (Decimales > 0)
            //{
            //    Resultado.Append(" " + SeparadorDecimalSalida + " ");
            //    Int32 EnteroDecimal = (Int32)Math.Round((Double)(Numero - (Int64)Numero) * Math.Pow(10, Decimales), 0);
            //    if (ConvertirDecimales)
            //    {
            //        Boolean esMascaraDecimalDefault = MascaraSalidaDecimal == MascaraSalidaDecimalDefault;
            //        Resultado.Append(Convertir((Decimal)EnteroDecimal, 0, null, null, EsMascaraNumerica, false, false, (ApocoparUnoParteDecimal && !EsMascaraNumerica/*&& !esMascaraDecimalDefault*/), false) + " "
            //            + (EsMascaraNumerica ? "" : MascaraSalidaDecimal));
            //    }
            //    else
            //        if (EsMascaraNumerica) Resultado.Append(EnteroDecimal.ToString(MascaraSalidaDecimal));
            //    else Resultado.Append(EnteroDecimal.ToString() + " " + MascaraSalidaDecimal);
            //}
            return Resultado.ToString().Trim();
        }

        #endregion FinNumerosAtexto

        /// <summary>
        /// Convierte el string actual a usa cadena MD5 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="TargetInvocationException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="FormatException"></exception>
        public static string ToMD5(this string value)
        {
            using (var provider = System.Security.Cryptography.MD5.Create())
            {
                StringBuilder builder = new StringBuilder();

                foreach (byte b in provider.ComputeHash(Encoding.UTF8.GetBytes(value)))
                    builder.Append(b.ToString("x2").ToLower());

                return builder.ToString().ToUpper();
            }
        }

        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self) => self.Select((item, index) => (item, index));

        /// <summary>
        /// Convierte la fecha a una cadena string MD5 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        /// <exception cref="TargetInvocationException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="FormatException"></exception>
        public static string ToMD5(this DateTime date)
        {
            var value = date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.ffff");

            using (var provider = System.Security.Cryptography.MD5.Create())
            {
                StringBuilder builder = new StringBuilder();

                foreach (byte b in provider.ComputeHash(Encoding.UTF8.GetBytes(value)))
                    builder.Append(b.ToString("x2").ToUpper());

                return builder.ToString();
            }
        }

        /// <summary>
        /// Convierte el string actual a usa cadena MD5, concatena los valores al inicio y final del string devuelto
        /// </summary>
        /// <param name="date">Texto intermedio de la cadena</param>
        /// <param name="initial">Texto inicial de la cadena</param>
        /// <param name="final">Texto final de la cadena</param>
        /// <returns></returns>
        /// <exception cref="TargetInvocationException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="FormatException"></exception>
        public static string ToMD5(this DateTime date, string initial,string final)
        {
            var value = initial + date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.ffff") + final;

            using (var provider = System.Security.Cryptography.MD5.Create())
            {
                StringBuilder builder = new StringBuilder();

                foreach (byte b in provider.ComputeHash(Encoding.UTF8.GetBytes(value)))
                    builder.Append(b.ToString("x2").ToUpper());

                return builder.ToString().ToUpper();
            }
        }

        /// <summary>
        /// Verfica que el String no sea "Vacío", "Nulo" o "Espacios en blanco"
        /// en caso de que sea alguno aplica una excepción con el título y mensaje que el usuario desee
        /// </summary>
        /// <param name="item">Objeto a evaluar</param>
        /// <param name="title">Titulo de la excepción</param>
        /// <param name="response">Cuerpo del mensaje</param>
        /// <exception cref="UserFriendlyException">Si el objeto es "Vacío", "Nulo" o "Espacios en blanco"</exception>
        public static void IsValidOrException(this string item, string title, string message)
        {
            if((item ?? "").Trim().Length == 0)
            {
                throw new UserFriendlyException(title, message);
            }
        }

        /// <summary>
        /// Verifica que el string no sea "Nulo", "Vacío" o "Espacios en blanco"
        /// </summary>
        /// <param name="item"></param>
        /// <returns>
        /// false si es "Nulo", "Vacío" o "Espacios en blanco"
        /// true is es diferente de "Nulo", "Vacío" o "Espacios en blanco"
        /// </returns>
        /// <example>string l = "";if(l.IsValid()) </example>
        public static bool IsValid(this string item)
        {
            var text = (item ?? "").Trim();

            if (text.Length == 0)
            {
                return false;
            }

            return true;
        }

        public static string[] SplitByLike(this string value)
        {
            return (value ?? "").Split(" ").Where(p => p.IsValid()).ToArray();
        }

        public static bool IsValidForLike(this string item)
        {
            var text = (item ?? "").Trim();

            if (text.Length == 0)
                return false;
            if (text.Split(" ").Where(p => p.IsValid()).Count() == 0)
                return false;

            return true;
        }

        /// <summary>
        /// Obtiene el codigo de excepción que se inserto en la variable
        /// </summary>
        /// <param name="value"></param>
        /// <returns>
        /// [string] con el código si la constante tiene valor
        /// null en caso de no tener código
        /// </returns>
        public static string GetCode(this Enum value)
        {
            Type type = value.GetType();
            FieldInfo fieldInfo = type.GetField(value.ToString());
            CodeAttribute[] attribs = fieldInfo.GetCustomAttributes(typeof(CodeAttribute), false) as CodeAttribute[];
            return attribs.Length > 0 ? attribs[0].StringValue : null;
        }

        /// <summary>
        /// Verifa que el registro sea mayor a 0, en caso contrario aplica una excepción de registro no encontrado
        /// </summary>
        /// <param name="value"></param>
        /// <param name="service"></param>
        /// <exception cref="UserFriendlyException"></exception>
        public static void VerifyCount(this int value, AbpServiceBase service)
        {
            if(value == 0)
                throw new UserFriendlyException(Localize("Notice", service.LocalizationManager), Localize("RecordNotFound", service.LocalizationManager));
        }

        /// <summary>
        /// Verifa que el registro ya no exista en base de datos
        /// </summary>
        /// <param name="value"></param>
        /// <param name="service"></param>
        /// <exception cref="UserFriendlyException"></exception>
        public static void VerifyIfExists(this int value, AbpServiceBase service)
        {
            if (value > 0)
            {
                throw new UserFriendlyException(Localize("Notice", service.LocalizationManager), Localize("RecordAlreadyExists", service.LocalizationManager));
            }
        }

        /// <summary>
        /// Verifa que el registro ya no exista en base de datos
        /// </summary>
        /// <param name="value"></param>
        /// <param name="service"></param>
        /// <exception cref="UserFriendlyException"></exception>
        public static void VerifyTableColumn(this string value, int min, int max, string title, string message)
        {
            var field = value ?? "";
            var fieldLength = field.Length;

            if (fieldLength < min || fieldLength > max)
                throw new UserFriendlyException(title, message);
        }

        public static string DecryptStringAES(string cipherText, string keyText, string ivText)
        {
            var keybytes = Encoding.UTF8.GetBytes(keyText);
            var ivbytes = Encoding.UTF8.GetBytes(ivText);

            var encrypted = Convert.FromBase64String(cipherText);
            var decriptedFromJavascript = DecryptStringFromBytes(encrypted, keybytes, ivbytes);
            return string.Format(decriptedFromJavascript);
        }

        private static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("key");
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException("key");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;
            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (var rijAlg = new RijndaelManaged())
            {
                //Settings
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;
                rijAlg.Key = key;
                rijAlg.IV = iv;
                // Create a decrytor to perform the stream transform.
                var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);
                try
                {
                    // Create the streams used for decryption.
                    using (var msDecrypt = new MemoryStream(cipherText))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                // Read the decrypted bytes from the decrypting stream
                                // and place them in a string.
                                plaintext = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
                catch
                {
                    plaintext = null;
                }
            }

            return plaintext;
        }

        public static string GenerateCaptchaCode(int charCount)
        {
            Random r = new Random();
            string s = "";

            for (int i = 0; i < charCount; i++)
            {
                int a = r.Next(3);
                int chars;
                switch (a)
                {
                    case 0:
                    case 1:
                        chars = r.Next(0, 9);
                        s += chars.ToString();
                        break;
                    case 2:
                        chars = r.Next(65, 90);
                        s += Convert.ToChar(chars).ToString();
                        break;
                    case 3:
                        chars = r.Next(97, 122);
                        s += Convert.ToChar(chars).ToString();
                        break;
                }
            }

            return s.ToUpperInvariant();
        }

        private static string Localize(string name, ILocalizationManager service)
        {
            return service.GetString(L(name));
        }

        private static string Localize(string name, ILocalizationManager service, params string[] args)
        {
            return string.Format(service.GetString(L(name)), args);
        }

        private static LocalizableString L(string name)
        {
            return new LocalizableString(name, ContableConsts.LocalizationSourceName);
        }
    }
}
