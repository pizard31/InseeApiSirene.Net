using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InseeApiSirene
{
    /// <summary>
    /// Classe utilitaire pour la sérialisation et la désérialisation JSON avec des options par défaut
    /// </summary>
    public static class OutilsJson
    {
        /// <summary>
        /// Options par défaut pour la sérialisation JSON, incluant l'indentation, la politique de nommage camelCase et l'ignorance des valeurs nulles
        /// </summary>
        public static readonly JsonSerializerOptions SerializerOptions = new JsonSerializerOptions()
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };

        /// <summary>
        /// Convertisseur de Int32 nullable
        /// </summary>
        public class Int32NullableStringConverter : JsonConverter<Int32?>
        {
            /// <summary>
            /// Désérialisation d'un Int32 à partir d'une chaîne de caractères, en traitant les valeurs nulles comme 0
            /// </summary>
            /// <param name="reader">Le lecteur JSON</param>
            /// <param name="typeToConvert">Le type à convertir</param>
            /// <param name="options">Les options de sérialisation</param>
            /// <returns>L'entier désérialisé</returns>
            public override Int32? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType == JsonTokenType.Null)
                {
                    return null;
                }
                if (Int32.TryParse(reader.GetString(), out Int32 result))
                {
                    return result;
                }
                return null;
            }

            /// <summary>
            /// Sérialisation d'un Int32 dans une chaîne de caractères, en traitant les valeurs nulles comme 0
            /// </summary>
            /// <param name="writer">Le writer JSON</param>
            /// <param name="value">La valeur à sérialiser</param>
            /// <param name="options">Les options de sérialisation</param>
            public override void Write(Utf8JsonWriter writer, Int32? value, JsonSerializerOptions options)
            {
                if (value.HasValue && value.Value != 0)
                {
                    writer.WriteNumberValue(value.Value);
                }
                else
                {
                    writer.WriteNullValue();
                }
            }
        }

        /// <summary>
        /// Convertisseur de Booléens en O / N
        /// </summary>
        public class BooleanOorNConverter : JsonConverter<Boolean>
        {
            /// <summary>
            /// Désérialisation d'un Boolean à partir d'une chaîne de caractères, en traitant les valeurs "O" comme true et les autres comme false
            /// </summary>
            /// <param name="reader">Le lecteur JSON</param>
            /// <param name="typeToConvert">Le type à convertir</param>
            /// <param name="options">Les options de sérialisation</param>
            /// <returns>Le Boolean désérialisé</returns>
            public override Boolean Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return reader.GetString() == "O";
            }

            /// <summary>
            /// Sérialisation d'un Boolean dans une chaîne de caractères, en traitant les valeurs true comme "O" et les valeurs false comme "N"
            /// </summary>
            /// <param name="writer">Le writer JSON</param>
            /// <param name="value">La valeur à sérialiser</param>
            /// <param name="options">Les options de sérialisation</param>
            public override void Write(Utf8JsonWriter writer, Boolean value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value ? "O" : "N");
            }
        }

        /// <summary>
        /// Convertisseur de Booléens nullable en O / N
        /// </summary>
        public class BooleanNullableOorNConverter : JsonConverter<Boolean?>
        {
            /// <summary>
            /// Désérialisation d'un Boolean à partir d'une chaîne de caractères, en traitant les valeurs "O" comme true et les autres comme false
            /// </summary>
            /// <param name="reader">Le lecteur JSON</param>
            /// <param name="typeToConvert">Le type à convertir</param>
            /// <param name="options">Les options de sérialisation</param>
            /// <returns>Le Boolean désérialisé</returns>
            public override Boolean? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType == JsonTokenType.Null)
                {
                    return null;
                }
                return reader.GetString() == "O";
            }

            /// <summary>
            /// Sérialisation d'un Boolean dans une chaîne de caractères, en traitant les valeurs true comme "O" et les valeurs false comme "N"
            /// </summary>
            /// <param name="writer">Le writer JSON</param>
            /// <param name="value">La valeur à sérialiser</param>
            /// <param name="options">Les options de sérialisation</param>
            public override void Write(Utf8JsonWriter writer, Boolean? value, JsonSerializerOptions options)
            {
                if (value.HasValue)
                {
                    writer.WriteNullValue();
                }
                else
                {
                    writer.WriteStringValue(value.Value ? "O" : "N");
                }
            }
        }

        /// <summary>
        /// Convertisseur d'énumérations nullable
        /// </summary>
        /// <typeparam name="T">Le type de l'énumération</typeparam>
        public class EnumNullableStringConverter<T> : JsonConverter<T> where T : struct, Enum
        {
            /// <summary>
            /// Désérialisation d'une énumération à partir d'une chaîne de caractères, en traitant les valeurs "null" ou nulles comme la valeur par défaut de l'énumération
            /// </summary>
            /// <param name="reader">Le lecteur JSON</param>
            /// <param name="typeToConvert">Le type à convertir</param>
            /// <param name="options">Les options de sérialisation</param>
            /// <returns>L'énumération désérialisée</returns>
            public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                // Si la valeur est null ou la chaîne "null", retourne la valeur par défaut (null pour un Nullable<T>)
                if (reader.TokenType == JsonTokenType.Null || reader.GetString() == "null")
                {
                    return default;
                }

                // Sinon, désérialise la chaîne en énumération
                string enumString = reader.GetString();
                if (Enum.TryParse<T>(enumString, ignoreCase: true, out T result))
                {
                    return result;
                }

                // Si la conversion échoue, retourne la valeur par défaut
                return default;
            }

            /// <summary>
            /// Sérialisation d'une énumération dans une chaîne de caractères, en traitant les valeurs nulles comme "null" et les autres comme leur représentation en chaîne de caractères
            /// </summary>
            /// <param name="writer">Le writer JSON</param>
            /// <param name="value">La valeur à sérialiser</param>
            /// <param name="options">Les options de sérialisation</param>
            public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
            {
                // Si la valeur est la valeur par défaut (0 pour les énumérations), écris "null"
                if (value.Equals(default(T)))
                {
                    writer.WriteNullValue();
                }
                else
                {
                    // Sinon, écris la valeur de l'énumération sous forme de chaîne
                    writer.WriteStringValue(value.ToString());
                }
            }
        }
    }
}
