﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NalkaTools.CodeAnalysis {
    using System;
    using System.Reflection;
    
    
    /// <summary>
    ///   Une classe de ressource fortement typée destinée, entre autres, à la consultation des chaînes localisées.
    /// </summary>
    // Cette classe a été générée automatiquement par la classe StronglyTypedResourceBuilder
    // à l'aide d'un outil, tel que ResGen ou Visual Studio.
    // Pour ajouter ou supprimer un membre, modifiez votre fichier .ResX, puis réexécutez ResGen
    // avec l'option /str ou régénérez votre projet VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Retourne l'instance ResourceManager mise en cache utilisée par cette classe.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("NalkaTools.CodeAnalysis.Resources", typeof(Resources).GetTypeInfo().Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Remplace la propriété CurrentUICulture du thread actuel pour toutes
        ///   les recherches de ressources à l'aide de cette classe de ressource fortement typée.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Properties should not be referenced in their own getter in order to avoid StackOverflowException..
        /// </summary>
        internal static string DontReadSelfInGetterDescription {
            get {
                return ResourceManager.GetString("DontReadSelfInGetterDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à The property &apos;{0}&apos; should not be referenced in its getter..
        /// </summary>
        internal static string DontReadSelfInGetterMessageFormat {
            get {
                return ResourceManager.GetString("DontReadSelfInGetterMessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Property is referenced in its getter..
        /// </summary>
        internal static string DontReadSelfInGetterTitle {
            get {
                return ResourceManager.GetString("DontReadSelfInGetterTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Use Destroyer instead..
        /// </summary>
        internal static string UseDestroyerCodeFix {
            get {
                return ResourceManager.GetString("UseDestroyerCodeFix", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Calling UnityEngine.Object.Destroy will not trigger Destroyer&apos;s events and the handlers added to them will not be invoked..
        /// </summary>
        internal static string UseDestroyerDescription {
            get {
                return ResourceManager.GetString("UseDestroyerDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à The call to UnityEngine.Object.Destroy won&apos;t trigger Destroyer&apos;s events, handlers for them won&apos;t be invoked..
        /// </summary>
        internal static string UseDestroyerMessageFormat {
            get {
                return ResourceManager.GetString("UseDestroyerMessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Potentially wrong call to UnityEngine.Object.Destroy..
        /// </summary>
        internal static string UseDestroyerTitle {
            get {
                return ResourceManager.GetString("UseDestroyerTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Use Instantiater instead..
        /// </summary>
        internal static string UseInstantiaterCodeFix {
            get {
                return ResourceManager.GetString("UseInstantiaterCodeFix", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Calling UnityEngine.Object.Instantiate will not trigger Instantiater&apos;s events and the handlers added to them will not be invoked..
        /// </summary>
        internal static string UseInstantiaterDescription {
            get {
                return ResourceManager.GetString("UseInstantiaterDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à The call to UnityEngine.Object.Instantiate won&apos;t trigger Instantiater&apos;s events, handlers for them won&apos;t be invoked..
        /// </summary>
        internal static string UseInstantiaterMessageFormat {
            get {
                return ResourceManager.GetString("UseInstantiaterMessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Potentially wrong call to UnityEngine.Object.Instantiate..
        /// </summary>
        internal static string UseInstantiaterTitle {
            get {
                return ResourceManager.GetString("UseInstantiaterTitle", resourceCulture);
            }
        }
    }
}
