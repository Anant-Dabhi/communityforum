﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.42
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 2.0.50727.42.
// 
#pragma warning disable 1591

namespace yaf.RegisterForum {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="RegisterSoap", Namespace="http://www.yetanotherforum.net/Register")]
    public partial class Register : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback RegisterForumOperationCompleted;
        
        private System.Threading.SendOrPostCallback LatestVersionOperationCompleted;
        
        private System.Threading.SendOrPostCallback LatestVersionDateOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public Register() {
            this.Url = "http://www.yetanotherforum.net/Register.asmx";
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event RegisterForumCompletedEventHandler RegisterForumCompleted;
        
        /// <remarks/>
        public event LatestVersionCompletedEventHandler LatestVersionCompleted;
        
        /// <remarks/>
        public event LatestVersionDateCompletedEventHandler LatestVersionDateCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.yetanotherforum.net/Register/RegisterForum", RequestNamespace="http://www.yetanotherforum.net/Register", ResponseNamespace="http://www.yetanotherforum.net/Register", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public long RegisterForum(long id, string name, string address) {
            object[] results = this.Invoke("RegisterForum", new object[] {
                        id,
                        name,
                        address});
            return ((long)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginRegisterForum(long id, string name, string address, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("RegisterForum", new object[] {
                        id,
                        name,
                        address}, callback, asyncState);
        }
        
        /// <remarks/>
        public long EndRegisterForum(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((long)(results[0]));
        }
        
        /// <remarks/>
        public void RegisterForumAsync(long id, string name, string address) {
            this.RegisterForumAsync(id, name, address, null);
        }
        
        /// <remarks/>
        public void RegisterForumAsync(long id, string name, string address, object userState) {
            if ((this.RegisterForumOperationCompleted == null)) {
                this.RegisterForumOperationCompleted = new System.Threading.SendOrPostCallback(this.OnRegisterForumOperationCompleted);
            }
            this.InvokeAsync("RegisterForum", new object[] {
                        id,
                        name,
                        address}, this.RegisterForumOperationCompleted, userState);
        }
        
        private void OnRegisterForumOperationCompleted(object arg) {
            if ((this.RegisterForumCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.RegisterForumCompleted(this, new RegisterForumCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.yetanotherforum.net/Register/LatestVersion", RequestNamespace="http://www.yetanotherforum.net/Register", ResponseNamespace="http://www.yetanotherforum.net/Register", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public long LatestVersion() {
            object[] results = this.Invoke("LatestVersion", new object[0]);
            return ((long)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginLatestVersion(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("LatestVersion", new object[0], callback, asyncState);
        }
        
        /// <remarks/>
        public long EndLatestVersion(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((long)(results[0]));
        }
        
        /// <remarks/>
        public void LatestVersionAsync() {
            this.LatestVersionAsync(null);
        }
        
        /// <remarks/>
        public void LatestVersionAsync(object userState) {
            if ((this.LatestVersionOperationCompleted == null)) {
                this.LatestVersionOperationCompleted = new System.Threading.SendOrPostCallback(this.OnLatestVersionOperationCompleted);
            }
            this.InvokeAsync("LatestVersion", new object[0], this.LatestVersionOperationCompleted, userState);
        }
        
        private void OnLatestVersionOperationCompleted(object arg) {
            if ((this.LatestVersionCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.LatestVersionCompleted(this, new LatestVersionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.yetanotherforum.net/Register/LatestVersionDate", RequestNamespace="http://www.yetanotherforum.net/Register", ResponseNamespace="http://www.yetanotherforum.net/Register", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.DateTime LatestVersionDate() {
            object[] results = this.Invoke("LatestVersionDate", new object[0]);
            return ((System.DateTime)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginLatestVersionDate(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("LatestVersionDate", new object[0], callback, asyncState);
        }
        
        /// <remarks/>
        public System.DateTime EndLatestVersionDate(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((System.DateTime)(results[0]));
        }
        
        /// <remarks/>
        public void LatestVersionDateAsync() {
            this.LatestVersionDateAsync(null);
        }
        
        /// <remarks/>
        public void LatestVersionDateAsync(object userState) {
            if ((this.LatestVersionDateOperationCompleted == null)) {
                this.LatestVersionDateOperationCompleted = new System.Threading.SendOrPostCallback(this.OnLatestVersionDateOperationCompleted);
            }
            this.InvokeAsync("LatestVersionDate", new object[0], this.LatestVersionDateOperationCompleted, userState);
        }
        
        private void OnLatestVersionDateOperationCompleted(object arg) {
            if ((this.LatestVersionDateCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.LatestVersionDateCompleted(this, new LatestVersionDateCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    public delegate void RegisterForumCompletedEventHandler(object sender, RegisterForumCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class RegisterForumCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal RegisterForumCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public long Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((long)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    public delegate void LatestVersionCompletedEventHandler(object sender, LatestVersionCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class LatestVersionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal LatestVersionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public long Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((long)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    public delegate void LatestVersionDateCompletedEventHandler(object sender, LatestVersionDateCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class LatestVersionDateCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal LatestVersionDateCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.DateTime Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.DateTime)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591