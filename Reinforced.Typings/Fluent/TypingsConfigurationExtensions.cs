﻿using System;
using System.Reflection;
using Reinforced.Typings.Attributes;
using Reinforced.Typings.Fluent.Interfaces;
using Reinforced.Typings.Generators;

namespace Reinforced.Typings.Fluent
{
    /// <summary>
    /// Extensions for fluent configuration
    /// </summary>
    public static class TypingsConfigurationExtensions
    {
        /// <summary>
        /// Ignores specified mambers during exporting
        /// </summary>
        public static T Ignore<T>(this T conf) where T : IIgnorable
        {
            conf.Ignore = true;
            return conf;
        }

        #region CodeGenerator extensions
        /// <summary>
        /// Specifies code generator for member
        /// </summary>
        public static IExportConfiguration<TsClassAttribute> WithCodeGenerator<T>(this IExportConfiguration<TsClassAttribute> conf) 
            where T : ITsCodeGenerator<Type>
        {
            conf.AttributePrototype.CodeGeneratorType = typeof (T);
            return conf;
        }

        /// <summary>
        /// Specifies code generator for member
        /// </summary>
        public static IExportConfiguration<TsInterfaceAttribute> WithCodeGenerator<T>(this IExportConfiguration<TsInterfaceAttribute> conf)
            where T : ITsCodeGenerator<Type>
        {
            conf.AttributePrototype.CodeGeneratorType = typeof(T);
            return conf;
        }

        /// <summary>
        /// Specifies code generator for member
        /// </summary>
        public static IExportConfiguration<TsEnumAttribute> WithCodeGenerator<T>(this IExportConfiguration<TsEnumAttribute> conf)
            where T : ITsCodeGenerator<Type>
        {
            conf.AttributePrototype.CodeGeneratorType = typeof(T);
            return conf;
        }

        /// <summary>
        /// Specifies code generator for member
        /// </summary>
        public static IExportConfiguration<TsPropertyAttribute> WithCodeGenerator<T>(this IExportConfiguration<TsPropertyAttribute> conf)
            where T : ITsCodeGenerator<MemberInfo>
        {
            conf.AttributePrototype.CodeGeneratorType = typeof(T);
            return conf;
        }

        /// <summary>
        /// Specifies code generator for member
        /// </summary>
        public static IExportConfiguration<TsFunctionAttribute> WithCodeGenerator<T>(this IExportConfiguration<TsFunctionAttribute> conf)
            where T : ITsCodeGenerator<MethodInfo>
        {
            conf.AttributePrototype.CodeGeneratorType = typeof(T);
            return conf;
        }

        /// <summary>
        /// Specifies code generator for member
        /// </summary>
        public static IExportConfiguration<TsParameterAttribute> WithCodeGenerator<T>(this IExportConfiguration<TsParameterAttribute> conf)
            where T : ITsCodeGenerator<ParameterInfo>
        {
            conf.AttributePrototype.CodeGeneratorType = typeof(T);
            return conf;
        }
        #endregion

        #region Reference extensions

        /// <summary>
        /// Adds reference directive to file containing single TS class typing. 
        /// It method is only used while splitting generated type sto different files
        /// </summary>
        /// <param name="configuration">Configurator</param>
        /// <param name="referenceFile">Path to referenced file</param>
        public static T AddReference<T>(this T configuration,string referenceFile) where T : IReferenceConfiguration
        {
            configuration.References.Add(new TsAddTypeReferenceAttribute(referenceFile));
            return configuration;
        }
        /// <summary>
        /// Adds reference directive to file containing single TS class typing. 
        /// It method is only used while splitting generated type sto different files
        /// </summary>
        /// <param name="configuration">Configurator</param>
        /// <param name="referencedType">Another generated type that should be referenced</param>
        public static T AddReference<T>(this T configuration, Type referencedType) where T : IReferenceConfiguration
        {
            configuration.References.Add(new TsAddTypeReferenceAttribute(referencedType));
            return configuration;
        }
        #endregion

        #region Auto export control - temporary disabled

        ///// <summary>
        ///// Makes generator automatically look up for type methods and export them. 
        ///// Please note that this configuration setting will also export static and private members with corresponding modifiers.
        ///// </summary>
        ///// <param name="conf">Configuration</param>
        ///// <param name="export">Export or not</param>
        //public static T AutomaticallyExportMethods<T>(this T conf, bool export = true) where T : IExportConfiguration<IAutoexportSwitchAttribute>
        //{
        //    conf.AttributePrototype.AutoExportMethods = export;
        //    return conf;
        //}

        ///// <summary>
        ///// Makes generator automatically look up for type properties and export them. 
        ///// Please note that this configuration setting will also export static and private members with corresponding modifiers.
        ///// </summary>
        ///// <param name="conf">Configuration</param>
        ///// <param name="export">Export or not</param>
        //public static T AutomaticallyExportProperties<T>(this T conf, bool export = true) where T : IExportConfiguration<IAutoexportSwitchAttribute>
        //{
        //    conf.AttributePrototype.AutoExportProperties = export;
        //    return conf;
        //}

        ///// <summary>
        ///// Makes generator automatically look up for type fields and export them as TS fields. 
        ///// Please note that this configuration setting will also export static and private members with corresponding modifiers.
        ///// </summary>
        ///// <param name="conf">Configuration</param>
        ///// <param name="export">Export or not</param>
        //public static T AutomaticallyExportFields<T>(this T conf, bool export = false) where T : IExportConfiguration<TsClassAttribute>
        //{
        //    conf.AttributePrototype.AutoExportFields = export;
        //    return conf;
        //}

        ///// <summary>
        ///// Makes generator automatically look up for type constructors and export them as empty constructors. 
        ///// </summary>
        ///// <param name="conf">Configuration</param>
        ///// <param name="export">Export or not</param>
        //public static T AutomaticallyExportConstructors<T>(this T conf, bool export = false) where T : IExportConfiguration<TsClassAttribute>
        //{
        //    conf.AttributePrototype.AutoExportConstructors = export;
        //    return conf;
        //}

        ///// <summary>
        ///// Sets default code generator for each method among class is being exported
        ///// </summary>
        ///// <param name="conf">Configuration</param>
        //public static IExportConfiguration<TsClassAttribute> DefaultMethodCodeGenerator<T>(this IExportConfiguration<TsClassAttribute> conf) 
        //    where T : ITsCodeGenerator<MethodInfo>
        //{
        //    conf.AttributePrototype.DefaultMethodCodeGenerator = typeof(T);
        //    return conf;
        //}
        #endregion

        #region Names and namespaces
        /// <summary>
        /// Overrides name of specified member
        /// </summary>
        /// <param name="conf">Configuration</param>
        /// <param name="name">Custom name to be used</param>
        public static T OverrideName<T>(this T conf, string name) where T : IExportConfiguration<INameOverrideAttribute>
        {
            conf.AttributePrototype.Name = name;
            return conf;
        }

        /// <summary>
        /// Configures exporter dont to export member to corresponding namespace
        /// </summary>
        public static T DontIncludeToNamespace<T>(this T conf,bool include = false) where T : IExportConfiguration<TsDeclarationAttributeBase>
        {
            conf.AttributePrototype.IncludeNamespace = include;
            return conf;
        }

        /// <summary>
        /// Configures exporter to export type to specified namespace
        /// </summary>
        /// <param name="conf">Configuration</param>
        /// <param name="nameSpace">Namespace name</param>
        public static T OverrideNamespace<T>(this T conf, string nameSpace) where T : IExportConfiguration<TsDeclarationAttributeBase>
        {
            conf.AttributePrototype.Namespace = nameSpace;
            return conf;
        }
        #endregion

        #region Types and strong types
        /// <summary>
        /// Overrides member type name on export with textual string. 
        /// Beware of using this setting because specified type may not present in your TypeScript code and 
        /// this will lead to TypeScript compilation errors
        /// </summary>
        /// <param name="conf">Configurator</param>
        /// <param name="typeName">TS-friendly type name</param>
        /// <returns></returns>
        public static T Type<T>(this T conf, string typeName) where T : IExportConfiguration<TsTypedAttributeBase>
        {
            conf.AttributePrototype.Type = typeName;
            return conf;
        }

        /// <summary>
        /// Overrides member type on export with strong type. 
        /// Feel free to use delegates here. It is very comfortable instead of regular TS functions syntax. 
        /// </summary>
        /// <param name="conf">Configurator</param>
        public static IExportConfiguration<TsTypedAttributeBase> Type<T>(this IExportConfiguration<TsTypedAttributeBase> conf) 
        {
            conf.AttributePrototype.StrongType = typeof(T);
            return conf;
        }

        /// <summary>
        /// Overrides member type on export with strong type. 
        /// Feel free to use delegates here. It is very comfortable instead of regular TS functions syntax. 
        /// </summary>
        /// <param name="conf">Configurator</param>
        /// <param name="type">Type to override with</param>
        /// <returns></returns>
        public static T Type<T>(this T conf, Type type) where T : IExportConfiguration<TsTypedAttributeBase>
        {
            conf.AttributePrototype.StrongType = type;
            return conf;
        }

        #region The same for methods
        /// <summary>
        /// Overrides member type name on export with textual string. 
        /// Beware of using this setting because specified type may not present in your TypeScript code and 
        /// this will lead to TypeScript compilation errors.
        /// Actually this method does the same as .Type call. Just for your convinence
        /// </summary>
        /// <param name="conf">Configurator</param>
        /// <param name="typeName">TS-friendly type name</param>
        /// <returns></returns>
        public static T Returns<T>(this T conf, string typeName) where T : IExportConfiguration<TsFunctionAttribute>
        {
            conf.AttributePrototype.Type = typeName;
            return conf;
        }

        /// <summary>
        /// Overrides member type on export with strong type. 
        /// Feel free to use delegates here. It is very comfortable instead of regular TS functions syntax. 
        /// Actually this method does the same as .Type call. Just for your convinence
        /// </summary>
        /// <param name="conf">Configurator</param>
        public static IExportConfiguration<TsTypedAttributeBase> Returns<T>(this IExportConfiguration<TsFunctionAttribute> conf)
        {
            conf.AttributePrototype.StrongType = typeof(T);
            return conf;
        }

        /// <summary>
        /// Overrides member type on export with strong type. 
        /// Feel free to use delegates here. It is very comfortable instead of regular TS functions syntax. 
        /// Actually this method does the same as .Type call. Just for your convinence
        /// </summary>
        /// <param name="conf">Configurator</param>
        /// <param name="type">Type to override with</param>
        /// <returns></returns>
        public static T Returns<T>(this T conf, Type type) where T : IExportConfiguration<TsFunctionAttribute>
        {
            conf.AttributePrototype.StrongType = type;
            return conf;
        }
        #endregion




        #endregion

        #region Various and specific
        /// <summary>
        /// Forces property to be a nullable. 
        /// When set to true then property will be generated as [property]? : [type] with 
        /// forcibly added question mark denoting nullable field. 
        /// </summary>
        /// <param name="conf">Configuration</param>
        /// <param name="force">Force nullable or not</param>
        public static T ForceNullable<T>(this T conf, bool force = false) where T : IExportConfiguration<TsPropertyAttribute>
        {
            conf.AttributePrototype.ForceNullable = force;
            return conf;
        }

        /// <summary>
        /// Sets parameter default value. 
        /// </summary>
        /// <param name="conf">Configuration</param>
        /// <param name="value">Default value for parameter</param>
        public static T DefaultValue<T>(this T conf, object value) where T : IExportConfiguration<TsParameterAttribute>
        {
            conf.AttributePrototype.DefaultValue = value;
            return conf;
        }

        /// <summary>
        /// Forces exporter to add I letter as interface prefix. 
        /// </summary>
        /// <param name="conf">Configuration</param>
        /// <param name="auto">Add I automatically or not</param>
        public static T AutoI<T>(this T conf, bool auto = true) where T : IExportConfiguration<TsInterfaceAttribute>
        {
            conf.AttributePrototype.AutoI = auto;
            return conf;
        }
        #endregion
    }
}
